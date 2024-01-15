using Cardridges.Models;
using LinqToDB;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Cardridges.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index() => View();

        public ActionResult List()
        {
            string win = User.Identity.Name;
            win = win.Substring(win.IndexOf("\\") + 1).ToLower();

            using (var db = new SiteContext())
            {
                if ((db.Users.FirstOrDefault(x => x.UName == win)?.UClass ?? "").Contains("cartridges"))
                {
                    return View("Worklist");
                }
                else
                {
                    return View("List", model: win);
                }
            }
        }

        public ActionResult Create(string user, int id, string comment)
        {
            try
            {
                int reqId = 0;

                using (var db = new DatabaseContext())
                {
                    reqId = db.UsersRequests
                        .Value(x => x.Win, user)
                        .Value(x => x.DeviceId, id)
                        .Value(x => x.Comment, comment)
                        .Value(x => x.Date, DateTime.Now)
                        .Value(x => x.Status, "Ожидает")
                        .InsertWithInt32Identity() ?? 0;
                }

                // При создании заявки на замену картриджа
                // создается по уведомлению для каждого из группы "Замена картриджей"

                using (var db = new SiteContext())
                {
                    var createUser = db.Users.Where(x => x.UName == user).FirstOrDefault();
                    if (createUser == null) return Content("done");

                    var adminsUids = db.Users.Where(x => x.UClass.Contains("cartridges")).Select(x => x.UID).ToList();
                    
                    foreach (var adminUid in adminsUids)
                    {
                        db.Notifications
                            .Value(x => x.N_UID, adminUid)
                            .Value(x => x.N_CreateUID, createUser.UID)
                            .Value(x => x.N_App, "Заявки на замену картриджей")
                            .Value(x => x.N_Watched, false)
                            .Value(x => x.N_DateCreated, DateTime.Now)
                            .Value(x => x.N_Note, "Создана заявка #" + reqId + " на замену картриджа пользователем: " + createUser.DisplayName)
                            .Value(x => x.N_Link, "http://www.vst.vitebsk.energo.net/pages/cartridges/home/list")
                            .Insert();
                    }
                }

                return Content("done");
            }
            catch (Exception e)
            {
                return Content("error| " + e.Message);
            }
        }

        public ActionResult Review(int id)
        {
            try
            {
                string win = "";
                win = User.Identity.Name;
                win = win.Substring(win.IndexOf("\\") + 1).ToLower();

                using (var db = new DatabaseContext())
                {
                    db.UsersRequests
                        .Where(x => x.Id == id)
                        .Set(x => x.Status, "В обработке")
                        .Set(x => x.ReviewUser, win)
                        .Update();

                    win = db.UsersRequests
                        .Where(x => x.Id == id)
                        .Select(x => x.Win)
                        .FirstOrDefault();
                }

                using (var db = new SiteContext())
                {
                    int uid = db.Users.Where(x => x.UName == win).FirstOrDefault()?.UID ?? 0;

                    

                    int createUid = db.Users.Where(x => x.UName == win).FirstOrDefault()?.UID ?? 0;

                    if (uid > 0 && createUid > 0)
                    {
                        db.Notifications
                            .Value(x => x.N_UID, uid)
                            .Value(x => x.N_CreateUID, createUid)
                            .Value(x => x.N_App, "Заявки на замену картриджей")
                            .Value(x => x.N_Watched, false)
                            .Value(x => x.N_DateCreated, DateTime.Now)
                            .Value(x => x.N_Note, "Ваша заявка была принята участком АСУТП")
                            .Insert();
                    }

                    db.Notifications
                        .Where(x => x.N_App == "Заявки на замену картриджей")
                        .Where(x => x.N_Note.Contains(" #" + id + " "))
                        .Delete();
                }

                return Content("done");
            }
            catch (Exception e)
            {
                return Content("error| " + e.Message);
            }
        }

        public ActionResult Finish(int id)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    db.UsersRequests
                        .Where(x => x.Id == id)
                        .Set(x => x.Status, "Выполнена")
                        .Update();

                    return Content("done");
                }
            }
            catch (Exception e)
            {
                return Content("error| " + e.Message);
            }
        }

        public ActionResult Hide(int id)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    db.UsersRequests
                        .Where(x => x.Id == id)
                        .Set(x => x.Status, "Скрыта")
                        .Update();

                    return Content("done");
                }
            }
            catch (Exception e)
            {
                return Content("error| " + e.Message);
            }
        }
    }
}