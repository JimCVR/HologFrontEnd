using Android.App;
using Android.Icu.Util;
using Android.InputMethodServices;
using HologFrontEnd.Models;
using HologFrontEnd.Resources;
using HologFrontEnd.Services;
using Java.Lang.Reflect;
using Java.Lang;
using Org.Apache.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Content.ClipData;
using static Android.Content.Res.Resources;
using static Android.Icu.Text.CaseMap;
using static Android.Provider.ContactsContract.CommonDataKinds;
using static Android.Util.EventLogTags;
using Item = HologFrontEnd.Models.Item;

namespace HologFrontEnd.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchItemView : ContentPage
    {
        ItemService itemService;
        MovieService movieService;
        BookService bookService;
        VideogameService videogameService;
        int touchCount;

        IList<Item> items;
        Category currentCategory;


        public SearchItemView(Category category)
        {
            InitializeComponent();
            items = new List<Item>();
            itemService = new ItemService();
            movieService = new MovieService();
            videogameService = new VideogameService();
            bookService = new BookService();
            this.currentCategory = category;
            touchCount = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            touchCount = 0;

        }

        async void OnClickButton(object sender, EventArgs e)
        {
            if (App.CheckConnection())
            {
                if (touchCount < 1)
                {
                    touchCount++;
                    Item item = new Item();
                    if (!string.IsNullOrEmpty(itemSearchBar.Text))
                    {
                        item.name = itemSearchBar.Text;
                        item.status = "pending";
                        item.picture = "no_picture.png";
                        item.custom = true;
                        string catId = currentCategory.Id.ToString();
                        item.categoryId = catId;
                        await itemService.createItemAsync(item, Constants.getAllItems);
                        await Navigation.PopModalAsync();
                    }
                }
            }
            else
            {
                await Navigation.PopModalAsync();
            }
        }

        async void SearchItems(string searchTerm)
        {          
            IList<Item> results = new List<Item>();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                switch (currentCategory.IconId)
                {
                    case 1:
                        try
                        {
                            results = new List<Item>();
                            IList<Item> movies = null;
                            IList<Item> games = null;
                            IList<Item> books = null;
                            IList<Item> series = null;
                            await Task.Run(async () =>
                            {
                                movies = movieService.GetMoviesByName(searchTerm).Result;
                                games = videogameService.GetVideogamesByName(searchTerm).Result;
                                books = bookService.GetBooksByName(searchTerm).Result;
                                series = movieService.GetSeriesByName(searchTerm).Result;
                            });
                            if (movies != null)
                            {
                                foreach (var movie in movies) { results.Add(movie); }
                            }
                            if (series != null)
                            {
                                foreach (var serie in series) { results.Add(serie); }
                            }
                            if (games != null)
                            {
                                foreach (var game in games) { results.Add(game); }
                            }
                            if (books != null)
                            {
                                foreach (var book in books) { results.Add(book); }
                            }
                        }
                        catch (System.Exception ex)
                        {
                            // Handle the exception here
                            Console.WriteLine("An error occurred: " + ex.Message);
                        }

                        break;
                    case 2:
                        results = await movieService.GetMoviesByName(searchTerm);
                        break;
                    case 3:
                        results = await videogameService.GetVideogamesByName(searchTerm);
                        break;

                    case 4:
                        results = await bookService.GetBooksByName(searchTerm);
                        break;
                    case 5:
                        results = await movieService.GetSeriesByName(searchTerm);
                        break;
                }
                if (results.Any())
                {
                    items = results;
                }
                else
                {
                    noItemLayout.IsVisible = true;
                    items = new List<Item>();
                }
                searchResults.ItemsSource = items;
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (App.CheckConnection()) { 
                if (touchCount < 1)
                {
                    touchCount++;
                    var collectionViewItem = (Frame)sender;
                    var item = (Item)collectionViewItem.BindingContext;
                    item.status = "pending";
                    item.custom = false;
                    string catId = currentCategory.Id.ToString();
                    item.categoryId = catId;
                    await itemService.createItemAsync(item, Constants.getAllItems);
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                await Navigation.PopModalAsync();
            }
        }

        private async void SearchButtonPressed(object sender, EventArgs e)
        {
            if (App.CheckConnection()) {
                var searchBar = (SearchBar)sender;
                if (!string.IsNullOrEmpty(searchBar.Text))
                {
                    SearchItems(searchBar.Text);
                }
            }
            else
            {
                await Navigation.PopModalAsync();
            }
        }

        private async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}