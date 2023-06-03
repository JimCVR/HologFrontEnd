using Android.App;
using HologFrontEnd.Resources;
using HologFrontEnd.Services;
using HologFrontEnd.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HologFrontEnd.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoriesModalBox : ContentPage
	{
        CategoryService restService;
        int touchCount;
        public CategoriesModalBox()
		{
            restService = new CategoryService();
			InitializeComponent();
            touchCount = 0;
		}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
            if (touchCount < 1) {
                touchCount++;
                Frame frame = (Frame)sender;
                string categoryTitle = "";
                long iconId = 0;
                if (frame == Varios)
                {
                    categoryTitle = "Varios";
                    iconId = 1;
                }
                else if (frame == Peliculas) {
                    categoryTitle = "Peliculas";
                    iconId = 2;
                }
                else if (frame == Videojuegos)
                {
                    categoryTitle = "Videojuegos";
                    iconId = 3;
                }
                else if (frame == Libros)
                {
                    categoryTitle = "Libros";
                    iconId = 4;
                }
                else if (frame == Series)
                {
                    categoryTitle = "Series";
                    iconId = 5;
                }


                String result = await DisplayPromptAsync(title: "Nombra tu nueva lista de " + categoryTitle, "", initialValue: categoryTitle, placeholder: "Simple y descriptivo");

                if (!String.IsNullOrEmpty(result) || !string.IsNullOrWhiteSpace(result))
                {
                    result = result.Trim();
                    Category category = new Category();
                    category.IconId = iconId;
                    category.Name = result;
                    category.UserId = App.DeviceId;
                    await restService.createCategoryAsync(category, Constants.getAllCategories);
                    await Navigation.PopModalAsync();

                } else if (result != null)
                {
                    await DisplayAlert("ERROR", "La categoría debe tener un nombre", "Cerrar");
                }
                touchCount = 0;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}