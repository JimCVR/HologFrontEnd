using Android.App;
using HologFrontEnd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HologFrontEnd.Models;
using HologFrontEnd.Resources;
using System.Threading.Tasks;

namespace HologFrontEnd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        CategoryService _restService;
        ISet<Category> categories;
        int touchCount;

        public MainPage()
        {
            _restService = new CategoryService();
            categories = new HashSet<Category>();
            touchCount = 0;
            InitializeComponent();
            StartRefreshAnimation();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            touchCount = 0;
            refreshCategory.IsRefreshing = true;
            CheckConnectivity();
        }

        private async void showCategories()
        {
            categories = await _restService.GetAllCategoriesAsync(Constants.getAllCategories);
            if (categories != null)
            {
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
                            category.Icon = "book_solid";
                            category.Color = "#67C657";
                            break;
                        case 5:
                            category.Icon = "tv_solid";
                            category.Color = "#F8AD55";
                            break;
                    }
                }
                if (!categories.Any())
                {
                    addCategoryButton.IsVisible = false;
                    noCategoryLayout.IsVisible = true;
                }
                else
                {
                    addCategoryButton.IsVisible = true;
                }
            }
        }

        private async void StartRefreshAnimation()
        {
            refreshActivityIndicator.IsRunning = true;
            refreshActivityIndicator.IsVisible = true;
            await Task.Delay(2000);
            StopRefreshAnimation();
        }

        private void StopRefreshAnimation()
        {
            refreshActivityIndicator.IsRunning = false;
            refreshActivityIndicator.IsVisible = false;
        }

        private void refreshButton_Clicked(object sender, EventArgs e)
        {
            CheckConnectivity();
        }

        private void CheckConnectivity()
        {

            if (App.CheckConnection())
            {
                noConnectionSL.IsVisible = false;
                absoluteLayout.IsVisible = true;
                showCategories();
            }
            else
            {
                absoluteLayout.IsVisible = false;
                noConnectionSL.IsVisible = true;
            }
            refreshCategory.IsRefreshing = false;
        }

        async void OnClickButton(object obj, EventArgs e)
        {
            if (touchCount < 1)
            {
                touchCount++;
                var modalPage = new CategoriesModalBox();
                await Navigation.PushModalAsync(modalPage);
            }
        }   

        private async void SwipeItem_Erase(object sender, EventArgs e)
        {
            bool confirmation = await DisplayAlert("Atención", "¿Estas seguro de eliminar esta categoría?", "Eliminar", "Cancelar");
            if (confirmation)
            {
                var collectionViewItem = (SwipeItemView)sender;
                var categoria = (Category)collectionViewItem.BindingContext;
                string uri = Constants.getAllCategories + "/";
                await _restService.deleteCategoryAsync(categoria, uri);
                showCategories();
            }
        }

        private async void SwipeItem_Edit(object sender, EventArgs e)
        {
            var collectionViewItem = (SwipeItemView)sender;
            var categoria = (Category)collectionViewItem.BindingContext;
            String newName = await DisplayPromptAsync(title: "Renombra tu lista", "", initialValue: categoria.Name, placeholder: "Simple y descriptivo");

            if (!String.IsNullOrEmpty(newName) || !string.IsNullOrWhiteSpace(newName))
            {
                categoria.Name = newName;
                string uri = Constants.getAllCategories + "/";
                await _restService.updateCategoryAsync(categoria, uri);
                showCategories();
            }
            else if (newName != null)
            {
                await DisplayAlert("ERROR", "La categoría debe tener un nombre", "Cerrar");
            }
        }

        private async void TappedCategory(object sender, EventArgs e)
        {
            if (touchCount < 1)
            {
                touchCount++;
                var collectionViewItem = (Frame)sender;
                await collectionViewItem.ScaleTo(1.1, 100);
                await collectionViewItem.ScaleTo(1, 100);
                var categoria = (Category)collectionViewItem.BindingContext;
                await Navigation.PushAsync(new CategoryPage(categoria));
            }
        }

        private void SearchCategories(object sender, EventArgs e)
        {
            var searchBar = (SearchBar)sender;
            if (!string.IsNullOrEmpty(searchBar.Text))
            {
                if (categories != null && categories.Any())
                {
                    var filteredItems = categories.Where(x => x.Name.ToLowerInvariant().Contains(searchBar.Text));
                    if (filteredItems != null)
                    {

                        filteredItems.ToList();
                        categoryCanvaView.ItemsSource = filteredItems;
                        noCategoryLayout.IsVisible = false;
                    }
                }
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                categoryCanvaView.ItemsSource = categories;
            }
        }
    }
}