using CsvHelper;
using InsqlDumper.Database;
using InsqlDumper.Runtime.Output;
using InsqlDumper.WWALMDB;
using InsqlDumper.WWALMDB.Output;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace InsqlDumper
{
	internal class Program
	{
		static void Main()
		{
			#if DEBUG
			DataConnection.TurnTraceSwitchOn();
			DataConnection.WriteTraceLine = (s, s1, tl) => Debug.WriteLine(s + " | " + tl);
			#endif

			List<Tag> tags;

			using (var db = new RuntimeContext())
			{
				tags = db.Tag
					.ToList();
			}

			var start = DateTime.Parse("07.10.2023");

			while (start < DateTime.Now)
			{
				Console.WriteLine(DateTime.Now + " | Processing " + start.ToString("dd.MM.yyyy"));

				var values = new List<TagValue>();

				for (int i = 0; i < tags.Count + 100; i += 100)
				{
					Console.Clear();
					Console.WriteLine(DateTime.Now + " | Restart connection");

					values.Clear();

					using (var db = new RuntimeContext())
					{
						for (int j = 0; j < 100; j++)
						{
							if (i + j >= tags.Count) break;

							var tag = tags[i + j];
							var dump = db.DumpTag(tag.TagName, start, start.AddDays(1), true);
							values.AddRange(dump);

							Console.WriteLine(DateTime.Now + " | Get values of " + tag.TagName + " (" + (i + j) + " of " + tags.Count + ") (" + dump.Count + " values)");
						}
					}

					using (var writer = new StreamWriter(Environment.CurrentDirectory + "\\Output\\" + start.ToString("yyyyMMdd") + " values " + i + "-" + (i + 99) + ".csv"))
					using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
					{
						csv.WriteRecords(values.OrderBy(x => x.DateTime).ThenBy(x => x.TagName));
					}
				}

				var alarms = new List<Alarm>();

				using (var db = new AlarmContext())
				{
					alarms = db.DumpAlarms(start, start.AddDays(1));
					Console.WriteLine(DateTime.Now + " | Get alarms (" + alarms.Count + " events)");
				}

				using (var writer = new StreamWriter(Environment.CurrentDirectory + "\\Output\\" + start.ToString("yyyyMMdd") + " alarms.csv"))
				using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
				{
					csv.WriteRecords(alarms.OrderBy(x => x.Date).ThenBy(x => x.TagName));
				}

				start = start.AddDays(1);
			}
			
			Console.WriteLine(DateTime.Now + " | Done");
			Console.ReadLine();
		}
	}
}
