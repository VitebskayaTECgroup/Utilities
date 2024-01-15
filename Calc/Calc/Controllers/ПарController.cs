using Calc.Models;
using Dapper;
using LinqToDB;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Calc.Controllers
{
	public class ПарController : Controller
    {
		public ActionResult КК()
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

		public ActionResult ВЗРД()
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

		public ActionResult ДОК()
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

		public ActionResult КОВРЫ()
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

		public ActionResult ВКШТ()
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

		public ActionResult РУБИКОН()
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

		public ActionResult ПаротрассаКИМ()
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

		public ActionResult КИМ2()
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

		public ActionResult КИМ()
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

		public ActionResult ОБЩИЙ_ОТПУСК_ТЕПЛА()
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

		public ActionResult СВОДНЫЙ_ЖУРНАЛ()
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

		public ActionResult Диагностика()
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
					if (name == "ДОК")
					{
						string sql = $@"
						SET QUOTED_IDENTIFIER OFF
						SELECT
							DateTime - 1    as [DateRec],
							U02_UN11P001_S1 as [PП22],
							U02_UN11T001_S1 as [tП22],
							U02_UN11F001_S1 as [GkП22],
							U02_UN11W001_S1 as [QП22],
							U02_UN12P001_S1 as [PП33],
							U02_UN12T001_S1 as [tП33],
							U02_UN12F001_S1 as [GkП33],
							U02_UN12W001_S1 as [QП33],
							U02_UN21T001_S1 as [tK],
							U02_UN21F001_S1 as [GK],
							U02_UN21W001_S1 as [QK],
							U02_VB00T001_S1 as [tХИ]
						FROM OpenQuery(INSQL,""SELECT DateTime, U02_UN11P001_S1, U02_UN11T001_S1, U02_UN11F001_S1, U02_UN11W001_S1, U02_UN12P001_S1, U02_UN12T001_S1, U02_UN12F001_S1, U02_UN12W001_S1, U02_UN21T001_S1, U02_UN21F001_S1, U02_UN21W001_S1, U02_VB00T001_S1
							FROM Runtime.dbo.AnalogWideHistory
							WHERE wwVersion = 'Latest' AND wwRetrievalMode = 'Cyclic'
							AND wwResolution = 86400000
							AND DateTime >= '{_start:yyyyMMdd} 01:05:00'
							AND DateTime <= '{_end:yyyyMMdd} 01:05:00'"")";

						var imported = conn.Query<ДОК>(sql).AsList();

						if (imported == null) return Content("error:Диапазон импортированных данных не получен");

						foreach (var row in imported)
						{
							db.ДОК
								.Where(x => x.DateRec.Date == row.DateRec.Date)
								.Set(x => x.PП22, row.PП22.AsPressure(2))
								.Set(x => x.tП22, row.tП22.AsTemperature(1))
								.Set(x => x.GkП22, row.GkП22.AsMass(0))
								.Set(x => x.QП22, row.QП22.AsHeat(1))
								.Set(x => x.PП33, row.PП33.AsPressure(2))
								.Set(x => x.tП33, row.tП33.AsTemperature(1))
								.Set(x => x.GkП33, row.GkП33.AsMass(0))
								.Set(x => x.QП33, row.QП33.AsHeat(1))
								.Set(x => x.tK, row.tK.AsTemperature(1))
								.Set(x => x.GK, row.GK.AsMass(0))
								.Set(x => x.QK, row.QK.AsHeat(1))
								.Set(x => x.tХИ, row.tХИ.AsTemperature(1))
								.Update();
						}

						return Content("done:Данные успешно импортированы");
					}
					else if (name == "ПаротрассаКИМ")
					{
						string sql = $@"
						SET QUOTED_IDENTIFIER OFF
						SELECT
							DateTime - 1    as [DateRec],
							U01_UN11P001_S1 as [PП],
							U01_UN11T001_S1 as [tП],
							U01_UN11F001_S1 as [GkП],
							U01_UN11W001_S1 as [QП],
							U01_UN21T001_S1 as [tK],
							U01_UN21F001_S1 as [GK],
							U01_UN21W001_S1 as [QK],
							U01_VB01T001_S1 as [tХИ]
						FROM OpenQuery(INSQL,""SELECT DateTime, U01_UN11P001_S1, U01_UN11T001_S1, U01_UN11F001_S1, U01_UN11W001_S1, U01_UN21T001_S1, U01_UN21F001_S1, U01_UN21W001_S1, U01_VB01T001_S1
							FROM Runtime.dbo.AnalogWideHistory
							WHERE wwVersion = 'Latest' AND wwRetrievalMode = 'Cyclic'
							AND wwResolution = 86400000
							AND DateTime >= '{_start:yyyyMMdd} 01:05:00'
							AND DateTime <= '{_end:yyyyMMdd} 01:05:00'"")";

						var imported = conn.Query<ПаротрассаКИМ>(sql).AsList();

						if (imported == null) return Content("error:Диапазон импортированных данных не получен");

						foreach (var row in imported)
						{
							db.ПаротрассаКИМ
								.Where(x => x.DateRec.Date == row.DateRec.Date)
								.Set(x => x.PП, row.PП.AsPressure(2))
								.Set(x => x.tП, row.tП.AsTemperature(1))
								.Set(x => x.GkП, row.GkП.AsMass(0))
								.Set(x => x.QП, row.QП.AsHeat(1))
								.Set(x => x.tK, row.tK.AsTemperature(1))
								.Set(x => x.GK, row.GK.AsMass(0))
								.Set(x => x.QK, row.QK.AsHeat(1))
								.Set(x => x.tХИ, row.tХИ.AsTemperature(1))
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