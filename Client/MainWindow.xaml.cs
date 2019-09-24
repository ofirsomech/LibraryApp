using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //HttpClient client = new HttpClient();
        //    try
        //    {
        //        //await client.GetAsync("https://localhost:44384/api/values").ContinueWith((res) =>
        //        //{
        //        //    Console.WriteLine(res);
        //        //});
                
        //        //var id = client.GetAsync("https://localhost:44384/api/values/getid").Result.Content.ReadAsAsync<int>().Result;
        //        var user = new User { Username = txtName.Text, Password = txtPass.Text, Type = UserTypes.User };
        //var json = JsonConvert.SerializeObject(user);
        //var response = await client.PostAsync("https://localhost:44384/api/values", json, new JsonMediaTypeFormatter());


        //Console.WriteLine("done");

        //    }
        //    catch (Exception err)
        //    {

        //        Console.WriteLine(err);
        //    }
            
        //}
    }
}
