using Android.App;
using HologFrontEnd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HologFrontEnd.Models;
using HologFrontEnd.Resources;

namespace HologFrontEnd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        CategoryService _restService;
        ISet<Category> categories;

        public MainPage()
        {
            _restService = new CategoryService();
            categories = new HashSet<Category>();
            InitializeComponent();           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            showCategories();
            
        }
        /*menu desplegable*/
        async void OnClickButton(object obj,EventArgs e)
        {
            addCategoryButton.IsEnabled = false;
            addFirstCategoryButton.IsEnabled = false;
            var modalPage = new CategoriesModalBox();
            modalPage.IsVisible = true;
            await Navigation.PushModalAsync(modalPage);
            addCategoryButton.IsEnabled = true;
            addFirstCategoryButton.IsEnabled = true;
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        private async void showCategories()
        {
            categories = await _restService.GetAllCategoriesAsync(Constants.getAllCategories);
            categoryCanvaView.ItemsSource = categories;
            foreach (var category in categories)
            {
                switch (category.IconId)
                {
                    case 1:
                        category.Icon = "cubes_solid";
                        category.Color = "#B379ED";
                        break;
                    case 2:
                        category.Icon = "film_solid";
                        category.Color = "#D44343";
                        break;
                    case 3:
                        category.Icon = "gamepad_solid";
                        category.Color = "#70BAFF";
                        break;
                    case 4:
                        category.Icon = "tv_solid";
                        category.Color = "#F8AD55";
                        break;
                    case 5:
                        category.Icon = "book_solid";
                        category.Color = "#67C657";                      
                        break;
                }
            }           

            if(!categories.Any())
            {
                addCategoryButton.IsVisible = false;
                noCategoryLayout.IsVisible = true;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CategoryPage());
        }

        private async void SwipeItem_Erase(object sender, EventArgs e)
        {
            bool confirmation = await DisplayAlert("Atención", "¿Estas seguro de eliminar esta categoría?", "Eliminar", "Cancelar");
            if (confirmation)
            { 
                var collectionViewItem = (SwipeItem)sender;
                var categoria = (Category)collectionViewItem.BindingContext;  
                string uri = Constants.getAllCategories + "/";
                await _restService.deleteCategoryAsync(categoria,uri);
            }
        }
        private async void SwipeItem_Edit(object sender, EventArgs e)
        {
            var collectionViewItem = (SwipeItem)sender;
            var categoria = (Category)collectionViewItem.BindingContext;
            String newName = await DisplayPromptAsync(title: "Renombra tu lista", "", initialValue: categoria.Name, placeholder: "Simple y descriptivo");

            if (newName != null)
            {
                categoria.Name = newName;
                string uri = Constants.getAllCategories + "/";
                await _restService.updateCategoryAsync(categoria, uri);
            }
        }

        private void TappedCategory(object sender, EventArgs e)
        {
            var collectionViewItem = (Frame)sender;
            var categoria = (Category)collectionViewItem.BindingContext;
            Navigation.PushAsync(new CategoryPage(categoria));
        }

        async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue;
            if (string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = string.Empty;
                categoryCanvaView.ItemsSource = categories;

            }
            searchTerm = searchTerm.ToLowerInvariant();
            if (searchTerm.Length > 2)
            {
                var filteredItems = categories.Where(x => x.Name.ToLowerInvariant().Contains(searchTerm)).ToList();
                categoryCanvaView.ItemsSource = filteredItems;
            }

        }
    }
}