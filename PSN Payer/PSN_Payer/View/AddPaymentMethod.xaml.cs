using PSN_Payer.Loader;
using PSN_Payer.Model;
using PSN_Payer.Resources;
using PSN_Payer.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSN_Payer.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPaymentMethod : ContentPage
    {
        public int checkVarInt = 0;
        public AddPaymentMethod()
        {
            InitializeComponent();
            BusyLoader obj = new BusyLoader();
            RelativeLayout objLayout = new RelativeLayout();
            objLayout = obj.CustomLoaderLayout(MainGrid);
            objLayout.Children[1].SetBinding(IsVisibleProperty, "IsRunning");
            Content = objLayout;
            VM.ValidatePaymentMethodFieldHandler += VM_ValidatePaymentMethodFieldHandler;
            VM.AddNewPaymentMethodHandler += VM_AddNewPaymentMethodHandler;
        }

        private void pickerMehodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(Convert.ToString(pickerMehodType.SelectedItem)))
                {
                    PaymentMethodInfo item = (PaymentMethodInfo)pickerMehodType.SelectedItem;
                    if (Convert.ToString(item.Type).ToLower().Equals(AppResource.IsCheck) || Convert.ToString(item.Type).ToLower().Equals(AppResource.IsSavings))
                    {
                        (new PaymentMethodViewModel()).SetIsCheckOrSaving();
                        stkNameOnCard.IsVisible = stkAcctNo.IsVisible = stkRoutingNo.IsVisible = stkNickName.IsVisible = PaymentMethodViewModel.IsCheck;
                        stkCardNo.IsVisible = stkExpDate.IsVisible = PaymentMethodViewModel.IsVisaVar;
                        txtNameOnCard.Placeholder = AppResource.LblNameOnCard.Replace("Card", item.Type);
                    }
                    else
                    {
                        (new PaymentMethodViewModel()).SetIsVisa();
                        stkNameOnCard.IsVisible = stkNickName.IsVisible =  stkCardNo.IsVisible = stkExpDate.IsVisible = PaymentMethodViewModel.IsVisaVar;
                        stkAcctNo.IsVisible = stkRoutingNo.IsVisible = PaymentMethodViewModel.IsCheck;
                        txtNameOnCard.Placeholder = AppResource.LblNameOnCard;
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void txtRoutingNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string routingNo = txtRoutingNo.Text;
                if (!string.IsNullOrEmpty(routingNo))
                {
                    if (int.TryParse(routingNo, out checkVarInt))
                    {
                        if (Convert.ToString(checkVarInt).Length > 9)
                        {
                            txtRoutingNo.Text = e.OldTextValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void txtCardNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string cardNo = txtCardNo.Text;
                if (!string.IsNullOrEmpty(cardNo))
                {
                    if (Convert.ToString(cardNo).Length > 16)
                    {
                        txtCardNo.Text = e.OldTextValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void txtNameOnCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string nameOnCard = txtNameOnCard.Text;
                if (!string.IsNullOrEmpty(nameOnCard))
                {
                    if (nameOnCard.Length > 50)
                    {
                        txtNameOnCard.Text = e.OldTextValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void txtNickName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string nickName = txtNickName.Text;
                if (!string.IsNullOrEmpty(nickName))
                {
                    if (nickName.Length > 50)
                    {
                        txtNickName.Text = e.OldTextValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void txtExpDateMonth_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string monthValue = txtExpDateMonth.Text;
                if (!string.IsNullOrEmpty(monthValue))
                {
                    if (int.TryParse(monthValue, out checkVarInt))
                    {
                        if (Convert.ToString(checkVarInt).Length > 2)
                        {
                            txtExpDateMonth.Text = e.OldTextValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void txtExpDateYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string yearValue = txtExpDateYear.Text;
                if (!string.IsNullOrEmpty(yearValue))
                {
                    if (int.TryParse(yearValue, out checkVarInt))
                    {
                        if (Convert.ToString(checkVarInt).Length > 4)
                        {
                            txtExpDateYear.Text = e.OldTextValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void txtAcctNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string acctNo = txtAcctNo.Text;
                if (!string.IsNullOrEmpty(acctNo))
                {
                    if (Convert.ToString(acctNo).Length > 16)
                    {
                        txtAcctNo.Text = e.OldTextValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void txtZipCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string zipCode = txtZipCode.Text;
                if (!string.IsNullOrEmpty(zipCode))
                {
                    if (int.TryParse(zipCode, out checkVarInt))
                    {
                        if (Convert.ToString(checkVarInt).Length > 6)
                        {
                            txtZipCode.Text = e.OldTextValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void VM_ValidatePaymentMethodFieldHandler()
        {
            try
            {
                if (pickerMehodType.SelectedIndex.Equals(-1))
                {
                    PaymentMethodViewModel.ValidationStatus = false;
                    await DisplayAlert("Warning", AppResource.PaymentMethodValMethodType, "OK");
                    return;
                }
                else if (string.IsNullOrEmpty(txtNameOnCard.Text) || txtNameOnCard.Text.Trim().Equals(""))
                {
                    PaymentMethodInfo item = (PaymentMethodInfo)pickerMehodType.SelectedItem;
                    if (Convert.ToString(item.Type).ToLower().Equals(AppResource.IsCheck) || Convert.ToString(item.Type).ToLower().Equals(AppResource.IsSavings))
                    {
                        PaymentMethodViewModel.ValidationStatus = false;
                        await DisplayAlert("Warning", AppResource.PaymentMethodValNameOn + " " + item.Type.ToLower(), "OK");
                    }
                    else
                    {
                        PaymentMethodViewModel.ValidationStatus = false;
                        await DisplayAlert("Warning", AppResource.PaymentMethodValNameOn + "card", "OK");
                    }
                    return;
                }
                else if (string.IsNullOrEmpty(txtNickName.Text) || txtNickName.Text.Trim().Equals(""))
                {
                    PaymentMethodViewModel.ValidationStatus = false;
                    await DisplayAlert("Warning", AppResource.PaymentMethodNickName, "OK");
                    return;
                }
                else
                {
                    PaymentMethodInfo item = (PaymentMethodInfo)pickerMehodType.SelectedItem;
                    if (Convert.ToString(item.Type).ToLower().Equals(AppResource.IsCheck) || Convert.ToString(item.Type).ToLower().Equals(AppResource.IsSavings))
                    {
                        if (string.IsNullOrEmpty(txtRoutingNo.Text) || txtRoutingNo.Text.Trim().Equals(""))
                        {
                            PaymentMethodViewModel.ValidationStatus = false;
                            await DisplayAlert("Warning", AppResource.PaymentMethodRoutingNo, "OK");
                            return;
                        }
                        else if (string.IsNullOrEmpty(txtAcctNo.Text) || txtAcctNo.Text.Trim().Equals(""))
                        {
                            PaymentMethodViewModel.ValidationStatus = false;
                            await DisplayAlert("Warning", AppResource.PaymentMethodAcctNo, "OK");
                            return;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtCardNo.Text)||txtCardNo.Text.Trim().Equals(""))
                        {
                            PaymentMethodViewModel.ValidationStatus = false;
                            await DisplayAlert("Warning", AppResource.PaymentMethodCardNo, "OK");                            
                            return;
                        }
                        else if (txtCardNo.Text.Trim().Length < 16)
                        {
                            PaymentMethodViewModel.ValidationStatus = false;
                            await DisplayAlert("Warning", AppResource.PaymentMethodValCardNo, "OK");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtExpDateMonth.Text)||txtExpDateMonth.Text.Trim().Equals(""))
                        {
                            PaymentMethodViewModel.ValidationStatus = false;
                            await DisplayAlert("Warning", AppResource.PaymentMethodMonth, "OK");
                            return;
                        }
                        else if ((Convert.ToInt32(txtExpDateMonth.Text) < 1) || (Convert.ToInt32(txtExpDateMonth.Text) > 12))
                        {
                            PaymentMethodViewModel.ValidationStatus = false;
                            await DisplayAlert("Warning", AppResource.PaymentMethodMonth1T12, "OK");                            
                            return;
                        }
                        if (string.IsNullOrEmpty(txtExpDateYear.Text)||txtExpDateYear.Text.Trim().Equals(""))
                        {
                            await DisplayAlert("Warning", AppResource.PaymentMethodYear, "OK");
                            PaymentMethodViewModel.ValidationStatus = false;
                            return;
                        }
                        else if ((DateTime.Now.Year > Convert.ToInt32(txtExpDateYear.Text)) || (2099 < Convert.ToInt32(txtExpDateYear.Text)))
                        {
                            PaymentMethodViewModel.ValidationStatus = false;
                            await DisplayAlert("Warning", AppResource.PaymentMethodValYear, "OK");                            
                            return;
                        }
                        if (DateTime.Now.Year == Convert.ToInt32(txtExpDateYear.Text) && DateTime.Now.Month > Convert.ToInt32(txtExpDateMonth.Text))
                        {
                            PaymentMethodViewModel.ValidationStatus = false;
                            await DisplayAlert("Warning", AppResource.PaymentMethodValDate, "OK");
                            return;
                        }
                        else if (string.IsNullOrEmpty(txtZipCode.Text)||txtZipCode.Text.Trim().Equals(""))
                        {
                            PaymentMethodViewModel.ValidationStatus = false;
                            await DisplayAlert("Warning", AppResource.PaymentMethodZip, "OK");
                            return;
                        }
                    }
                }
                PaymentMethodViewModel.ValidationStatus = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void txtExpDateYear_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                int resultYear = 0, resultMonth = 0;
                if (txtExpDateYear.Text.Length == 4)
                {
                    if (int.TryParse(txtExpDateYear.Text, out resultYear))
                    {
                        int currentYear = System.DateTime.Now.Year;
                        int currentMonth = System.DateTime.Now.Month;

                        if (currentYear == resultYear)
                        {
                            if (!string.IsNullOrEmpty(txtExpDateMonth.Text) && int.TryParse(txtExpDateMonth.Text, out resultMonth) && resultYear == currentYear)
                            {
                                if (resultMonth >= currentMonth)
                                {
                                    UpdateMonthValue();
                                }
                                else
                                {
                                    await DisplayAlert("Error", AppResource.DateNotbeinPast, "OK");
                                    txtExpDateMonth.Text = "";
                                }
                            }
                        }
                        else if (resultYear > currentYear)
                        {
                            UpdateMonthValue();
                        }
                        else if (resultYear < currentYear)
                        {
                            UpdateMonthValue();
                            await DisplayAlert("Error", AppResource.DateNotbeinPast, "OK");
                            txtExpDateYear.Text = "";
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", AppResource.DateNotbeinPast, "OK");
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public void UpdateMonthValue()
        {
            try
            {
                string mnthVal = txtExpDateMonth.Text.TrimStart('0');
                if (!string.IsNullOrEmpty(mnthVal))
                {
                    switch (mnthVal)
                    {
                        case "1":
                            txtExpDateMonth.Text = "01";
                            break;
                        case "2":
                            txtExpDateMonth.Text = "02";
                            break;
                        case "3":
                            txtExpDateMonth.Text = "03";
                            break;
                        case "4":
                            txtExpDateMonth.Text = "04";
                            break;
                        case "5":
                            txtExpDateMonth.Text = "05";
                            break;
                        case "6":
                            txtExpDateMonth.Text = "06";
                            break;
                        case "7":
                            txtExpDateMonth.Text = "07";
                            break;
                        case "8":
                            txtExpDateMonth.Text = "08";
                            break;
                        case "9":
                            txtExpDateMonth.Text = "09";
                            break;
                        case "10":
                            txtExpDateMonth.Text = "10";
                            break;
                        case "11":
                            txtExpDateMonth.Text = "11";
                            break;
                        case "12":
                            txtExpDateMonth.Text = "12";
                            break;
                        default:
                            txtExpDateMonth.Text = "01";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public bool checkCardNumberWithDropdownSelection()
        {
            try
            {
                int accountType = pickerMehodType.SelectedIndex + 1;
                if (!string.IsNullOrEmpty(txtCardNo.Text))
                {
                    string firstchar = txtCardNo.Text.Substring(0, 1).ToString();
                    if (accountType == 3)
                    {
                        if (firstchar == "4")
                            return true;
                        else
                            return false;
                    }
                    else if (accountType == 4)
                    {

                        if (firstchar == "5")
                            return true;
                        else
                            return false;
                    }
                    else if (accountType == 5)
                    {

                        if (firstchar == "6")
                            return true;
                        else
                            return false;
                    }
                    else if (accountType == 6)
                    {
                        if (firstchar == "3")
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    if (accountType == 1 || accountType == 2)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }


        private async void VM_AddNewPaymentMethodHandler()
        {
            try
            {
                bool flagcheckCardNumber = checkCardNumberWithDropdownSelection();
                if (flagcheckCardNumber == false)
                {
                    await DisplayAlert("Warning", AppResource.CardNoNotMatched, "OK");
                    return;
                }
                VM.IsRunning = true;
                PaymentMethodInfo item = (PaymentMethodInfo)pickerMehodType.SelectedItem;
             
                if (PaymentMethodModel.ErrNum == "0")
                {
                    await DisplayAlert("Result", AppResource.PaymentMethodAdded, "OK");
                    await Navigation.PushModalAsync(new MainPage(new PaymentMethod()));
                }
                else
                {
                    await DisplayAlert("Error", PaymentMethodModel.Result, "OK");
                }
                VM.IsRunning = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                VM.IsRunning = false;
            }
        }
    }
}