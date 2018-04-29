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

    public class CPReadWriteModel
    {
        clsDAL objDAL = new clsDAL();

        public string ContentDate { get; set; }
        public string ContentText { get; set; }


        public List<AirtelModel> GetUploadedData(int SheetID, string ContentMonth, string ContentYear, int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {

            string sqlQuery = " EXEC spContentUploadDataRead " + SheetID + ",'" + ContentMonth + "','" + ContentYear + "'";

            IDataReader dr = objDAL.GetDataReader(sqlQuery, KeyConstant.Server_Sahbox_17);
            List<AirtelModel> objAirtelModelList = new List<AirtelModel>();

            while (dr.Read())
            {
                AirtelModel objAirtelModel = new AirtelModel();

                objAirtelModel.ExcelFileName = "Excel Name";
                objAirtelModel.ContentDate = Convert.ToString(dr["Content_Date"], null);
                objAirtelModel.TextLen = Convert.ToInt32(dr["Text_Len"], null);
                objAirtelModel.ContentText = Convert.ToString(dr["Content_Text"], null);
                objAirtelModel.UploadStatus = Convert.ToString(dr["Status"], null);
                objAirtelModel.ContentCode = Convert.ToString(dr["Code"], null);
                if(SheetID==80)
                {
                    objAirtelModel.HorosCopeCode = Convert.ToString(dr["HoroscopeCode"], null);
                    objAirtelModel.HorosCopeName = Convert.ToString(dr["HoroscopeName"], null);
                }
                objAirtelModelList.Add(objAirtelModel);
            }

            total = objAirtelModelList.Count();
            var records = objAirtelModelList.Select(a => a);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                // records = OperatorDataList.Where(p => p.KEYWORD.Contains(searchString) || p.SERVICE_ID.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
            {
                if (direction.Trim().ToLower() == "asc")
                {
                    //records = SortHelper.OrderBy(records, sortBy);
                }
                else
                {
                    // records = SortHelper.OrderByDescending(records, sortBy);
                }
            }

            if (page.HasValue && limit.HasValue)
            {
                int start = (page.Value - 1) * limit.Value;
                records = records.Skip(start).Take(limit.Value);
            }

            return records.ToList();
        }

        public void DataSync(int SheetID, string ContentMonth, string ContentYear)
        {
            string sqlQuery = " EXEC spContentSync " + SheetID + ",'" + ContentMonth + "','" + ContentYear + "' ";
            IDataReader dr = objDAL.GetDataReader(sqlQuery, KeyConstant.Server_Sahbox_17);            
        }

        #region Airtel

        public void DataSyncAirtel(int SheetID, string ContentMonth, string ContentYear)
        {
            string sqlQuery = " EXEC spGetContentDBInfo " + SheetID + " ";
            IDataReader dr = objDAL.GetDataReader(sqlQuery, KeyConstant.Server_Sahbox_17);
            clsContentDBInfo objContentDBInfoModel;

            while (dr.Read())
            {
                objContentDBInfoModel = new clsContentDBInfo();

                objContentDBInfoModel.dbServer = Convert.ToString(dr["dbServer"], null);
                objContentDBInfoModel.dbName = Convert.ToString(dr["dbName"], null);
                objContentDBInfoModel.dbTable = Convert.ToString(dr["dbTable"], null);
                InsertAirtel(objContentDBInfoModel.dbServer, objContentDBInfoModel.dbName, objContentDBInfoModel.dbTable, SheetID, ContentMonth, ContentYear);
            }
        }

        private void InsertAirtel(string dbServer, string dbName, string dbTable, int SheetID, string ContentMonth, string ContentYear)
        {
            string sqlQuerySelect, sqlQueryInsert, sqlQueryUpdate = String.Empty;
            int StatusInsert = 0;
            sqlQuerySelect = "SELECT Content_Date,Content_Text,Text_Len FROM tbl_Upload WHERE db_info=" + SheetID + " AND ContentMonth='" + ContentMonth + "' AND Year(Content_Date)='" + ContentYear + "' AND Status=0";
            IDataReader dr = objDAL.GetDataReader(sqlQuerySelect, KeyConstant.Server_Sahbox_17);
            while (dr.Read())
            {
                AirtelModel objAirtelModel = new AirtelModel();

                objAirtelModel.ContentDate = Convert.ToString(dr["Content_Date"], null);
                objAirtelModel.TextLen = Convert.ToInt32(dr["Text_Len"], null);
                objAirtelModel.ContentText = Convert.ToString(dr["Content_Text"], null);

                if (SheetID == 20)
                {
                    sqlQueryInsert = " INSERT INTO " + dbTable + " VALUES('" + objAirtelModel.ContentDate + "','" + objAirtelModel.ContentText + "')";
                }
                else
                {
                    sqlQueryInsert = " INSERT INTO " + dbTable + " (date,sms_eng) VALUES('" + objAirtelModel.ContentDate + "','" + objAirtelModel.ContentText + "')";
                }
                sqlQueryUpdate = " UPDATE tbl_Upload SET Status=1 WHERE db_info=" + SheetID + " AND Content_Date='" + objAirtelModel.ContentDate + "' ";

                if (dbName == "WARID_CONTENT")
                {
                    objDAL.ExecuteQuery(sqlQueryInsert, KeyConstant.Server_31_WaridContent);
                    StatusInsert = 1;
                }
                else if (dbName == "WARID")
                {
                    objDAL.ExecuteQuery(sqlQueryInsert, KeyConstant.Server_31_Warid);
                    StatusInsert = 1;
                }


                if (StatusInsert == 1)
                {
                    objDAL.ExecuteQuery(sqlQueryUpdate, KeyConstant.Server_Sahbox_17);
                }

                //clsDAL.db.CreateConnection().Close();
            }
        }

        #endregion

        #region Airtel Horoscope
        public void DataSyncAirtelHoroscope(int SheetID, string ContentMonth, string ContentYear)
        {
            string sqlQuery = " EXEC spGetContentDBInfo " + SheetID + " ";
            IDataReader dr = objDAL.GetDataReader(sqlQuery, KeyConstant.Server_Sahbox_17);
            clsContentDBInfo objContentDBInfoModel;

            while (dr.Read())
            {
                objContentDBInfoModel = new clsContentDBInfo();

                objContentDBInfoModel.dbServer = Convert.ToString(dr["dbServer"], null);
                objContentDBInfoModel.dbName = Convert.ToString(dr["dbName"], null);
                objContentDBInfoModel.dbTable = Convert.ToString(dr["dbTable"], null);
                InsertAirtelHoroscope(objContentDBInfoModel.dbServer, objContentDBInfoModel.dbName, objContentDBInfoModel.dbTable, SheetID,ContentMonth, ContentYear);
            }
        }

        private void InsertAirtelHoroscope(string dbServer, string dbName, string dbTable, int SheetID, string ContentMonth, string ContentYear)
        {
            string sqlQuerySelect, sqlQueryInsert, sqlQueryUpdate = String.Empty;
            int StatusInsert = 0;
            sqlQuerySelect = " SELECT Content_Date, HoroscopeCode, HoroscopeName, Content_Text, Text_Len FROM tbl_AirtelHoroscope WHERE "
                           + " Status= 0 AND Year(Content_Date)='" + ContentYear + "' AND DateName(Month , DateAdd(Month ,Month(Content_Date) ,-1 ))='" + ContentMonth + "'";
            IDataReader dr = objDAL.GetDataReader(sqlQuerySelect, KeyConstant.Server_Sahbox_17);
            while (dr.Read())
            {
                AirtelModel objAirtelModel = new AirtelModel();

                objAirtelModel.ContentDate = Convert.ToString(dr["Content_Date"], null);
                objAirtelModel.HorosCopeCode = Convert.ToString(dr["HoroscopeCode"], null);
                objAirtelModel.HorosCopeName = Convert.ToString(dr["HoroscopeName"], null);
                objAirtelModel.ContentText = Convert.ToString(dr["Content_Text"], null);
                objAirtelModel.TextLen = Convert.ToInt32(dr["Text_Len"], null);

                sqlQueryInsert = " INSERT INTO " + dbTable + "(HOR_DATE, HOR_CODE, HOR_NAME, SMS1) VALUES('" + objAirtelModel.ContentDate + "','" + objAirtelModel.HorosCopeCode + "','" + objAirtelModel.HorosCopeName + "','" + objAirtelModel.ContentText + "')";

                sqlQueryUpdate = " UPDATE tbl_AirtelHoroscope SET Status=1 WHERE db_info=" + SheetID + " AND Content_Date='" + objAirtelModel.ContentDate + "' AND HoroscopeCode='" + objAirtelModel.HorosCopeCode + "'";

                if (dbName == "WARID_CONTENT")
                {
                    objDAL.ExecuteQuery(sqlQueryInsert, KeyConstant.Server_31_WaridContent);
                    StatusInsert = 1;
                }
                else if (dbName == "WARID")
                {
                    objDAL.ExecuteQuery(sqlQueryInsert, KeyConstant.Server_31_Warid);
                    StatusInsert = 1;
                }


                if (StatusInsert == 1)
                {
                    objDAL.ExecuteQuery(sqlQueryUpdate, KeyConstant.Server_Sahbox_17);
                }

                //clsDAL.db.CreateConnection().Close();
            }
        }
        #endregion
    }
}