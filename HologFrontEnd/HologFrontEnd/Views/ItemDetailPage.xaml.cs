using Android.App;
using HologFrontEnd.Models;
using HologFrontEnd.Resources;
using HologFrontEnd.Services;
using System;
using Xamarin.Forms;

namespace HologFrontEnd.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemService restService;
        Item item;
        ToolbarItem deleteButton;
        ToolbarItem editButton;
        public ItemDetailPage(Item item)
        {
            InitializeComponent();

            restService = new ItemService();
            this.item = item;
            this.editButton = new ToolbarItem();
            this.deleteButton = new ToolbarItem();
            CheckCustomItem();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            showDetails();
        }

        private async void showDetails() {
            string uri = Constants.getAllItems+"/"+item.id;
            item = await restService.GetItemByIdAsync(uri);
            BindingContext = item;
            switch (item.status)
            {
                case "pending":
                    pending.IsChecked = true;
                    break;
                case "inProgress":
                    inProgress.IsChecked = true;
                    break;
                case "complete":
                    complete.IsChecked = true;
                    break;
            }
             if (string.IsNullOrEmpty(item.description))
             {
                descRectangle.IsVisible = false;
                descriptionLabel.IsVisible = false;
            }
            else {
                descRectangle.IsVisible = true;
                descriptionLabel.IsVisible = true;
            }
             if (string.IsNullOrEmpty(item.date))
             {
                
                dateLayout.IsVisible = false;
             }else 
            {
                dateLayout.IsVisible = true;
            }
             if (string.IsNullOrEmpty(item.score))
             {
                scoreLayout.IsVisible = false;
             }
            else
            {
                scoreLayout.IsVisible = true;
            }
        }

        private void CheckCustomItem()
        {
            if (item.custom)
            {
                editButton.IconImageSource = "icon_note.png";
                editButton.Clicked += ToolbarItem_edit;
                this.ToolbarItems.Add(editButton);
            }
            this.ToolbarItems.Add(deleteButton);
            deleteButton.IconImageSource = "trash_can_regular.png";
            deleteButton.Clicked += ToolbarItem_delete;

        }

        private async void TappedButton(object sender, EventArgs e)
        {
            string uri = Constants.getAllItems+"/";
            RadioButton statusButton = (RadioButton)sender;
            
            switch (statusButton.Value)
            {
                case "pending":
                    item.status = "pending";
                    break;
                case "inProgress":
                    item.status = "inProgress";                    
                    break;
                case "complete":
                    item.status = "complete";                
                    break;
            }
            
            item = await restService.updateItemAsync(item, uri);
        }

        private void ToolbarItem_edit(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewItem(item));
        } 
        
        private async void ToolbarItem_delete(object sender, EventArgs e)
        {
            bool confirmation = await DisplayAlert("Atención", "¿Estas seguro de eliminar este elemento?", "Eliminar", "Cancelar");
            if (confirmation)
            {
                string uri = Constants.getAllItems + "/";
                await restService.deleteItemAsync(item, uri);
                await Navigation.PopAsync();
            }
        }

        
    }
}