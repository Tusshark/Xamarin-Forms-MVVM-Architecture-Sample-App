using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN_Payer.Model
{
    public class HomeModel
    {
        public static string CustName { get; set; }
        public static string CompName { get; set; }
        public static string Balance { get; set; }
        public static string DueDate { get; set; }
        public static string payOffDate { get; set; }
        public static string LastPayment { get; set; }
        public static string LastPayAmt { get; set; }
        public static string MinPayAmt { get; set; }
        public static string Payoff { get; set; }
        public static string MinPay { get; set; }
    }
}
