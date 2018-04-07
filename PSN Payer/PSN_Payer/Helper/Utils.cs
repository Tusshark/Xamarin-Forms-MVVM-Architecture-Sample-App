using PSN_Payer.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Xamarin.Forms;

namespace PSN_Payer.Helper
{
    public class Utils
    {
        public static string StartingRequestXML()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            stringBuilder.Append("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            stringBuilder.Append("<soap:Body>");
            return stringBuilder.ToString();
        }

        public static string RequestURL()
        {
            return "https://www.paymentservicenetwork.com/PSNSoftNet/\"";
        }

        public static string EndingRequestXML()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("</soap:Body>");
            stringBuilder.Append("</soap:Envelope>");
            return stringBuilder.ToString();
        }

        public static string FilePath()
        {
           return DependencyService.Get<IRemember>().GetFilePath();
        }

        public static string ServicePath()
        {
            return AppResource.ServicePathBeta;
            //return AppResource.ServicePathProduction;
        }
    }
}
