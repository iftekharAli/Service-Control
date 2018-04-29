using ServiceControl.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ServiceControl.Models
{
    public class DropDownListModel
    {
        public string Operator { get; set; }

        #region Sheet
        public string SheetID { get; set; }
        public string SheetName { get; set; }
        public SelectList MobileList { get; set; }
        public static int getSelectedValue { get; set; }
        #endregion

        #region Month
        public string MonthName { get; set; }
        public string MonthValue { get; set; }
        public static string getSelectedMonth { get; set; }
        public SelectList MonthList { get; set; }
        #endregion

        #region Year
        public string YearName { get; set; }
        public string YearValue { get; set; }
        public static string getSelectedYear { get; set; }
        public SelectList YearList { get; set; }
        #endregion


        clsDAL objDAL = new clsDAL();

        public IEnumerable<DropDownListModel> GetDataList(string ExcelID)
        {
            string sqlQuery = " EXEC spGetExcelSheetName '" + ExcelID + "' ";

            IDataReader dr = objDAL.GetDataReader(sqlQuery, KeyConstant.Server_Sahbox_17);
            List<DropDownListModel> objList = new List<DropDownListModel>();

            while (dr.Read())
            {
                DropDownListModel objDropDownListModel = new DropDownListModel();

                objDropDownListModel.SheetID = Convert.ToString(dr["id"], null);
                objDropDownListModel.SheetName = Convert.ToString(dr["SheetNameShow"], null);

                objList.Add(objDropDownListModel);
            }

            //total = objAirtelModelList.Count();
            var records = objList.Select(a => a);

            return records;
        }


        public IEnumerable<DropDownListModel> GetMonthYearList(int MonthYear)
        {
            string sqlQuery = " EXEC spGetMonthYear " + MonthYear + " ";

            IDataReader dr = objDAL.GetDataReader(sqlQuery, KeyConstant.Server_Sahbox_17);
            List<DropDownListModel> objList = new List<DropDownListModel>();
            int i = 1;
            while (dr.Read())
            {
                DropDownListModel objDropDownListModel = new DropDownListModel();

                if(MonthYear==1)
                {
                    objDropDownListModel.MonthName = Convert.ToString(dr["MonthYear"], null);
                    objDropDownListModel.MonthValue = Convert.ToString(dr["MonthYear"], null);
                }
                else if(MonthYear==2)
                {
                    objDropDownListModel.YearName = Convert.ToString(dr["MonthYear"], null);
                    objDropDownListModel.YearValue = Convert.ToString(dr["MonthYear"], null);
                }

                objList.Add(objDropDownListModel);
                i++;
            }

            var records = objList.Select(a => a);

            return records;
        }


        public IEnumerable<DropDownListModel> GetOperatorName()
        {
            List<DropDownListModel> objList = new List<DropDownListModel>();

            string[] Operator = new string[3];
            Operator[0] = KeyConstant.Robi;
            Operator[1] = KeyConstant.Banglalink;
            Operator[2] = KeyConstant.GP;

            for (int i = 0; i < Operator.Length; i++)
            {
                DropDownListModel objDropDownListModel = new DropDownListModel();
                objDropDownListModel.SheetID = i.ToString();
                objDropDownListModel.Operator = Operator[i].ToString();
                objList.Add(objDropDownListModel);
            }
            var records = objList.Select(a => a);
            return records;
        }
    }
}