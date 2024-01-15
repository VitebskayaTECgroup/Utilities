using AngleSharp;
using AngleSharp.Html.Parser;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherParser
{
	public class Work
    {
		public static async Task Do(int i)
		{
			try
			{
				string url = "https://yandex.by/pogoda/vitebsk/details?via=ms";
				string path = @"\\insql" + i + @"\d$\Inetpub\wwwroot\pages\weather\";
				//string path = @"D:/weather/";

				string raw = await Loader.LoadAsync(url);
				var document = new HtmlParser().ParseDocument(raw);

				if (!Directory.Exists(path + "css/")) Directory.CreateDirectory(path + "css/");
				if (!Directory.Exists(path + "img/")) Directory.CreateDirectory(path + "img/");

				// Удаляем ненужные ноды
				var body = document.Body.QuerySelector(".content");
				body.QuerySelector(".breadcrumbs").Remove();
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
					if (href.StartsWith("//")) link.SetAttribute("href", "https:" + href);
					if (href.StartsWith("file://")) link.Remove();
				}

				foreach (var link in document.QuerySelectorAll("[src]"))
				{
					var href = link.GetAttribute("src");
					if (href.StartsWith("//")) link.SetAttribute("src", "https:" + href);
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
							if (!File.Exists(path + "css\\" + name))
							{
								var css = await new HttpClient().GetByteArrayAsync(href);
								File.WriteAllBytes(path + "css\\" + name, css);
								Console.WriteLine("Loaded\n");
							}
							else
							{
								Console.WriteLine("Already exists\n");
							}

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
						img.Remove();
					}
				}

				// Сохраняем итоговую страницу
				File.WriteAllText(path + "index.html", document.Head.OuterHtml + "<body style='background: #fff; color: #000;'><div style='padding: 20px;'>" + document.Body.InnerHtml + "</div></body>");
			}
			catch (Exception e)
			{
				File.WriteAllText(AppContext.BaseDirectory + "err.txt", e.Message + "\n" + e.StackTrace);
			}
		}

		public static async Task DoFirst()
		{
			try
			{
				string url = "https://yandex.by/pogoda/vitebsk/details?via=ms";
				string path = @"\\web\wwwroot\content\html\weather\";
				string curl = "http://www.vst.vitebsk.energo.net/content/html/weather/";

				string raw = await Loader.LoadAsync(url);
				var document = new HtmlParser().ParseDocument(raw);

				if (!Directory.Exists(path + "css/")) Directory.CreateDirectory(path + "css/");
				if (!Directory.Exists(path + "img/")) Directory.CreateDirectory(path + "img/");

				// Удаляем скрипты
				foreach (var script in document.QuerySelectorAll("script"))
				{
					script.Remove();
				}

				// Удаляем скрытые элементы
				var nodes = document.Body.QuerySelectorAll(".a11y-hidden");
				foreach (var node in nodes)
                {
					node.Remove();
                }

				// Делаем ссылки неактивными
				foreach (var a in document.QuerySelectorAll("a"))
				{
					a.SetAttribute("href", null);
				}

				// Получаем первую табличку с сегодня
				var table = document.Body.QuerySelector(".forecast-details__day-info").QuerySelectorAll(".weather-table__row");
				document.Body.InnerHtml = "";
				
				// Собираем итоговую страницу
				var table1 = document.CreateElement("table");
				foreach (var tr in table)
                {
					var cells = tr.QuerySelectorAll("td,th");
					for (int k = 0; k < cells.Length; k++)
                    {
						if (k > 2)
                        {
							cells[k].Remove();
                        }
                    }
					table1.AppendChild(tr);
                }
				document.Body.AppendChild(table1);

				//Нормализуем оставшиеся ссылки
				foreach (var link in document.QuerySelectorAll("[href]"))
                {
                    var href = link.GetAttribute("href");
                    if (href.StartsWith("//")) link.SetAttribute("href", "https:" + href);
                    if (href.StartsWith("file://")) link.Remove();
                }

                foreach (var link in document.QuerySelectorAll("[src]"))
                {
                    var href = link.GetAttribute("src");
                    if (href.StartsWith("//")) link.SetAttribute("src", "https:" + href);
                    if (href.StartsWith("file://")) link.Remove();
                }

                // Скачиваем файлы стилей и изменяем ссылки на них
                foreach (var link in document.QuerySelectorAll("link"))
                {
					link.Remove();
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

                        img.SetAttribute("src", curl + "img/" + name);
						img.RemoveAttribute("class");
				}
                    catch (Exception)
                    {
                        Console.WriteLine("Error\n");
                        img.Remove();
                    }
                }

				// Удаляем лишние стили
				document.Body.QuerySelector("table").ClassList.Add("parsed");
				var style = document.CreateElement("style");
				style.InnerHtml = @"
					.parsed div { display: inline-block; }
					.weather-table__daypart { display: inline-block; width: 60px; }
					.weather-table__temp { color: #1e8ef7; font-weight: bold; }";
				document.Body.AppendChild(style);

				// Сохраняем итоговую страницу
				File.WriteAllText(path + "index.html", document.Body.InnerHtml.Replace("прояснениями", "проясн."));
			}
			catch (Exception e)
			{
				File.WriteAllText(AppContext.BaseDirectory + "err.txt", e.Message + "\n" + e.StackTrace);
			}
		}
	}
}
