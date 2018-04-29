using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceControl.Models;

namespace ServiceControl.Controllers
{
    public class FollowTrailController : Controller
    {
        //
        // GET: /Report/
        public ActionResult TrailRobiSDP()
        {
            return View();
        }

        #region Robi
        [HttpGet]
        public JsonResult GetFollowTrailRobi(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            string FileName = DateTime.Today.ToString("yyyy-MM-dd") + " - 6624.txt";
            var records = new TrailModelGrid().GetFollowTrail(FileName);
            return Json(new { records }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        

        public ActionResult TrailBLinkText()
        {
            return View();
        }

        #region BLink
        [HttpGet]
        public JsonResult GetFollowTrailBlink(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            string FileName = DateTime.Today.ToString("yyyy-MM-dd") + " - 6624.txt";
            var records = new TrailModelGrid().GetFollowTrail(FileName);
            return Json(new { records}, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }    
}
