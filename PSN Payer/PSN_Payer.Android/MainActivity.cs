using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PSN_Payer.Model;

namespace PSN_Payer.Droid
{
    [Activity(Label = "PSN_Payer", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            var metrics = Resources.DisplayMetrics;
            Common.DeviceOS = Build.Brand;
            Common.DeviceSDK = Build.VERSION.Sdk;
            Common.Brand = Build.Brand;
            Common.Device = Build.Device;
            Common.Serial = Build.Serial;
            Common.Hardware = Build.Hardware;
            Common.ScreenHeight = ConvertPixelsToDp(metrics.HeightPixels);
            Common.ScreenWidth = ConvertPixelsToDp(metrics.WidthPixels);
            Common.Model = Build.Model;
            Common.AppVersion = Convert.ToString(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
        private int ConvertPixelsToDp(float pixelValue)
        {
            var objDP = (int)(pixelValue / Resources.DisplayMetrics.Density);
            return objDP;
        }

        public override void OnBackPressed()
        {
            return;
        }

    }
}

