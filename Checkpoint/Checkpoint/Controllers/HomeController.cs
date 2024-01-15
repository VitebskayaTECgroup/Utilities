using Checkpoint.Models;
using System;
using System.Web.Mvc;
using LinqToDB;
using System.Linq;
using System.Net;

namespace Checkpoint.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index() => View();

		public ActionResult Test() => View("Test");

		public ActionResult List(string search, string date, string department, string group, string onlybad, string onlyunseen, string onlyrefused) 
		{
			using (var db = new DatabaseContext())
			{
				var model = db.GetTableData(search, date, department, group, onlybad, onlyunseen, onlyrefused);

				return View("List", model);
			}
		}

		public ActionResult Timeline(string search, string date, string type)
		{
			using (var db = new DatabaseContext())
			{
				var viewDate = DateTime.TryParse(date, out DateTime _date) ? _date : DateTime.Today;
				var typeId = int.TryParse(type, out int i) ? i : 0;

				if (string.IsNullOrEmpty(search))
				{
					var events = from ev in db.EVENTS
								 from et in db.EVENT_DESC.LeftJoin(x => x.ED_CODE == ev.EV_TYPE)
								 from ow in db.OWNERS.LeftJoin(x => x.OW_ID == ev.EV_OW_ID)
								 from od in db.OWNER_DEPS.LeftJoin(x => x.OD_ID == ow.OW_DEP)
								 from ca in db.OWNER_CARDS.LeftJoin(x => x.OC_VALUE == ev.EV_CA_VALUE)
								 from ag in db.ACCESS_GROUPS.LeftJoin(x => x.AGR_ID == ca.OC_ACCESS_GROUP)
								 where ev.EV_TSTAMP >= viewDate
									&& ev.EV_TSTAMP < viewDate.AddDays(1)
									&& ca.OC_RETDATE == null
									&& (typeId == 0 || typeId == et.ED_CODE)
								 orderby ev.EV_TSTAMP descending
								 select new Timeline
								 {
									 Id = ev.EV_ID,
									 Date = ev.EV_TSTAMP,
									 Type = et.ED_NAME,
									 InitialType = ev.EV_TYPE,
									 Direction = Event.FromType(ev.EV_ADDR, ev.EV_TYPE),
									 Official = ow.OW_LNAME + ' ' + ow.OW_FNAME + ' ' + ow.OW_MNAME,
									 Group = ag.AGR_NAME,
									 Department = od.OD_NAME,
								 };

					return View("Timeline", events.ToList());
				}
				else
				{
					var _events = from ev in db.EVENTS
								  from et in db.EVENT_DESC.LeftJoin(x => x.ED_CODE == ev.EV_TYPE)
								  from ow in db.OWNERS.LeftJoin(x => x.OW_ID == ev.EV_OW_ID)
								  from od in db.OWNER_DEPS.LeftJoin(x => x.OD_ID == ow.OW_DEP)
								  from ca in db.OWNER_CARDS.LeftJoin(x => x.OC_VALUE == ev.EV_CA_VALUE)
								  from ag in db.ACCESS_GROUPS.LeftJoin(x => x.AGR_ID == ca.OC_ACCESS_GROUP)
								  where ev.EV_TSTAMP >= viewDate && ev.EV_TSTAMP <= viewDate.AddDays(1) && ca.OC_RETDATE == null
								  select new Timeline
								  {
									  Id = ev.EV_ID,
									  Date = ev.EV_TSTAMP,
									  Type = et.ED_NAME,
									  InitialType = ev.EV_TYPE,
									  Direction = Event.FromType(ev.EV_ADDR, ev.EV_TYPE),
									  Official = ow.OW_LNAME + ' ' + ow.OW_FNAME + ' ' + ow.OW_MNAME,
									  Group = ag.AGR_NAME,
									  Department = od.OD_NAME,
								  };

					var events = _events.ToList();

					events = events
						.Where(x => (x.Type + x.Direction + x.Official + x.Group + x.Department).ToLower().Contains(search.ToLower()))
						.ToList();

					return View("Timeline", events);
				}
			}
		}

		public ActionResult TimelineUpdate(string last, string search, string date, string type)
		{
			using (var db = new DatabaseContext())
			{
				var lastDate = DateTime.TryParse(last, out DateTime _date) ? _date : DateTime.Today;
				var viewDate = DateTime.TryParse(date, out _date) ? _date : DateTime.Today;
				var typeId = int.TryParse(type, out int i) ? i : 0;

				if (string.IsNullOrEmpty(search))
				{
					var events = from ev in db.EVENTS
								 from et in db.EVENT_DESC.LeftJoin(x => x.ED_CODE == ev.EV_TYPE)
								 from ow in db.OWNERS.LeftJoin(x => x.OW_ID == ev.EV_OW_ID)
								 from od in db.OWNER_DEPS.LeftJoin(x => x.OD_ID == ow.OW_DEP)
								 from ca in db.OWNER_CARDS.LeftJoin(x => x.OC_VALUE == ev.EV_CA_VALUE)
								 from ag in db.ACCESS_GROUPS.LeftJoin(x => x.AGR_ID == ca.OC_ACCESS_GROUP)
								 where ev.EV_TSTAMP > lastDate
									&& ca.OC_RETDATE == null
									&& (typeId == 0 || typeId == et.ED_CODE)
								 orderby ev.EV_TSTAMP descending
								 select new Timeline
								 {
									 Id = ev.EV_ID,
									 Date = ev.EV_TSTAMP,
									 Type = et.ED_NAME,
									 InitialType = ev.EV_TYPE,
									 Direction = Event.FromType(ev.EV_ADDR, ev.EV_TYPE),
									 Official = ow.OW_LNAME + ' ' + ow.OW_FNAME + ' ' + ow.OW_MNAME,
									 Group = ag.AGR_NAME,
									 Department = od.OD_NAME,
								 };

					return View("TimelineUpdate", events.ToList());
				}
				else
				{
					var _events = from ev in db.EVENTS
								  from et in db.EVENT_DESC.LeftJoin(x => x.ED_CODE == ev.EV_TYPE)
								  from ow in db.OWNERS.LeftJoin(x => x.OW_ID == ev.EV_OW_ID)
								  from od in db.OWNER_DEPS.LeftJoin(x => x.OD_ID == ow.OW_DEP)
								  from ca in db.OWNER_CARDS.LeftJoin(x => x.OC_VALUE == ev.EV_CA_VALUE)
								  from ag in db.ACCESS_GROUPS.LeftJoin(x => x.AGR_ID == ca.OC_ACCESS_GROUP)
								  where ev.EV_TSTAMP > lastDate && ca.OC_RETDATE == null
								  select new Timeline
								  {
									  Id = ev.EV_ID,
									  Date = ev.EV_TSTAMP,
									  Type = et.ED_NAME,
									  InitialType = ev.EV_TYPE,
									  Direction = Event.FromType(ev.EV_ADDR, ev.EV_TYPE),
									  Official = ow.OW_LNAME + ' ' + ow.OW_FNAME + ' ' + ow.OW_MNAME,
									  Group = ag.AGR_NAME,
									  Department = od.OD_NAME,
								  };

					var events = _events.ToList();

					events = events
						.Where(x => (x.Type + x.Direction + x.Official + x.Group + x.Department).ToLower().Contains(search.ToLower()))
						.ToList();

					return View("TimelineUpdate", events);
				}
			}
		}


		public ActionResult Card(int Id) => View(model: Id);

		public ActionResult Events(int Id) => View(model: Id);
	}
}