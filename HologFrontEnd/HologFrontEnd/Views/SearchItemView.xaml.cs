using Android.App;
using HologFrontEnd.Models;
using HologFrontEnd.Resources;
using HologFrontEnd.Services;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HologFrontEnd.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchItemView : ContentPage
	{
		ItemService itemService;
		ISet<Item> items;


        public SearchItemView ()
		{
			InitializeComponent();
            itemService = new ItemService();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
			GetItems();
        }
        async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTerm = e.NewTextValue;
			if(string.IsNullOrEmpty(searchTerm) )
			{
				searchTerm = string.Empty;
                searchResults.ItemsSource = new List<Item>();

            }
			searchTerm = searchTerm.ToLowerInvariant();
			if(searchTerm.Length > 2 ) {
                var filteredItems = items.Where(x => x.name.ToLowerInvariant().Contains(searchTerm)).ToList();
                searchResults.ItemsSource = filteredItems;
            }
			
        }

		async void GetItems()
		{
            string uri = Constants.getAllItems;
            items = await itemService.GetAllItemsAsync(uri);
        }
    }

}