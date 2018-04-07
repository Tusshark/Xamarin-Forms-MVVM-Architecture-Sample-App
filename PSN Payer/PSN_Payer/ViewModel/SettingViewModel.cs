using Plugin.Connectivity;
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
using Xamarin.Forms;

namespace PSN_Payer.ViewModel
{
    public class SettingViewModel : ObservableObject
    {

        #region Properties and Event declaration

        private Setting model;

        public delegate void ChangePasswordErrors(string errorCode, string message);
        public event ChangePasswordErrors ChangePasswordErrorsHandler;

        public delegate bool InvalidEntryEvent(string oldPassword, string newPassword, string confirmPassword);
        public event InvalidEntryEvent InvalidEntriesHandler;

        public delegate void ChangePasswordResponse();
        public event ChangePasswordResponse ChangePasswordResponseHandler;

        private string _passwordWarning;

        public string PasswordWarning
        {
            get { return _passwordWarning; }
            set {
                _passwordWarning = value;
                OnPropertyChanged(nameof(PasswordWarning));
            }
        }

        private string _buttonBackground;

        public string ButtonBackground
        {
            get { return _buttonBackground; }
            set {
                _buttonBackground = value;
                OnPropertyChanged(nameof(ButtonBackground));
            }
        }

        private Command _changePassword;

        public Command ChangePassword
        {
            get { return _changePassword; }
            set {
                _changePassword = value;
                OnPropertyChanged(nameof(ChangePassword));
            }
        }

        private string _oldPassword;

        public string OldPassword
        {
            get { return model.OldPassword; }
            set {
                SetProperty(ref model.OldPassword, value);
                ((Command)ChangePassword).ChangeCanExecute();
            }
        }

        private string _newPassword;

        public string NewPassword
        {
            get { return model.NewPassword; }
            set
            {
                SetProperty(ref model.NewPassword, value);
                ((Command)ChangePassword).ChangeCanExecute();
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return model.ConfirmPassword; }
            set
            {
                SetProperty(ref model.ConfirmPassword, value);
                ((Command)ChangePassword).ChangeCanExecute();
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

        private string _submitButtonText;

        public string SubmitButtonText
        {
            get { return _submitButtonText; }
            set {
                _submitButtonText = value;
                OnPropertyChanged(nameof(SubmitButtonText));
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

        public SettingViewModel()
        {
            try
            {
                model = new Setting();
                ButtonBackground = AppResource.BtnBackgroundColor;
                SubmitButtonText = "Submit";
                PasswordWarning = AppResource.SettingPassWarning;
                MainHeading = AppResource.FilledMandatory;
                ChangePassword = new Command( () =>
                {
                    if (InvalidEntriesHandler(model.OldPassword, model.NewPassword, model.ConfirmPassword))
                    {
                        var strConnection = CrossConnectivity.Current.IsConnected;
                        if (strConnection == true)
                        {
                            IsRunning = true;
                            SubmitButtonText = "Please wait...";
                            // call Api to set New password

                            if (!string.IsNullOrEmpty(Setting.Result) || !string.IsNullOrEmpty(Setting.ErrNum))
                            {
                                IsRunning = false;
                                ChangePasswordResponseHandler();
                                LoginModel.OldPassword = model.OldPassword;
                            }
                            else
                            {
                                IsRunning = false;
                                ChangePasswordErrorsHandler("", Setting.Result);
                            }
                        }
                        else
                        {
                            IsRunning = false;
                            ChangePasswordErrorsHandler("", AppResource.CrossConnectivityError);
                        }
                    }
                    else
                    {

                    }
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

    }
}
