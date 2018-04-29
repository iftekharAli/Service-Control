using ServiceControl.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace ServiceControl.Models
{

    public class TrailModel
    {
        public string Time { get; set; }
        public string MSISDN { get; set; }
        public string SMS { get; set; }
    }

    public class TrailModelStatus
    {
        public string TPS { get; set; }
    }
}