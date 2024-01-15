using Checkpoint.Models;
using LinqToDB;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Checkpoint.Controllers
{
	public class PrintController : Controller
	{
		public ActionResult Index(int Id, string date, string mode = "day")
		{
			using (var db = new DatabaseContext())
			{
				var _date = DateTime.TryParse(date, out DateTime _d) ? _d : DateTime.Today;

				var _owner = from ow in db.OWNERS
							 from od in db.OWNER_DEPS.LeftJoin(x => x.OD_ID == ow.OW_DEP)
							 from oj in db.OWNER_JOBS.LeftJoin(x => x.OJ_ID == ow.OW_JOB)
							 from ca in db.OWNER_CARDS.LeftJoin(x => x.OC_OW_ID == ow.OW_ID && x.OC_RETDATE == null)
							 from ag in db.ACCESS_GROUPS.LeftJoin(x => x.AGR_ID == ca.OC_ACCESS_GROUP)
							 where ow.OW_ID == Id
							 select new
							 {
								 ow.OW_ID,
								 ow.OW_TABNUM,
								 ow.OW_LNAME,
								 ow.OW_FNAME,
								 ow.OW_MNAME,
								 od.OD_NAME,
								 oj.OJ_NAME,
								 ag.AGR_NAME,
								 Src = (ow.OW_LNAME + ' ' + ow.OW_FNAME + ' ' + ow.OW_MNAME).ToLower() + ".jpg",
							 };

				var owner = _owner.FirstOrDefault();

				var next = _date.AddDays(1).AddSeconds(-1);
				var prev = mode == "month"
					? _date.AddMonths(-1).AddDays(1)
					: mode == "week"
						? _date.AddDays(-6)
						: _date;

				var events = db.EVENTS
					.Where(x => x.EV_OW_ID == Id && x.EV_TSTAMP <= next && x.EV_TSTAMP > prev)
					.OrderBy(x => x.EV_TSTAMP)
					.Select(x => new
					{
						Date = x.EV_TSTAMP.ToString("dd.MM.yyyy"),
						Time = x.EV_TSTAMP.ToString("HH:mm:ss"),
						Type = x.EV_ADDR == 1 ? "Вход 1" :
							x.EV_ADDR == 2 ? "Выход 1" :
							x.EV_ADDR == 1001 ? "Вход 2" :
							x.EV_ADDR == 1002 ? "Выход 2" :
							"Неопределено",
					})
					.ToList()
					.GroupBy(x => x.Date)
					.Select(x => new
					{
						Date = x.Key,
						Events = x.Select(y => new
						{
							y.Time,
							y.Type,
						})
					})
					.ToList();

				var path = Url.Action("docs/1.xlsx", "content");
				var excel = new XSSFWorkbook();
				var sheet = excel.CreateSheet();

				var row = sheet.CreateRow(0);
				row.CreateCell(1).SetCellValue("Филиал \"Витебская ТЭЦ\" РУП \"Витебскэнерго\"");
				sheet.AddMergedRegion(new CellRangeAddress(0, 0, 1, 6));

				row = sheet.CreateRow(2);
				row.CreateCell(1).SetCellValue("Цех");
				row.CreateCell(3).SetCellValue(owner.AGR_NAME);

				row = sheet.CreateRow(3);
				row.CreateCell(1).SetCellValue("Подразделение");
				row.CreateCell(3).SetCellValue(owner.OD_NAME);

				row = sheet.CreateRow(4);
				row.CreateCell(1).SetCellValue("Табельный №");
				row.CreateCell(3).SetCellValue(owner.OW_TABNUM);

				row = sheet.CreateRow(5);
				row.CreateCell(1).SetCellValue("Фамилия");
				row.CreateCell(3).SetCellValue(owner.OW_LNAME);

				row = sheet.CreateRow(6);
				row.CreateCell(1).SetCellValue("Имя");
				row.CreateCell(3).SetCellValue(owner.OW_FNAME);

				row = sheet.CreateRow(7);
				row.CreateCell(1).SetCellValue("Отчество");
				row.CreateCell(3).SetCellValue(owner.OW_MNAME);

				for (int i = 2; i < 8; i++)
				{
					sheet.AddMergedRegion(new CellRangeAddress(i, i, 1, 2));
					sheet.AddMergedRegion(new CellRangeAddress(i, i, 3, 6));
				}

				row = sheet.CreateRow(9);
				row.CreateCell(1).SetCellValue("Дата");
				row.CreateCell(3).SetCellValue("Событие");
				row.CreateCell(5).SetCellValue("Время события");
				sheet.AddMergedRegion(new CellRangeAddress(9, 9, 1, 2));
				sheet.AddMergedRegion(new CellRangeAddress(9, 9, 3, 4));
				sheet.AddMergedRegion(new CellRangeAddress(9, 9, 5, 6));

				int currentRow = 10;
				foreach (var e in events)
				{
					int startRow = currentRow;
					foreach (var t in e.Events)
					{
						row = sheet.CreateRow(currentRow);
						row.CreateCell(3).SetCellValue(t.Type);
						row.CreateCell(5).SetCellValue(t.Time);

						sheet.AddMergedRegion(new CellRangeAddress(currentRow, currentRow, 3, 4));
						sheet.AddMergedRegion(new CellRangeAddress(currentRow, currentRow, 5, 6));

						currentRow++;
					}

					sheet.GetRow(startRow).CreateCell(1).SetCellValue(e.Date);
					sheet.AddMergedRegion(new CellRangeAddress(startRow, currentRow - 1, 1, 2));
				}

				using (var stream = new FileStream(Server.MapPath(path), FileMode.Create))
				{
					excel.Write(stream);
				}

				return Content(path);
			}
		}

		public ActionResult Table(string search, string date, string department, string group, string onlybad, string onlyunseen, string onlyrefused)
		{
			// Получаем данные по выбранным критериям поиска
			List<Owner> model;
			var viewDate = (DateTime.TryParse(date, out DateTime _date) ? _date : DateTime.Today).ToString("d MMMM yyyy г.");
			string filter = "";

			using (var db = new DatabaseContext())
			{
				model = db.GetTableData(search, date, department, group, onlybad, onlyunseen, onlyrefused);

				var departmentId = int.TryParse(department, out int i) ? i : 0;
				var groupId = int.TryParse(group, out i) ? i : 0;
				onlybad = onlybad == "true" ? "с нарушениями" : "";
				onlyunseen = onlyunseen == "true" ? "не прошедшие" : "";

				department = db.OWNER_DEPS
					.Where(x => x.OD_ID == departmentId)
					.Select(x => x.OD_NAME)
					.FirstOrDefault() ?? "Все";

				group = db.ACCESS_GROUPS
					.Where(x => x.AGR_ID == groupId)
					.Select(x => x.AGR_NAME)
					.FirstOrDefault() ?? "Все";

				if (!string.IsNullOrEmpty(search))
				{
					filter += "запрос: \"" + search + "\"";
				}
				if (onlybad != "")
				{
					if (filter != "") { filter += ", "; }
					filter += onlybad;
				}
				if (onlyunseen != "")
				{
					if (filter != "") { filter += ", "; }
					filter += onlyunseen;
				}
				if (filter == "")
				{
					filter = "без фильтра"; 
				}
			}

			// Формируем Excel распечатку

			var path = Url.Action("docs/", "content") + "Проходная за " + viewDate + ".xlsx";
			var excel = new XSSFWorkbook();
			var sheet = excel.CreateSheet();

			sheet.PrintSetup.Landscape = true;

			var row = sheet.CreateRow(0);
			row.CreateCell(0).SetCellValue("Филиал \"Витебская ТЭЦ\" РУП \"Витебскэнерго\"");
			sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 8));

			row = sheet.CreateRow(1);
			row.CreateCell(0).SetCellValue("Дата");
			row.CreateCell(2).SetCellValue(viewDate);
			sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, 1));
			sheet.AddMergedRegion(new CellRangeAddress(1, 1, 2, 3));

			row = sheet.CreateRow(2);
			row.CreateCell(0).SetCellValue("Группа");
			row.CreateCell(2).SetCellValue(group);
			sheet.AddMergedRegion(new CellRangeAddress(2, 2, 0, 1));
			sheet.AddMergedRegion(new CellRangeAddress(2, 2, 2, 3));

			row = sheet.CreateRow(3);
			row.CreateCell(0).SetCellValue("Подразделение");
			row.CreateCell(2).SetCellValue(department);
			sheet.AddMergedRegion(new CellRangeAddress(3, 3, 0, 1));
			sheet.AddMergedRegion(new CellRangeAddress(3, 3, 2, 3));

			row = sheet.CreateRow(4);
			row.CreateCell(0).SetCellValue("Фильтр");
			row.CreateCell(2).SetCellValue(filter);
			sheet.AddMergedRegion(new CellRangeAddress(4, 4, 0, 1));
			sheet.AddMergedRegion(new CellRangeAddress(4, 4, 2, 3));

			row = sheet.CreateRow(5);
			row.CreateCell(0).SetCellValue("Группа");
			row.CreateCell(1).SetCellValue("Подразделение");
			row.CreateCell(2).SetCellValue("Таб. №");
			row.CreateCell(3).SetCellValue("Ф.И.О.");
			row.CreateCell(4).SetCellValue("Вход");
			row.CreateCell(5).SetCellValue("Выход");
			row.CreateCell(6).SetCellValue("Вход");
			row.CreateCell(7).SetCellValue("Выход");
			row.CreateCell(8).SetCellValue("Время на раб.");

			int currentRow = 6;
			foreach (var owner in model)
			{
				row = sheet.CreateRow(currentRow);

				row.CreateCell(0).SetCellValue(owner.Group);
				row.CreateCell(1).SetCellValue(owner.Department);
				row.CreateCell(2).SetCellValue(owner.TableId);
				row.CreateCell(3).SetCellValue(owner.Official);
				row.CreateCell(4).SetCellValue(owner.WorkEnter.ToTime());
				row.CreateCell(5).SetCellValue(owner.DinnerLeave.ToTime());
				row.CreateCell(6).SetCellValue(owner.DinnerReturn.ToTime());
				row.CreateCell(7).SetCellValue(owner.WorkLeave.ToTime());
				row.CreateCell(8).SetCellValue(owner.Summary);

				currentRow++;
			}

			for (int i = 0; i < 9; i++)
			{
				sheet.AutoSizeColumn(i, true);
			}

			using (var stream = new FileStream(Server.MapPath(path), FileMode.Create))
			{
				excel.Write(stream);
			}

			return Content(path);
		}
	}
}