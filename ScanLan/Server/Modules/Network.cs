using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ScanLan.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace ScanLan.Modules
{
	public static class Network
	{
		public static List<Endpoint> Endpoints { get; set; } = new List<Endpoint>();

		public static void Process()
		{
			// Пинг всех доступных конечных точек
			DateTime date = DateTime.Now;
			Console.WriteLine("Опрос сети запущен.");

			LoadFromFile();
			Exchange();
			GetFromXLS();
			SaveToFile();

			Console.WriteLine("Опрос сети завершен.\n\tВремя выполнения: {0}", (DateTime.Now - date).ToString("g"));
		}

		public static Endpoint GetByMac(string mac)
		{
			return Endpoints.FirstOrDefault(x => x.MacAddress == mac);
		}

		static void LoadFromFile()
		{
			try
			{
				Endpoints = JsonConvert.DeserializeObject<List<Endpoint>>(File.ReadAllText(@"\\web\files\Служебные\network.json"));
			}
			catch { }
		}

		static void Exchange()
		{
			var tasks = new List<Task>();
			foreach (var sub in new[] { 8, 9, 97, 99, 100 })
			{
				for (int i = 1; i <= 254; i++)
				{
					string ip = "10.178." + sub + "." + i;
					tasks.Add(Task.Run(() => UpdateIp(ip)));
				}
			}
			Task.WaitAll(tasks.ToArray());
		}

		static void GetFromXLS()
		{
			IWorkbook book;

			try
			{
				using (var fs = new FileStream(@"\\fs\offices\asup\-=admins=-\ip.xls", FileMode.Open, FileAccess.Read))
				{
					book = new HSSFWorkbook(fs);
				}
			}
			catch
			{
				return;
			}

			for (int i = 0; i < book.NumberOfSheets; i++)
			{
				var sheet = book.GetSheetAt(i);
				int k = 0, errCount = 0;

				while (errCount < 10)
				{
					try
					{
						var row = sheet.GetRow(k++);
						if (row != null)
						{
							string ip = (row.GetCell(0)?.StringCellValue ?? "").Trim();
							string dns = row.GetCell(1)?.StringCellValue ?? "";
							string desc = row.GetCell(2)?.StringCellValue ?? "" + " " + row.GetCell(3)?.StringCellValue ?? "";

							if (ip.Split(new[] { '.' }).Length == 4 && ip.Length <= 16)
							{
								Console.WriteLine(ip + " " + dns + " " + desc);

								var endpoint = Endpoints.FirstOrDefault(x => x.Ip == ip);
								if (endpoint != null)
								{
									if (string.IsNullOrEmpty(endpoint.DnsName)) endpoint.DnsName = dns;
									if (string.IsNullOrEmpty(endpoint.Desc)) endpoint.Desc = desc;
								}
								else
								{
									Endpoints.Add(new Endpoint
									{
										Ip = ip,
										DnsName = dns,
										Desc = desc,
										LastUpdate = DateTime.MinValue,
										MacAddress = "",
									});
								}
							}
						}
						else
						{
							errCount++;
						}
					}
					catch
					{
						errCount++;
					}
				}
			}
		}

		static void SaveToFile()
		{
			try
			{
				File.WriteAllText(@"\\web\files\Служебные\network.json", JsonConvert.SerializeObject(Endpoints));
			}
			catch { }
		}

		static string GetMac(string ip)
		{
			using (Process console = new Process())
			{
				console.StartInfo = new ProcessStartInfo
				{
					FileName = "arp",
					Arguments = "-a " + ip,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				};

				console.Start();

				string[] s = console.StandardOutput.ReadToEnd().ToLower().Split('-');

				return s.Length >= 8 ? $"{s[3].Substring(Math.Max(0, s[3].Length - 2))}-{s[4]}-{s[5]}-{s[6]}-{s[7]}-{s[8].Substring(0, 2)}" : "";
			}
		}

		static void UpdateIp(string ip)
		{
			string dns = "", mac = "";

			using (Ping ping = new Ping())
			{
				try
				{
					PingReply reply = ping.Send(ip, 150);

					if (reply.Status == IPStatus.Success)
					{
						try { mac = GetMac(ip); } catch { }
						try { dns = Dns.GetHostEntry(ip).HostName.ToUpper().Replace(".VST.VITEBSK.ENERGO.NET", ""); } catch { }

						lock (Endpoints)
						{
							var endpoint = Endpoints.FirstOrDefault(x => x.Ip == ip);

							if (endpoint != null)
							{
								endpoint.DnsName = dns;
								endpoint.LastUpdate = DateTime.Now;
								endpoint.MacAddress = mac;
							}
							else
							{
								Endpoints.Add(new Endpoint
								{
									Ip = ip,
									DnsName = dns,
									MacAddress = mac,
									LastUpdate = DateTime.Now,
								});
							}
						}

						Console.WriteLine("Пинг: " + ip + " => [" + mac + "] " + dns);
					}
				}
				catch { }
			}
		}
	}
}