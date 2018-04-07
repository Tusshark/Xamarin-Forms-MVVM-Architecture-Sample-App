using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN_Payer.Model
{
    public class Setting
    {
        public string OldPassword = string.Empty;
        public  string NewPassword = string.Empty;
        public string ConfirmPassword = string.Empty;
        public static string Result { get; set; }
        public static string ErrNum { get; set; }
    }
}
