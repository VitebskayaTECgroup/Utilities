using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Looker.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Threading;

namespace Looker
{
    static class Program
    {
        const string CONNECTION_STRING = "DataSource=guard2; Database=guard2:D:\\ib\\IBNET.GDB; User=SYSDBA; Password=masterkey;";

        static LookForm look = null;

        static Thread Thread { get; set; }

        public static Configuration Configuration { get; set; }

        static void ShowLook(string fio, string guild, string position, int direction, string filename = null)
        {
            Configuration.Save();
            Thread?.Abort();

            void _Create()
            {
                try
                {
                    look = new LookForm
                    {
                        AutoClose = true,
                        TopMost = true,
                        TopLevel = true
                    };

                    look.Direction.Text = direction == 51 ? "Вход" : "Выход";
                    look.FIO.Text = fio;
                    look.Guild.Text = guild;
                    look.Position.Text = position;

                    link1: 

                    // Для прохода по кнопке
                    if (filename != null)
                    {
                        // 1: поиск в локальном кэше
                        if (File.Exists(Application.UserAppDataPath + @"\Cache\" + filename))
                        {
                            look.Pic.Image = Image.FromFile(Application.UserAppDataPath + @"\Cache\" + filename);
                        }
                        // 2: поиск в папке с программой, копирование в локальный кэш
                        else if (File.Exists(Application.StartupPath + @"\Cache\" + filename))
                        {
                            look.Pic.Image = Image.FromFile(Application.StartupPath + @"\Cache\" + filename);
                            File.Copy(Application.StartupPath + @"\Cache\" + filename, Application.UserAppDataPath + @"\Cache\" + filename);
                        }
                        // 3: поиск в папке с фотографиями, копирование в локальный кэш
                        else if (File.Exists(Configuration.ImagesPath + filename))
                        {
                            look.Pic.Image = Image.FromFile(Configuration.ImagesPath + filename);
                            File.Copy(Configuration.ImagesPath + filename, Application.UserAppDataPath + @"\Cache\" + filename);
                        }
                    }
                    // Для прохода по пропуску
                    else
                    {
                        // ключевые слова для поиска по имени
                        string[] names = fio.ToLower().Split(' ');

                        // список файлов в локальном кэше
                        string[] filesInCache = Directory.GetFiles(Application.UserAppDataPath + @"\Cache\");

                        // список файлов в папке с фотографиями
                        string[] filesInImagesPath = Directory.GetFiles(Configuration.ImagesPath);

                        // 1: поиск в локальном кэше
                        bool foundInCache = false;
                        foreach (var file in filesInCache)
                        {
                            // для каждого файла из локального кэша
                            string searching = file.ToLower();
                            bool found = true;

                            foreach (var name in names)
                            {
                                // false, если хотя бы одно из ключевых слов не найдено (т.е. будет искаться полное соответствие)
                                found = found && !string.IsNullOrEmpty(name) && searching.Contains(name);
                            }
                            
                            // если файл найден
                            if (found && names.Length > 0)
                            {
                                look.Pic.Image = Image.FromFile(file);
                                foundInCache = true;
                                break;
                            }
                        }

                        // 2: поиск в папке с фотографиями
                        bool foundInImagesPath = false;
                        if (!foundInCache)
                        {
                            foreach (var file in filesInImagesPath)
                            {
                                // для каждого файла из локального кэша
                                string searching = file.ToLower();
                                bool found = true;

                                foreach (var name in names)
                                {
                                    // false, если хотя бы одно из ключевых слов не найдено (т.е. будет искаться полное соответствие)
                                    found = found && !string.IsNullOrEmpty(name) && searching.Contains(name);
                                }

                                // если файл найден
                                if (found && names.Length > 0)
                                {
                                    look.Pic.Image = Image.FromFile(file);
                                    File.Copy(file, Application.UserAppDataPath + @"\Cache\" + fio.ToLower() + ".jpg");
                                    foundInImagesPath = true;
                                    break;
                                }
                            }
                        }

                        if (!foundInImagesPath && !foundInCache)
                        {
                            filename = "notfound.jpg";
                            goto link1;
                        }
                    }

                    look.Timer.Start();
                    look.ShowDialog();
                }
                catch (Exception e)
                {
                    File.WriteAllText(Application.UserAppDataPath + "\\log.txt", e.Message);
                }
            }

            Thread = new Thread(_Create);
            Thread.Start();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LookForm form = null;
            Configuration = Configuration.Load();

            if (!Directory.Exists(Application.UserAppDataPath + @"\Cache\")) Directory.CreateDirectory(Application.UserAppDataPath + @"\Cache\");

            using (var icon = new NotifyIcon())
            {
                icon.Icon = Icon.ExtractAssociatedIcon("Looker.ico");
                icon.Visible = true;
                icon.ContextMenu = new ContextMenu(new [] {

                    new MenuItem("Показать", (s, e) => 
                    {
                        form = new LookForm
                        {
                            AutoClose = false
                        };
                        form.Show();
                    }),

                    new MenuItem("Скрыть", (s, e) => 
                    {
                        form?.Hide();
                    }),

                    new MenuItem("Выход", (s, e) => 
                    {
                        icon.Visible = false;
                        Application.Exit();
                    }),

                });
                
                var listener = new FbRemoteEvent(CONNECTION_STRING);
                bool first = true;

                listener.RemoteEventCounts += (sender, e) =>
                {
                    try
                    {
                        if (first)
                        {
                            first = false;
                            return;
                        }

                        using (var conn = new FbConnection(CONNECTION_STRING))
                        {
                            var ev = conn.Query<Event>("SELECT EV_DATE, EV_CODE, EV_OBJID, EV_CARDID, EV_OW_ID FROM EVENTS WHERE EVENTS.EV_ID = (SELECT MAX(EV_ID) FROM EVENTS)").FirstOrDefault();

                            if (ev != null)
                            {
                                switch (ev.EV_CODE)
                                {
                                    case EVENT_TYPE.CART:

                                        var u = conn.Query<User>("SELECT OW_LASTNAME, OW_FIRSTNAME, OW_MIDDLENAME, OW_WS_ID FROM OWNER WHERE OW_ID = @EV_OW_ID", ev).FirstOrDefault();
                                        u.OW_FIRSTNAME = u.OW_FIRSTNAME.Trim();
                                        u.OW_MIDDLENAME = u.OW_MIDDLENAME.Trim();
                                        u.OW_LASTNAME = u.OW_LASTNAME.Trim();

                                        u.WS_NAME = conn.Query<string>("SELECT WS_NAME FROM WORKSITE WHERE WS_ID = @OW_WS_ID", u).FirstOrDefault() ?? "";
                                        u.WS_NAME = u.WS_NAME.Trim();

                                        u.AG_NAME = conn.Query<string>("SELECT AGROUPS.AG_NAME FROM CARDS INNER JOIN AGROUPS ON AGROUPS.AG_ID = CARDS.CA_AG_ID WHERE CARDS.CA_VALUE = @EV_CARDID", ev).FirstOrDefault() ?? "";
                                        u.AG_NAME = u.AG_NAME.Trim();

                                        ShowLook(
                                            u.OW_LASTNAME + " " + u.OW_FIRSTNAME + " " + u.OW_MIDDLENAME,
                                            u.WS_NAME,
                                            u.AG_NAME,
                                            ev.EV_OBJID
                                        );

                                        break;

                                    case EVENT_TYPE.OPERATOR:

                                        ShowLook(
                                           "Проход по разрешению оператора",
                                           "",
                                           "",
                                           ev.EV_OBJID,
                                           "operator.jpg"
                                        );

                                        break;

                                    case EVENT_TYPE.UNKNOWN:

                                        ShowLook(
                                           "Неизвестный пропуск",
                                           "",
                                           "",
                                           ev.EV_OBJID,
                                           "warning.jpg"
                                        );

                                        break;

                                    case EVENT_TYPE.BREAK:
                                    case EVENT_TYPE.BREAK_ALT:

                                        ShowLook(
                                           "Несанкционированный проход!",
                                           "",
                                           "",
                                           ev.EV_OBJID,
                                           "error.jpg"
                                        );

                                        break;
                                }
                            }
                        }
                    }
                    catch (Exception exp)
                    {
                        File.WriteAllText(Application.UserAppDataPath + "\\log.txt", exp.Message);
                    }
                };

                listener.QueueEvents(new[] { "checkpoint_now" });

                Application.Run();
            }
        }
    }
}
