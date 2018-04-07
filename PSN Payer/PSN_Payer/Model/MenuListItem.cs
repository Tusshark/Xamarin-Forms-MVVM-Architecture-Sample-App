using PSN_Payer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSN_Payer.Model
{
    public class MenuListItem
    {
        public int id { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Arrow { get; set; }
        public string BalanceMsg { get; set; }
        //public Color RuntimeColor { get; set; }
        private bool _isVisibleBalance;
        public bool IsVisibleBalance
        {
            get {
                return _isVisibleBalance = id == 1 ? true : false;
            }
            set {
                _isVisibleBalance = value;
            }
        }

        private Color _runtimeColor;
        public Color RuntimeColor
        {
            get
            {
                return id % 2 == 0 ? Color.FromHex(AppResource.LoaderBackGround) : Color.White;
            }
            set
            {
                _runtimeColor = value;
            }
        }

        public Command SelectedMenuItem { get; set; }
    }





}
