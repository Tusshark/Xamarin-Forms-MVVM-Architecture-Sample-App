using Plugin.Connectivity;
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
    public class PaymentMethodViewModel : ObservableObject
    {

        #region Properties and Delegate Declarations

        public ObservableRangeCollection<PaymentMethodModel> PaymentMethodList { get; set; }
        public ObservableRangeCollection<PaymentMethodInfo> MethodTypeList { get; set; }
        public ICommand BtnSaveClick { get; set; }
        public ICommand BtnCancelClick { get; set; }

        public delegate void ValidatePaymentMethodField();
        public event ValidatePaymentMethodField ValidatePaymentMethodFieldHandler;

        public delegate void AddNewPaymentMethod();
        public event AddNewPaymentMethod AddNewPaymentMethodHandler;

        public static bool IsCheck = false, IsVisaVar = true, ValidationStatus = true;

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set {
                _isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }

        private string _listBackGround;

        public string ListBackGround
        {
            get { return _listBackGround; }
            set {
                _listBackGround = value;
                OnPropertyChanged(nameof(ListBackGround));
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

        private Command _addPaymentMethod;

        public Command AddPaymentMethod
        {
            get { return _addPaymentMethod; }
            set {
                _addPaymentMethod = value;
                OnPropertyChanged(nameof(AddPaymentMethod));
            }
        }

        private Command _removePaymentMethod;

        public Command RemovePaymentMethod
        {
            get { return _removePaymentMethod; }
            set {
                _removePaymentMethod = value;
                OnPropertyChanged(nameof(RemovePaymentMethod));
            }
        }


        private string _removeImage;

        public string RemoveImage
        {
            get { return _removeImage; }
            set {
                _removeImage = value;
                OnPropertyChanged(nameof(RemoveImage));
            }
        }

        private string _lblMethodType;

        public string LblMethodType
        {
            get { return _lblMethodType; }
            set {
                _lblMethodType = value;
                OnPropertyChanged(nameof(LblMethodType));
            }
        }

        private string _selectedMethodType;

        public string SelectedMethodType
        {
            get { return _selectedMethodType; }
            set {
                _selectedMethodType = value;
                OnPropertyChanged(nameof(SelectedMethodType));
            }
        }

        private string _pickerOutlineColor;

        public string PickerOutlineColor
        {
            get { return _pickerOutlineColor; }
            set {
                _pickerOutlineColor = value;
                OnPropertyChanged(nameof(PickerOutlineColor));
            }
        }

        private string _lblNameOnCard;

        public string LblNameOnCard
        {
            get { return _lblNameOnCard; }
            set {
                _lblNameOnCard = value;
                OnPropertyChanged(nameof(LblNameOnCard));
            }
        }

        private string _lblNickName;

        public string LblNickName
        {
            get { return _lblNickName; }
            set {
                _lblNickName = value;
                OnPropertyChanged(nameof(LblNickName));
            }
        }

        private string _lblCardNo;

        public string LblCardNo
        {
            get { return _lblCardNo; }
            set {
                _lblCardNo = value;
                OnPropertyChanged(nameof(LblCardNo));
            }
        }

        private string _lblExpDate;

        public string LblExpDate
        {
            get { return _lblExpDate; }
            set {
                _lblExpDate = value;
                OnPropertyChanged(nameof(LblExpDate));
            }
        }

        private string _lblZipCode;

        public string LblZipCode
        {
            get { return _lblZipCode; }
            set {
                _lblZipCode = value;
                OnPropertyChanged(nameof(LblZipCode));
            }
        }

        private string _lblRoutingNo;

        public string LblRoutingNo
        {
            get { return _lblRoutingNo; }
            set {
                _lblRoutingNo = value;
                OnPropertyChanged(nameof(LblRoutingNo));
            }
        }

        private string _lblAcctNo;

        public string LblAcctNo
        {
            get { return _lblAcctNo; }
            set {
                _lblAcctNo = value;
                OnPropertyChanged(nameof(LblAcctNo));
            }
        }

        private bool _isCheckOrSaving;

        public bool IsCheckOrSaving
        {
            get { return _isCheckOrSaving; }
            set {
                _isCheckOrSaving = value;
                OnPropertyChanged(nameof(IsCheckOrSaving));
            }
        }

        private bool _isVisa;

        public bool IsVisa
        {
            get { return _isVisa; }
            set {
                _isVisa = value;
                OnPropertyChanged(nameof(IsVisa));
            }
        }

        private string _btnCancelBackground;

        public string BtnCancelBackground
        {
            get { return _btnCancelBackground; }
            set {
                _btnCancelBackground = value;
                OnPropertyChanged(nameof(BtnCancelBackground));
            }
        }

        private string _btnSaveBackground;

        public string BtnSaveBackground
        {
            get { return _btnSaveBackground; }
            set {
                _btnSaveBackground = value;
                OnPropertyChanged(nameof(BtnSaveBackground));
            }
        }

        private string _mainHeading;

        public string MainHeading
        {
            get { return _mainHeading; }
            set {
                _mainHeading = value;
                OnPropertyChanged(nameof(MainHeading));
            }
        }

        #endregion

        public PaymentMethodViewModel()
        {
            try
            {
                IsVisa = IsVisaVar;
                IsCheckOrSaving = IsCheck;
                RemoveImage = AppResource.RejectedStatus;
                ListBackGround = AppResource.LoaderBackGround;
                BtnBackGround = AppResource.BtnBackgroundColor;
                LblMethodType = AppResource.LblMethodType;
                PickerOutlineColor = AppResource.PickerOutlineColor;
                LblNameOnCard = AppResource.LblNameOnCard;
                LblNickName = AppResource.LblNickName;
                LblCardNo = AppResource.LblCardNo;
                LblExpDate = AppResource.LblExpDate;
                LblAcctNo = AppResource.LblAcctNo;
                LblZipCode = AppResource.LblZipCode;
                MainHeading = AppResource.FilledMandatory;
                LblRoutingNo = AppResource.LblRoutingNo;
                BtnSaveBackground = AppResource.BtnBackgroundColor;
                BtnCancelBackground = AppResource.BtnBackgroundColor;
                PaymentMethodList = new ObservableRangeCollection<PaymentMethodModel>();
                MethodTypeList = new ObservableRangeCollection<PaymentMethodInfo>();

                AddPaymentMethod = new Command(async () =>
                  {
                      await App.Current.MainPage.Navigation.PushModalAsync(new MainPage(new View.AddPaymentMethod()));
                  });

                RemovePaymentMethod = new Command(async (req) =>
                  {
                      if(!string.IsNullOrEmpty(Convert.ToString(req)))
                      {
                          var obj = await App.Current.MainPage.DisplayAlert("Alert", AppResource.ConfirmToDelete, "OK", "Cancel");
                          if (obj == true)
                          {
                              IsRunning = true;
                               // Call API to Delete or Remove banking detail
                              if (DeleteBankingModel.Result.Equals("Success") && DeleteBankingModel.ErrNum.Equals("0"))
                              {
                                 
                              }
                              else
                              {
                                  await App.Current.MainPage.DisplayAlert("Error", DeleteBankingModel.Result, "OK");
                              }
                              IsRunning = false;
                          }
                      }
                  });

                BtnCancelClick = new Command(async () =>
                  {
                      IsRunning = true;
                      await App.Current.MainPage.Navigation.PushModalAsync(new MainPage(new PaymentMethod()));
                      IsRunning = false;
                  });

                BtnSaveClick = new Command(async () =>
                 {
                     var strConnection = CrossConnectivity.Current.IsConnected;
                     if (strConnection == true)
                     {
                         ValidatePaymentMethodFieldHandler();
                         if (ValidationStatus)
                         {   
                             AddNewPaymentMethodHandler();
                         }
                     }
                     else
                     {
                         await App.Current.MainPage.DisplayAlert("Error", AppResource.CrossConnectivityError, "OK");
                     }
                 });

              
                GetPaymentMethodList();
                GetMethodTypeList();
              
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                IsRunning = false;
            }
        }

        public async void GetPaymentMethodList()
        {
            try
            {
                IsRunning = true;
               // Get Payment details
              
                IsRunning = false;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                IsRunning = false;
            }
            
        }

        public void GetMethodTypeList()
        {
            try
            {
                //MethodTypeList.AddRange(PaymentMethodService.objmethod);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public void SetIsCheckOrSaving()
        {
            IsCheck = true;
            IsVisaVar = false;            
        }

        public void SetIsVisa()
        {
            IsCheck = false;
            IsVisaVar = true;
        }
    }
}
