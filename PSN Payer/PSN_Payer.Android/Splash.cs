﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;

namespace PSN_Payer.Droid
{
    [Activity(Theme ="@style/Theme.Splash", MainLauncher =true, NoHistory =true)]
    public class Splash : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Thread.Sleep(1000); // Simulate a long loading process on app startup.
            StartActivity(typeof(MainActivity));
        }
    }
}