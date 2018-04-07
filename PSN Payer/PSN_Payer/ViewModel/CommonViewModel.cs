using PSN_Payer.Helper;
using PSN_Payer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN_Payer.ViewModel
{
    public class CommonViewModel : ObservableObject
    {
        public CommonViewModel()
        {
            //SetBtnBackgroundColor();
        }

        #region Properties Declaration

        string _btnColor = string.Empty;

        public string btnColor
        {
            get { return _btnColor; }
            set {
                _btnColor = value;
                OnPropertyChanged(nameof(btnColor));
            }
        }


        #endregion

        //set all buttons background color from here
        public void SetBtnBackgroundColor()
        {
            btnColor = AppResource.BtnBackgroundColor;
        }

    }
}
