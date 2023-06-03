using Android.App;
using Xamarin.Forms.Xaml;

[assembly: Application(UsesCleartextTraffic = true)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]