using LinqToDB;
using MnemonicaBase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MnemonicaBase.Controllers
{
	public class BaseController : Controller
	{
		public ActionResult Live(string tag = "")
		{
			var tags = tag.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

			using (var db = new DatabaseContext())
			{
				var values = db.Live
					.Where(x => tags.Contains(x.TagName))
					.ToList()
					.Select(x => double.TryParse(x.Value, out double d)
						? "try{" + x.TagName + "=" + d + "}catch(e){};"
						: "try{" + x.TagName + "='" + x.Value + "'}catch(e){};"
					);

				var builder = new StringBuilder();
				builder.AppendLine("try{Time='" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "'}catch(e){};");

				foreach (var value in values)
				{
					builder.AppendLine(value);
				}

				return Content(builder.ToString());
			}
		}

		public ActionResult Last(string tag = "")
		{
			try
			{
				var tagNames = tag.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

				using (var db = new DatabaseContext())
				{
					var tags = db.Tags
						.Where(x => tagNames.Contains(x.TagName))
						.ToDictionary(x => x.TagName, x => x);

					var last = db.Live
						.Where(x => tagNames.Contains(x.TagName))
						.Select(x => new
						{
							x.TagName,
							LastChange = x.Date,
							TimeAfterChange = DateTime.Now - x.Date,
						})
						.ToDictionary(x => tags[x.TagName], x => new { x.LastChange, x.TimeAfterChange });

					return Json(new { Error = string.Empty, Data = last });
				}
			}
			catch (Exception ex)
			{
				return Json(new { Error = ex.Message, Data = new { } });
			}
		}

		public ActionResult Log(string tag = "", int take = 10, int skip = 0)
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					var tags = db.Tags
						.Where(x => string.IsNullOrEmpty(tag) || tag.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Contains(x.TagName))
						.ToDictionary(x => x.TagName, x => x);

					var last = db.History
						.Where(x => tags.Keys.Contains(x.TagName))
						.OrderByDescending(x => x.Date)
						.Skip(skip)
						.Take(take)
						.Select(x => new
						{
							x.TagName,
							Name = tags[x.TagName].Description,
							Date = x.Date.ToString("dd.MM.yyyy HH:mm:ss"),
							x.Value,
						})
						.ToList();

					return Json(new { Error = string.Empty, Data = last });
				}
			}
			catch (Exception ex)
			{
				return Json(new { Error = ex.Message, Data = new { } });
			}
		}

		public ActionResult History(string tag, string date1, string date2)
		{
			var tagNames = tag.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

			if (!DateTime.TryParse(date1, out DateTime d1) || !DateTime.TryParse(date2, out DateTime d2))
			{
				return Json(new { Error = "Даты не распознаны", Data = new { } });
			}

			using (var db = new DatabaseContext())
			{
				var tags = db.Tags
					.Where(x => tagNames.Contains(x.TagName))
					.ToDictionary(x => x.TagName, x => x);

				var values = db.History
					.Where(x => tagNames.Contains(x.TagName))
					.Where(x => x.Date >= d1 && x.Date <= d2)
					.ToList();

				var id = values
					.GroupBy(x => x.TagName)
					.Select(g => g.OrderByDescending(x => x.Id).Select(x => x.Id).First())
					.ToList();

				db.History
					.Where(x => id.Contains(x.Id))
					.ToList()
					.ForEach(x =>
					{
						x.Date = d1;
						values.Add(x);
					});

				var history = values
					.GroupBy(x => x.TagName)
					.Select(g => new
					{
						TagName = g.Key,
						Values = g
							.OrderBy(x => x.Date)
							.Select(x => new { x.Date, x.Value })
							.ToList()
					})
					.ToDictionary(x => tags[x.TagName], x => x.Values);

				return Json(new { Error = string.Empty, Data = history });
			}
		}

		public ActionResult Summary(string tag, string date1, string date2)
		{
			var tagNames = tag.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

			if (!DateTime.TryParse(date1, out DateTime d1) || !DateTime.TryParse(date2, out DateTime d2))
			{
				return Json(new { Error = "Даты не распознаны", Data = new { } });
			}

			using (var db = new DatabaseContext())
			{
				var tags = db.Tags
					.Where(x => tagNames.Contains(x.TagName))
					.ToDictionary(x => x.TagName, x => x);

				var values = db.History
					.Where(x => tagNames.Contains(x.TagName))
					.Where(x => x.Date >= d1 && x.Date <= d2)
					.ToList();

				var id = values
					.GroupBy(x => x.TagName)
					.Select(g => g.OrderByDescending(x => x.Id).Select(x => x.Id).First())
					.ToList();

				db.History
					.Where(x => id.Contains(x.Id))
					.ToList()
					.ForEach(x =>
					{
						x.Date = d1;
						values.Add(x);
					});

				var summary = new Dictionary<DbTag, Dictionary<string, TimeSpan>>();

				var history = values
					.GroupBy(x => x.TagName)
					.Select(g => new
					{
						TagName = g.Key,
						Values = g
							.OrderBy(x => x.Date)
							.ToList()
					})
					.ToList();

				foreach (var obj in history)
				{
					var states = obj.Values
						.GroupBy(x => x.Value)
						.Select(g => g.Key)
						.ToDictionary(x => x, x => TimeSpan.Zero);

					DateTime previous = d1;
					foreach (var record in obj.Values)
					{
						states[record.Value] += (record.Date - previous);
						previous = record.Date;
					}

					summary.Add(tags[obj.TagName], states);
				}

				return Json(new { Error = string.Empty, Data = summary });
			}
		}

		public ActionResult Update(string tag, string value)
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					if (db.Live.Any(x => x.TagName == tag))
					{
						db.Live
							.Where(x => x.TagName == tag)
							.Set(x => x.Value, value)
							.Set(x => x.Date, DateTime.Now)
							.Update();
					}
					else
					{
						db.Live
							.Value(x => x.TagName, tag)
							.Value(x => x.Value, value)
							.Value(x => x.Date, DateTime.Now)
							.Insert();
					}

					db.History
						.Value(x => x.TagName, tag)
						.Value(x => x.Value, value)
						.Value(x => x.Date, DateTime.Now)
						.Insert();

					return Json(new { Done = "Значение тега [" + tag + "] записано как [" + value + "]" });
				}
			}
			catch (Exception ex)
			{
				return Json(new { Error = ex.Message });
			}
		}

		public ActionResult Rename(string tag, string desc)
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					if (db.Tags.Any(x => x.TagName == tag))
					{
						db.Tags
							.Where(x => x.TagName == tag)
							.Set(x => x.Description, desc)
							.Update();
					}

					return Json(new { Done = "Тег [" + tag + "] назван как [" + desc + "]" });
				}
			}
			catch (Exception ex)
			{
				return Json(new { Error = ex.Message });
			}
		}

		public ActionResult Create(string scheme = "", string tag = null, string desc = null, string value = null)
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					if (string.IsNullOrEmpty(tag))
					{
						var id = db.Tags
							.Where(x => x.TagName.StartsWith(scheme))
							.Select(x => x.Id)
							.DefaultIfEmpty(0)
							.Max();

						tag = scheme + "_" + (++id);
					}

					if (db.Tags.Any(x => x.TagName == tag))
					{
						return Json(new { Error = "Такой тег уже определён в данной базе" });
					}

					db.Tags
						.Value(x => x.TagName, tag)
						.Value(x => x.Description, desc)
						.Insert();

					if (db.Live.Any(x => x.TagName == tag))
					{
						db.Live
							.Where(x => x.TagName == tag)
							.Set(x => x.Value, value)
							.Set(x => x.Date, DateTime.Now)
							.Update();
					}
					else
					{
						db.Live
							.Value(x => x.TagName, tag)
							.Value(x => x.Value, value)
							.Value(x => x.Date, DateTime.Now)
							.Insert();
					}

					db.History
						.Value(x => x.TagName, tag)
						.Value(x => x.Value, value)
						.Value(x => x.Date, DateTime.Now)
						.Insert();

					return Json(new {
						Done = "Создан тег [" + tag + "]: [" + desc + "], его значение записано как [" + value + "]",
						Tag = tag
					});
				}
			}
			catch (Exception ex)
			{
				return Json(new { Error = ex.Message });
			}
		}
	}
}