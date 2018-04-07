using PSN_Payer.Loader;
using PSN_Payer.Model;
using PSN_Payer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSN_Payer.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage
    {
        public Master()
        {
            Common.IsNavigation = true;
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, AppResource.DashboardIcon);
            stackHome.BackgroundColor = Color.FromHex(AppResource.LoaderBackGround);
            BusyLoader obj = new BusyLoader();
            RelativeLayout objLayout = new RelativeLayout();
            objLayout = obj.CustomLoaderLayout(MainGrid);
            objLayout.Children[1].SetBinding(IsVisibleProperty, "IsRunning");
            Content = objLayout;
            VM.LoginErrorsHandler += VM_LoginErrorsHandler;
        }
        private async Task VM_LoginErrorsHandler(string errorMsg)
        {
            await DisplayAlert("Error", errorMsg, "OK");
        }
    }
}