using HologFrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HologFrontEnd.Services
{
    public class CategoryService
    {
        HttpClient client;
        public CategoryService()
        {
            client = new HttpClient();
        }

        public async Task<ISet<Category>> GetAllCategoriesAsync(String uri)
        {
            ISet<Category> categories = new HashSet<Category>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<ISet<Category>>(content);
                    return categories;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tError{0}", ex.Message);
            }
            return null;
        }

        public async Task<Category> GetCategoryByIdAsync(String uri)
        {
            Category category = new Category();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    category = JsonConvert.DeserializeObject<Category>(content);
                    return category;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tError{0}", ex.Message);
            }
            return null;
        }

        public async Task<Category> createCategoryAsync(Category category, String uri)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return category;
                }                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tError{0}", ex.Message);
            }
            return null;
        }

        public async Task<Category> updateCategoryAsync(Category category, String uri)
        {

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(uri + category.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    return category;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tError{0}", ex.Message);
            }
            return null;
        }

        public async Task<Category> deleteCategoryAsync(Category category, String uri)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri + category.Id);
                if (response.IsSuccessStatusCode)
                {
                    return category;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tError{0}", ex.Message);
            }
            return null;
        }

    }
}
