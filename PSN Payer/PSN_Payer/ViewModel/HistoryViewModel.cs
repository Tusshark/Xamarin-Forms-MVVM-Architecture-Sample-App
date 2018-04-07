using PSN_Payer.Helper;
using PSN_Payer.Model;
using PSN_Payer.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PSN_Payer.ViewModel
{
    public class HistoryViewModel : ObservableObject
    {
        #region Properties and Delegate Declarations

        public ObservableRangeCollection<HistoryModel> HistoryList { get; set; }
        public List<HistoryModel> _historyList { get; set; }

        private string _historyHeadMessage;

        public string HistoryHeadMessage
        {
            get { return _historyHeadMessage; }
            set {
                _historyHeadMessage = value;
                OnPropertyChanged(nameof(HistoryHeadMessage));
            }
        }

        private string _messageColor;

        public string MessageColor
        {
            get { return _messageColor; }
            set {
                _messageColor = value;
                OnPropertyChanged(nameof(MessageColor));
            }
        }

        private string _listHeaderBackGround;

        public string ListHeaderBackGround
        {
            get { return _listHeaderBackGround; }
            set {
                _listHeaderBackGround = value;
                OnPropertyChanged(nameof(ListHeaderBackGround));
            }
        }

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set {
                _isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }

        #endregion

        public HistoryViewModel()
        {
            try
            {
                ListHeaderBackGround = AppResource.BtnBackgroundColor;
                HistoryHeadMessage = AppResource.HistoryHeadMessage;
                _historyList = new List<HistoryModel>();
                //_historyList.Add(new HistoryModel { Amount = "10", TransDate = "sdf", PaymentType = "sdf", ReceiptID = "sdf", TransStatus = "sdf", HistoryListItemBackground="fgd" });
                HistoryList = new ObservableRangeCollection<HistoryModel>();
                MakeHistoryList();
                
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public async void MakeHistoryList()
        {
            IsRunning = true;
            await GetHistoryList();
            IsRunning = false;
        }

        public async Task GetHistoryList()
        {
           // Call API to get history data
            if (_historyList.Count > 0)
            {
                HistoryList.AddRange(_historyList);
            }
        }
     
    }
}
