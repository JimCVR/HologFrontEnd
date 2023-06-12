using HologFrontEnd.Models;
using HologFrontEnd.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace HologFrontEnd.Services
{
    public class BookService
    {
        HttpClient client;

        public BookService() 
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://www.googleapis.com/");
          
        }

        public async Task<IList<Item>> GetBooksByName(string bookName)
        {
            IList<Book> books = new List<Book>();
            IList<Item> items = new List<Item>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + Constants.bookParam+bookName);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content.Contains("items"))
                    {
                        var json = JObject.Parse(content);
                        books = json["items"].ToObject<IList<Book>>();
                    }
                    foreach (var it in books)
                    {
                        items.Add(BookToItemMapper.transform(it));
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tError{0}", ex.Message);
            }
            return items;
        }
    }
}
