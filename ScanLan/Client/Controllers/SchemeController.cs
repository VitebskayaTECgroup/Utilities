using Client.Models;
using LinqToDB;
using Newtonsoft.Json;
using ScanLan.Models;
using System.Linq;
using System.Web.Mvc;

namespace Client.Controllers
{
	public class SchemeController : Controller
	{
		public ActionResult Index()
		{
			var file = System.IO.File.ReadAllText(@"\\web\files\Служебные\scanlan.json");
			var model = JsonConvert.DeserializeObject<Cache>(file);

			using (var db = new DatabaseContext())
			{
				var switches = db.Switches
					.Select(x => new
					{
						x.Ip,
						x.X,
						x.Y,
					})
					.ToList();

				foreach (var @switch in model.Switches)
				{
					foreach (var s in switches)
					{
						if (@switch.Ip == s.Ip)
						{
							@switch.X = s.X;
							@switch.Y = s.Y;
							break;
						}
					}
				}
			}

			return View(model);
		}

		public ActionResult Update(int X, int Y, string mac)
		{
			using (var db = new DatabaseContext())
			{
				db.Switches
					.Where(x => x.Mac == mac)
					.Set(x => x.X, X)
					.Set(x => x.Y, Y)
					.Update();
			}

			return Json(new { X, Y, mac });
		}
	}
}