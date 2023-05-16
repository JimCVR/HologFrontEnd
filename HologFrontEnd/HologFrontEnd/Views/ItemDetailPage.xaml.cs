using Android.App;
using HologFrontEnd.Models;
using HologFrontEnd.Resources;
using HologFrontEnd.Services;
using Xamarin.Forms;

namespace HologFrontEnd.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemService restService;
        Item item;
        public ItemDetailPage(Item item)
        {
            InitializeComponent();
            //BindingContext = new ItemDetailViewModel();
            restService = new ItemService();
            this.item = item;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            showDetails();
        }


        private async void showDetails() {

            string uri = Constants.webServiceUri + Constants.user + Constants.userId + Constants.itemEndpoint+"/"+item.id;
            Item newItem = await restService.GetItemByIdAsync(uri);
            BindingContext = newItem;

        
        }


        private async void StatusChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton statusButton = (RadioButton)sender;
            if (statusButton.IsChecked)
            {
                if (statusButton.Value.Equals("pending"))
                {
                    
                    
                    await DisplayAlert("Alerta", "Boton " + statusButton.Value + " pulsado", "OK");
                }
                else if (statusButton.Value.Equals("inProgress"))
                {
                    
                    await DisplayAlert("Alerta", "Boton " + statusButton.Value + " pulsado", "OK");
                }
                else if (statusButton.Value.Equals("complete"))
                {
                    
                    await DisplayAlert("Alerta", "Boton " + statusButton.Value + " pulsado", "OK");
                }
            }
        }
    }
}