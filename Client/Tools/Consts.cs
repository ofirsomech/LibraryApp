using Client.Views;
using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Tools
{
    static class Consts
    {
        public static User ActiveUser { get; set; }
        public static string url = "https://localhost:44384/api/library";



        public static async Task<ObservableCollection<AbstractItem>> GetAllAvialiabeItems(HttpClient client, ObservableCollection<AbstractItem> items, string type1, string type2, string orderBy)
        {
            var response = await Consts.GetItemsFromApi(client, type1);
            var res = await response.Content.ReadAsStringAsync();
            var listBooks = JsonConvert.DeserializeObject<List<Book>>(res);


            response = await Consts.GetItemsFromApi(client, type2);
            res = await response.Content.ReadAsStringAsync();
            var listJornals = JsonConvert.DeserializeObject<List<Jornal>>(res);

            var listItems = new List<AbstractItem>();
            listItems.AddRange(listBooks);
            listItems.AddRange(listJornals);
            if (orderBy == "printDate")
                items = new ObservableCollection<AbstractItem>(listItems.OrderByDescending(i => i.PrintDate));
            if (orderBy == "price")
                items = new ObservableCollection<AbstractItem>(listItems.OrderByDescending(i => i.Price));
            if (orderBy == "title")
                items = new ObservableCollection<AbstractItem>(listItems.OrderByDescending(i => i.Title));
            if (orderBy == "publisher")
                items = new ObservableCollection<AbstractItem>(listItems.OrderByDescending(i => i.Publisher));
            return items;
        }


        public static async Task<HttpResponseMessage> PostItem(HttpClient client, string method, string json)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return await client.PostAsync($"{url}/{method}", json, new JsonMediaTypeFormatter());
        }
        public static async Task<HttpResponseMessage> EditItem(HttpClient client, string method, string json)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return await client.PutAsync($"{url}/{method}", json, new JsonMediaTypeFormatter());
        }

        public static async Task<HttpResponseMessage> GetItemsFromApi(HttpClient client, string method)
        {
            //client.BaseAddress = new Uri($"{url}/{method}");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return await client.GetAsync($"{url}/{method}");
        }

        public static async Task<HttpResponseMessage> SelecteditemPutAsync(HttpClient client, Guid guid, string json, string method)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return await client.PutAsync($"{url}/{method}/{guid}", json, new JsonMediaTypeFormatter());
        }


        public static void Close()
        {
            NavigateTool.Nav(new MainLibraryPage());
        }

        public static async Task<bool> SelectetItemHandler(AbstractItem SelectedIndex, HttpClient Client, string method)
        {

            try
            {
                if (SelectedIndex == null || String.IsNullOrEmpty(SelectedIndex.Title))
                {
                    throw new Exception("You need select item before delete!");
                }

                var json = JsonConvert.SerializeObject(SelectedIndex);

                HttpResponseMessage response = new HttpResponseMessage();
                if (method == "delete")
                {
                    response = await SelecteditemPutAsync(Client, SelectedIndex.ISBN, json, "delete");
                }
                if (method == "buy")
                {
                    response = await SelecteditemPutAsync(Client, SelectedIndex.ISBN, json, "buy");

                }
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
                return false;
            }
        }


    }
}
