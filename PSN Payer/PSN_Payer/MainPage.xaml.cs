using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PSN_Payer.View;
using PSN_Payer.Resources;
using System.Diagnostics;

namespace PSN_Payer
{
    public partial class MainPage : MasterDetailPage
    {
        Master masterPage;
        public MainPage(Page page)
        {
            try
            {
                InitializeComponent();
                NavigationPage.SetTitleIcon(this, AppResource.DashboardIcon);
                masterPage = new View.Master();
                masterPage.Title = "Menu";
                masterPage.Icon = "appicon.png";
                Master = masterPage;
                var navpage = new NavigationPage(page);
                navpage.BarBackgroundColor = Color.FromHex(AppResource.BtnBackgroundColor);
                navpage.BarTextColor = Color.White;

                Detail = navpage;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            
        }
    }
}
