using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceControl.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (SessionValueSet.Session_UserID_Value != KeyConstant.Session_UserID_Value)
                return View();
            else
                return RedirectToAction("Login", "Account");

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