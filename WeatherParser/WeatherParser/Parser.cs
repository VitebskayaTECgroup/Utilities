using AngleSharp.Html.Parser;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherParser
{
	public static class Parser
	{
		public static async Task MeteoByAsync()
		{
			try
			{
				string url = "https://meteo.by/vitebsk/";
				string path = AppContext.BaseDirectory + "\\Parsed\\MeteoBy\\";
				string raw;

				using (WebClient webClient = new WebClient())
				{
					webClient.Encoding = Encoding.GetEncoding(1251);
					raw = webClient.DownloadString(url);
				}

				raw = raw.Replace("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=windows-1251\" />", "");
				var document = new HtmlParser().ParseDocument(raw);

				if (!Directory.Exists(path + "css/")) Directory.CreateDirectory(path + "css/");
				if (!Directory.Exists(path + "img/")) Directory.CreateDirectory(path + "img/");

				// Удаляем ненужные ноды
				var body = document.Body.QuerySelector(".b-content");
				foreach (var node in body.Children)
				{
					if (node.ClassName == "b-weather") continue;
					if (node.ClassName == "part-nb") continue;
					
					node.Remove();
				}
				foreach (var node in body.Children)
				{
					if (node.ClassName == "b-weather") continue;
					if (node.ClassName == "part-nb") continue;

					node.Remove();
				}

				document.Body.InnerHtml = "";
				document.Body.AppendChild(body);

				// Удаляем скрипты
				foreach (var script in document.QuerySelectorAll("script"))
				{
					script.Remove();
				}

				// Делаем ссылки неактивными
				foreach (var a in document.QuerySelectorAll("a"))
				{
					a.SetAttribute("href", null);
				}

				// Нормализуем оставшиеся ссылки
				foreach (var link in document.QuerySelectorAll("[href]"))
				{
					var href = link.GetAttribute("href");
					if (href.StartsWith("/")) link.SetAttribute("href", "https://meteo.by" + href);
					if (href.StartsWith("file://")) link.Remove();
				}

				foreach (var link in document.QuerySelectorAll("[src]"))
				{
					var href = link.GetAttribute("src");
					if (href.StartsWith("/")) link.SetAttribute("src", "https://meteo.by" + href);
					if (href.StartsWith("file://")) link.Remove();
				}

				// Скачиваем файлы стилей и изменяем ссылки на них
				foreach (var link in document.QuerySelectorAll("link"))
				{
					if (link.GetAttribute("rel") == "stylesheet")
					{
						var href = link.GetAttribute("href");
						var name = href.Substring(href.LastIndexOf("/") + 1);
						Console.WriteLine("CSS: " + href);

						try
						{
							var css = await new HttpClient().GetByteArrayAsync(href);
							File.WriteAllBytes(path + "css\\" + name, css);
							Console.WriteLine("Loaded\n");

							// Нормализация встроенных в css ссылок на картинки
							raw = File.ReadAllText(path + "css\\" + name);
							var images = raw
								.Split(new[] { "/images/" }, StringSplitOptions.RemoveEmptyEntries)
								.Where(x => x.IndexOf(")") > -1)
								.Select(x => "/images/" + x.Substring(0, x.IndexOf(")")).Replace("\"", ""))
								.ToList();

							foreach (var img in images)
							{
								var imgName = img.Substring(img.LastIndexOf("/") + 1);
								Console.WriteLine("IMG: " + img + " | HREF: " + imgName);

								try
								{
									if (!File.Exists(path + "img\\" + imgName))
									{
										var image = await new HttpClient().GetByteArrayAsync("https://meteo.by" + img);
										File.WriteAllBytes(path + "img\\" + imgName, image);
										Console.WriteLine("Loaded\n");
									}
									else
									{
										Console.WriteLine("Already exists\n");
									}

									raw = raw.Replace(img, "../img/" + imgName);
								}
								catch (Exception)
								{
									Console.WriteLine("Error\n");
									//img.Remove();
								}
							}

							raw = raw.Replace("..\\img", "img");

							File.WriteAllText(path + "css\\" + name, raw);

							link.SetAttribute("href", "css/" + name);
						}
						catch (Exception)
						{
							Console.WriteLine("Error\n");
							link.Remove();
						}
					}
				}

				// Скачиваем картинки и изменяем ссылки на них
				foreach (var img in document.QuerySelectorAll("img"))
				{
					var href = img.GetAttribute("src");
					var name = href.Substring(href.LastIndexOf("/") + 1);
					Console.WriteLine("IMG: " + href);

					try
					{
						if (!File.Exists(path + "img\\" + name))
						{
							var image = await new HttpClient().GetByteArrayAsync(href);
							File.WriteAllBytes(path + "img\\" + name, image);
							Console.WriteLine("Loaded\n");
						}
						else
						{
							Console.WriteLine("Already exists\n");
						}

						img.SetAttribute("src", "img/" + name);
					}
					catch (Exception)
					{
						Console.WriteLine("Error\n");
						//img.Remove();
					}
				}

				// Сохраняем итоговую страницу
				File.WriteAllText(path + "index.html", document.Head.OuterHtml + "<body style='background: #fff; color: #000;'><div style='padding: 20px;'>" + document.Body.InnerHtml + "</div></body>");

				// Выгрузка для главной страницы
				string html = "<table class='t-weather'>";
				var table = document.Body.QuerySelector(".t-weather");
				var rows = table.QuerySelectorAll(".time");
				foreach (var row in rows)
				{
					html += "<tr>" + row.QuerySelector(".temp").OuterHtml + row.QuerySelector(".icon").OuterHtml + "</tr>";
				}
				html += "</table>";

				File.WriteAllText(path + "main.html", html);
			}
			catch (Exception e)
			{
				File.WriteAllText(AppContext.BaseDirectory + "err.txt", e.Message + "\n" + e.StackTrace);
			}
		}
	}
}
