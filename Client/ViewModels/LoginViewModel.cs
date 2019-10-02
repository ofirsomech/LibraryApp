using Client.Models;
using Client.Tools;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RelayCommand = GalaSoft.MvvmLight.Command.RelayCommand;

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
            CanClick = true;
            LoginCommand = new RelayCommand(Login);
            RegisterCommand =new RelayCommand(Register);
            PasswordChangedCommand = new RelayCommand<PasswordBox>(ExecutePasswordChangedCommand);
        }

        private async void Login()
        {
            try
            {
                if (String.IsNullOrEmpty(TextUser) || String.IsNullOrEmpty(Password))
                    throw new Exception("Please type your username and password!");

                if(TextUser.Length < 3)
                {
                    throw new Exception("Username must contains 3 letters and more!");
                }
                if (Password.Length < 4)
                {
                    throw new Exception("Password must contains 4 digits and more!");
                }





                CanClick = false;
                var json = GetUser(TextUser, Password);
                var response = await PostUser("login", json);
                var userJson = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {

                    Consts.ActiveUser = JsonConvert.DeserializeObject<LoginModel>(userJson);
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
                CanClick = true;

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
                CanClick = true;
            }


        }

        /// <summary>
        /// Get user json 
        /// </summary>
        private string GetUser(string username, string password)
        {
            var user = new LoginModel { Username = TextUser, Password = Password, Type = Models.UserTypes.User };
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
