using Client.Models;
using LinqToDB;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Client.Controllers
{
	public class NetworkController : Controller
	{
		// Таблица IP-адресов

		public ActionResult Table(string mask = "10.178.9.")
		{
			using (var db = new DatabaseContext())
			{
				var model = new NetworkTable
				{
					NetworkMask = mask,
					Endpoints = db.Network
						.Where(x => x.Ip.Contains(mask))
						.ToList()
						.OrderBy(x => int.Parse(x.Ip.Replace(mask, "")))
						.ToList()
				};

				return View(model);
			}
		}

		public ActionResult Card(string ip)
		{
			var file = System.IO.File.ReadAllText(@"\\web\files\Служебные\scanlan.json");
			var scheme = JsonConvert.DeserializeObject<Cache>(file);

			using (var db = new DatabaseContext())
			{
				var endpoint = db.Network
					.Where(x => x.Ip == ip)
					.FirstOrDefault();

				var model = new Endpoint
				{
					Ip = endpoint.Ip,
					Name = endpoint.Name,
					DnsName = endpoint.DnsName,
					MacAddress = endpoint.MacAddress,
					Description = endpoint.Description,
					Login = endpoint.Login,
					Password = endpoint.Password,
					LastUpdate = endpoint.LastUpdate,
				};

				foreach (var @switch in scheme.Switches)
				{
					foreach (var port in @switch.Ports)
					{
						if (port.Devices.Count(x => x.Mac == model.MacAddress) > 0 && port.Type == 1)
						{
							model.Port = port.Name;
							model.Switch = new Switch
							{
								Ip = @switch.Ip,
								Mac = @switch.Mac,
								Name = @switch.Name,
								Model = @switch.Model,
							};
						}
					}
				}

				return View(model);
			}
		}

		[HttpPost]
		public ActionResult Submit([Bind(Include = "Ip,Name,Description,Login,Password")] NetworkEndpoint endpoint)
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					var old = db.Network.FirstOrDefault(x => x.Ip == endpoint.Ip);

					if (old == null) return Content("errorАдрес " + endpoint.Ip + " не найден в базе данных");

					string text = null;
					if (old.Name != endpoint.Name) text += "NAME[" + old.Name + "|" + endpoint.Name + "]";
					if (old.Login != endpoint.Login) text += "LOGIN[" + old.Login + "|" + endpoint.Login + "]";
					if (old.Password != endpoint.Password) text += "PASS[" + old.Password + "|" + endpoint.Password + "]";
					if (old.Description != endpoint.Description) text += "DESC[" + old.Description + "|" + endpoint.Description + "]";

					if (text != null)
					{
						text = "IP[" + endpoint.Ip + "]" + text;

						db.Logs
							.Value(x => x.Date, DateTime.Now)
							.Value(x => x.Username, User.Identity.Name.ToLower().Replace("vst\\", ""))
							.Value(x => x.Text, text)
							.Insert();
					}

					db.Network
						.Where(x => x.Ip == endpoint.Ip)
						.Set(x => x.Name, endpoint.Name)
						.Set(x => x.Description, endpoint.Description)
						.Set(x => x.Login, endpoint.Login)
						.Set(x => x.Password, endpoint.Password)
						.Update();

					return Content("success");
				}
			}
			catch (Exception e)
			{
				return Content("error" + e.Message);
			}
		}


		// Логи

		public ActionResult Logs()
		{
			using (var db = new DatabaseContext())
			{
				var model = db.Logs
					.OrderByDescending(x => x.Date)
					.Take(30)
					.ToList();

				return View(model);
			}
		}

		public ActionResult OldLogs(string date)
		{
			using (var db = new DatabaseContext())
			{
				var model = db.Logs
					.Where(x => x.Date <= DateTime.Parse(date))
					.OrderByDescending(x => x.Date)
					.Take(30)
					.ToList();

				return View(model);
			}
		}

		[HttpPost]
		public ActionResult RemoveLog(string date)
		{
			try
			{
				using (var db = new DatabaseContext())
				{
					db.Logs
						.Where(x => x.Date == DateTime.Parse(date))
						.Delete();

					return Content("success");
				}
			}
			catch (Exception e)
			{
				return Content("error" + e.Message);
			}
		}
	}
}