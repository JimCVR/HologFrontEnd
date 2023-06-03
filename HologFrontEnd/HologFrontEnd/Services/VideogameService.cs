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
    class VideogameService
    {
        HttpClient client;

        public VideogameService() 
        {
            client = new HttpClient();
            client.BaseAddress= new Uri("https://api.rawg.io/api/games");
        }
        public async Task<IList<Item>> GetVideogamesByName(string gameName)
        {
            IList<Videogame> videogames = new List<Videogame>();
            IList<Item> items = new List<Item>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + Constants.videogamesAPIkey+Constants.gameParam1 + gameName + Constants.gameParam2);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    videogames = JObject.Parse(content)["results"].ToObject<IList<Videogame>>();
                    foreach (var it in videogames)
                    {
                        items.Add(VideogameToItemMapper.transform(it));
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
