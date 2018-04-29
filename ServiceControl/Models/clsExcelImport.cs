using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ServiceControl.Models
{
    public class clsExcelImport
    {
        public DataSet ExcelImport(string ext, string path)
        {
            try
            {
                //ext = ".xlsx";
                //path = "F:\\Documents\\Airtel.xlsx";
                string ConStr = "";


                if (ext.Trim() == ".xls")
                {
                    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (ext.Trim() == ".xlsx")
                {
                    ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }

                OleDbConnection conn = new OleDbConnection(ConStr);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DataSet ds = new DataSet();
                var sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                if (sheets == null || sheets.Rows.Count < 1) throw new InvalidOperationException("CantReadWorksheets");

                foreach (DataRow sheet in sheets.Rows)
                {
                    var tableName = sheet["Table_Name"].ToString();
                    var sql = "SELECT * FROM [" + tableName + "]";

                    var adap = new OleDbDataAdapter(sql, conn);
                    adap.Fill(ds, tableName);
                }
                conn.Close();

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}