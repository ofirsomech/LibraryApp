using Client.Models;
using Client.Tools;
using GalaSoft.MvvmLight.Command;
using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Client.ViewModels
{
    class LoginViewModel
    {
        private string url = "https://localhost:44384/api/user";

        public HttpClient client { get; set; }
        public string TextUser { get; set; }
        public string Password { get; set; }



        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public LoginViewModel()
        {
            client = new HttpClient();
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }
        public async void Login()
        {
            try
            {
                var json = GetUser(TextUser, Password);
                var response = await PostUser("login", json);
                if (response.IsSuccessStatusCode)
                {
                    MainWindow page = new MainWindow();
                    NavigateTool.NavFromLogin();
                }
                else
                    MessageBox.Show("You entered wrong username/password!\nPlease try again.");
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }

        public async void Register()
        {
            try
            {
               var json = GetUser(TextUser, Password);

                var response = await PostUser("create", json);
                    
                
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("The account was created ! Please login with your new details");
                }
                else
                    MessageBox.Show("Please choose another username");

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }


        }


         private string GetUser(string username, string password)
        {
            var user = new User { Username = TextUser, Password = Password, Type = UserTypes.User };
            var json = JsonConvert.SerializeObject(user);
            return json;
        }

        async Task<HttpResponseMessage> PostUser(string method , string json)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return await client.PostAsync($"{url}/{method}", json, new JsonMediaTypeFormatter());
        }
    }
}
