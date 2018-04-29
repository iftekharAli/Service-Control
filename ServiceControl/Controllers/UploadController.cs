using ServiceControl.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceControl.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/
        public ActionResult Airtel1(DropDownListModel obj)
        {
            obj = new DropDownListModel();
            obj.MobileList = new SelectList(obj.GetDataList("1"), "SheetID", "SheetName"); // model binding
            obj.MonthList = new SelectList(obj.GetMonthYearList(1), "MonthValue", "MonthName"); // model binding
            obj.YearList = new SelectList(obj.GetMonthYearList(2), "YearValue", "YearName"); // model binding

            //ViewBag.ExcelSheetList = new SelectList(obj.GetDataList(), "ExcelID", "SheetName", 1); // Viewbag
            ActionModel.ActionNameGet = KeyConstant.actionAirtel1;
            return View(obj);
        }
        public ActionResult Airtel2(DropDownListModel obj)
        {
            obj = new DropDownListModel();
            obj.MobileList = new SelectList(obj.GetDataList("2"), "SheetID", "SheetName");
            obj.MonthList = new SelectList(obj.GetMonthYearList(1), "MonthValue", "MonthName"); // model binding
            obj.YearList = new SelectList(obj.GetMonthYearList(2), "YearValue", "YearName"); // model binding
            ActionModel.ActionNameGet = KeyConstant.actionAirtel2;
            return View(obj);
        }
        public ActionResult AirtelHoroscope(DropDownListModel obj)
        {
            obj = new DropDownListModel();
            obj.MobileList = new SelectList(obj.GetDataList("7"), "SheetID", "SheetName");
            obj.MonthList = new SelectList(obj.GetMonthYearList(1), "MonthValue", "MonthName"); // model binding
            obj.YearList = new SelectList(obj.GetMonthYearList(2), "YearValue", "YearName"); // model binding
            ActionModel.ActionNameGet = KeyConstant.actionAirtelHoroscope;
            return View(obj);
        }
        public ActionResult BuddyService(DropDownListModel obj)
        {
            obj = new DropDownListModel();
            obj.MobileList = new SelectList(obj.GetDataList("3"), "SheetID", "SheetName");
            obj.MonthList = new SelectList(obj.GetMonthYearList(1), "MonthValue", "MonthName"); // model binding
            obj.YearList = new SelectList(obj.GetMonthYearList(2), "YearValue", "YearName"); // model binding
            ActionModel.ActionNameGet = KeyConstant.actionBuddyService;
            return View(obj);
        }
        public ActionResult BuddyServiceRamadan(DropDownListModel obj)
        {
            obj = new DropDownListModel();
            obj.MobileList = new SelectList(obj.GetDataList("8"), "SheetID", "SheetName");
            obj.MonthList = new SelectList(obj.GetMonthYearList(1), "MonthValue", "MonthName"); // model binding
            obj.YearList = new SelectList(obj.GetMonthYearList(2), "YearValue", "YearName"); // model binding
            ActionModel.ActionNameGet = KeyConstant.actionBuddyServiceRamadan;
            return View(obj);
        }
        public ActionResult BuddyServiceIntl(DropDownListModel obj)
        {
            obj = new DropDownListModel();
            obj.MobileList = new SelectList(obj.GetDataList("9"), "SheetID", "SheetName");
            obj.MonthList = new SelectList(obj.GetMonthYearList(1), "MonthValue", "MonthName"); // model binding
            obj.YearList = new SelectList(obj.GetMonthYearList(2), "YearValue", "YearName"); // model binding
            ActionModel.ActionNameGet = KeyConstant.actionBuddyServiceIntl;
            return View(obj);
        }
        public ActionResult ShaboxHoroscope(DropDownListModel obj)
        {
            obj = new DropDownListModel();
            obj.MobileList = new SelectList(obj.GetDataList("4"), "SheetID", "SheetName");
            obj.MonthList = new SelectList(obj.GetMonthYearList(1), "MonthValue", "MonthName"); // model binding
            obj.YearList = new SelectList(obj.GetMonthYearList(2), "YearValue", "YearName"); // model binding
            ActionModel.ActionNameGet = KeyConstant.actionShaboxHoroscope;

            return View(obj);
        }
        public ActionResult ShaboxPart1(DropDownListModel obj)
        {
            obj = new DropDownListModel();
            obj.MobileList = new SelectList(obj.GetDataList("5"), "SheetID", "SheetName");
            obj.MonthList = new SelectList(obj.GetMonthYearList(1), "MonthValue", "MonthName"); // model binding
            obj.YearList = new SelectList(obj.GetMonthYearList(2), "YearValue", "YearName"); // model binding
            ActionModel.ActionNameGet = KeyConstant.actionShaboxPart1;

            return View(obj);
        }
        public ActionResult ShaboxPart2(DropDownListModel obj)
        {
            obj = new DropDownListModel();
            obj.MobileList = new SelectList(obj.GetDataList("6"), "SheetID", "SheetName");
            obj.MonthList = new SelectList(obj.GetMonthYearList(1), "MonthValue", "MonthName"); // model binding
            obj.YearList = new SelectList(obj.GetMonthYearList(2), "YearValue", "YearName"); // model binding
            ActionModel.ActionNameGet = KeyConstant.actionShaboxPart2;

            return View(obj);
        }

        [HttpGet]
        public JsonResult GetUploadedContent(int? SheetID, int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total;
            var records = (dynamic)null;
            if (DropDownListModel.getSelectedValue <= 0)
                DropDownListModel.getSelectedValue = 1;

            if (DropDownListModel.getSelectedMonth != null)
            {
                if (DropDownListModel.getSelectedMonth.Contains("Select"))
                    DropDownListModel.getSelectedMonth = String.Format("{0:MMMM}", DateTime.Now);

                if (DropDownListModel.getSelectedYear.Contains("Select"))
                    DropDownListModel.getSelectedYear = String.Format("{0:yyyy}", DateTime.Now);
            }
            else
            {
                DropDownListModel.getSelectedMonth = String.Format("{0:MMMM}", DateTime.Now);
                DropDownListModel.getSelectedYear = String.Format("{0:yyyy}", DateTime.Now);
            }

            records = new CPReadWriteModel().GetUploadedData(DropDownListModel.getSelectedValue, DropDownListModel.getSelectedMonth, DropDownListModel.getSelectedYear, page, limit, sortBy, direction, searchString, out total);
            return Json(new { records, total }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FileUpload_(HttpPostedFileBase file, string command)
        {

            if (command == "FileUpload")
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var fileExt = Path.GetExtension(file.FileName);

                    string FolderName = DateTime.Today.ToString("MMM") + "-" + DateTime.Today.ToString("yyyy");
                    string FolderLoc = "~/App_Data/Uploads/" + FolderName;

                    if (!System.IO.Directory.Exists(Server.MapPath(@"" + FolderLoc)))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(@"" + FolderLoc));
                    }
                    var path = Path.Combine(Server.MapPath(FolderLoc), fileName);
                    file.SaveAs(path);

                    //string path1 = HttpContext.Server.MapPath(FolderLoc + "/" + fileName);

                    clsExcelImport objExcelImport = new clsExcelImport();

                    AirtelUpload objAirtelUpload = new AirtelUpload();
                    objAirtelUpload.ContentUpload(objExcelImport.ExcelImport(fileExt, path), fileName);
                }
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult Airtel1(string command, DropDownListModel obj)
        {
            if (command == "GridLoad")
            {
                DropDownListModel.getSelectedValue = Convert.ToInt32(obj.SheetID);
                DropDownListModel.getSelectedMonth = obj.MonthValue;
                DropDownListModel.getSelectedYear = obj.YearValue;
            }
            if (command == "Sync")
            {
                DropDownListModel.getSelectedValue = Convert.ToInt32(obj.SheetID);
                CPReadWriteModel objCPModelReadWrite = new CPReadWriteModel();
                //objCPModelReadWrite.DataSync(DropDownListModel.getSelectedValue);
                objCPModelReadWrite.DataSyncAirtel(DropDownListModel.getSelectedValue, DropDownListModel.getSelectedMonth, DropDownListModel.getSelectedYear);
            }
            return RedirectToAction(ActionModel.ActionNameGet, obj);
        }

        [HttpPost]
        public ActionResult Airtel2(string command, DropDownListModel obj)
        {
            if (command == "GridLoad")
            {
                DropDownListModel.getSelectedValue = Convert.ToInt32(obj.SheetID);
                DropDownListModel.getSelectedMonth = obj.MonthValue;
                DropDownListModel.getSelectedYear = obj.YearValue;
            }
            if (command == "Sync")
            {
                DropDownListModel.getSelectedValue = Convert.ToInt32(obj.SheetID);
                CPReadWriteModel objCPModelReadWrite = new CPReadWriteModel();
                if (DropDownListModel.getSelectedValue == 80)
                {
                    objCPModelReadWrite.DataSyncAirtelHoroscope(DropDownListModel.getSelectedValue, DropDownListModel.getSelectedMonth, DropDownListModel.getSelectedYear);
                }
                else
                {
                    objCPModelReadWrite.DataSyncAirtel(DropDownListModel.getSelectedValue, DropDownListModel.getSelectedMonth, DropDownListModel.getSelectedYear);
                }
            }
            return RedirectToAction(ActionModel.ActionNameGet, obj);
        }

        [HttpPost]
        public ActionResult BuddyService(string command, DropDownListModel obj)
        {
            if (command == "GridLoad")
            {
                DropDownListModel.getSelectedValue = Convert.ToInt32(obj.SheetID);
                DropDownListModel.getSelectedMonth = obj.MonthValue;
                DropDownListModel.getSelectedYear = obj.YearValue;
            }
            if (command == "Sync")
            {
                DropDownListModel.getSelectedValue = Convert.ToInt32(obj.SheetID);
                DropDownListModel.getSelectedMonth = obj.MonthValue;
                DropDownListModel.getSelectedYear = obj.YearValue;

                CPReadWriteModel objCPModelReadWrite = new CPReadWriteModel();

                objCPModelReadWrite.DataSync(DropDownListModel.getSelectedValue, DropDownListModel.getSelectedMonth, DropDownListModel.getSelectedYear);

            }
            return RedirectToAction(ActionModel.ActionNameGet, obj);
        }
        [HttpPost]
        public ActionResult BuddyServiceRamadan(string command, DropDownListModel obj)
        {
            if (command == "GridLoad")
            {
                DropDownListModel.getSelectedValue = Convert.ToInt32(obj.SheetID);
                DropDownListModel.getSelectedMonth = obj.MonthValue;
                DropDownListModel.getSelectedYear = obj.YearValue;
            }
            if (command == "Sync")
            {
                DropDownListModel.getSelectedValue = Convert.ToInt32(obj.SheetID);
                DropDownListModel.getSelectedMonth = obj.MonthValue;
                DropDownListModel.getSelectedYear = obj.YearValue;

                CPReadWriteModel objCPModelReadWrite = new CPReadWriteModel();

                objCPModelReadWrite.DataSync(DropDownListModel.getSelectedValue, DropDownListModel.getSelectedMonth, DropDownListModel.getSelectedYear);

            }
            return RedirectToAction(ActionModel.ActionNameGet, obj);
        }
         [HttpPost]
        public ActionResult BuddyServiceIntl(string command, DropDownListModel obj)
        {
            if (command == "GridLoad")
            {
                DropDownListModel.getSelectedValue = Convert.ToInt32(obj.SheetID);
                DropDownListModel.getSelectedMonth = obj.MonthValue;
                DropDownListModel.getSelectedYear = obj.YearValue;
            }
            if (command == "Sync")
            {
                DropDownListModel.getSelectedValue = Convert.ToInt32(obj.SheetID);
                DropDownListModel.getSelectedMonth = obj.MonthValue;
                DropDownListModel.getSelectedYear = obj.YearValue;

                CPReadWriteModel objCPModelReadWrite = new CPReadWriteModel();

                objCPModelReadWrite.DataSync(DropDownListModel.getSelectedValue, DropDownListModel.getSelectedMonth, DropDownListModel.getSelectedYear);

            }
            return RedirectToAction(ActionModel.ActionNameGet, obj);
        }
    }
}