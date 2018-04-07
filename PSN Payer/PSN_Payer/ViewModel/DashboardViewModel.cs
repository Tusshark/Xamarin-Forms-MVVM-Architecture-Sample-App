using Plugin.Connectivity;
using PSN_Payer.Helper;
using PSN_Payer.Model;
using PSN_Payer.Resources;
using PSN_Payer.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSN_Payer.ViewModel
{
    public class DashboardViewModel : ObservableObject
    {

        #region Properties and command declarations

        public delegate Task LoginErrors(string errorMsg);
        public event LoginErrors LoginErrorsHandler;

        public ObservableCollection<MenuListItem> MenuList { get; set; }

        private string _headerImage;

        public string HeaderImage
        {
            get { return _headerImage; }
            set {
                _headerImage = value;
                OnPropertyChanged(nameof(HeaderImage));
            }
        }

        private string _boxViewBackground;

        public string BoxViewBackground
        {
            get { return _boxViewBackground; }
            set {
                _boxViewBackground = value;
                OnPropertyChanged(nameof(BoxViewBackground));
            }
        }

        private string _greetingMsg;

        public string GreetingMsg
        {
            get { return _greetingMsg; }
            set {
                _greetingMsg = value;
                OnPropertyChanged(nameof(GreetingMsg));
            }
        }

        private string _businessName;

        public string BusinessName
        {
            get { return _businessName; }
            set {
                _businessName = value;
                OnPropertyChanged(nameof(BusinessName));
            }
        }

        private bool _isRunnning;

        public bool IsRunning
        {
            get { return _isRunnning; }
            set {
                _isRunnning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }

        private string _balanceMsg;

        public string BalanceMsg
        {
            get { return _balanceMsg; }
            set {
                _balanceMsg = value;
                OnPropertyChanged(nameof(BalanceMsg));
            }
        }

        private Command _selectedMenuItem;

        public Command SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set {
                _selectedMenuItem = value;
                OnPropertyChanged(nameof(SelectedMenuItem));
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

        private string _sideMenuImage;

        public string SideMenuImage
        {
            get { return _sideMenuImage; }
            set {
                _sideMenuImage = value;
                OnPropertyChanged(nameof(SideMenuImage));
            }
        }

        private string _dashboardIcon;

        public string DashboardIcon
        {
            get { return _dashboardIcon; }
            set {
                _dashboardIcon = value;
                OnPropertyChanged(nameof(DashboardIcon));
            }
        }

        private Command _dashboardCommand;

        public Command DashboardCommand
        {
            get { return _dashboardCommand; }
            set {
                _dashboardCommand = value;
                OnPropertyChanged(nameof(DashboardCommand));
            }
        }


        #endregion

        public DashboardViewModel()
        {
            try
            {
                HeaderImage = AppResource.DashboardLogo; 
                BoxViewBackground = AppResource.BtnBackgroundColor;
                MenuList = new ObservableCollection<MenuListItem>();
                AppVersion = "v" + Common.AppVersion;
                SideMenuImage = AppResource.DashboardLogo;
                DashboardIcon = AppResource.DashboardIcon;
                Load();

                SelectedMenuItem = new Command(async (req) =>
                {
                   if(req.Equals("Logout"))
                    {
                        IsRunning = true;
                
                        Common.IsNavigation = false;
               
                        await App.Current.MainPage.Navigation.PushModalAsync(new Login());
                        IsRunning = false;
                    }
                   else if(req.Equals("Make Payment"))
                    {
                        IsRunning = true;
                        await App.Current.MainPage.Navigation.PushModalAsync(new MainPage(new MakePaymentEnterPaymentInfo()));
                        IsRunning = false;
                    }
                   else if(req.Equals("History"))
                    {
                        IsRunning = true;
                        await App.Current.MainPage.Navigation.PushModalAsync(new MainPage(new History()));
                        IsRunning = false;
                    }
                   else if(req.Equals("Settings"))
                    {
                        IsRunning = true;
                        await App.Current.MainPage.Navigation.PushModalAsync(new MainPage(new View.Setting()));
                        IsRunning = false;
                    }
                   else if(req.Equals("Payment Method"))
                    {
                        IsRunning = true;
                        await App.Current.MainPage.Navigation.PushModalAsync(new MainPage(new PaymentMethod()));
                        IsRunning = false;
                    }
                   else if(req.Equals("Bills"))
                    {
                        IsRunning = true;
                        await App.Current.MainPage.Navigation.PushModalAsync(new MainPage(new Bills()));
                        IsRunning = false;
                    }
                   else
                    {
                   
                        Common.IsNavigation = false;
                  
                        await App.Current.MainPage.Navigation.PushModalAsync(new Login());
                    }
                });

                DashboardCommand = new Command(async () =>
                  {
                      IsRunning = true;
                      await App.Current.MainPage.Navigation.PushModalAsync(new Dashboard());
                      IsRunning = false;
                  });

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }


        public async void Load()
        {
            await GetDashboardData();
        }

        public void Menu()
        {
            MenuList.Clear();

            MenuList.Add(new MenuListItem
            {
                Arrow = AppResource.MenuItemImageArrow,
                Icon = AppResource.MenuItemIconMakePayment,
                Name = "Make Payment",
                BalanceMsg = Common.BalanceValue,
                id = 1,
                SelectedMenuItem = SelectedMenuItem
            });
            MenuList.Add(new MenuListItem
            {
                Arrow = AppResource.MenuItemImageArrow,
                Icon = AppResource.MenuItemIconHistory,
                Name = "History",
                BalanceMsg = "",
                id = 2,
                SelectedMenuItem = SelectedMenuItem
            });
            MenuList.Add(new MenuListItem
            {
                Arrow = AppResource.MenuItemImageArrow,
                Icon = AppResource.MenuItemIconSettings,
                Name = "Settings",
                BalanceMsg = "",
                id = 3,
                SelectedMenuItem = SelectedMenuItem
            });
            MenuList.Add(new MenuListItem
            {
                Arrow = AppResource.MenuItemImageArrow,
                Icon = AppResource.MenuItemIconPaymentMethod,
                Name = "Payment Method",
                BalanceMsg = "",
                id = 4,
                SelectedMenuItem = SelectedMenuItem
            });
            MenuList.Add(new MenuListItem
            {
                Arrow = AppResource.MenuItemImageArrow,
                Icon = AppResource.MenuItemIconBills,
                Name = "Bills",
                BalanceMsg = "",
                id = 5,
                SelectedMenuItem = SelectedMenuItem
            });
            MenuList.Add(new MenuListItem
            {
                Arrow = "",
                Icon = AppResource.MenuItemIconLogout,
                Name = "Logout",
                BalanceMsg = "",
                id = 6,
                SelectedMenuItem = SelectedMenuItem
            });

            Common.MenuList = MenuList.AsEnumerable().ToList();
        }
        public async Task GetDashboardData()
        {
            try
            {
                var strConnection = CrossConnectivity.Current.IsConnected;
                if (strConnection == true)
                {
                    IsRunning = true;
                    Menu();
                    if(Common.IsNavigation)
                    {
                        // call API to get Home data

                        if (DateTime.Now.Hour < 12)
                        {
                            GreetingMsg = "Good Morning " + HomeModel.CustName;
                        }
                        else if (DateTime.Now.Hour < 17)
                        {
                            GreetingMsg = "Good Afternoon " + HomeModel.CustName;
                        }
                        else
                        {
                            GreetingMsg = "Good Evening " + HomeModel.CustName;
                        }
                 
                        Common.GreetingMsg = GreetingMsg;
                        Common.BusinessName = BusinessName;
                    }
                    else
                    {
                     
                       // Call API to get Payment methods

                    
                       // Call the API to get home data

              
                      // Call the API to get Payment Details


                        if (DateTime.Now.Hour < 12)
                        {
                            GreetingMsg = "Good Morning " + HomeModel.CustName;
                        }
                        else if (DateTime.Now.Hour < 17)
                        {
                            GreetingMsg = "Good Afternoon " + HomeModel.CustName;
                        }
                        else
                        {
                            GreetingMsg = "Good Evening " + HomeModel.CustName;
                        }

      

                        Common.BalanceValue = "Balance: " + HomeModel.Balance + " Balance as of: " + HomeModel.DueDate;
                        Common.GreetingMsg = GreetingMsg;
                        Common.BusinessName = BusinessName;
                    }
                    
                    IsRunning = false;
                }
                else
                {
                   await LoginErrorsHandler(AppResource.CrossConnectivityError);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            Menu();
        }
    }
}
