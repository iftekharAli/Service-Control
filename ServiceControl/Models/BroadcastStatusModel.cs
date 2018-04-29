using ServiceControl.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServiceControl.Models
{

    public class BroadcastStatusModelRobi
    {
        public string RowID { get; set; }
        public string Processing { get; set; }
        public string Port { get; set; }
        public string Status { get; set; }

    }

    public class BroadcastStatusModelBL
    {
        public string RowID { get; set; }
        public string Processing { get; set; }
        public string Sent { get; set; }
        public string Port { get; set; }
        public string Service { get; set; }

    }

    public class BroadcastStatusModelGrid
    {
        clsDAL objDAL = new clsDAL();

        public List<BroadcastStatusModelRobi> GetBroadcastStatusRobi(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {

            string sqlQuery = " EXEC spBroadcastStatusRobi ";

            IDataReader dr = objDAL.GetDataReader(sqlQuery, KeyConstant.Server_Sahbox_17);
            List<BroadcastStatusModelRobi> OperatorDataList = new List<BroadcastStatusModelRobi>();

            while (dr.Read())
            {
                BroadcastStatusModelRobi objBroadcastStatusModel = new BroadcastStatusModelRobi();

                objBroadcastStatusModel.RowID = Convert.ToString(dr["RowID"], null);
                objBroadcastStatusModel.Processing = Convert.ToString(dr["Processing"], null);
                objBroadcastStatusModel.Port = Convert.ToString(dr["Port"], null);
                objBroadcastStatusModel.Status = Convert.ToString(dr["Status"], null);              

                OperatorDataList.Add(objBroadcastStatusModel);
            }

            total = OperatorDataList.Count();
            var records = OperatorDataList.Select(a => a);



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

        public List<BroadcastStatusModelBL> GetBroadcastStatusBL(string spName, int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {

            //string sqlQuery = " EXEC spBroadcastStatusBL ";

            string sqlQuery = " EXEC " + spName;

            IDataReader dr = objDAL.GetDataReader(sqlQuery, KeyConstant.Server_Sahbox_17);
            List<BroadcastStatusModelBL> OperatorDataList = new List<BroadcastStatusModelBL>();

            while (dr.Read())
            {
                BroadcastStatusModelBL objBroadcastStatusModel = new BroadcastStatusModelBL();

                objBroadcastStatusModel.RowID = Convert.ToString(dr["RowID"], null);
                objBroadcastStatusModel.Processing = Convert.ToString(dr["Processing"], null);
                objBroadcastStatusModel.Sent = Convert.ToString(dr["Sent"], null);
                objBroadcastStatusModel.Port = Convert.ToString(dr["Port"], null);
                objBroadcastStatusModel.Service = Convert.ToString(dr["Service"], null);

                OperatorDataList.Add(objBroadcastStatusModel);
            }

            total = OperatorDataList.Count();
            var records = OperatorDataList.Select(a => a);



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
    }
}