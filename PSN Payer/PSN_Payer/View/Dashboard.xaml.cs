using PSN_Payer.Loader;
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
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
            BusyLoader obj = new BusyLoader();
            RelativeLayout objLayout = new RelativeLayout();
            objLayout = obj.CustomLoaderLayout(MainGrid);
            objLayout.Children[1].SetBinding(IsVisibleProperty, "IsRunning");
            Content = objLayout;
            VM.LoginErrorsHandler += VM_LoginErrorsHandler;
        }
       
        protected override void OnAppearing()
        {
            base.OnAppearing();
            headStack.Padding = new Thickness(0, -10, 0, 0);
        }

        private async Task VM_LoginErrorsHandler(string errorMsg)
        {
            await DisplayAlert("Error", errorMsg, "OK");
        }

    }
}