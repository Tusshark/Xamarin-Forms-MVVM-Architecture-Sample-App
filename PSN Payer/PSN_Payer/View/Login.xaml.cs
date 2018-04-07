using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSN_Payer.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PSN_Payer.Model;
using PSN_Payer.ViewModel;
using PSN_Payer.Loader;
using System.Diagnostics;

namespace PSN_Payer.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            try
            {
                InitializeComponent();
                BusyLoader obj = new BusyLoader();
                RelativeLayout objLayout = new RelativeLayout();
                objLayout = obj.CustomLoaderLayout(MainGrid);
                objLayout.Children[1].SetBinding(IsVisibleProperty, "IsRunning");
                Content = objLayout;
                VM.InvalidCredentialsHandler += VM_InvalidCredentialsHandler;
                VM.LoginResponseHandler += VM_LoginResponseHandler;
                VM.LoginErrorsHandler += VM_LoginErrorsHandler;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
            {
                this.BackgroundImage = AppResource.LoginScreenBackgroundPortrait;
                MainStack.Padding = new Thickness(10, 0, 10, 0);
                Task<string> obj = VM.dataRead();
            }
        }

        private async void VM_LoginErrorsHandler(string errorCode,string message)
        {
            if (!string.IsNullOrEmpty(errorCode))
            {
                if(errorCode.Equals("101"))
                {
                    await DisplayAlert("Error", AppResource.LoginError101, "OK");
                }
                else if(errorCode.Equals("102"))
                {
                    await DisplayAlert("Error", AppResource.LoginError102, "OK");
                }
                else if(errorCode.Equals("150"))
                {
                    await DisplayAlert("Error", AppResource.LoginError150, "OK");
                }
                else if(errorCode.Equals("195"))
                {
                    await DisplayAlert("Error", AppResource.LoginError195, "OK");
                }
                else if(errorCode.Equals("199"))
                {
                    await DisplayAlert("Error", AppResource.LoginError199, "OK");
                }
                else
                {
                    await DisplayAlert("Error", AppResource.LoginScreenInCorrectCredentials, "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", message, "OK");
            }
        }

        private async void VM_LoginResponseHandler()
        {
            if(LoginViewModel.IsRemember)
            {
                await Navigation.PushModalAsync(new Dashboard());
            }
            else
            { }
        }

        private bool VM_InvalidCredentialsHandler(string userID, string password, bool remember)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(userID))
            {
                if (string.IsNullOrEmpty(password))
                {
                     DisplayAlert("Alert", AppResource.ReqMsgLoginPassword, "OK");
                }
                else
                {
                    status = true;
                }
            }
            else if(string.IsNullOrEmpty(userID))
            {
                 DisplayAlert("Alert", AppResource.ReqMsgLoginUserID, "OK");
            }
            else
            {
                status = true;
            }
            return status;
        }

    }
}