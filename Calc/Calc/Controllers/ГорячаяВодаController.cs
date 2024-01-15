using Calc.Models;
using Dapper;
using LinqToDB;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Calc.Controllers
{
	public class ГорячаяВодаController : Controller
    {
		public ActionResult Центральная()
		{
			DateTime date = DateTime.TryParse("01." + Request.QueryString.Get("date")?.Replace('_', '.'), out DateTime d)
				? d
				: new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

			using (var db = new DatabaseContext())
			{
				if (db.CreateEmptyMonth(date))
				{
					Response.Redirect(Request.Url.OriginalString);
				}
			}

			return View();
		}

		public ActionResult Западная()
		{
			DateTime date = DateTime.TryParse("01." + Request.QueryString.Get("date")?.Replace('_', '.'), out DateTime d)
				? d
				: new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

			using (var db = new DatabaseContext())
			{
				if (db.CreateEmptyMonth(date))
				{
					Response.Redirect(Request.Url.OriginalString);
				}
			}

			return View();
		}

		public ActionResult Монолит()
		{
			DateTime date = DateTime.TryParse("01." + Request.QueryString.Get("date")?.Replace('_', '.'), out DateTime d)
				? d
				: new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

			using (var db = new DatabaseContext())
			{
				if (db.CreateEmptyMonth(date))
				{
					Response.Redirect(Request.Url.OriginalString);
				}
			}

			return View();
		}

		public ActionResult Док()
		{
			DateTime date = DateTime.TryParse("01." + Request.QueryString.Get("date")?.Replace('_', '.'), out DateTime d)
				? d
				: new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

			using (var db = new DatabaseContext())
			{
				if (db.CreateEmptyMonth(date))
				{
					Response.Redirect(Request.Url.OriginalString);
				}
			}

			return View();
		}

		public ActionResult Лучёса()
		{
			DateTime date = DateTime.TryParse("01." + Request.QueryString.Get("date")?.Replace('_', '.'), out DateTime d)
				? d
				: new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

			using (var db = new DatabaseContext())
			{
				if (db.CreateEmptyMonth(date))
				{
					Response.Redirect(Request.Url.OriginalString);
				}
			}

			return View();
		}

		public ActionResult Баланс()
		{
			DateTime date = DateTime.TryParse("01." + Request.QueryString.Get("date")?.Replace('_', '.'), out DateTime d)
				? d
				: new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

			using (var db = new DatabaseContext())
			{
				if (db.CreateEmptyMonth(date))
				{
					Response.Redirect(Request.Url.OriginalString);
				}
			}

			return View();
		}

		public ActionResult Отчёт()
		{
			DateTime date = DateTime.TryParse("01." + Request.QueryString.Get("date")?.Replace('_', '.'), out DateTime d)
				? d
				: new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

			using (var db = new DatabaseContext())
			{
				if (db.CreateEmptyMonth(date))
				{
					Response.Redirect(Request.Url.OriginalString);
				}
			}

			return View();
		}
		
		public ActionResult Import(string start, string end, string name)
		{
			if (!DateTime.TryParse(start, out DateTime _start)) return Content("error:Начальная дата не распознана");
			if (!DateTime.TryParse(end, out DateTime _end)) return Content("error:Конечная дата не распознана");

			using (var db = new DatabaseContext())
			{
				_start = _start.AddDays(1);
				_end = _end.AddDays(1);

				using (var conn = new SqlConnection(this.Runtime()))
				{
					if (name == "Центральная")
					{
						string sql = $@"
							SET QUOTED_IDENTIFIER OFF
							SELECT
								DateTime - 1    as [DateRec],
								U04_UM11F001_S1 as [Gпрям],
								U04_UM11T002_S1 as [Тпрям],
								U04_UM21F001_S1 as [Gобр],
								U04_UM21T002_S1 as [Тобр],
								U04_VB01T001_S1 as [Тист],
								U04_UM41F001_S1 as [Gподпитки],
								U04_UM11W001_S1 as [Qпрям],
								U04_UM21W001_S1 as [Qобр],
								U04_UM41T001_S1 as [Tподп]
							FROM OpenQuery(INSQL, ""SELECT DateTime, U04_UM11F001_S1, U04_UM11T002_S1, U04_UM21F001_S1, U04_UM21T002_S1, U04_VB01T001_S1, U04_UM41F001_S1, U04_UM11W001_S1, U04_UM21W001_S1, U04_UM41T001_S1
							FROM Runtime.dbo.AnalogWideHistory
							WHERE wwVersion = 'Latest'
							AND wwRetrievalMode = 'Cyclic'
							AND wwResolution = 86400000
							AND DateTime >= '{_start:yyyyMMdd} 01:05:00'
							AND DateTime <= '{_end:yyyyMMdd} 01:05:00'"")";

						var imported = conn.Query<НОВАЯ_ГОРВЦЕНТРАЛЬНАЯ>(sql).AsList();

						if (imported == null) return Content("error:Диапазон импортированных данных не получен");

						foreach (var row in imported)
						{
							db.НОВАЯ_ГОРВЦЕНТРАЛЬНАЯ
								.Where(x => x.DateRec.Date == row.DateRec.Date)
								.Set(x => x.Gпрям, row.Gпрям.AsMass(0))
								.Set(x => x.Тпрям, row.Тпрям.AsTemperature(2))
								.Set(x => x.Gобр, row.Gобр.AsMass(0))
								.Set(x => x.Тобр, row.Тобр.AsTemperature(2))
								.Set(x => x.Тист, row.Тист.AsTemperature(2))
								.Set(x => x.Gподпитки, row.Gподпитки.AsMass())
								.Set(x => x.Qпрям, row.Qпрям.AsHeat(1))
								.Set(x => x.Qобр, row.Qобр.AsHeat(1))
								.Set(x => x.Tподп, row.Tподп.AsTemperature(2))
								.Update();
						}

						return Content("done:Данные успешно импортированы");
					}
					else if (name == "Западная")
					{
						string sql = $@"
						SET QUOTED_IDENTIFIER OFF
						SELECT
							DateTime - 1    as [DateRec],
							U05_UM11F001_S1 as [Gпрям],
							U05_UM11T002_S1 as [Тпрям],
							U05_UM21F001_S1 as [Gобр],
							U05_UM21T002_S1 as [Тобр],
							U05_VB01T001_S1 as [Тист],
							U05_UM11W001_S1 as [Qпрям],
							U05_UM21W001_S1 as [Qобр]
						FROM OpenQuery(INSQL,""SELECT DateTime, U05_UM11F001_S1, U05_UM11T002_S1, U05_UM21F001_S1, U05_UM21T002_S1, U05_VB01T001_S1, U05_UM11W001_S1, U05_UM21W001_S1
							FROM Runtime.dbo.AnalogWideHistory
							WHERE wwVersion = 'Latest' AND wwRetrievalMode = 'Cyclic'
							AND wwResolution = 86400000
							AND DateTime >= '{_start:yyyyMMdd} 01:05:00'
							AND DateTime <= '{_end:yyyyMMdd} 01:05:00'"")";

						var imported = conn.Query<НОВАЯ_ГОРВЗАПАДНАЯ>(sql).AsList();

						if (imported == null) return Content("error:Диапазон импортированных данных не получен");

						foreach (var row in imported)
						{
							db.НОВАЯ_ГОРВЗАПАДНАЯ
								.Where(x => x.DateRec.Date == row.DateRec.Date)
								.Set(x => x.Gпрям, row.Gпрям.AsMass(0))
								.Set(x => x.Тпрям, row.Тпрям.AsTemperature(2))
								.Set(x => x.Gобр, row.Gобр.AsMass(0))
								.Set(x => x.Тобр, row.Тобр.AsTemperature(2))
								.Set(x => x.Тист, row.Тист.AsTemperature(2))
								.Set(x => x.Qпрям, row.Qпрям.AsHeat(1))
								.Set(x => x.Qобр, row.Qобр.AsHeat(1))
								.Update();
						}

						return Content("done:Данные успешно импортированы");
					}
					else if (name == "Монолит")
					{
						string sql = $@"
						SET QUOTED_IDENTIFIER OFF
						SELECT
							DateTime - 1    as [DateRec],
							U07_UM11G001_S1 as [Gпрям],
							U07_UM11T002_S1 as [Тпрям],
							U07_UM21G001_S1 as [Gобр],
							U07_UM21T002_S1 as [Тобр],
							U07_VB01T001_S1 as [Тист],
							U07_UM11W001_S1 as [Qпрям],
							U07_UM21W001_S1 as [Qобр]
						FROM OpenQuery(INSQL,""SELECT DateTime, U07_UM11G001_S1, U07_UM11T002_S1, U07_UM21G001_S1, U07_UM21T002_S1, U07_VB01T001_S1, U07_UM11W001_S1, U07_UM21W001_S1
							FROM Runtime.dbo.AnalogWideHistory
							WHERE wwVersion = 'Latest' AND wwRetrievalMode = 'Cyclic'
							AND wwResolution = 86400000
							AND DateTime >= '{_start:yyyyMMdd} 01:05:00'
							AND DateTime <= '{_end:yyyyMMdd} 01:05:00'"")";

						var imported = conn.Query<НОВАЯ_ГОРВМОНОЛИТ>(sql).AsList();

						if (imported == null) return Content("error:Диапазон импортированных данных не получен");

						foreach (var row in imported)
						{
							db.НОВАЯ_ГОРВМОНОЛИТ
								.Where(x => x.DateRec.Date == row.DateRec.Date)
								.Set(x => x.Gпрям, row.Gпрям.AsMass(0))
								.Set(x => x.Тпрям, row.Тпрям.AsTemperature(2))
								.Set(x => x.Gобр, row.Gобр.AsMass(0))
								.Set(x => x.Тобр, row.Тобр.AsTemperature(2))
								.Set(x => x.Тист, row.Тист.AsTemperature(2))
								.Set(x => x.Qпрям, row.Qпрям.AsHeat(1))
								.Set(x => x.Qобр, row.Qобр.AsHeat(1))
								.Update();
						}

						return Content("done:Данные успешно импортированы");
					}
					else if (name == "Док")
					{
						string sql = $@"
						SET QUOTED_IDENTIFIER OFF
						SELECT
							DateTime - 1    as [DateRec],
							U06_UM11F001_S1 as [Gпрям],
							U06_UM11T002_S1 as [Тпрям],
							U06_UM21F001_S1 as [Gобр1],
							U06_UM21T002_S1 as [Тобр1],
							U06_UM22F001_S1 as [Gобр2],
							U06_UM22T002_S1 as [Тобр2],
							U06_VB00T001_S1 as [Тист],
							U06_UM11W001_S1 as [Qпрям],
							U06_UM21W001_S1 as [Qобр1],
							U06_UM22W001_S1 as [Qобр2]
						FROM OpenQuery(INSQL,""SELECT DateTime, U06_UM11F001_S1, U06_UM11T002_S1, U06_UM21F001_S1, U06_UM21T002_S1, U06_UM22F001_S1, U06_UM22T002_S1, U06_VB00T001_S1, U06_UM11W001_S1, U06_UM21W001_S1, U06_UM22W001_S1
							FROM Runtime.dbo.AnalogWideHistory
							WHERE wwVersion = 'Latest' AND wwRetrievalMode = 'Cyclic'
							AND wwResolution = 86400000
							AND DateTime >= '{_start:yyyyMMdd} 01:05:00'
							AND DateTime <= '{_end:yyyyMMdd} 01:05:00'"")";

						var imported = conn.Query<НОВАЯ_ГОРВДОК>(sql).AsList();

						if (imported == null) return Content("error:Диапазон импортированных данных не получен");

						foreach (var row in imported)
						{
							db.НОВАЯ_ГОРВДОК
								.Where(x => x.DateRec.Date == row.DateRec.Date)
								.Set(x => x.Gпрям, row.Gпрям.AsMass(0))
								.Set(x => x.Тпрям, row.Тпрям.AsTemperature(2))
								.Set(x => x.Gобр1, row.Gобр1.AsMass(0))
								.Set(x => x.Тобр1, row.Тобр1.AsTemperature(2))
								.Set(x => x.Gобр2, row.Gобр2.AsMass(0))
								.Set(x => x.Тобр2, row.Тобр2.AsTemperature(2))
								.Set(x => x.Тист, row.Тист.AsTemperature(2))
								.Set(x => x.Qпрям, row.Qпрям.AsHeat(1))
								.Set(x => x.Qобр1, row.Qобр1.AsHeat(1))
								.Set(x => x.Qобр2, row.Qобр2.AsHeat(1))
								.Update();
						}

						return Content("done:Данные успешно импортированы");
					}
					else if (name == "Лучёса")
					{
						string sql = $@"
						SET QUOTED_IDENTIFIER OFF
						SELECT
							DateTime - 1    as [DateRec],
							U08_UM11F001_S1 as [Gпрям],
							U08_UM11T002_S1 as [Тпрям],
							U08_UM21F001_S1 as [Gобр],
							U08_UM21T002_S1 as [Тобр],
							U08_VB00T001_S1 as [Тист],
							U08_UM11W001_S1 as [Qпрям],
							U08_UM21W001_S1 as [Qобр]
						FROM OpenQuery(INSQL,""SELECT DateTime, U08_UM11F001_S1, U08_UM11T002_S1, U08_UM21F001_S1, U08_UM21T002_S1, U08_VB00T001_S1, U08_UM11W001_S1, U08_UM21W001_S1
							FROM Runtime.dbo.AnalogWideHistory
							WHERE wwVersion = 'Latest' AND wwRetrievalMode = 'Cyclic'
							AND wwResolution = 86400000
							AND DateTime >= '{_start:yyyyMMdd} 01:05:00'
							AND DateTime <= '{_end:yyyyMMdd} 01:05:00'"")";

						var imported = conn.Query<НОВАЯ_ГОРВЛУЧЕСА>(sql).AsList();

						if (imported == null) return Content("error:Диапазон импортированных данных не получен");

						foreach (var row in imported)
						{
							db.НОВАЯ_ГОРВЛУЧЕСА
								.Where(x => x.DateRec.Date == row.DateRec.Date)
								.Set(x => x.Gпрям, row.Gпрям.AsMass(0))
								.Set(x => x.Тпрям, row.Тпрям.AsTemperature(2))
								.Set(x => x.Gобр, row.Gобр.AsMass(0))
								.Set(x => x.Тобр, row.Тобр.AsTemperature(2))
								.Set(x => x.Тист, row.Тист.AsTemperature(2))
								.Set(x => x.Qпрям, row.Qпрям.AsHeat(1))
								.Set(x => x.Qобр, row.Qобр.AsHeat(1))
								.Update();
						}

						return Content("done:Данные успешно импортированы");
					}
				}
			}

			return Content("error:страница, для которой необходимо выполнить импорт, не найдена");
		}
	}
}