using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN_Payer.Model
{
    public class PaymentMethodInfo
    {
        public string Type { get; set; }
        public string Fee { get; set; }
        public string FeeType { get; set; }
        public string NSF { get; set; }
        public string LowFee { get; set; }
        public string LowFeeType { get; set; }
        public string LowFeeFixed { get; set; }
    }
}
