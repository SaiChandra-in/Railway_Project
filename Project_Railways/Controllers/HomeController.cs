using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project_Railways.Models;


namespace Project_Railways.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin model)
        {
            using (RailwayInfoEntities r = new RailwayInfoEntities())
            {
                bool IsValidUser = r.Admins.Any(x => x.username.ToLower() == model.username.ToLower() && x.password == model.password);
                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.username,false);
                    return RedirectToAction("Index", "Trains");
                }
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}