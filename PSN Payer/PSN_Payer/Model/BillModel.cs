using PSN_Payer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PSN_Payer.Model
{
    public class BillModel
    {
        public int id { get; set; }
        public string BillDate { get; set; }
        public string EBillLinkUrl { get; set; }
        public string Arrow { get; set; }
        public string BillSign { get; set; }

        private Color _runtimeColor;
        public Color RuntimeColor
        {
            get
            {
                //Color.FromHex(AppResource.LoaderBackGround)
                return id % 2 == 0 ? Color.White : Color.White;
            }
            set
            {
                _runtimeColor = value;
            }
        }

        public ICommand RedirectToBill { get; set; }
    }
}
