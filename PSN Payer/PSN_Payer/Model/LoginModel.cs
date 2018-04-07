using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN_Payer.Model
{
    public class LoginModel
    {
        public static string SessionID { get; set; }
        public static string Result { get; set; }
        public static string ErrorCode { get { return "0"; }  }
        public static string OldPassword { get; set; }
        public static int CheckSum { get; set; }
        public string UserID = string.Empty;
        public string Password = string.Empty;
        public bool Remember = false;
    }
}
