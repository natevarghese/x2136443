using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using x2136443.Services;

namespace x2136443.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer) { }

        public override void OnCreate()
        {
            base.OnCreate();

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this);
        }
    }
}
