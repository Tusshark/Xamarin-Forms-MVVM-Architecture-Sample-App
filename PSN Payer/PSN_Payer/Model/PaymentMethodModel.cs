using PSN_Payer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSN_Payer.Model
{
    public class PaymentMethodModel
    {
        public static string TrackingID { get; set; }

        public static string Status { get; set; }

        public static string AuthCode { get; set; }

        public static string Description { get; set; }

        public static string Method { get; set; }

        public string Type { get; set; }

        public string Fee { get; set; }

        public string FeeType { get; set; }

        public string NSF { get; set; }

        public static string RateType { get; set; }

        public static string FixedRate { get; set; }

        public static string FixedInd { get; set; }

        public static string Result { get; set; }

        public static string ErrNum { get; set; }

        public string MethodType { get; set; }
        public Command RemovePaymentMethod { get; set; }
        public string RemoveImage { get; set; }
        public string AccountNumber { get; set; }
        public string AccountNickname { get; set; }
        public string CCExpDT { get; set; }
        public string Bankname { get; set; }
        public string BankingID { get; set; }
        public string LowFee { get; set; }
        public string LowFeeType { get; set; }
        public string LowFeeFixed { get; set; }
        public PaymentMethodModel(string _Type, string _Fee, string Fee_Type, string NSF_Fee, string _LowFee, string _LowFeeType, string _LowFeeFixed)
        {
            this.Type = _Type;
            this.Fee = _Fee;
            this.FeeType = Fee_Type;
            this.NSF = NSF_Fee;
            this.LowFee = _LowFee;
            this.LowFeeType = _LowFeeType;
            this.LowFeeFixed = _LowFeeFixed;
        }

        public PaymentMethodModel(string Method_Type, string Account_Number, string Account_Nickname, string CC_ExpDT, string Bank_name, string Banking_ID)
        {
            this.MethodType = Method_Type;
            this.AccountNumber = Account_Number;
            this.AccountNickname = Account_Nickname;
            this.CCExpDT = CC_ExpDT;
            this.Bankname = Bank_name;
            this.BankingID = Banking_ID;
            this.RemoveImage = AppResource.RejectedStatus;
        }
    }
}
