using HologFrontEnd.Views;
using Plugin.DeviceInfo;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HologFrontEnd
{
    public partial class App : Application
    {
        public static string DeviceId = CrossDeviceInfo.Current.Id;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
