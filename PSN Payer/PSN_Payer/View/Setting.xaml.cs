using PSN_Payer.Loader;
using PSN_Payer.Model;
using PSN_Payer.Resources;
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
    public partial class Setting : ContentPage
    {
        public Setting()
        {
            InitializeComponent();
            //headerLogo.Icon = AppResource.HeaderNavLogo;
            //headerSpaces.Text = Common.CalculatedSpaceForNav();
            BusyLoader obj = new BusyLoader();
            RelativeLayout objLayout = new RelativeLayout();
            objLayout = obj.CustomLoaderLayout(MainGrid);
            objLayout.Children[1].SetBinding(IsVisibleProperty, "IsRunning");
            Content = objLayout;
            NavigationPage.SetTitleIcon(this, AppResource.DashboardIcon);
            VM.ChangePasswordErrorsHandler += VM_ChangePasswordErrorsHandler;
            VM.InvalidEntriesHandler += VM_InvalidEntriesHandler;
            VM.ChangePasswordResponseHandler += VM_ChangePasswordResponseHandler;
        }

        private async void VM_ChangePasswordResponseHandler()
        {
            await DisplayAlert("Success", AppResource.SettingPassChanged, "OK");
            await Navigation.PushModalAsync(new Dashboard());
        }

        private  bool VM_InvalidEntriesHandler(string oldPassword, string newPassword, string confirmPassword)
        {
            bool status = false;
            try
            {
                if(!string.IsNullOrEmpty(oldPassword))
                {
                    if(!string.IsNullOrEmpty(newPassword))
                    {
                        if(!string.IsNullOrEmpty(confirmPassword))
                        {
                            if(LoginModel.OldPassword.Equals(oldPassword))
                            {
                                if (confirmPassword.ToLower().Equals(newPassword.ToLower()))
                                {
                                    status = true;
                                }
                                else
                                {
                                    DisplayAlert("Alert", AppResource.SettingPassDontMatch, "OK");
                                }
                            }
                            else
                            {
                                DisplayAlert("Alert", AppResource.SettingCorrectOldPassError, "OK");
                            }
                        }
                        else
                        {
                             DisplayAlert("Alert", AppResource.SettingConfirmPassError,"OK");
                        }
                    }
                    else
                    {
                         DisplayAlert("Alert", AppResource.SettingNewPassError, "OK");
                    }
                }
                else
                {
                     DisplayAlert("Alert", AppResource.SettingOldPassError, "OK");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            return status;
        }

        private void VM_ChangePasswordErrorsHandler(string errorCode, string message)
        {
            throw new NotImplementedException();
        }
    }
}