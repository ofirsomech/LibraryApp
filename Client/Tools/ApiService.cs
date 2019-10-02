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
    class ApiService
    {
        /// <summary>
        /// This methos return the ObservableCollection<AbstractItem> and update the UI
        /// </summary>
        /// <param name="client"></param>
        /// <param name="items">the ObservableCollection<AbstractItem></param>
        /// <param name="type1">the type of the item</param>
        /// <param name="type2">the type of the item</param>
        /// <param name="orderBy">chose how you want to order the list</param>
        /// <returns></returns>
        public static async Task<ObservableCollection<AbstractItem>> GetAllAvialiabeItems(HttpClient client, ObservableCollection<AbstractItem> items, string type1, string type2, string orderBy)
        {
            var response = await GetItemsFromApi(client, type1);
            var res = await response.Content.ReadAsStringAsync();
            var listBooks = JsonConvert.DeserializeObject<List<Book>>(res);


            response = await GetItemsFromApi(client, type2);
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

        /// <summary>
        /// Add a book or jornal to the list of items on the library
        /// </summary>
        /// <param name="client"></param>
        /// <param name="method">"Create/book" or "Create/jornal"</param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostItem(HttpClient client, string method, string json)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return await client.PostAsync($"{Consts.url}/{method}", json, new JsonMediaTypeFormatter());
        }

        public static async Task<HttpResponseMessage> EditItem(HttpClient client, string method, string json)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return await client.PutAsync($"{Consts.url}/{method}", json, new JsonMediaTypeFormatter());
        }

        /// <summary>
        /// Handle on the get req of get all items of specipic (book or jornal)
        /// </summary>
        /// <param name="client"></param>
        /// <param name="method">"book" or "jornal"</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> GetItemsFromApi(HttpClient client, string method)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return await client.GetAsync($"{Consts.url}/{method}");
        }

        /// <summary>
        /// Function thad handle the put req of selected item from the list
        /// </summary>
        /// <param name="client"></param>
        /// <param name="guid">The guid of the item </param>
        /// <param name="json">The json of the edited item</param>
        /// <param name="method">If its book or jornal</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SelecteditemPutAsync(HttpClient client, Guid guid, string json, string method)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return await client.PutAsync($"{Consts.url}/{method}/{guid}", json, new JsonMediaTypeFormatter());
        }

        /// <summary>
        /// Its the function that work on the items thad choose from the list
        /// </summary>
        /// <param name="SelectedIndex">selected item from the list</param>
        /// <param name="Client"></param>
        /// <param name="method">Its a string thad you send id its "delete" or "edit"</param>
        /// <returns></returns>
        public static async Task<bool> SelectetItemHandler(AbstractItem SelectedIndex, HttpClient Client, string method)
        {

            try
            {
                    if (SelectedIndex == null || String.IsNullOrEmpty(SelectedIndex.Title))
                    {
                        throw new Exception($"You need select item before {method}!");
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
