using PSN_Payer.Helper;
using PSN_Payer.Model;
using PSN_Payer.Resources;
using PSN_Payer.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PSN_Payer.ViewModel
{
    public class MakePaymentViewModel : ObservableObject
    {
        #region Properties and Delegate Declarations

        public ICommand BtnEditInfo { get; set; }
        public ObservableRangeCollection<MethodType> MethodTypeList { get; set; }
        public ICommand BtnAnotherPayment { get; set; }
        public ICommand BtnDashBoard { get; set; }

        private string _paymentInfoIcon;

        public string PaymentInfoIcon
        {
            get { return _paymentInfoIcon; }
            set {
                _paymentInfoIcon = value;
                OnPropertyChanged(nameof(PaymentInfoIcon));
            }
        }

        private string _confirmPaymentIcon;

        public string ConfirmPaymentIcon
        {
            get { return _confirmPaymentIcon; }
            set {
                _confirmPaymentIcon = value;
                OnPropertyChanged(nameof(ConfirmPaymentIcon));
            }
        }

        private string _paymentReceiptIcon;

        public string PaymentReceiptIcon
        {
            get { return _paymentReceiptIcon; }
            set {
                _paymentReceiptIcon = value;
                OnPropertyChanged(nameof(PaymentReceiptIcon));
            }
        }

        private bool _paymentInfoVisibility;

        public bool PaymentInfoVisibility
        {
            get { return _paymentInfoVisibility; }
            set {
                _paymentInfoVisibility = value;
                OnPropertyChanged(nameof(PaymentInfoVisibility));
            }
        }

        private string _paymentInfoHeadMsg;

        public string PaymentInfoHeadMsg
        {
            get { return _paymentInfoHeadMsg; }
            set {
                _paymentInfoHeadMsg = value;
                OnPropertyChanged(nameof(PaymentInfoHeadMsg));
            }
        }

        private string _btnBackGround;

        public string BtnBackGround
        {
            get { return _btnBackGround; }
            set {
                _btnBackGround = value;
                OnPropertyChanged(nameof(BtnBackGround));
            }
        }

        private string _currentDate;

        public string CurrentDate
        {
            get { return _currentDate; }
            set {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
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

        private string _paymentAmount;

        public string PaymentAmount
        {
            get { return _paymentAmount; }
            set {
                _paymentAmount = value;
                OnPropertyChanged(nameof(PaymentAmount));
            }
        }

        #region Method Type Class For Make Payment Screen

        public class MethodType
        {
            public string Type { get; set; }
            public string PaymentMethodType { get; set; }
        }

        #endregion

        private bool _confirmPaymentVisibility;

        public bool ConfirmPaymentVisibility
        {
            get { return _confirmPaymentVisibility; }
            set {
                _confirmPaymentVisibility = value;
                OnPropertyChanged(nameof(ConfirmPaymentVisibility));
            }
        }

        private string _confirmPaymentHeadMsg;

        public string ConfirmPaymentHeadMsg
        {
            get { return _confirmPaymentHeadMsg; }
            set {
                _confirmPaymentHeadMsg = value;
                OnPropertyChanged(nameof(ConfirmPaymentHeadMsg));
            }
        }

        private string _payingToValue;

        public string PayingToValue
        {
            get { return _payingToValue; }
            set {
                _payingToValue = value;
                OnPropertyChanged(nameof(PayingToValue));
            }
        }

        private string _paymentMethodValue;

        public string PaymentMethodValue
        {
            get { return _paymentMethodValue; }
            set {
                _paymentMethodValue = value;
                OnPropertyChanged(nameof(PaymentMethodValue));
            }
        }

        private string _balanceDueValue;

        public string BalanceDueValue
        {
            get { return _balanceDueValue; }
            set {
                _balanceDueValue = value;
                OnPropertyChanged(nameof(BalanceDueValue));
            }
        }

        private string _paymentDateValue;

        public string PaymentDateValue
        {
            get { return _paymentDateValue; }
            set {
                _paymentDateValue = value;
                OnPropertyChanged(nameof(PaymentDateValue));
            }
        }

        private string _paymentAmountValue;

        public string PaymentAmountValue
        {
            get { return _paymentAmountValue; }
            set {
                _paymentAmountValue = value;
                OnPropertyChanged(nameof(PaymentAmountValue));
            }
        }

        private string _convenienceFeeValue;

        public string ConvenienceFeeValue
        {
            get { return _convenienceFeeValue; }
            set {
                _convenienceFeeValue = value;
                OnPropertyChanged(nameof(ConvenienceFeeValue));
            }
        }

        private string _totalFeeValue;

        public string TotalFeeValue
        {
            get { return _totalFeeValue; }
            set {
                _totalFeeValue = value;
                OnPropertyChanged(nameof(TotalFeeValue));
            }
        }

        private string _receiptNo;

        public string ReceiptNo
        {
            get { return _receiptNo; }
            set {
                _receiptNo = value;
                OnPropertyChanged(nameof(ReceiptNo));
            }
        }

        private string _confimationOnEmail;

        public string ConfimationOnEmail
        {
            get { return _confimationOnEmail; }
            set {
                _confimationOnEmail = value;
                OnPropertyChanged(nameof(ConfimationOnEmail));
            }
        }

        private string _transactionStatus;

        public string TransactionStatus
        {
            get { return _transactionStatus; }
            set {
                _transactionStatus = value;
                OnPropertyChanged(nameof(TransactionStatus));
            }
        }

        private bool _confirmationVisibility;

        public bool ConfirmationVisibility
        {
            get { return _confirmationVisibility; }
            set {
                _confirmationVisibility = value;
                OnPropertyChanged(nameof(ConfirmationVisibility));
            }
        }

        private bool _stkReceiptVisibility;

        public bool StkReceiptVisibility
        {
            get { return _stkReceiptVisibility; }
            set {
                _stkReceiptVisibility = value;
                OnPropertyChanged(nameof(StkReceiptVisibility));
            }
        }

        private bool _paymentReceiptVisibility;

        public bool PaymentReceiptVisibility
        {
            get { return _paymentReceiptVisibility; }
            set {
                _paymentReceiptVisibility = value;
                OnPropertyChanged(nameof(PaymentReceiptVisibility));
            }
        }

        private string _statusImage;

        public string StatusImage
        {
            get { return _statusImage; }
            set {
                _statusImage = value;
                OnPropertyChanged(nameof(StatusImage));
            }

        }


        #endregion


        public MakePaymentViewModel()
        {
            try
            {
                PaymentInfoIcon = AppResource.PayInfoFilled;
                PaymentReceiptIcon = AppResource.PayReceiptBlank;
                ConfirmPaymentIcon = AppResource.ConfPayBlank;
                PaymentInfoVisibility = true;
                PaymentInfoHeadMsg = AppResource.PaymentInfoHeadMsg;
                BtnBackGround = AppResource.BtnBackgroundColor;
                ConfirmPaymentHeadMsg = AppResource.ConfirmPaymentHeadMsg;
                CurrentDate = Convert.ToString(System.DateTime.Now.Date.ToString(AppResource.DateFormat));
                MethodTypeList = new ObservableRangeCollection<MethodType>();
                GetMethodTypeList();

                BtnEditInfo = new Command(() =>
                {
                    PaymentInfoIcon = AppResource.PayInfoFilled;
                    PaymentReceiptIcon = AppResource.PayReceiptBlank;
                    ConfirmPaymentIcon = AppResource.ConfPayBlank;
                    PaymentInfoVisibility = true;
                    ConfirmPaymentVisibility = false;
                });

                BtnAnotherPayment = new Command(async () =>
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new MainPage(new MakePaymentEnterPaymentInfo()));
                });

                BtnDashBoard = new Command(async () =>
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new Dashboard());
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public void GetMethodTypeList()
        {
            try
            {
                IsRunning = true;
                MethodTypeList.Clear();
                List<PaymentMethodModel> lstData = new List<PaymentMethodModel>(); // Get Payment Method list from API
                foreach (PaymentMethodModel obj in lstData)
                {
                    MethodType cls = new MethodType();
                    cls.Type = obj.MethodType.Trim() + ":" + obj.AccountNickname + "(" + obj.AccountNumber + ")";
                    cls.PaymentMethodType = obj.AccountNickname + "(" + obj.AccountNumber + ")";
                    MethodTypeList.Add(cls);
                }
                IsRunning = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public async void SetToShowPaymentReceiptSection()
        {
            try
            {
                PaymentReceiptVisibility = true;
                ConfirmPaymentVisibility = false;
                PaymentInfoVisibility = false;
                PaymentInfoIcon = AppResource.PayInfoFilled;
                ConfirmPaymentIcon = AppResource.ConfPayFilled;
                PaymentReceiptIcon = AppResource.PayReceiptFilled;

                if (ProcessPaymentModel.ErrNum.Equals("703"))
                {
                    TransactionStatus = ProcessPaymentModel.Result;
                    StkReceiptVisibility = false;
                    ConfirmationVisibility = false;
                    StatusImage = AppResource.RejectedStatus;
                }
                else if (ProcessPaymentModel.ErrNum.Equals("0"))
                {
                    TransactionStatus = ProcessPaymentModel.Result;
                    StkReceiptVisibility = true;
                    ConfirmationVisibility = true;
                    ReceiptNo = ProcessPaymentModel.Receipt;
                    ConfimationOnEmail = AppResource.ConfirmationOnEmail;
                    StatusImage = AppResource.CheckedStatus;
                }
                else if (ProcessPaymentModel.ErrNum == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", AppResource.UnknownError, "OK");
                    TransactionStatus = AppResource.UnknownError;
                    StkReceiptVisibility = false;
                    ConfirmationVisibility = false;
                    StatusImage = AppResource.RejectedStatus;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", ProcessPaymentModel.Result, "OK");
                    StatusImage = AppResource.RejectedStatus;
                    StkReceiptVisibility = false;
                    ConfirmationVisibility = false;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
