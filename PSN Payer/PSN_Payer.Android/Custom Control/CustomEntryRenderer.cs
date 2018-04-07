
using Xamarin.Forms.Platform.Android;
using PSN_Payer.Droid.Custom_Control;
using PSN_Payer.Custom_Control;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using Android.Content.Res;
using Android.Text;
using Android.Graphics;
using UIKit;

[assembly: ExportRenderer(typeof(CustomEntry),typeof(CustomEntryRenderer))]
namespace PSN_Payer.Droid.Custom_Control
{
    public class CustomEntryRenderer:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                //Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.White);
                gd.SetCornerRadius(25);
                gd.GetPadding(new Rect(3, 0, 0, 0));
                gd.SetStroke(2, Android.Graphics.Color.Rgb(153, 153, 153));
                this.Control.SetBackgroundDrawable(gd);
            }
        }
    }
}