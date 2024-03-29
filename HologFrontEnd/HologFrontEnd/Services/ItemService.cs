﻿using HologFrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HologFrontEnd.Services
{
    class ItemService
    {
        HttpClient client;
        public ItemService()
        {
            client = new HttpClient();
        }

        public async Task<ISet<Item>> GetAllItemsAsync(String uri)
        {
            ISet<Item> items = new HashSet<Item>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<ISet<Item>>(content);
                    return items;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tError{0}", ex.Message);
            }
            return null;
        }

        public async Task<Item> GetItemByIdAsync(String uri)
        {
            Item item = new Item();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<Item>(content);
                    return item;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tError{0}", ex.Message);
            }
            return null;
        }

        public async Task<Item> createItemAsync(Item item, String uri)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return item;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tError{0}", ex.Message);
            }
            return null;
        }

        public async Task<Item> updateItemAsync(Item item, String uri)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(uri+item.id, content);
                if (response.IsSuccessStatusCode)
                {
                    return item;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tError{0}", ex.Message);
            }
            return null;
        }

        public async Task<Item> deleteItemAsync(Item item, String uri)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri + item.id);
                if (response.IsSuccessStatusCode)
                {
                    return item;
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
