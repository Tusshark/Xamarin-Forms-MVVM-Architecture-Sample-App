using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSN_Payer.Model;
using PSN_Payer.Helper;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Connectivity;
using System.Diagnostics;
using PSN_Payer.Resources;
using System.Net.Http;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace PSN_Payer.ViewModel
{
    public class LoginViewModel : ObservableObject
    {

        #region Properties and Command declartions
        public static bool IsRemember = false;
        public ICommand LoginCommand { get; set; }
        public ICommand SignUp { get; set; }

        public delegate bool InvalidCredentialsEvent(string userID,string password,bool remember);
        public event InvalidCredentialsEvent InvalidCredentialsHandler;

        public delegate void LoginErrors(string errorCode,string message);
        public event LoginErrors LoginErrorsHandler;

        public delegate void LoginResponse();
        public event LoginResponse LoginResponseHandler;

        private LoginModel model;

        public string UserID
        {
            get { return model.UserID; }
            set
            {
                SetProperty(ref model.UserID, value);
               ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public string Password
        {
            get { return model.Password; }
            set
            {
                SetProperty(ref model.Password, value);
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public bool Remember
        {
            get { return model.Remember; }
            set
            {
                SetProperty(ref model.Remember, value);
               ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        private bool _IsRunning;

        public bool IsRunning
        {
            get { return _IsRunning; }
            set {
                _IsRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }

        private string _appVersion;

        public string AppVersion
        {
            get { return _appVersion; }
            set {
                _appVersion = value;
                OnPropertyChanged(nameof(AppVersion));
            }
        }

        private string _loginButtonText;

        public string LoginButtonText
        {
            get { return _loginButtonText; }
            set {
                _loginButtonText = value;
                OnPropertyChanged(nameof(LoginButtonText));
            }
        }


        #endregion

        public LoginViewModel()
        {
            model = new LoginModel();
            AppVersion = "v" + Common.AppVersion;
            LoginButtonText = "LOGIN";
            LoginCommand = new Command(async () =>
            {
                if (InvalidCredentialsHandler(model.UserID, model.Password, model.Remember))
                {
                    var strConnection = CrossConnectivity.Current.IsConnected;
                    if (strConnection == true)
                    {
                        IsRunning = true;
                        LoginButtonText = "PLEASE WAIT...";
                        ((Command)LoginCommand).ChangeCanExecute();

                   // Call API to authenticate user
                       
                        if (!string.IsNullOrEmpty(LoginModel.ErrorCode) || !string.IsNullOrEmpty(LoginModel.Result))
                        {
                            if (LoginModel.ErrorCode.Equals("0"))
                            {
                                if (model.Remember)
                                {
                                    IsRemember = true;
                                    string saveText = "Email:" + model.UserID;
                                    await DependencyService.Get<IRemember>().SaveTextAsync("temp.txt", saveText);
                                }
                                else
                                {
                                    await DependencyService.Get<IRemember>().SaveTextAsync("temp.txt", null);
                                }
                                IsRunning = false;
                                LoginModel.OldPassword = Password;
                                LoginResponseHandler();

                                ((Command)LoginCommand).ChangeCanExecute();
                            }
                            else
                            {
                                IsRunning = false;
                                LoginErrorsHandler(LoginModel.ErrorCode, "");
                                LoginButtonText = "LOGIN";
                            }
                        }
                        else
                        {
                            IsRunning = false;
                            LoginErrorsHandler("", AppResource.LoginScreenInCorrectCredentials);
                            LoginButtonText = "LOGIN";
                        }
                    }
                    else
                    {
                        IsRunning = false;
                        LoginErrorsHandler("", AppResource.CrossConnectivityError);
                    }
                }
                else
                {

                }
            });

            SignUp = new Command(() =>
              {
                  try
                  {
                      var strConnection = CrossConnectivity.Current.IsConnected;
                      if(strConnection==true)
                      {
                          Device.OpenUri(new Uri(AppResource.SignUpURL));
                      }
                      else
                      {
                          IsRunning = false;
                          LoginErrorsHandler("", AppResource.CrossConnectivityError);
                      }
                  }
                  catch(Exception ex)
                  {
                      Debug.WriteLine(ex.ToString());
                  }
              });
        }


        public async Task<string> dataRead()
        {
            try
            {
                var loginInfo = await DependencyService.Get<IRemember>().GetTextAsync("temp.txt");
              string  remValue = loginInfo.ToString();

                if (!string.IsNullOrEmpty(remValue))
                {
                    string[] strsplit = Convert.ToString(remValue).Split(' ');

                    foreach (var item in strsplit)
                    {
                        string[] strSplitByName = item.Split(':');
                        if (strSplitByName[0] == "Email")
                        {
                            UserID = strSplitByName[1];
                            Remember = true;
                        }

                    }
                }
                return loginInfo.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return null;
            }

        }
    }
}
