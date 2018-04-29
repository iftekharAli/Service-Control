using ServiceControl.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ServiceControl.Controllers
{
    public class TrailModelGrid
    {
        public List<TrailModel> GetFollowTrail(string FileName)
        {
            List<TrailModel> objTrailModelList = new List<TrailModel>();         
            string[] GetText = new string[3];

            StreamReader reader = new StreamReader(@"\\192.168.10.5\\Log\\" + FileName + ""); //pick appropriate Encoding

            reader.BaseStream.Seek(0, SeekOrigin.End);
            int count = 0;
            while ((count < 23) && (reader.BaseStream.Position > 0))
            {
                reader.BaseStream.Position--;
                int c = reader.BaseStream.ReadByte();
                if (reader.BaseStream.Position > 0)
                    reader.BaseStream.Position--;
                if (c == Convert.ToInt32('\n'))
                {
                    ++count;
                }
            }
            string str = reader.ReadToEnd();
            string[] arr = str.Replace("\r", "").Split('\n');
            reader.Close();

            List<string> listahb = new List<string>();


            TrailModel objTrailModel;
            for (int i = 0; i < 21; i++)
            {
                if (arr[i].Length > 20)
                {
                    objTrailModel = new TrailModel();
                    GetText = TextSplit(arr[i]);
                    objTrailModel.Time = GetText[0];
                    objTrailModel.MSISDN = GetText[1];
                    objTrailModel.SMS = GetText[2];
                    if (i <= 5)
                        objTrailModelList.Add(objTrailModel);

                    listahb.Add(GetText[0]);
                }
            }

            //GlobalVariables.listahb = listahb;
            TPS(listahb);

            return objTrailModelList;
        }
        public void TPS(List<string> listahb)
        {
            string ahb = "";
            Dictionary<object, int> tmp = new Dictionary<object, int>();

            foreach (Object obj in listahb)
                if (!tmp.ContainsKey(obj))
                    tmp.Add(obj, 1);
                else tmp[obj]++;

            ahb = tmp.Values.Max().ToString();

            //TrailModelStatus objTrailModelStatus = new TrailModelStatus();
            //objTrailModelStatus.TPS = ahb;

            GlobalVariables.TPS = ahb;
        }
        private string[] TextSplit(string getLine)
        {
            string[] rtnMessage = new string[3];

            string FindTime_From = "SendTime=";
            string FindTime_To = "TrID=";

            string FindMSISDN_From = "	mNo=";
            string FindMSISDN_To = "	SMS";

            string FindSMS_From = "	SMS=";
            string FindSMS_To = "	sId=";

            int startTime = getLine.IndexOf(FindTime_From) + FindTime_From.Length;
            int endTime = getLine.IndexOf(FindTime_To, startTime);
            rtnMessage[0] = getLine.Substring(startTime, endTime - startTime);


            int startMSISDN = getLine.IndexOf(FindMSISDN_From) + FindMSISDN_From.Length;
            int endMSISDN = getLine.IndexOf(FindMSISDN_To, startMSISDN);
            rtnMessage[1] = getLine.Substring(startMSISDN, endMSISDN - startMSISDN);

            int startSMS = getLine.IndexOf(FindSMS_From) + FindSMS_From.Length;
            int endSMS = getLine.IndexOf(FindSMS_To, startSMS);
            rtnMessage[2] = getLine.Substring(startSMS, endSMS - startSMS);

            return rtnMessage;
        }
    }
}