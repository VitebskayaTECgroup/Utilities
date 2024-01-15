using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calc.Controllers
{
	public class DefaultController : Controller
	{
		public ActionResult Index() => View();

		public ActionResult Erase(string date, string page)
		{
			if (!DateTime.TryParse(date, out DateTime _date)) return Content("error:Дата не распознана");

			using (var conn = new SqlConnection(this.Calc()))
			{
				foreach (var key in Request.Form.AllKeys)
				{
					if (key.Contains("."))
					{
						string table = key.Substring(0, key.IndexOf('.'));
						string field = key.Substring(key.IndexOf('.') + 1);
						bool isError = false;
						string exception = null;

						var oldValue = conn.Query<double?>("SELECT " + field + " FROM _" + table + "_ WHERE DateRec = '" + _date.ToString("yyyyMMdd") + "'")
								.AsList()
								.FirstOrDefault() ?? null;

						try
						{
							conn.Execute("UPDATE _" + table + "_ SET " + field + " = NULL WHERE DateRec = '" + _date.ToString("yyyyMMdd") + "'");
						}
						catch (Exception e1)
						{
							exception = e1.Message;
							isError = true;
						}

						conn.Execute("INSERT INTO Logs (Timestamp, Username, Page, Tablename, Date, Name, Value, OldValue, IsError, Exception) " +
							"VALUES (@Timestamp, @Username, @Page, @Tablename, @Date, @Name, NULL, @OldValue, @IsError, @Exception)", new
						{
							Timestamp = DateTime.Now,
							Username = User.Identity.Name,
							Page = HttpUtility.UrlDecode(page),
							Tablename = table,
							_date.Date,
							Name = field,
							OldValue = oldValue,
							IsError = isError,
							Exception = exception,
						});
					}
				}
			}

			return Content("done:Данные обновлены");
		}

		public ActionResult Commit(string date, string page)
		{
			if (!DateTime.TryParse(date, out DateTime _date)) return Content("error:Дата не распознана");

			using (var conn = new SqlConnection(this.Calc()))
			{
				foreach (var key in Request.Form.AllKeys)
				{
					if (key.Contains("."))
					{
						string table = key.Substring(0, key.IndexOf('.'));
						string field = key.Substring(key.IndexOf('.') + 1);
						string data = Request.Form.Get(key);
						bool isError = false;
						string exception = null;

						var oldValue = conn.Query<double?>("SELECT " + field + " FROM _" + table + "_ WHERE DateRec = '" + _date.ToString("yyyyMMdd") + "'")
								.AsList()
								.FirstOrDefault() ?? null;

						try
						{
							conn.Execute("UPDATE _" + table + "_ SET " + field + " = '" + data + "' WHERE DateRec = '" + _date.ToString("yyyyMMdd") + "'");
						}
						catch (Exception e1)
						{
							exception = e1.Message;
							isError = true;
						}

						if (data != oldValue.ToString())
						{
							conn.Execute("INSERT INTO Logs (Timestamp, Username, Page, Tablename, Date, Name, Value, OldValue, IsError, Exception) " +
								"VALUES (@Timestamp, @Username, @Page, @Tablename, @Date, @Name, @Value, @OldValue, @IsError, @Exception)", new
								{
									Timestamp = DateTime.Now,
									Username = User.Identity.Name,
									Page = HttpUtility.UrlDecode(page),
									Tablename = table,
									_date.Date,
									Name = field,
									Value = data,
									OldValue = oldValue,
									IsError = isError,
									Exception = exception,
								});
						}
					}
				}
			}

			return Content("done:Данные обновлены");
		}
	}
}