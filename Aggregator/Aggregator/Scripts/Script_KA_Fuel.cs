using Aggregator.Models;
using Dapper;
using LinqToDB;
using System;
using System.IO;
using System.Linq;

namespace Aggregator.Scripts
{
	public class Script_KA_Fuel
	{
		public static void Execute(int serverIndex)
		{
			try
			{
				Console.WriteLine("SCRIPT KA_FUEL");

				using (var calc = new CalcContext(serverIndex))
				{
					// Найти даты, по которым данных нет, начиная со стартовой до текущей
					var start = calc.KA_Fuel
						.Select(x => x.DateTime)
						.DefaultIfEmpty(DateTime.Parse("01.01.2017 00:00"))
						.Max();

					int days = Convert.ToInt32((DateTime.Now - start).TotalDays);

					Console.WriteLine("DAYS: " + days);

					string read = File.ReadAllText("sql/ka_fuel.sql");

					using (var runtime = new RuntimeContext(serverIndex))
					{
						for (int i = 1; i < days; i++)
						{
							// Для каждой даты по часу получить часовые данные и посчитать часовые
							DateTime date = start.AddDays(i);

							if (date < DateTime.Today)
							{
								var day = runtime.Connection.QueryFirstOrDefault<KA_Fuel>(read
									.Replace("@start", date.ToString("yyyyMMdd"))
									.Replace("@finish", date.AddDays(1).ToString("yyyyMMdd")));

								if (day != null)
								{
									calc.KA_Fuel
										.Delete(x => x.DateTime == day.DateTime);
								}

								calc.Insert(day);
								Console.WriteLine("DONE : " + date);
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				File.AppendAllText(AppContext.BaseDirectory + "err.log", DateTime.Now + "\n" + e.Message + "\n" + e.StackTrace);
			}
		}
	}
}