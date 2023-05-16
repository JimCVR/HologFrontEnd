using HologFrontEnd.Models;
using HologFrontEnd.Resources;
using HologFrontEnd.Services;
using Android.App;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HologFrontEnd.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
      
        Category category;
        ItemService restService;

        public CategoryPage()
        {
            InitializeComponent();
        }

        public CategoryPage(Category category)
        {
            restService = new ItemService();
            InitializeComponent();
            //BindingContext = _viewModel = new CategoriesViewModel();
            this.category = category;
            Title = category.Name;          
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
             ShowItems();
        }

        private async void ShowItems()
        {
           ISet<Item> items = await restService.GetAllItemsAsync(Constants.getAllCategories+"/"+category.Id+Constants.itemEndpoint);
           ItemsListView.ItemsSource = items;
        }

        async void OnClickButton(object obj, EventArgs e)
        {
            addCategoryButton.IsEnabled = false;
            var modalPage = new SearchItemView();
            modalPage.IsVisible = true;
            await Navigation.PushModalAsync(modalPage);    
            addCategoryButton.IsEnabled = true;           
        }

        private async void TappedItem(object sender, EventArgs e)
        {
            var collectionViewItem = (StackLayout)sender;
            var item = (Item)collectionViewItem.BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(item));
        }
    }
}