using LinqToDB;
using Logger_Library.Agent;
using Logger_Library.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Logger_Server.Http.Api
{
	public static class Agent
	{
		public static object Reply(AgentReply reply)
		{
			Console.WriteLine("Get reply from [" + reply.AgentId + "], " + reply.Logs.Count + " logs, " + reply.Specifications.Count + " specs");

			try
			{

				using (var db = new DatabaseContext())
				{
					foreach (var log in reply.Logs)
					{
						db.Logs
							.Value(x => x.AgentId, log.AgentId)
							.Value(x => x.TimeStamp, log.TimeStamp)
							.Value(x => x.Journal, log.Journal)
							.Value(x => x.Source, log.Source)
							.Value(x => x.EventId, log.EventId)
							.Value(x => x.Category, log.Category)
							.Value(x => x.Type, log.Type)
							.Value(x => x.Computer, log.Computer)
							.Value(x => x.Username, log.Username)
							.Value(x => x.Message, log.Message)
							.Insert();
					}

					foreach (var spec in reply.Specifications)
					{
						db.Specifications
							.Value(x => x.AgentId, spec.AgentId)
							.Value(x => x.Computer, spec.Computer)
							.Value(x => x.Category, spec.Category)
							.Value(x => x.Device, spec.Device)
							.Value(x => x.Field, spec.Field)
							.Value(x => x.Value, spec.Value)
							.Insert();
					}
				}

				return new { Done = true };
			}
			catch (Exception e)
			{
				return new { Done = false, Error = e };
			}
		}

		public static object List()
		{
			using (var db = new DatabaseContext())
			{
				var agents = db.Agents
					.Select(x => new
					{
						x.Id,
						x.Endpoint,
						LastTimeAlive = x.LastTimeAlive.ToString("dd.MM.yyyy HH:mm:ss"),
						x.Description,
					})
					.ToList();

				return agents;
			}
		}

		public static object Add(AgentForm form)
		{
			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					UseShellExecute = false,
					FileName = "cmd.exe",
					RedirectStandardError = true,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};

			var output = new List<string>();

			// Остановка службы
			Command("/c sc \\\\" + form.Endpoint + " stop Logger");

			// Копирование файлов
			string source = Environment.CurrentDirectory + "\\Content\\Agent\\";
			string dest = "\\\\" + form.Endpoint + "\\c$\\Program Files\\Logger\\";
			if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);
			dest += "Logger Agent\\";
			if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);
			foreach (var file in Directory.GetFiles(source))
			{
				Console.WriteLine(" > " + file);
				Console.WriteLine(" < " + file.Replace(source, dest));
				File.Copy(file, file.Replace(source, dest), true);
			}

			// Пересоздание и запуск службы
			Command("/c sc \\\\" + form.Endpoint + " create Logger DisplayName=\"Logger Agent\" binPath=\"C:\\Program Files\\Logger\\Logger Agent\\Logger Agent.exe\" start=auto");
			Command("/c sc \\\\" + form.Endpoint + " start Logger");

			return new { Done = "Агент установлен", Output = output };

			void Command(string cmd)
			{
				process.StartInfo.Arguments = cmd;
				output.Add(cmd);
				process.Start();
				process.WaitForExit();
				output.Add(process.StandardOutput.ReadToEnd());
			}
		}
	}

	public class AgentForm
	{
		public string Endpoint;
		public string Description;
	}
}
