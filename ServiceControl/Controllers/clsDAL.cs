using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace ServiceControl.Controllers
{
    public class clsDAL
    {
        public static Database db;// = DatabaseFactory.CreateDatabase("CN_Userinfo");

        #region Execute Query

        public void ExecuteQuery(String sqlQuery,string ConnectionString_)
        {
            try
            {
                db = DatabaseFactory.CreateDatabase(ConnectionString_);
                //IDataReader dr = db.ExecuteReader(CommandType.Text, sqlQuery);

                using (IDataReader dr = db.ExecuteReader(CommandType.Text, sqlQuery))
                {
                    // Use the values in the rows as required.
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDataTable(string sqlQuery,string ConnectionString_)
        {
            try
            {
                db = DatabaseFactory.CreateDatabase(ConnectionString_);
                DataTable dt = new DataTable();
                using (IDataReader dr = db.ExecuteReader(CommandType.Text, sqlQuery))
                {
                    dt.Load(dr);
                    return dt;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void ahb19(DataTable dt, string ConnectionString_)
        {

            db = DatabaseFactory.CreateDatabase(ConnectionString_);
                // Create a table with some rows. 
                DataTable table = dt;

                // Get a reference to a single row in the table. 
                DataRow[] rowArray = table.Select();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy((db.ConnectionString)))
                {
                    bulkCopy.DestinationTableName = "dbo.tbl_test1";

                    try
                    {
                        // Write the array of rows to the destination.
                        bulkCopy.WriteToServer(rowArray);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
        }


        public IDataReader GetDataReader(string sqlQuery, string ConnectionString_)
        {
            try
            {
                db = DatabaseFactory.CreateDatabase(ConnectionString_);
                IDataReader dr = db.ExecuteReader(CommandType.Text, sqlQuery);
                db.CreateConnection().Close();
                return dr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GridView
        public void GridViewBind(String sqlQuery, GridView gv)
        {
            IDataReader dr = db.ExecuteReader(CommandType.Text, sqlQuery);
            DataTable dt = new DataTable();
            dt.Load(dr);
            gv.DataSource = dt;
            gv.DataBind();
        }
        #endregion

        #region DropDownList

        public void DropDownBind(SqlDataSource sqlDS, string sqlQuery, string DataTextField, string DataValueField, DropDownList ddl)
        {
            try
            {
                sqlDS.ConnectionString = db.ConnectionString;

                sqlDS.SelectCommand = sqlQuery;

                
                ddl.DataSource = sqlDS;
                ddl.DataTextField = DataTextField;
                ddl.DataValueField = DataValueField;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddl.SelectedIndex = 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DropDownBind(SqlDataSource sqlDS, string sqlQuery, string DataTextField, string DataValueField, DropDownList ddl, string AddItem, string AddValue, string SelectedValue)
        {
            try
            {
                sqlDS.ConnectionString = db.ConnectionString;

                sqlDS.SelectCommand = sqlQuery;


                ddl.DataSource = sqlDS;
                ddl.DataTextField = DataTextField;
                ddl.DataValueField = DataValueField;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem(AddItem, AddValue));
                ddl.SelectedValue = SelectedValue;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Single Table
        public void DropDownBind(string col_item, string col_value, string table_name, DropDownList ddl)
        {
            try
            {
                string sql = "SELECT " + col_item + "," + col_value + " FROM " + table_name + "";
                IDataReader dr = db.ExecuteReader(CommandType.Text, sql);
                DataTable dt = new DataTable();
                dt.Load(dr);

                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("", "0"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddl.Items.Add(new ListItem(dt.Rows[i].ItemArray[0].ToString(), dt.Rows[i].ItemArray[1].ToString()));
                }
            }

            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Single Table With Where Value
        public void DropDownBind(string col_item, string col_value, string table_name, string WhereColumn, string WhereValue, DropDownList ddl)
        {
            try
            {
                string sql = "SELECT " + col_item + "," + col_value + " FROM " + table_name + " WHERE " + WhereColumn + " ='" + WhereValue + "'";
                IDataReader dr = db.ExecuteReader(CommandType.Text, sql);
                DataTable dt = new DataTable();
                dt.Load(dr);

                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("", "0"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddl.Items.Add(new ListItem(dt.Rows[i].ItemArray[0].ToString(), dt.Rows[i].ItemArray[1].ToString()));
                }
            }

            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // [Item + Value] Single Table With Where Value
        public void DropDownBind(string col_item, string col_code, string col_value, string table_name, string WhereColumn, string WhereValue, DropDownList ddl)
        {
            try
            {
                string sql = "SELECT " + col_item + "," + col_code + "," + col_value + " FROM " + table_name + " WHERE " + WhereColumn + " ='" + WhereValue + "'";
                IDataReader dr = db.ExecuteReader(CommandType.Text, sql);
                DataTable dt = new DataTable();
                dt.Load(dr);

                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("", "0"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddl.Items.Add(new ListItem(dt.Rows[i][0].ToString() + " : " + dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString()));
                }
            }

            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Three Table [INNER JOIN]

        public void DropDownBind(string col_item, string col_value, string table_name1, string table_name2, string table_name3, string Join1, string Join2, string Join3, string Join4, string FilterColumn, string FilterData, DropDownList ddl)
        {
            try
            {
                String sql = " SELECT DISTINCT " + col_item + ", " + col_value + " FROM " + table_name1 + " INNER JOIN " + table_name2 + " ON " + Join1 + " = " + Join2 + " INNER JOIN " + table_name3 + " ON " + Join3 + " = " + Join4 + " WHERE (" + FilterColumn + " ='" + FilterData + "')";
                IDataReader dr = db.ExecuteReader(CommandType.Text, sql);
                DataTable dt = new DataTable();
                dt.Load(dr);

                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("", "0"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddl.Items.Add(new ListItem(dt.Rows[i].ItemArray[0].ToString(), dt.Rows[i].ItemArray[1].ToString()));
                }
            }

            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // As a Gridview Row Template Field

        public void DropDownBind(SqlDataSource sqlDS, GridView gridview, int RowIndex,string ControlName, string DataTextField, string DataValueField, string TableName, string SelectedValue)
        {
            try
            {
                sqlDS.ConnectionString = db.ConnectionString;
                DropDownList ddl_ = (DropDownList)gridview.Rows[RowIndex].FindControl(ControlName);

                sqlDS.SelectCommand = " SELECT " + DataValueField + ", " + DataTextField + " FROM " + TableName + "";

                ddl_.DataSource = sqlDS;
                ddl_.DataTextField = DataTextField;
                ddl_.DataValueField = DataValueField;
                ddl_.DataBind();

                ddl_.SelectedIndex = ddl_.Items.IndexOf(ddl_.Items.FindByValue(SelectedValue));
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // As a Gridview Footer Template Field

        public void DropDownBind(SqlDataSource sqlDS, GridView gridview, string ControlName, string DataTextField, string DataValueField, string TableName)
        {
            try
            {
                sqlDS.ConnectionString = db.ConnectionString; //ConfigurationManager.ConnectionStrings["RemoteApplicationServices"].ConnectionString;

                DropDownList ddl_ = (DropDownList)gridview.FooterRow.FindControl(ControlName);

                sqlDS.SelectCommand = " SELECT " + DataValueField + ", " + DataTextField + " FROM " + TableName + "";

                ddl_.DataSource = sqlDS;
                ddl_.DataTextField = DataTextField;
                ddl_.DataValueField = DataValueField;
                ddl_.DataBind();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // As a Gridview Footer Template Field

        public void DropDownBind(SqlDataSource sqlDS, GridView gridview, String sqlQuery, string ControlName, string DataTextField, string DataValueField, string FalseParam)
        {
            try
            {
                sqlDS.ConnectionString = db.ConnectionString; //ConfigurationManager.ConnectionStrings["RemoteApplicationServices"].ConnectionString;

                DropDownList ddl_ = (DropDownList)gridview.FooterRow.FindControl(ControlName);

                sqlDS.SelectCommand = sqlQuery;

                ddl_.DataSource = sqlDS;
                ddl_.DataTextField = DataTextField;
                ddl_.DataValueField = DataValueField;
                ddl_.DataBind();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        


        #endregion

        #region Message
        public void Message(bool ConfirmMessage, bool WarningMessage, String Exception, HtmlGenericControl DivConfirm, HtmlGenericControl DivWarning, Label lblConfirm, Label lblWarning)
        {
            DivConfirm.Visible = ConfirmMessage;
            if (ConfirmMessage == true) lblConfirm.Text = Exception.ToString();
            DivWarning.Visible = WarningMessage;
            if (WarningMessage == true) lblWarning.Text = Exception.ToString();
        }

        public void Message(bool ConfirmMessage, bool WarningMessage, HtmlGenericControl divConfirmMessage, HtmlGenericControl divWarningMessage)
        {
            divConfirmMessage.Visible = ConfirmMessage;
            divWarningMessage.Visible = WarningMessage;
        }
        #endregion

        #region Hash Code Generator
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
        #endregion

        #region Get GridView Controls Value

        public String[] FindControlRow(GridView gridview, int RowIndex, string[] ControlNames)
        {
            try
            {
                String[] ControlValue = new String[ControlNames.Length];
                for (int i = 0; i < ControlNames.Length; i++)
                {
                    string ControlType = ControlNames[i].Substring(0, 3);

                    switch (ControlType)
                    {
                        case "lbl":
                            Label lbl_ = (Label)gridview.Rows[RowIndex].FindControl(ControlNames[i]);
                            ControlValue[i] = lbl_.Text.ToString();
                            break;

                        case "txt":
                            TextBox txt_ = (TextBox)gridview.Rows[RowIndex].FindControl(ControlNames[i]);
                            ControlValue[i] = txt_.Text.ToString();
                            break;

                        case "lbt":
                            LinkButton lbtn_ = (LinkButton)gridview.Rows[RowIndex].FindControl(ControlNames[i]);
                            ControlValue[i] = lbtn_.Text.ToString();
                            break;

                        case "ddl":
                            DropDownList ddl_ = (DropDownList)gridview.Rows[RowIndex].FindControl(ControlNames[i]);
                            ControlValue[i] = ddl_.SelectedItem.ToString();
                            break;

                        case "dlv":
                            DropDownList dlv_ = (DropDownList)gridview.Rows[RowIndex].FindControl(ControlNames[i]);
                            ControlValue[i] = dlv_.SelectedValue.ToString();
                            break;

                        default:
                            throw new Exception("Object Naming Convention Error.");
                    }
                }

                return ControlValue;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String[] FindControlRow(GridView gridview, string[] ControlNames)
        {
            try
            {
              
                String[] ControlValue = new String[ControlNames.Length];
                //GridViewRow dr = gridview.Rows[RowIndex];
                //for (int i = 0; i < ControlNames.Length; i++)
                //{
                //    String ControlType = ControlNames[i].Substring(0, 3);


                //    switch (ControlType)
                //    {
                //        case "lbl":
                //            Label lbl_ = (Label)dr.Cells[0].FindControl(ControlNames[i]);
                //            ControlValue[i] = lbl_.Text.ToString();
                //            break;

                //        case "txt":
                //            TextBox txt_ = (TextBox)dr.Cells[0].FindControl(ControlNames[i]);
                //            ControlValue[i] = txt_.Text.ToString();
                //            break;

                //        case "ddl":

                //            break;

                //        case "chk":

                //            break;

                //        default:
                //            throw new Exception("Object Naming Convention Error.");
                //    }
                //}

                return ControlValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String[] FindControlCell(GridView gridview, int RowIndex, string[] ControlNames) // Not Completed
        {
            try
            {
                GridViewRow dr = gridview.Rows[RowIndex];
                String[] ControlValue = new String[ControlNames.Length];

                for (int i = 0; i < ControlNames.Length; i++)
                {
                    string ControlType = ControlNames[i].Substring(0, 3);

                    switch (ControlType)
                    {
                        case "lbl":
                            Label lbl_ = (Label)gridview.Rows[RowIndex].FindControl(ControlNames[i]);
                            ControlValue[i] = lbl_.Text.ToString();
                            break;

                        case "txt":
                            TextBox txt_ = (TextBox)gridview.Rows[RowIndex].FindControl(ControlNames[i]);
                            ControlValue[i] = txt_.Text.ToString();
                            break;

                        case "ddl":
                            DropDownList ddl_ = (DropDownList)gridview.Rows[RowIndex].FindControl(ControlNames[i]);
                            ControlValue[i] = ddl_.SelectedItem.ToString();
                            break;

                        case "dlv":
                            DropDownList dlv_ = (DropDownList)gridview.Rows[RowIndex].FindControl(ControlNames[i]);
                            ControlValue[i] = dlv_.SelectedValue.ToString();
                            break;

                        default:
                            throw new Exception("Object Naming Convention Error.");
                    }
                }

                return ControlValue;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String[] FindControlFooter(GridView gridview, string[] ControlNames)
        {
            try
            {
                String[] ControlValue = new String[ControlNames.Length];
                for (int i = 0; i < ControlNames.Length; i++)
                {
                    string ControlType = ControlNames[i].Substring(0, 3);

                    switch (ControlType)
                    {
                        case "lbl":
                            Label lbl_ = (Label)gridview.FooterRow.FindControl(ControlNames[i]);
                            ControlValue[i] = lbl_.Text.ToString();
                            break;

                        case "txt":
                            TextBox txt_ = (TextBox)gridview.FooterRow.FindControl(ControlNames[i]);
                            ControlValue[i] = txt_.Text.ToString();
                            break;

                        case "ddl":
                            DropDownList ddl_ = (DropDownList)gridview.FooterRow.FindControl(ControlNames[i]);
                            ControlValue[i] = ddl_.SelectedItem.ToString();
                            break;

                        case "dlv":
                            DropDownList dlv_ = (DropDownList)gridview.FooterRow.FindControl(ControlNames[i]);
                            ControlValue[i] = dlv_.SelectedValue.ToString();
                            break;

                        default:
                            throw new Exception("Object Naming Convention Error.");
                    }
                }

                return ControlValue;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Clear Controls Value

        #region Clear Textbox Value

        public void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = String.Empty;
                ClearInputs(ctrl.Controls);
            }
        }
        #endregion

        #region Clear Dropdown Selected Value
        public void ClearSelection(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is DropDownList)
                    ((DropDownList)ctrl).SelectedIndex = 0;
                ClearSelection(ctrl.Controls);
            }
        }
        #endregion

        #endregion

        

        #region Previous Page
        public void PreviousPage(string Url)
        {
            try
            {
                HttpContext.Current.Response.Redirect(Url);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Store Procedure
        public DataTable ExecuteStoredProc(String SPName, object[] Para)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ErpCon"].ConnectionString);
                Database db = DatabaseFactory.CreateDatabase("ErpCon");
                DataTable dt = new DataTable();
                System.Data.Common.DbCommand cmd;
                conn.Open();
                if (Para == null)
                {                    
                    cmd = db.GetStoredProcCommand(SPName);
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 600;
                    dt.Load(cmd.ExecuteReader());
                }
                else
                {
                    cmd = db.GetStoredProcCommand(SPName, Para);
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 600;
                    //dt.Load(db.ExecuteReader(SPName, Para));
                    dt.Load(cmd.ExecuteReader());
                }
                if (conn.State == ConnectionState.Open) conn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion

        #region TextBox Length Validator

        public void TextBoxLengthSet(ControlCollection ctrls)
        {
            try
            {
                foreach (Control ctrl in ctrls)
                {
                    //if (ctrl is TextBox)
                    //{
                    //    switch((TextBox)ctrl).ID)
                    //    ((TextBox)ctrl).Text = String.Empty;
                    //}
                    ClearInputs(ctrl.Controls);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void TextBoxLengthValidator(ControlCollection ctrls)
        //{
        //    try
        //    {
        //        string TextBG = "";
        //        foreach (Control ctrl in ctrls)
        //        {
        //            if (ctrl is TextBoxExt)
        //            {
        //                TextBG = ((TextBoxExt)ctrl).GroupName;

        //                switch (TextBG)
        //                {
        //                    case "Client Name":
        //                        if (((TextBoxExt)ctrl).Text.Length > 30)
        //                        {
        //                            throw new Exception("Client Name Can't be more than 30 Character");
        //                        }
        //                        break;
        //                }
        //            }
        //            TextBoxLengthValidator(ctrl.Controls);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion

        

        # region Date Format

        public String DateFormatLedger(string Date)
        {
            try
            {
                string day = Date.Substring(0, 2);
                string Month = Date.Substring(3, 2);
                string Year = Date.Substring(6, 2);
                Date = Month + "/" + day + "/" + Year;

                return Date;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String DateFormat(string Date)
        {
            try
            {
                string ReturnValue = "";

                if (Date != String.Empty)
                {
                    string FindSlash1st = Date.Substring(2, 1);
                    string FindSlash2nd = Date.Substring(5, 1);

                    if (FindSlash1st == "/" && FindSlash2nd == "/")
                    {
                        int DayCheck = Convert.ToInt16(Date.Substring(0, 2));
                        int MonthCheck = Convert.ToInt16(Date.Substring(3, 2));

                        if ((DayCheck >= 1 && DayCheck <= 31) && (MonthCheck >= 1 && MonthCheck <= 12))
                        {
                            string[] GetYear = Date.Split('/');
                            int YearLength = Convert.ToInt16(GetYear[2].Length);
                            //ReturnValue = YearLength.ToString();
                            if (YearLength == 2)
                            {
                                DateTime dt = DateTime.ParseExact(Date, "dd/MM/yy", null);
                                Date = dt.ToString("MM/dd/yy");
                                ReturnValue = Date;
                            }
                            else if (YearLength == 4)
                            {
                                DateTime dt = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
                                Date = dt.ToString("MM/dd/yyyy");
                                ReturnValue = Date;
                            }
                            else
                            {
                                ReturnValue = "Invalid Date Time Format";
                            }
                        }
                        else
                        {
                            ReturnValue = "Invalid Date Time Format";
                        }
                    }
                    else
                    {
                        ReturnValue = "Invalid Date Time Format";
                    }
                }
                else
                {
                    ReturnValue = "Invalid Date Time Format";
                }
                return ReturnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        
    }
}