using PSN_Payer.Helper;
using PSN_Payer.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using PSN_Payer.Resources;
using System.Windows.Input;

namespace PSN_Payer.ViewModel
{
    public class BillViewModel : ObservableObject
    {

        #region Properties and Delegates declarations

        public ObservableRangeCollection<BillModel> BillsList { get; set; }
        public List<BillModel> _billList { get; set; }

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set {
                _isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }

        private Command _redirectToBill;

        public Command RedirectToBill
        {
            get { return _redirectToBill; }
            set {
                _redirectToBill = value;
                OnPropertyChanged(nameof(RedirectToBill));
            }
        }

        private string _billHeadMsg;

        public string BillHeadMsg
        {
            get { return _billHeadMsg; }
            set {
                _billHeadMsg = value;
                OnPropertyChanged(nameof(BillHeadMsg));
            }
        }

        private string _billSign;

        public string BillSign
        {
            get { return _billSign; }
            set {
                _billSign = value;
                OnPropertyChanged(nameof(BillSign));
            }
        }

        private string _stkHeadBackGround;

        public string StkHeadBackGround
        {
            get { return _stkHeadBackGround; }
            set {
                _stkHeadBackGround = value;
                OnPropertyChanged(nameof(StkHeadBackGround));
            }
        }

        #endregion

        public BillViewModel()
        {
            try
            {
                StkHeadBackGround = AppResource.LoaderBackGround;
                BillsList = new ObservableRangeCollection<BillModel>();
                _billList = new List<BillModel>();
                Load();

                RedirectToBill = new Command((req) =>
                {
                    if(!string.IsNullOrEmpty(Convert.ToString(req)))
                    {
                        Device.OpenUri(new Uri(Convert.ToString(req)));
                    }
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public async void Load()
        {
            IsRunning = true;
            await GetBillsData();
            IsRunning = false;
        }

        public async Task GetBillsData()
        {
            try
            {
                // Call the API for Bill Data
                _billList = new List<BillModel>();
                if (_billList.Count > 0)
                {
                    _billList.Where(_ => !string.IsNullOrEmpty(_.EBillLinkUrl)).Select(_ => { _.RedirectToBill = RedirectToBill; return _; }).ToList();
                    //BillHeadMsg = "Total bills generated: " + _billList.Count;
                    BillsList.AddRange(_billList);
                }
                else
                {
                    BillHeadMsg = "No record found.";
                   await App.Current.MainPage.DisplayAlert("Alert", AppResource.NoBillGenerated, "OK");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

    }
}
