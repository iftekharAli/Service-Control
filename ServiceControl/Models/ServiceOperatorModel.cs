using ServiceControl.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServiceControl.Models
{
    public class ServiceOperatorModel
    {

        //[Required(ErrorMessage = "*Hello")]
        
        public string Live { get; set; }
        public string Expired { get; set; }
        public string Operator { get; set; }
    }

    public class ServiceOperatorModelGrid
    {
        clsDAL objDAL = new clsDAL();


        public List<ServiceOperatorModel> GetRecords(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {

            string sqlQuery = " EXEC spGetServiceList ";
            IDataReader dr = objDAL.GetDataReader(sqlQuery, KeyConstant.Server_Sahbox_17);
            List<ServiceOperatorModel> OperatorDataList = new List<ServiceOperatorModel>();

            while (dr.Read())
            {
                ServiceOperatorModel objServiceOperatorModel = new ServiceOperatorModel();

                objServiceOperatorModel.Live = Convert.ToString(dr["Live"], null);
                objServiceOperatorModel.Expired = Convert.ToString(dr["Expired"], null);
                objServiceOperatorModel.Operator = Convert.ToString(dr["Operator"], null);

                OperatorDataList.Add(objServiceOperatorModel);
            }

            total = OperatorDataList.Count();
            var records = OperatorDataList.Select(a => a);



            if (!string.IsNullOrWhiteSpace(searchString))
            {
                records = OperatorDataList.Where(p => p.Operator.Contains(searchString) || p.Live.Contains(searchString));
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

        public void Save(ServiceOperatorModel player)
        {
        }

        public void Remove(int id)
        {
        }
    }
}