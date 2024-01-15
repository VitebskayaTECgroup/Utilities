using Checkpoint.Database;
using Checkpoint.Models;
using LinqToDB;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Checkpoint.Controllers
{
	public class FixController : Controller
	{
		public ActionResult Create(int Id, int Type, string Date)
		{
			using (var db = new DatabaseContext())
			{
				var owner = db.OWNERS.FirstOrDefault(x => x.OW_ID == Id);
				var card = db.OWNER_CARDS.FirstOrDefault(x => x.OC_OW_ID == Id && x.OC_RETDATE == null && x.OC_ARCHIVE == 0).OC_VALUE;

				db.Insert(new EVENTS
				{
					EV_OW_ID = Id,
					EV_CA_VALUE = card,
					EV_ADDR = Type,
					EV_TSTAMP = DateTime.Parse(Date),
					EV_TYPE = 1,
					EV_FLAGS = 0,
					EV_SECTION_PIN = null,
				});
			}

			return Content("");
		}

		public ActionResult Update(int Id, string Date)
		{
			using (var db = new DatabaseContext())
			{
				db.EVENTS
					.Where(x => x.EV_ID == Id)
					.Set(x => x.EV_TSTAMP, DateTime.Parse(Date))
					.Update();
			}

			return Content("");
		}

		public ActionResult Delete(int Id)
		{
			using (var db = new DatabaseContext())
			{
				db.EVENTS
					.Where(x => x.EV_ID == Id)
					.Delete();
			}

			return Content("");
		}
	}
}