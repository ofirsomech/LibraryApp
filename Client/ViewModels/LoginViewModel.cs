using Client.Models;
using Client.Tools;
using GalaSoft.MvvmLight;
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
    class LoginViewModel : ViewModelBase
    {
        private string url = "https://localhost:44384/api/user";

        public HttpClient client { get; set; }
        public string TextUser { get; set; }
        public string Password { get; set; }


        private bool _canClick;
        public bool CanClick
        {
            get
            {
                return _canClick;
            }
            set
            {
                _canClick = value;
                RaisePropertyChanged();
            }
        }


        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public RelayCommand<PasswordBox> PasswordChangedCommand { get; set; }



        public LoginViewModel()
        {
            client = new HttpClient();
            //TextUser = "admin";
            //Password = "1234";
            CanClick = true;
            LoginCommand = new GalaSoft.MvvmLight.Command.RelayCommand(Login);
            RegisterCommand = new GalaSoft.MvvmLight.Command.RelayCommand(Register);
            PasswordChangedCommand = new RelayCommand<PasswordBox>(ExecutePasswordChangedCommand);
        }

        private async void Login()
        {
            try
            {
                if (String.IsNullOrEmpty(TextUser) || String.IsNullOrEmpty(Password))
                    throw new Exception("Please type your username and password!");





                CanClick = false;
                var json = GetUser(TextUser, Password);
                var response = await PostUser("login", json);
                var userJson = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {

                    MainWindow page = new MainWindow();
                    Consts.ActiveUser = JsonConvert.DeserializeObject<User>(userJson);
                    NavigateTool.NavFromLogin();
                }
                else
                {
                    MessageBox.Show("You entered wrong username/password!\nPlease try again.");
                    CanClick = true;
                }

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }

        private void ExecutePasswordChangedCommand(PasswordBox obj)
        {
            if (obj != null)
                Password = obj.Password;
        }


        public async void Register()
        {
            try
            {
                CanClick = false;
                var json = GetUser(TextUser, Password);

                var response = await PostUser("create", json);


                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("The account was created ! Please login with your new details");
                    CanClick = true;
                }
                else
                {
                    MessageBox.Show("Please choose another username");
                    CanClick = true;
                }

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

        async Task<HttpResponseMessage> PostUser(string method, string json)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return await client.PostAsync($"{url}/{method}", json, new JsonMediaTypeFormatter());
        }
    }
}
