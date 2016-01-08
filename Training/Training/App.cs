using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using XLabs.Forms.Mvvm;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Mvvm;
using Training.ViewModel;
using Training.View;
using XLabs.Forms.Services;
using XLabs.Platform.Services;
using Xamarin.Forms;

namespace Training
{
    public class App : Application
    {
        public App()
        {
            var app = Resolver.Resolve<IXFormsApp>();
            if (app == null)
            {
                return;
            }

            app.Closing += (o, e) => Debug.WriteLine("Application Closing");
            app.Error += (o, e) => Debug.WriteLine("Application Error");
            app.Initialize += (o, e) => Debug.WriteLine("Application Initialized");
            app.Resumed += (o, e) => Debug.WriteLine("Application Resumed");
            app.Rotation += (o, e) => Debug.WriteLine("Application Rotated");
            app.Startup += (o, e) => Debug.WriteLine("Application Startup");
            app.Suspended += (o, e) => Debug.WriteLine("Application Suspended");

            ViewFactory.Register<MainView, MainViewModel>();
            ViewFactory.Register<ScanView, ScanViewModel>();
            ViewFactory.Register<MapView, MapViewModel>();

            var mainPage = (Page)ViewFactory.CreatePage(typeof(MainViewModel));

            Resolver.Resolve<IDependencyContainer>()
                .Register<INavigationService>(t => new NavigationService(mainPage.Navigation));

            MainPage = new NavigationPage(mainPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
