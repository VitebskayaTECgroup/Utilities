using Paniker.Models;
using System.Data.Odbc;
using System.Net;
using System.Web;

namespace Paniker.Services
{
	public class DatabaseService : BackgroundService
	{
		public DatabaseService(ILoggerFactory loggerFactory)
		{
			Logger = loggerFactory.CreateLogger<DatabaseService>();
		}

		ILogger Logger { get; }

		List<TopicInfo> Topics { get; set; } = new List<TopicInfo>();

		string[] Excluded { get; set; } = new string[0];

		DateTime LastEcho { get; set; } = DateTime.MinValue;

		protected override async Task ExecuteAsync(CancellationToken token)
		{
			Logger.LogInformation("Запуск службы проверки");
			//await Send($"Logger started", token);

			while (!token.IsCancellationRequested)
			{
				Logger.LogInformation("Выполняем проверку");

				try
				{
					var excluded = File.ReadAllLines((Environment.ProcessPath ?? (Environment.CurrentDirectory + "\\excluded.exe")).Replace("Paniker.exe", "excluded.txt"));
					Excluded = excluded ?? Excluded;
				}
				catch { }
				
				Task.WaitAll(new Task[] {
					//Task.Run(() => Check(1, token), token),
					Task.Run(() => Check(2, token), token),
				}, token);

				Logger.LogInformation("Ожидание до следующей проверки");

				await Task.Delay(TimeSpan.FromMinutes(1), token);
			}

			Logger.LogInformation("Служба проверки остановлена");
			//await Send($"Logger stopped", token);
		}

		void Check(int serverNumber, CancellationToken token)
		{
			if (DateTime.Now - LastEcho >= TimeSpan.FromDays(1))
			{
				if (LastEcho != DateTime.MinValue)
				{
					Task.Run(() => Send("Logger жив", token), token);
				}
				else 
				{
					Task.Run(() => Send("Logger запущен", token), token);
				}
				
				LastEcho = DateTime.Now;
			}

			var tags = new Dictionary<string, string>();

			try
			{
				using var conn = new OdbcConnection();
				conn.ConnectionString = "Driver={SQL Server}; Server=INSQL" + serverNumber + "; Database=Runtime; Uid=wwAdmin; Pwd=wwAdmin;";
				conn.Open();

				using var command = new OdbcCommand();
				command.Connection = conn;
				command.CommandText = "EXEC vst_Paniker";
				command.CommandTimeout = 10;

				using var reader = command.ExecuteReader();

				while (reader.Read())
				{
					tags.Add(reader.GetString(0), reader.GetString(1));
				}

				reader.Close();
				conn.Close();
			}
			catch { }

			try
			{
				using var conn = new OdbcConnection();
				conn.ConnectionString = "Driver={SQL Server}; Server=WEB\\SQLEXPRESS; Database=Site; Uid=Site; Pwd=Site;";
				conn.Open();

				using var command = new OdbcCommand();
				command.Connection = conn;
				command.CommandText = "SELECT Keyword, Value FROM Constants WHERE Keyword IN ('GasInsql1', 'GasInsql2')";
				command.CommandTimeout = 10;

				using var reader = command.ExecuteReader();

				while (reader.Read())
				{
					tags.Add(reader.GetString(0), reader.GetString(1));
				}

				reader.Close();
				conn.Close();
			}
			catch { }

			foreach (var tag in tags)
			{
				if (Excluded.Contains(tag.Key))
				{
					//Logger.LogWarning("Excluded tag {Key}", tag.Key);
				}
				else
				{
					var topic = Topics.FirstOrDefault(x => x.Name == tag.Key);
					if (topic == null)
					{
						Topics.Add(new TopicInfo
						{
							Name = tag.Key,
							CurrentState = tag.Value,
							CountOfChange = 0,
							NewState = tag.Value
						});
					}
					else
					{
						if (tag.Value != topic.CurrentState)
						{
							if (tag.Value == topic.NewState)
							{
								topic.CountOfChange++;
							}
							else
							{
								topic.NewState = tag.Value;
								topic.CountOfChange = 0;
							}
						}
						else
						{
							topic.CountOfChange = 0;
						}

						if (topic.CountOfChange > 1)
						{
							Logger.LogWarning("{Key}  >>>  {Value}", tag.Key, tag.Value);
							Task.Run(() => Send($"{tag.Key}  >>>  {tag.Value}", token), token);

							topic.CountOfChange = 0;
							topic.CurrentState = topic.NewState;
						}
					}
				}
			}
		}

		async Task Send(string text, CancellationToken token)
		{
			string botId = "5615415781:AAGdakmBjFQVKpzajxfGYPu_ID8-Iv2pxrw";
			string chatId = "-1001877460791"; 
			string url = $"https://api.telegram.org/bot{botId}/sendMessage?chat_id={chatId}&text=";

			using var client = new HttpClient();
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

			string message = HttpUtility.UrlEncode($"{text}");

			try
			{
				await client.GetStringAsync(url + message, token);
				Logger.LogInformation("Сообщение отправлено");
			}
			catch (WebException e)
			{
				Logger.LogError("Ошибка при отправке сообщения! {Message}", e.Message);
			}
			finally
			{
				await Task.Delay(TimeSpan.FromSeconds(2), token);
			}
		}
	}
}
