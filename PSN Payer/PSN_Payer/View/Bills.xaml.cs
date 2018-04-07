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
    public partial class Bills : ContentPage
    {
        public Bills()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, AppResource.DashboardIcon);
            BusyLoader obj = new BusyLoader();
            RelativeLayout objLayout = new RelativeLayout();
            objLayout = obj.CustomLoaderLayout(MainGrid);
            objLayout.Children[1].SetBinding(IsVisibleProperty, "IsRunning");
            Content = objLayout;
        }
    }
}