using ServiceControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceControl.Controllers
{
    [NoCache]
    public class ServiceController : Controller
    {
        clsDAL objDAL;

        
        public ActionResult Operator()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetRecords(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
            var records = new ServiceOperatorModelGrid().GetRecords(page, limit, sortBy, direction, searchString, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Operator(string Operator)
        {
            string sqlQuery = "SELECT  * FROM tbl_Key_Map_Master";
            objDAL.ExecuteQuery(sqlQuery, KeyConstant.Server_Sahbox_17_Blink);

            return View();
        }

        #region ServiceDetails
        public ActionResult ServiceDetails()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDetailRecords(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
            var records = new ServiceDetailsModelGrid().GetDeatilRecords(page, limit, sortBy, direction, searchString, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }
}