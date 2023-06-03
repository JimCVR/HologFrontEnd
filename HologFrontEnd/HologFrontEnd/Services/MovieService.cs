using HologFrontEnd.Models;
using HologFrontEnd.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace HologFrontEnd.Services
{
    
    class MovieService
    {
        HttpClient client;
        public MovieService() 
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.themoviedb.org/3/search");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Constants.token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IList<Item>> GetMoviesByName(string movieName)
        {
            IList<Movie> movies = new List<Movie>();
            IList<Item> items = new List<Item>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress+Constants.movieEndPoint+movieName+Constants.queryParams);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    movies = JObject.Parse(content)["results"].ToObject<IList<Movie>>();
                    foreach (var it in movies)
                    {
                        items.Add(MovieToItemMapper.transform(it));
                    }
                    return items;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine("\tError{0}", ex.Message);
            }
            return null;
        }

        public async Task<IList<Item>> GetSeriesByName(string seriesName)
        {
            IList<Series> series = new List<Series>();
            IList<Item> items = new List<Item>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress+Constants.seriesEndPoint + seriesName + Constants.queryParams);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    series = JObject.Parse(content)["results"].ToObject<IList<Series>>();
                    foreach (var it in series)
                    {
                        items.Add(SeriesToItemMapper.transform(it));
                    }
                    return items;
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
