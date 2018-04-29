using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceControl.Models;
using System.Data;

namespace ServiceControl.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        clsDAL objDAL;

        
        [HttpGet]
        public ActionResult Login()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel objLoginModel)
        {
            if (ModelState.IsValid)
            {
                if(LoginVerify(objLoginModel.UserName, objLoginModel.Password)==true)
                    return RedirectToAction("Index", "Home");

                else
                    Session[KeyConstant.Session_UserID] = KeyConstant.Session_UserID_Value;
                    return RedirectToAction("Login", "Account");
            }
            return View();
        }

        private bool LoginVerify(string UserName, string Password)
        {
            bool IsAuthenticate = false;
            string PasswordHashCode = CommonMethod.MD5Hash(Password);

            //DataTable dt = new DataTable();
            //objDAL = new clsDAL();
            //string sqlQuery = "EXEC sp_Login '" + UserName + "','" + PasswordHashCode + "'";
            //dt = objDAL.GetDataTable(sqlQuery,KeyConstant.Server_Local);

            //if (dt.Rows.Count == 1)
            //{
                IsAuthenticate = true;
                Session[KeyConstant.Session_UserID] = "arefin"; //dt.Rows[0]["UserId"].ToString();
                SessionValueSet.Session_UserID_Value = "arefin"; //dt.Rows[0]["UserId"].ToString(); 
            //}

            return IsAuthenticate;
        }


        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationModel objRegModel)
        {            
            if (ModelState.IsValid)
            {
                if (objRegModel.Password == objRegModel.ConfirmPassword)
                {
                    string PasswordHashCode = CommonMethod.MD5Hash(objRegModel.Password);

                    objDAL = new clsDAL();
                    string sqlQuery = "EXEC sp_Account '" + KeyConstant.COMMAND_INSERT + "','" + objRegModel.UserName + "','" + objRegModel.Name + "','" + objRegModel.UserID + "','" + objRegModel.RoleName + "','" + PasswordHashCode + "'";
                    objDAL.ExecuteQuery(sqlQuery, KeyConstant.Server_Local);

                }
            }
            return View();
        }

	}
}