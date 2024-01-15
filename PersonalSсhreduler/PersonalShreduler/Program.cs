using Dapper;
using LinqToDB;
using PersonalShreduler.Checkpoint;
using PersonalShreduler.Dbf;
using PersonalShreduler.Site;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalSсhreduler
{
	class Program
	{
		static void Main()
		{
			try
			{
				// Загрузка данных с выгрузки 1С
				var personal = new List<Person>();

				using (var db = new OleDbConnection(ConfigurationManager.ConnectionStrings["Dbf"].ToString()))
				{
					string sql = @"SELECT
                        TABNOMER AS [TabId],
                        FAMILY   AS [Family],
                        PODR     AS [Guild],
                        DATAROJD AS [BirthDate]
                    FROM T2Vvod.DBF";

					personal = db.Query<Person>(sql).ToList();
				}

				personal = personal
					.Where(x => x.TabId != 0)
					.Where(x => !string.IsNullOrEmpty(x.Guild))
					.Where(x => !string.IsNullOrEmpty(x.Family))
					.ToList();

				foreach (var person in personal)
				{
					person.Sanitaze();
				}

				using (var db = new SiteContext())
				{
					db.Personal
						.Where(x => !personal.Select(k => k.TabId).Contains(x.TabId))
						.Delete();

					foreach (var person in personal)
					{
						if (db.Personal.Count(x => x.TabId == person.TabId) == 0)
						{
							db.Insert(new Personal
							{
								TabId = person.TabId,
								Family = person.Family,
								BirthDate = person.BirthDate,
								Guild = person.Guild,
								OnWork = false,
							});
						}
					}

					// Инфа с проходной, чтобы отслеживать, кто есть на работе

					var identities = db.Personal.Select(x => x.TabId).ToList();

					using (var db2 = new CheckpointContext())
					{
						var tabs = db2.OWNERS
							.ToDictionary(x => x.OW_ID, x => int.TryParse(x.OW_TABNUM.Trim(), out int i) ? i : 0);

						var events = db2.EVENTS
							.Where(x => x.EV_TSTAMP > DateTime.Today.AddDays(-1) && x.EV_OW_ID != null)
							.OrderBy(x => x.EV_TSTAMP)
							.ToList()
							.Select(x => new
							{
								x.EV_TSTAMP,
								x.EV_ADDR,
								TabId = tabs[x.EV_OW_ID.Value]
							})
							.ToList()
							.GroupBy(x => x.TabId)
							.Select(g => new
							{
								TabId = g.Key,
								OnWork = (g.OrderByDescending(x => x.EV_TSTAMP).FirstOrDefault()?.EV_ADDR ?? 0) == 1
							})
							.ToList();

						foreach (var ev in events)
						{
							Console.WriteLine(ev.TabId + " | " + ev.OnWork);

							db.Personal
								.Where(x => x.TabId == ev.TabId)
								.Set(x => x.OnWork.Value, ev.OnWork)
								.Update();
						}


					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message, e.StackTrace);
				File.WriteAllText(AppContext.BaseDirectory + "err.txt", e.Message + "\n" + e.StackTrace);
			}

			Task.Delay(5000).Wait();
		}
	}
}