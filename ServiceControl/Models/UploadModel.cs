using ServiceControl.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ServiceControl.Models
{

    public class AirtelModel
    {
        public string ExcelFileName { get; set; }
        public string ContentDate { get; set; }
        public string ContentText { get; set; }
        public string ContentMonth { get; set; }
        public int TextLen { get; set; }
        public string SheetName { get; set; }
        public string HorosCopeCode { get; set; }
        public string HorosCopeName { get; set; }
        public string UploadStatus { get; set; }
        public string ContentCode { get; set; }       
    }

    public class AirtelUpload
    {
        public void ContentUpload(DataSet ds, string strExcelFileName)
        {
            DataTable dt;
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                dt = new DataTable();               
                dt = ds.Tables[i];
                if (dt.Rows.Count > 1)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["Date"].ToString().Length >= 9)
                        {
                            AirtelExcelFileFormat(dt, j, strExcelFileName);
                        }
                    }
                    if (ExcelFileName(strExcelFileName) == "AirtelHoroscope")
                        break;
                }
            }
        }

        private void AirtelExcelFileFormat(DataTable dt, int Rows, string strExcelFileName)
        {
            if(ExcelFileName(strExcelFileName)=="AirtelHoroscope")
            {
                ContentUpAirtelHorosCope(dt, Rows, ExcelFileName(strExcelFileName));
            }
            else
            {
                ContentUpGeneric(dt, Rows, ExcelFileName(strExcelFileName));
            }
        }

        private void ContentUpGeneric(DataTable dt, int Rows, string strExcelFileName)
                  {
            clsDAL objDAL;
            AirtelModel objAirtelModel = new AirtelModel();
            objAirtelModel.ExcelFileName = strExcelFileName;
            objAirtelModel.ContentDate = Convert.ToDateTime(dt.Rows[Rows]["Date"].ToString()).ToString("MM/dd/yyyy");
            objAirtelModel.ContentText = dt.Rows[Rows]["F2"].ToString().Replace("'", null);
            objAirtelModel.ContentMonth = ContentMonth(objAirtelModel.ContentDate);
            objAirtelModel.SheetName = TableNameFormat(dt.TableName.ToString());

            objDAL = new clsDAL();
            string sqlQuery = "EXEC spContentUpload '" + objAirtelModel.ExcelFileName + "','" + objAirtelModel.ContentDate + "',N'"
                                + objAirtelModel.ContentText + "','" + objAirtelModel.ContentMonth + "','" + objAirtelModel.SheetName + "'";
            objDAL.ExecuteQuery(sqlQuery, KeyConstant.Server_Sahbox_17);
        }

        private void ContentUpAirtelHorosCope(DataTable dt, int Rows, string strExcelFileName)
        {
            clsDAL objDAL;
            AirtelModel objAirtelModel = new AirtelModel();
            objAirtelModel.ExcelFileName = strExcelFileName;
            objAirtelModel.ContentDate = Convert.ToDateTime(dt.Rows[Rows]["Date"].ToString()).ToString("MM/dd/yyyy");
            objAirtelModel.HorosCopeCode = dt.Rows[Rows]["Do Not Touch These Columns (B,C,D,E,F)"].ToString().Replace("'", null);
            objAirtelModel.HorosCopeName = dt.Rows[Rows]["F3"].ToString().Replace("'", null);
            objAirtelModel.ContentText = dt.Rows[Rows]["F4"].ToString().Replace("'", null);
            objAirtelModel.ContentMonth = ContentMonth(objAirtelModel.ContentDate);
            objAirtelModel.SheetName = TableNameFormat(dt.TableName.ToString());

            objDAL = new clsDAL();
            string sqlQuery = "EXEC spContentUploadAirtelHoroscope '" + objAirtelModel.ExcelFileName + "','" + objAirtelModel.ContentDate
                            + "','" + objAirtelModel.HorosCopeCode + "','" + objAirtelModel.HorosCopeName + "',N'" + objAirtelModel.ContentText
                            + "','" + objAirtelModel.ContentMonth + "','" + objAirtelModel.SheetName + "'";
            objDAL.ExecuteQuery(sqlQuery, KeyConstant.Server_Sahbox_17);
        }

        private string ExcelFileName(string FileName)
        {
            FileName = FileName.Split('#')[0].Trim().Replace(" ", null);
            return FileName;
        }
        private string TableNameFormat(string TableName)
        {
            TableName = TableName.Replace(" ", "").Replace("#", "").Replace("$", "").Replace("'", "");          
            return TableName;
        }

        private string ContentMonth(string ContentDate)
        {
            string[] MonthNumber = ContentDate.Split('/');
            string MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(MonthNumber[0]));  //DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(MonthNumber[0].ToString()));
            return MonthName;
        }

        //private string DataBaseInfo(string DataField)
        //{
        //    FieldInfo field = typeof(KeyConstant).GetField(DataField, BindingFlags.Public | BindingFlags.Static);
        //    return Convert.ToString(field.GetValue(null));
        //}
    }
}