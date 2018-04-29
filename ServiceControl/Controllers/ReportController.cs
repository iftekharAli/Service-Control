using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceControl.Models;

namespace ServiceControl.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        public ActionResult Operator()
        {
            return View();
        }


        #region Broadcast Status

        public ActionResult Broadcast()
        {
            return View();
        }

        #region Robi
        [HttpGet]
        public JsonResult GetBroadcastStatusRobi(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
            var records = new BroadcastStatusModelGrid().GetBroadcastStatusRobi(page, limit, sortBy, direction, searchString, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Banglalink
        [HttpGet]
        public JsonResult GetBroadcastStatusBL(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
            var records = new BroadcastStatusModelGrid().GetBroadcastStatusBL("spBroadcastStatusBL", page, limit, sortBy, direction, searchString, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GP
        [HttpGet]
        public JsonResult GetBroadcastStatusGP(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
            var records = new BroadcastStatusModelGrid().GetBroadcastStatusBL("spBroadcastStatusGP", page, limit, sortBy, direction, searchString, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region TT
        [HttpGet]
        public JsonResult GetBroadcastStatusTT(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
            var records = new BroadcastStatusModelGrid().GetBroadcastStatusBL("spBroadcastStatusTT", page, limit, sortBy, direction, searchString, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region Broadcast Status
        public ActionResult ChargingStatus(DropDownListModel obj)
        {
            obj = new DropDownListModel();
            obj.MobileList = new SelectList(obj.GetOperatorName(), "SheetID", "Operator"); // model binding
           
            return View(obj);
        }
        #endregion

    }

    
}
