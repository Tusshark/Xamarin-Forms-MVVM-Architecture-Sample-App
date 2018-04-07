using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN_Payer.Model
{
    public class HistoryModel
    {
        public string PaymentType { get; set; }
        public string Amount { get; set; }
        public string ReceiptID { get; set; }
        public string TransDate { get; set; }
        public string TransStatus { get; set; }
        public string HistoryListItemBackground { get; set; }
        public string StatusImage { get; set; }
    }
}
