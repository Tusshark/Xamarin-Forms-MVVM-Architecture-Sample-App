using Plugin.Connectivity;
using PSN_Payer.Loader;
using PSN_Payer.Model;
using PSN_Payer.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSN_Payer.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MakePaymentEnterPaymentInfo : ContentPage
    {
        public static string accountType = string.Empty;
        public static bool NSFStatus = false;
        public static string strNSF = string.Empty;
        public static string FeeValue = string.Empty;
        public static string strAccountType = string.Empty;
        public static string strAccountNumber = string.Empty;
        public static string strBankId = string.Empty;
        public static string strPaymentAmt = string.Empty;
        public static string strDate = Convert.ToString(DateTime.Now.Date.ToString(AppResource.DateFormat));
        public MakePaymentEnterPaymentInfo()
        {
            InitializeComponent();
            //headerLogo.Text = "Make Payment";
            //headerSpaces.Text = Common.CalculatedSpaceForNav();
            NavigationPage.SetTitleIcon(this, AppResource.DashboardIcon);
            BusyLoader obj = new BusyLoader();
            RelativeLayout objLayout = new RelativeLayout();
            objLayout = obj.CustomLoaderLayout(MainGrid);
            objLayout.Children[1].SetBinding(IsVisibleProperty, "IsRunning");
            Content = objLayout;
        }

        private void txtPaymentDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(e.NewDate)))
                {
                    VM.CurrentDate = Convert.ToString(e.NewDate.Date.ToString(AppResource.DateFormat));
                    strDate = VM.CurrentDate;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void txtPymntAmt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string valAmt = string.Empty;
                if (!string.IsNullOrEmpty(txtPymntAmt.Text) && txtPymntAmt.Text != "$")
                {
                    valAmt = txtPymntAmt.Text.Replace("$", "");
                }
                else
                {
                    valAmt = txtPymntAmt.Text;
                    lblConvnFeeValue.Text = "$0.00";
                    lblTotalFee.Text = "$0.00";
                }
                if (!string.IsNullOrEmpty(valAmt))
                {
                    if (valAmt.Length > 10)
                    {
                        valAmt = e.OldTextValue;
                        txtPymntAmt.Text = valAmt;
                    }
                }
                if (!string.IsNullOrEmpty(txtPymntAmt.Text))
                {
                    Match match= Regex.Match(txtPymntAmt.Text.Replace("$", ""), @"^\d+(?:\.\d{0,2})?$");
                    if (!match.Success && !string.IsNullOrEmpty(txtPymntAmt.Text.Replace("$", "")))
                    {
                        valAmt = e.OldTextValue;
                        txtPymntAmt.Text = valAmt;
                    }
                }
                VM.PaymentAmount = txtPymntAmt.Text;
                strPaymentAmt = VM.PaymentAmount;
                if (pckMethodType.SelectedIndex != -1)
                {
                    string accountType = pckMethodType.Items[pckMethodType.SelectedIndex];
                    string[] strSplitByName = accountType.Split(':');
                    accountType = strSplitByName[0];
                    string[] arr = { };
                    foreach (var method in VM.MethodTypeList)
                    {
                        arr = method.Type.ToString().Split(':');
                        if (accountType == arr[1])
                        {
                            break;
                        }
                    }
                    // FUNCTION CALLED FOR CALCULATION OF LOW FEE
                    //LowFeeCalculationMethod(Convert.ToString(arr[0]));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void txtPymntAmt_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                String valAmount = txtPymntAmt.Text;
                if (!string.IsNullOrEmpty(txtPymntAmt.Text))
                {
                    string chkAmt = txtPymntAmt.Text.Replace("$", "");
                    if (!chkAmt.Contains("$"))
                    {
                        txtPymntAmt.Text = txtPymntAmt.Text = "$" + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDouble(chkAmt)));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void pckMethodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pckMethodType.IsEnabled = false;
                if (pckMethodType.SelectedIndex == -1) { }
                else
                {
                    accountType = pckMethodType.Items[pckMethodType.SelectedIndex];
                    string[] strSplitByName = accountType.Split(':');
                    accountType = strSplitByName[0];
                    strAccountType = pckMethodType.Items[pckMethodType.SelectedIndex];
                    string[] arr = { };

                    foreach (var method in VM.MethodTypeList)
                    {
                        arr = method.Type.ToString().Split(':');
                        if (arr[0] == "CHECK" || arr[0] == "SAVINGS")
                        {
                            NSFStatus = true;
                        }
                        else
                        {
                            NSFStatus = false;
                        }

                        if (accountType == arr[1])
                        {
                            break;
                        }
                    }

                    //foreach (var item in PaymentMethodService.objmethod)
                    //{
                    //    if (item.Type == arr[0])
                    //    {
                    //        if (!string.IsNullOrEmpty(item.NSF))
                    //        {
                    //            string[] arrnsf = { };
                    //            arrnsf = item.NSF.Split('$');
                    //            arrnsf = arrnsf[1].Split(' ');
                    //            strNSF = arrnsf[0].Replace("$", "");
                    //            break;
                    //        }
                    //    }
                    //}

                    // FUNCTION CALLED FOR CALCULATION OF LOW FEE
                    //LowFeeCalculationMethod(Convert.ToString(arr[0]));
                }
                pckMethodType.IsEnabled = true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void btnContinue_Clicked(object sender, EventArgs e)
        {
            btnContinue.IsEnabled = false;
            btnContinue.Text = "Please Wait...";
            //foreach (var item in MakePaymentService.listPaymentDetailMethod)
            //{
            //    if (item.AccountNickname + "(" + item.AccountNumber + ")" == MakePaymentEnterPaymentInfo.strAccountType)
            //    {
            //        strAccountNumber = item.AccountNumber;
            //        strBankId = item.BankingID;
            //        break;
            //    }
            //}
            var strConnection = CrossConnectivity.Current.IsConnected;
            if (strConnection == true)
            {
                if (pckMethodType.SelectedIndex == -1)
                {
                    await DisplayAlert("Warning", AppResource.PaymentMethodValMethodType, "OK");
                    btnContinue.IsEnabled = true; btnContinue.Text = "Continue";
                    return;
                }
                else if (string.IsNullOrEmpty(VM.CurrentDate))
                {
                    await DisplayAlert("Warning", AppResource.MakePaymentReq2, "OK");
                    btnContinue.IsEnabled = true; btnContinue.Text = "Continue";
                    return;
                }
                else if (string.IsNullOrEmpty(txtPymntAmt.Text))
                {
                    await DisplayAlert("Warning", AppResource.MakePaymentReq1, "OK");
                    btnContinue.IsEnabled = true; btnContinue.Text = "Continue";
                    return;
                }

                string[] arr = { };
                foreach (var method in VM.MethodTypeList)
                {
                    arr = method.Type.ToString().Split(':');
                    if (accountType == arr[1])
                    {
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(accountType))
                {
                    if (arr[0] == "CHECK" || arr[0] == "SAVINGS")
                    {
                        string strMinCheck = MakePaymentModel.MinCheck;
                        string strMaxCheck = MakePaymentModel.MaxCheck;
                        if (!String.IsNullOrEmpty(txtPymntAmt.Text))
                        {
                            if ((Convert.ToDecimal(txtPymntAmt.Text.Replace("$", "")) >= Convert.ToDecimal(strMinCheck) && Convert.ToDecimal(txtPymntAmt.Text.Replace("$", "")) <= Convert.ToDecimal(strMaxCheck)) == false)
                            {
                                string strmsg = AppResource.MakePaymentVal1Msg + " $" + MakePaymentModel.MinCheck + " and $" + MakePaymentModel.MaxCheck;
                                await DisplayAlert("Warning", strmsg, "OK");
                                btnContinue.IsEnabled = true; btnContinue.Text = "Continue";
                                return;
                            }
                        }
                    }
                    else
                    {
                        string strMinCheck1 = MakePaymentModel.MinCredit;
                        string strMaxCheck1 = MakePaymentModel.MaxCredit;
                        if (!String.IsNullOrEmpty(txtPymntAmt.Text))
                        {
                            if ((Convert.ToDecimal(txtPymntAmt.Text.Replace("$", "")) >= Convert.ToDecimal(strMinCheck1) && Convert.ToDecimal(txtPymntAmt.Text.Replace("$", "")) <= Convert.ToDecimal(strMaxCheck1)) == false)
                            {
                                string strmsg = AppResource.MakePaymentVal1Msg + " $" + MakePaymentModel.MinCredit + " and $" + MakePaymentModel.MaxCredit;
                                await DisplayAlert("Warning", strmsg, "OK");
                                btnContinue.IsEnabled = true; btnContinue.Text = "Continue";
                                return;
                            }
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Warning", AppResource.PaymentMethodValMethodType, "OK");
                    btnContinue.IsEnabled = true; btnContinue.Text = "Continue";
                    return;
                }

                VM.ConfirmPaymentIcon = AppResource.ConfPayFilled;
                VM.ConfirmPaymentVisibility = true;
                VM.PaymentInfoVisibility = false;
                //VM.PayingToValue = MakePaymentService.bname;
                VM.PaymentMethodValue = strAccountType.Split('(').ToArray().ElementAt(0) + "********" + strAccountNumber;
                VM.BalanceDueValue = HomeModel.Balance;
                VM.ConvenienceFeeValue = lblConvnFeeValue.Text;
                VM.PaymentAmountValue = txtPymntAmt.Text;
                VM.PaymentDateValue = VM.CurrentDate;
                VM.TotalFeeValue = lblTotalFee.Text;
                btnEditInfo.BackgroundColor = Color.FromHex(AppResource.BtnBackgroundColor);
                btnProcessPayment.BackgroundColor = Color.FromHex(AppResource.BtnBackgroundColor);
                btnContinue.IsEnabled = true; btnContinue.Text = "Continue";
                btnProcessPayment.Text = NSFStatus == true ? "Continue" : "Process Payment";

            }
            else
            {
                await DisplayAlert("Warning", AppResource.CrossConnectivityError, "OK");
            }
        }

        private async void btnProcessPayment_Clicked(object sender, EventArgs e)
        {
            try
            {
                var strConnection = CrossConnectivity.Current.IsConnected;
                if (strConnection == true)
                { 
                    bool obj = true;
                    if (!btnProcessPayment.Text.ToLower().Equals("process payment"))
                    {
                        if (NSFStatus == true)
                        {
                            VM.IsRunning = true;
                            obj = await DisplayAlert("Confirmation", AppResource.CheckConfirmMsg, "OK", "Cancel");
                            btnProcessPayment.IsEnabled = true;
                            VM.IsRunning = false;
                        }
                       if (obj == true) { btnProcessPayment.Text = "Process Payment"; return; }
                    }
                    
                    if (btnProcessPayment.Text.ToLower().Equals("process payment"))
                    {
                        btnProcessPayment.IsEnabled = false;
                        btnEditInfo.IsEnabled = false;
                        NavigationPage.SetHasNavigationBar(this, false);
                        VM.IsRunning = true;
                      //  MakePaymentService objMakePayment = new MakePaymentService();
                        //await objMakePayment.ProcessPayment();                      
                        if (ProcessPaymentModel.Result.Replace(" ", "").ToLower().Equals(AppResource.PaymentMethodIncorrectDefinition))
                        {
                            await DisplayAlert("Error", ProcessPaymentModel.Result, "OK");
                            btnEditInfo.IsEnabled = true;
                            btnProcessPayment.IsEnabled = true;
                            NavigationPage.SetHasNavigationBar(this, true);
                        }
                        else
                        {
                            btnEditInfo.IsEnabled = false;
                            btnProcessPayment.IsEnabled = false;
                            VM.SetToShowPaymentReceiptSection();
                            NavigationPage.SetHasNavigationBar(this, true);
                        }
                        VM.IsRunning = false;
                    }
                }
                else
                {
                    await DisplayAlert("Warning", AppResource.CrossConnectivityError, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}