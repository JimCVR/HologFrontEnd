using Android.Widget;
using HologFrontEnd.Models;
using HologFrontEnd.Resources;
using HologFrontEnd.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Content.ClipData;
using Item = HologFrontEnd.Models.Item;

namespace HologFrontEnd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItem : ContentPage
    {
        ItemService itemService;
        Item item;
        public NewItem(Item item)
        {
            InitializeComponent();
            this.item = item;
            itemService = new ItemService();
            BindingContext = this.item;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (App.CheckConnection()) { 
                if (String.IsNullOrEmpty(title.Text)) {
                    await DisplayAlert("Error", "¡Debes ponerle nombre al elemento!", "OK");
                }
                else 
                {
                    item.name = title.Text;
                    item.description = description.Text;
                    await itemService.updateItemAsync(item, Constants.getAllItems+"/");
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await DisplayAlert("Error","Conexión a internet perdida","OK");
                await Navigation.PopAsync();
            }
        }
    }
}