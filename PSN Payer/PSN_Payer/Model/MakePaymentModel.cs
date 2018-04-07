using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN_Payer.Model
{
    public class MakePaymentModel
    {
        public static string MinCheck { get; set; }
        public static string MaxCheck { get; set; }
        public static string MinCredit { get; set; }
        public static string MaxCredit { get; set; }
        public static string StartDay { get; set; }
        public static string EndDay { get; set; }
        public static string Required_Field { get; set; }
    }
}
