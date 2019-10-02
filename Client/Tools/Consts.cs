using Client.Views;
using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Tools
{
    static class Consts
    {
        public static User ActiveUser { get; set; }
        public static string url = "https://localhost:44384/api/library";


    }
}
