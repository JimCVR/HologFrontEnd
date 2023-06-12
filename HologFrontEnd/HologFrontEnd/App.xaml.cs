using HologFrontEnd.Views;
using Plugin.DeviceInfo;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HologFrontEnd
{
    public partial class App : Application
    {
        public static string DeviceId = CrossDeviceInfo.Current.Id;
        public static NetworkAccess connectivity = Connectivity.NetworkAccess;
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
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

        public static bool CheckConnection()
        {
            SetConnection();
            return connectivity == NetworkAccess.Internet;
        }

        public static void SetConnection()
        {
            connectivity = Connectivity.NetworkAccess;
        }  
    }
}
