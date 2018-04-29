using ServiceControl.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServiceControl.Models
{
    public class ServiceDetailsModel
    {

        //[Required(ErrorMessage = "*Hello")]
                
        public string KEYWORD { get; set; }
        public string SERVICE_NAME { get; set; }
        public string SERVICE_ID { get; set; }
        public string REPLY_SCRIPT_ON { get; set; }
        public string REPLY_SCRIPT_OFF { get; set; }
        public string SUBSCRIPTION_CODE { get; set; }


    }

    public class ServiceDetailsModelGrid
    {
        clsDAL objDAL = new clsDAL();


        public List<ServiceDetailsModel> GetDeatilRecords(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {

            string sqlQuery = " SELECT a.KEYWORD,a.SERVICE_NAME, a.SERVICE_ID, b.REPLY_SCRIPT_ON, b.REPLY_SCRIPT_OFF, b.SUBSCRIPTION_CODE "
                            + " FROM tbl_Key_Map_Master a "
                            + " INNER JOIN tbl_Key_Map_Subscription b ON a.SERVICE_ID=b.SERVICE_ID "
                            + " ORDER BY a.KEYWORD ";

            IDataReader dr = objDAL.GetDataReader(sqlQuery, KeyConstant.Server_Sahbox_17_Blink);
            List<ServiceDetailsModel> OperatorDataList = new List<ServiceDetailsModel>();

            while (dr.Read())
            {
                ServiceDetailsModel objServiceOperatorModel = new ServiceDetailsModel();

                objServiceOperatorModel.KEYWORD = Convert.ToString(dr["KEYWORD"], null);
                objServiceOperatorModel.SERVICE_NAME = Convert.ToString(dr["SERVICE_NAME"], null);
                objServiceOperatorModel.SERVICE_ID = Convert.ToString(dr["SERVICE_ID"], null);
                objServiceOperatorModel.REPLY_SCRIPT_ON = Convert.ToString(dr["REPLY_SCRIPT_ON"], null);
                objServiceOperatorModel.REPLY_SCRIPT_OFF = Convert.ToString(dr["REPLY_SCRIPT_OFF"], null);
                objServiceOperatorModel.SUBSCRIPTION_CODE = Convert.ToString(dr["SUBSCRIPTION_CODE"], null);

                OperatorDataList.Add(objServiceOperatorModel);
            }

            total = OperatorDataList.Count();
            var records = OperatorDataList.Select(a => a);



            if (!string.IsNullOrWhiteSpace(searchString))
            {
                records = OperatorDataList.Where(p => p.KEYWORD.Contains(searchString) || p.SERVICE_ID.Contains(searchString));
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

        public void Save(ServiceDetailsModel player)
        {
        }

        public void Remove(int id)
        {
        }
    }
}