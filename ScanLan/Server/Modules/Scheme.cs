using Newtonsoft.Json;
using ScanLan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ScanLan.Modules
{
	public static class Scheme
	{
		static List<SwitchPortMac> FDB { get; set; } = new List<SwitchPortMac>();

		static List<Switch> Storage { get; set; } = new List<Switch>();

		static List<Link> Links { get; set; } = new List<Link>();

		static List<string> SwitchesMacs { get; set; }

		public static void Process()
		{
			try
			{
				Console.WriteLine("Начато построение схемы");
				DateTime date = DateTime.Now;

				// работа с FDB таблицами
				LoadFDBFromFile();
				var exchanges = Storage
					.Where(x => !string.IsNullOrEmpty(x.Commands))
					.Select(x => Task.Run(() =>
					{
						Console.WriteLine("Коммутатор {0} [{1}], выполняется опрос", x.Ip, x.Model);
						try
						{
							GetOutput(x);
							ParseOutput(x);
						}
						catch { }
					}))
					.Append(Task.Run(() => Network.Process()))
					.ToArray();

				Task.WaitAll(exchanges);
				SaveFDBToFile();
				Console.WriteLine("Опрос коммутаторов завершён.\n\tВремя выполнения: {0}", (DateTime.Now - date).ToString("g"));

				// построение схемы и вычисление связей
				SwitchesMacs = Storage.Select(x => x.Mac).ToList();
				UpdateFromNetwork();
				RemoveDuplicates();
				BuildScheme();
				PrepareSwitches();
				CheckLinksIfOneFound();
				CheckLinksIfManyFound();
				ListSwitchesWithoutLinks();
				SaveSchemeToFile();
				Console.WriteLine("Создано связей: {0}", Links.Count);
				Console.WriteLine("Построение схемы коммутаторов завершено.\n\tВремя завершения: {0}\n\tВремя выполнения: {1}", DateTime.Now.ToString(), (DateTime.Now - date).ToString("g"));
			}
			catch (Exception e)
			{
				Console.WriteLine("Ошибка при построении схемы! {0}", e.Message);
			}

			void GetOutput(Switch @switch)
			{
				@switch.Output = new List<string>();

				using (TcpClient socket = new TcpClient(@switch.Ip, 23))
				{
					using (NetworkStream stream = socket.GetStream())
					{
						StreamWriter output = new StreamWriter(stream);
						StreamReader input = new StreamReader(stream);

						Task reader = new Task(() =>
						{
							try
							{
								string line;
								while ((line = input.ReadLine()) != null)
								{
									@switch.Output.Add(line);
									//Console.WriteLine(@switch.Ip + " => " + line);
								}
							}
							catch (Exception e)
							{
								Console.WriteLine("{0} > Ошибка! {1}", @switch.Ip, e.Message);
							}
						});

						Task writer = new Task(() =>
						{
							try
							{
								var commands = @switch.GetCommands();
								int delay = @switch.Model.Contains("3COM")
									? 1000
									: @switch.Model.Contains("HP")
										? 3000
										: 500;

								foreach (string command in commands)
								{
									//Console.WriteLine(@switch.Ip + " <= " + command);
									output.WriteLine(command);
									output.Flush();

									Task.Delay(delay).Wait();
									if (delay >= 3000) delay = 1000;
								}
							}
							catch (Exception e)
							{
								Console.WriteLine("{0} < Ошибка! {1}", @switch.Ip, e.Message);
							}
						});

						reader.Start();
						writer.Start();
						writer.Wait();
					}
				}
			}

			void ParseOutput(Switch @switch)
			{
				foreach (string line in @switch.Output)
				{
					string vlan = "error",  mac = "error", portName = "error";
					string[] parts = SplitLine(line);

					// Поиск данных в строках ответа
					switch (@switch.Model)
					{
						case "Cisco Catalyst 2960":
						case "Cisco Catalyst 3560":
							if (parts.Length == 4)
							{
								portName = ParseAsPortname(parts[3]);
								vlan = ParseAsVLAN(parts[0]);
								mac = ParseAsMac(parts[1]);
							}
							break;

						case "3COM 3300 XM":
							if (parts.Length == 5)
							{
								portName = ParseAsPortname(parts[1]);
								vlan = ParseAsVLAN(parts[3]);
								mac = ParseAsMac(parts[2]);
							}
							break;

						case "D-Link DES-3028P":
						case "D-Link DES-3526":
							if (parts.Length == 5)
							{
								portName = ParseAsPortname(parts[3]);
								vlan = ParseAsVLAN(parts[0]);
								mac = ParseAsMac(parts[2]);
							}
							break;

						case "D-Link DGS-1210-10":
						case "D-Link DGS-1210-20":
						case "D-Link DGS-1210-28":
						case "D-Link DGS-1210-28P":
						case "D-Link DGS-1510-28X":
							if (parts.Length == 4)
							{
								portName = ParseAsPortname(parts[3]);
								vlan = ParseAsVLAN(parts[0]);
								mac = ParseAsMac(parts[1]);
							}
							break;

						case "HPE 1950 10gbs":
							if (parts.Length == 5)
							{
								portName = ParseAsPortname(parts[3]);
								vlan = ParseAsVLAN(parts[1]);
								mac = ParseAsMac(parts[0]);
							}
							break;

						case "Huawei S5720-28P-LI-AC":
							if (parts.Length == 4)
							{
								portName = ParseAsPortname(parts[2]);
								vlan = ParseAsVLAN(parts[1]);
								mac = ParseAsMac(parts[0]);
							}
							break;
					}

					// Обработка найденных данных
					if (mac != "error" && portName != "error")
					{
						var entry = FDB.FirstOrDefault(x => x.SwitchIp == @switch.Ip && x.PortName == portName && x.Mac == mac);
						if (entry == null)
						{
							entry = new SwitchPortMac
							{
								Mac = mac,
								PortName = portName,
								SwitchIp = @switch.Ip,
								Vlan = vlan,
								Date = DateTime.Now,
							};
							FDB.Add(entry);
						}

						entry.Date = DateTime.Now;
					}
				}

				string[] SplitLine(string source)
				{
					string[] parts = source
						.Replace("\n", "")
						.Replace("\t", "")
						.Replace("\r", "")
						.Replace("\f", "")
						.Replace("\b", "")
						.Replace("--More--", "")
						.Replace("←[K", "")
						.Replace("Enter <CR> for more or 'q' to quit--:", "")
						.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
						.Where(x => !string.IsNullOrEmpty(x))
						.ToArray();
					Console.WriteLine("[" + parts.Length + "]:" + string.Join("|", parts));
					return parts;
				}

				string ParseAsVLAN(string source)
				{
					return source.Trim();
				}

				string ParseAsMac(string source)
				{
					if (!source.Contains(".") && !source.Contains("-") && !source.Contains(":")) return "error";
					source = source
						.Trim()
						.ToLower()
						.Replace(".", "")
						.Replace(":", "")
						.Replace("-", "");

					return source.Length == 12
						? source.Insert(10, "-").Insert(8, "-").Insert(6, "-").Insert(4, "-").Insert(2, "-")
						: "error";
				}

				string ParseAsPortname(string source)
				{
					source = source.Trim().ToUpper();
					return string.IsNullOrEmpty(source) || source == "CPU"
						? "error"
						: source;
				}
			}
		}

		static void LoadFDBFromFile()
		{
			try
			{
				Storage = JsonConvert.DeserializeObject<List<Switch>>(File.ReadAllText(@"\\web\files\Служебные\switches.json"));
			}
			catch { }
			
			try 
			{
				FDB = JsonConvert.DeserializeObject<List<SwitchPortMac>>(File.ReadAllText(@"\\web\files\Служебные\scheme.json"));
			} 
			catch 
			{
				FDB = new List<SwitchPortMac>();
			}
			
			Links = new List<Link>();
		}

		static void SaveFDBToFile()
		{
			File.WriteAllText(@"\\web\files\Служебные\scheme.json", JsonConvert.SerializeObject(FDB));
		}


		// методы обработки схемы

		static void UpdateFromNetwork()
		{
			foreach (var entry in FDB)
			{
				var endpoint = Network.Endpoints.FirstOrDefault(x => x.MacAddress == entry.Mac);
				if (endpoint != null)
				{
					entry.Dns = endpoint.DnsName;
					entry.Ip = endpoint.Ip;
					entry.Desc = endpoint.Desc;
				}
				else
				{
					var sw = Storage.FirstOrDefault(x => x.Mac == entry.Mac);
					if (sw != null)
					{
						entry.Ip = sw.Ip;
						entry.Dns = sw.Model;
						entry.Desc = sw.Name;
					}
				}
			}
		}

		static void RemoveDuplicates()
		{
			var duplicates = FDB
				.GroupBy(x => x.Mac)
				.Select(g => new
				{
					Mac = g.Key,
					Pairs = g
						.Select(x => new
						{
							x.SwitchIp,
							x.PortName,
							Devices = FDB
								.Where(y => y.SwitchIp == x.SwitchIp && y.PortName == x.PortName)
								.Select(y => y.Mac)
								.ToList()
						})
						.ToList()
				})
				.Where(x => x.Pairs.Count > 1)
				.Where(x => !SwitchesMacs.Contains(x.Mac))
				.ToList();

			// проход по тем дубликатам, где есть хоть один порт, на котором только этот мак
			// оставляем тот порт, где мак единственный, прочие вхождения удаляем
			foreach (var entry in duplicates)
			{
				var endpoint = Network.GetByMac(entry.Mac) ?? new Endpoint { Ip = "?", DnsName = "?", Desc = "?", MacAddress = entry.Mac };
				Console.WriteLine("Найден дубликат: {0}", endpoint.Ip);
				foreach (var pair in entry.Pairs)
				{
					Console.WriteLine("Вхождение: {0} {1} ({2} устр.)", pair.SwitchIp, pair.PortName, pair.Devices.Count);
				}

				if (entry.Pairs.Count(y => y.Devices.Count == 1) > 0)
				{
					int removed = 0;
					foreach (var pair in entry.Pairs.Where(x => x.Devices.Count > 1))
					{
						Console.WriteLine("Недостоверно: {0} {1}", pair.SwitchIp, pair.PortName);
						removed += FDB.RemoveAll(x => x.SwitchIp == pair.SwitchIp && x.PortName == pair.PortName && x.Mac == entry.Mac);
					}
					Console.WriteLine("Удалено дубликатов: {0}", removed);
				}
			}
		}

		static void BuildScheme()
		{
			foreach (var @switch in Storage)
			{
				@switch.Ports = FDB
					.Where(x => x.SwitchIp == @switch.Ip)
					.GroupBy(x => x.PortName)
					.Select(g => new Port
					{
						Name = g.Key,
						Devices = g
							.Select(x => new Device
							{
								Mac = x.Mac,
								Date = x.Date,
								Vlan = x.Vlan,
								Desc = x.Desc,
								Dns = x.Dns,
								Ip = x.Ip,
							})
							.ToList(),
					})
					.ToList();
			}
		}

		static void PrepareSwitches()
		{
			foreach (var @switch in Storage)
			{
				foreach (var port in @switch.Ports)
				{
					bool found = false;

					// Поиск мак-адресов других коммутаторов в таблице мак-адресов текущего
					foreach (var mac in port.Devices)
					{
						foreach (var switchMac in SwitchesMacs)
						{
							if (mac.Mac == switchMac)
							{
								found = true;
								break;
							}
						}
						if (found) break;
					}

					// Определение типа подключений на порте
					if (found)
					{
						port.Type = (int)PortType.Switch;
						//port.Devices = port.Devices.Where(x => SwitchesMacs.Contains(x.Mac)).ToList();
					}
					else if (port.Devices.Count == 1)
					{
						port.Type = (int)PortType.Device;
					}
					else
					{
						port.Type = (int)PortType.DevicesGroup;
					}

					Console.WriteLine("{0} : {1} : {2}", @switch.Ip, port.Name, port.Type);
				}
			}
		}

		static void CheckLinksIfOneFound()
		{
			foreach (var @switch in Storage)
			{
				foreach (var port in @switch.Ports.Where(x => x.Type == (int)PortType.Switch))
				{
					var intersect = port.Devices
						.Select(x => x.Mac)
						.Where(x => SwitchesMacs.Contains(x))
						.ToList();

					int count = intersect.Count;

					if (count == 1)
					{
						// Если найден всего один мак-адрес, то тут все просто и понятно, это прямая связь.
						// Остается только найти обратный конец связи. Его достоверность не учитывается.
						var connectedSwitch = FindSwitchByMac(intersect[0]);
						var found = false;

						foreach (var checkingPort in connectedSwitch.Ports.Where(x => x.Type == (int)PortType.Switch))
						{
							if (!found && checkingPort.Devices.Select(x => x.Mac).Contains(@switch.Mac))
							{
								CreateLink(@switch, port, connectedSwitch, checkingPort);
								found = true;
							}
						}
					}
				}
			}
		}

		static void CheckLinksIfManyFound()
		{
			foreach (var @switch in Storage)
			{
				foreach (var port in @switch.Ports.Where(x => x.Type == (int)PortType.Switch))
				{
					// Найдены мак-адреса других коммутаторов, значит точно есть связь
					// Чтобы ее найти, нужно обойти все возможные варианты.
					// Если при обходе в исходный коммутатор лежит на отличном от других вариантов порте
					// Значит на этом порте и находится связь
					var intersect = port.Devices
						.Select(x => x.Mac)
						.Where(x => SwitchesMacs.Contains(x))
						.ToList();

					int count = intersect.Count;

					var found = false;

					foreach (var checkingSwitch in intersect.Select(x => FindSwitchByMac(x)))
					{
						if (!found)
						{
							foreach (var checkingPort in checkingSwitch.Ports.Where(x => x.Type == (int)PortType.Switch))
							{
								if (!found)
								{
									// Здесь мы находим все коммутаторы, которые есть на проверямом порте возможной связи
									var switchesMacs = checkingPort.Devices
										.Select(x => x.Mac)
										.Where(x => SwitchesMacs.Contains(x))
										.ToList();

									// Связь есть, если этот массив коммутаторов содержит мак исходного и не содержит маки других вариантов
									var notFoundAnotherCases = switchesMacs.Where(x => intersect.Contains(x)).Count();

									if (switchesMacs.Contains(@switch.Mac) && notFoundAnotherCases == 0)
									{
										CreateLink(@switch, port, checkingSwitch, checkingPort);
										found = true;
									}
								}
							}
						}
					}
				}
			}
		}

		static void ListSwitchesWithoutLinks()
		{
			foreach (var @switch in Storage)
			{
				var ports = @switch.Ports.Where(x => x.Type == (int)PortType.Switch);

				foreach (var port in ports)
				{
					var switches = port.Devices
						.Select(x => x.Mac)
						.Where(x => SwitchesMacs.Contains(x))
						.Select(x => FindSwitchByMac(x))
						.ToList();

					if (switches.Count > 0)
					{
						port.Link = new LinkPart
						{
							Ip = switches[0].Mac,
							PortName = "Неизвестен"
						};
					}
				}
			}
		}

		static void SaveSchemeToFile()
		{
			File.WriteAllText(@"\\web\files\Служебные\scanlan.json", JsonConvert.SerializeObject(new
			{
				Switches = Storage
					.OrderBy(x => x.Ip)
					.Select(x => new
					{
						x.Ip,
						x.Mac,
						x.Name,
						x.Model,
						Ports = x.Ports.OrderBy(y => y.Name).Select(y => new
						{
							y.Name,
							y.Type,
							y.Link,
							Devices = y.Devices.OrderBy(z => z.Ip).Select(z => new
							{
								z.Mac,
								z.Ip,
								z.Dns,
								z.Desc,
								z.Vlan,
								Online = (DateTime.Now - z.Date) < TimeSpan.FromMinutes(5),
							}),
							Switches = y.Devices
								.Where(z => SwitchesMacs.Contains(z.Mac))
								.Select(z => z.Ip),
						}),
						x.Output,
					})
					.ToList(),
				Links,
			}));

			Console.WriteLine("Схема сохранена в файл");
		}

		static void CreateLink(Switch s1, Port p1, Switch s2, Port p2)
		{
			p1.Type = (int)PortType.Connect;
			p2.Type = (int)PortType.Connect;
			p1.Devices = new List<Device>();
			p2.Devices = new List<Device>();

			p1.Link = new LinkPart
			{
				Ip = s2.Ip,
				PortName = p2.Name
			};

			p2.Link = new LinkPart
			{
				Ip = s1.Ip,
				PortName = p1.Name
			};

			Links.Add(new Link
			{
				Begin = new LinkPart
				{
					Ip = s1.Ip,
					PortName = p1.Name
				},
				End = new LinkPart
				{
					Ip = s2.Ip,
					PortName = p2.Name
				}
			});
		}

		static Switch FindSwitchByMac(string mac)
		{
			foreach (var s in Storage) if (s.Mac == mac) return s;
			return new Switch();
		}
	}
}