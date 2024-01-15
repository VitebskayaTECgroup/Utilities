using Aggregator.Models;
using Dapper;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aggregator.Scripts
{
	public class Script_KA5
    {
        public static void Execute(int serverIndex)
        {
            try
            {
                Console.WriteLine("SCRIPT KA5");

                using (var calc = new CalcContext(serverIndex))
                {
                    // получаем самую старую дату из таблицы агрегированных данных
                    var startDate = calc.KA5
                        .Where(x => x.Mode == "Complete")
                        .Select(x => x.DateTime)
                        .DefaultIfEmpty(DateTime.Parse("02.07.2021 00:00:00"))
                        .Max();
                    Console.WriteLine("START DATE : " + startDate);

                    // составляем список дат, для которых нужно выполнить агрегирование
                    var days = (DateTime.Now.Date - startDate).Days;
                    var dates = new List<DateTime>();

                    for (int i = 1; i < days + 1; i++)
                    {
                        dates.Add(startDate.AddDays(i));
                    }

                    // Отбрасываем записи, для которых расчёт уже завершен. Они будут пересозданы
                    var uncompleted = calc.KA5
                        .Where(x => x.Mode == "Uncomplete")
                        .Select(x => x.DateTime)
                        .ToList();

                    foreach (var date in uncompleted)
                    {
                        if (!dates.Contains(date)) dates.Add(date);
                    }

                    calc.KA5
                        .Where(x => x.Mode == "Uncomplete")
                        .Delete();

                    Console.WriteLine("DAYS TO EXECUTE : " + dates.Count);

                    // по каждому из дней получаем сводную запись и записываем в агрегированную таблицу
                    var query = File.ReadAllText("sql/ka5.sql");

                    using (var runtime = new RuntimeContext(serverIndex))
                    {
                        foreach (var date in dates)
                        {

                            string sql = query
                                .Replace("@Mode", date == DateTime.Today ? "Uncomplete" : "Complete")
                                .Replace("@Start", date.ToString("yyyyMMdd") + " 00:00:00.000")
                                .Replace("@End", date.ToString("yyyyMMdd") + " 23:59:00.000");

                            var record = runtime.Connection.QueryFirstOrDefault<KA5>(sql);

                            calc.Insert(record);

                            Console.WriteLine("DATE " + date + " AGGREGATED");
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