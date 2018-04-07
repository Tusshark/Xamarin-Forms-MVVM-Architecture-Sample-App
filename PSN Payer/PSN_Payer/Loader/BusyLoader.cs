using PSN_Payer.Model;
using PSN_Payer.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSN_Payer.Loader
{
    public class BusyLoader
    {
        /// <summary>
        /// Used for Activity Indicator mapping
        /// </summary>
        /// <param name="overlay"></param>
        /// <returns></returns>
        public RelativeLayout CustomLoaderLayout(Xamarin.Forms.View  MainGrid)
        {
            try
            {
                ActivityIndicator indicator = new ActivityIndicator();
                AbsoluteLayout overlay = new AbsoluteLayout();
                indicator.IsRunning = true;
                indicator.IsVisible = true;
                AbsoluteLayout.SetLayoutFlags(overlay, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(overlay, new Rectangle(5, 5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                AbsoluteLayout.SetLayoutFlags(indicator, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(indicator, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                overlay.Children.Add(indicator);
                overlay.IsVisible = true;
                overlay.WidthRequest = Common.ScreenWidth;
                overlay.HeightRequest = Common.ScreenHeight;
                overlay.BackgroundColor = Color.FromHex(AppResource.LoaderBackGround);
                overlay.Opacity = 0.3;
                RelativeLayout relativeLayout = new RelativeLayout();
                relativeLayout.Children.Add(
                           MainGrid,
                           Constraint.Constant(0),
                           Constraint.Constant(0),
                           Constraint.RelativeToParent((parent) => { return parent.Width; }),
                           Constraint.RelativeToParent((parent) => { return parent.Height; }));

                relativeLayout.Children.Add(
                   overlay,
                   Constraint.Constant(0),
                   Constraint.Constant(0),
                   Constraint.RelativeToParent((parent) => { return parent.Width; }),
                   Constraint.RelativeToParent((parent) => { return parent.Height; }));
                
                return relativeLayout;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace.ToString());
                return null;
            }
        }
    }
}
