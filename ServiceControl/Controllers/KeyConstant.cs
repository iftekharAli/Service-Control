using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceControl.Controllers
{
    public static class KeyConstant
    {

        #region SESSION

        public static string Session_UserID = "SessionUserID";
        public static string Session_UserID_Value = "None";

        #endregion

        #region SERVER

        public static string Server_Local = "CN_Userinfo";
        public static string Server_Sahbox_17_Blink = "CN_Server_Sahbox_17_Blink";
        public static string Server_Sahbox_17 = "CN_Server_Sahbox_17_ServiceControlDB_AHB";
        public static string Server_31_Warid = "CN_Server_31_Airtel";
        public static string Server_31_WaridContent = "CN_Server_31_AirtelContent";

        #endregion

        #region Operator

        public static string Robi = "ROBI";
        public static string Airtel = "AIRTEL";
        public static string GP = "GP";
        public static string Banglalink = "Banglalink";
        public static string Teletalk = "Teletalk";
        public static string CityCell = "CityCell";

        #endregion

        #region CRUD operation KEY

        public static string COMMAND_INSERT = "INSERT";
        public static string COMMAND_UPDATE = "UPDATE";
        public static string COMMAND_DELETE = "DELETE";

        public static readonly string COMMAND_MANUAL_INSERT = "MANUAL_INSERT";
        public static readonly string COMMAND_MANUAL_UPDATE = "MANUAL_UPDATE";
        public static readonly string COMMAND_MANUAL_DELETE = "MANUAL_DELETE";

        public static readonly string COMMAND_EXCEL_INSERT = "EXCEL_INSERT";
        public static readonly string COMMAND_EXCEL_UPDATE = "EXCEL_UPDATE";
        public static readonly string COMMAND_EXCEL_DELETE = "EXCEL_DELETE";

        #endregion

        #region Permission KEY

        public static readonly int COMMAND_CanVisit = 0;
        public static readonly int COMMAND_CanEdit = 1;
        public static readonly int COMMAND_CanDelete = 2;
        public static readonly int COMMAND_CanExecute = 3;

        #endregion

        #region Excel File ID of Monthly Content

        public static readonly string Airtel1 = "Airtel1";
        public static readonly string Airtel2 = "Airtel2";
        public static readonly string BuddyService = "BuddyService";
        public static readonly string ShaboxHoroscope = "ShaboxHoroscope";
        public static readonly string ShaboxPart1 = "ShaboxPart-1";
        public static readonly string ShaboxPart2 = "ShaboxPart2";
        public static readonly string AirtelHoroscope = "AirtelHoroscope";

        #endregion

        #region Action Name

        public static string actionAirtel1 = "Airtel1";
        public static string actionAirtel2 = "Airtel2";
        public static string actionBuddyService = "BuddyService";
        public static string actionBuddyServiceRamadan = "BuddyServiceRamadan";
        public static string actionBuddyServiceIntl = "BuddyServiceIntl";
        
        public static string actionAirtelHoroscope = "AirtelHoroscope";
        public static string actionShaboxPart1 = "ShaboxPart1";
        public static string actionShaboxPart2 = "ShaboxPart2";
        public static string actionShaboxHoroscope = "ShaboxHoroscope";

        #endregion
    }
}