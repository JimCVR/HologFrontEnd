using HologFrontEnd.Models;
using HologFrontEnd.Resources;
using HologFrontEnd.Services;
using Android.App;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Widget;
using System.Threading.Tasks;
using Javax.Security.Auth;
using Android.Webkit;
using System.Linq;

namespace HologFrontEnd.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        Category category;
        ItemService restService;
        int touchCount;

        public CategoryPage(Category category)
        {
            restService = new ItemService();
            InitializeComponent();
            this.category = category;
            Title = category.Name;
            touchCount = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            touchCount = 0;
            CheckConnectivity();
        }

        private async void ShowItems()
        {
            ISet<Item> items = await restService.GetAllItemsAsync(Constants.getAllCategories + "/" + category.Id + Constants.itemEndpoint);
            if (!items.Any())
            {
                noItemLayout.IsVisible = true;
            }
            else
            {
                noItemLayout.IsVisible = false;
                ItemsListView.ItemsSource = items;
            }        
        }

        private void CheckConnectivity()
        {

            if (App.CheckConnection())
            {
                noConnectionSL.IsVisible = false;
                absoluteLayout.IsVisible = true;
                ShowItems();
            }
            else
            {
                absoluteLayout.IsVisible = false;
                noConnectionSL.IsVisible = true;
            }
            //refreshCategory.IsRefreshing = false;
        }

        async void OnClickButton(object obj, EventArgs e)
        {
            if (touchCount < 1)
            {
                touchCount++;
                var modalPage = new SearchItemView(category);
                await Navigation.PushModalAsync(modalPage);
            }

        }

        private async void TappedItem(object sender, EventArgs e)
        {
            if (touchCount < 1)
            {
                touchCount++;
                var collectionViewItem = (StackLayout)sender;
                await collectionViewItem.ScaleTo(1.1, 100);
                await collectionViewItem.ScaleTo(1, 100);
                var item = (Item)collectionViewItem.BindingContext;
                await Navigation.PushAsync(new ItemDetailPage(item));
            }
        }

        private void refreshButton_Clicked(object sender, EventArgs e)
        {
            CheckConnectivity();
        }
    }
}