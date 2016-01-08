using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Mvvm;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Training.Droid
{
    [Activity(Label = "Training", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            global::Xamarin.FormsMaps.Init(this, bundle);

            SetIoc();

            LoadApplication(new App());
        }

        private void SetIoc()
        {
            var resolverContainer = new SimpleContainer();

            var app = new XFormsAppDroid();

            resolverContainer.Register(t => AndroidDevice.CurrentDevice)
                .Register<IDependencyContainer>(t => resolverContainer)
                .Register<IXFormsApp>(app);

            Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }
}

