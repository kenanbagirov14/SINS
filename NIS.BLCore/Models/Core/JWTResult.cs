using System;
using System.Collections.Generic;
using System.Text;

namespace NIS.BLCore.Models.Core
{
    public class JWTResult
    {
        public string access_token { get; set; }
        public bool error { get; set; }
        public string errorMessage { get; set; }
        public string Department { get; set; }
        public string Roles { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
    }
}
