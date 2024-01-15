using Calc.Models;
using System;
using System.Web.Mvc;

namespace Calc.Controllers
{
	public class ВодаController : Controller
    {
		public ActionResult Речная()
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

		public ActionResult Горвода()
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

		public ActionResult Техническая()
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

		public ActionResult Сырая()
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

		public ActionResult Содержание_О2()
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
	}
}