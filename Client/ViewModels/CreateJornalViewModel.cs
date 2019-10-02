using Client.Tools;
using Client.Views;
using GalaSoft.MvvmLight.Command;
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
using System.Windows.Input;
using RelayCommand = GalaSoft.MvvmLight.Command.RelayCommand;

namespace Client.ViewModels
{
    class CreateJornalViewModel
    {
        public string HeaderText { get; set; }

        public Jornal Jornal { get; set; }

        public ObservableCollection<CategoryBook> BooksCategories { get; set; } = new ObservableCollection<CategoryBook>(Enum.GetValues(typeof(CategoryBook)).Cast<CategoryBook>().ToList());

        public ICommand JornalCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public HttpClient Client { get; set; } = new HttpClient();

        public CreateJornalViewModel()
        {
            if (!String.IsNullOrEmpty(MainLibraryViewModel.SelectedItem.Title))
            {
                HeaderText = $"Edit {MainLibraryViewModel.SelectedItem.Title}";
                Jornal = (Jornal)MainLibraryViewModel.SelectedItem;
                JornalCommand = new RelayCommand(EditJornalHandler);
            }
            else
            {
                HeaderText = "Create Jornal";
                Jornal = new Jornal();
                Jornal.PrintDate = DateTime.Now;
                JornalCommand = new RelayCommand(CreateJornalHandler);
            }
            CloseCommand = new RelayCommand(NavigateTool.Close);
        }


        void CreateJornalHandler()
        {
            SubmitHanler("create/jornal");
        }

        void EditJornalHandler()
        {
            SubmitHanler("edit/jornal");
        }

        /// <summary>
        /// its update or jornal , depend which method sent to the function.
        /// </summary>
        /// <param name="method">"Create" or "Edit"</param>
        private async void SubmitHanler(string method)
        {
            try
            {
                var jornal = this.Jornal;
                if (jornal.Price <= 0 && String.IsNullOrEmpty(jornal.Title) && String.IsNullOrWhiteSpace(jornal.Title) && String.IsNullOrEmpty(jornal.Month) && String.IsNullOrWhiteSpace(jornal.Month))
                {
                    throw new Exception("You need enter a valid title , price and month. Pleate try again!");
                }
                if (String.IsNullOrEmpty(jornal.Title) && String.IsNullOrWhiteSpace(jornal.Title))
                {
                    throw new Exception("You need enter a valid title. Pleate try again!");
                }
                if (String.IsNullOrEmpty(jornal.Month) && String.IsNullOrWhiteSpace(jornal.Month))
                {
                    throw new Exception("You need enter a valid month. Pleate try again!");
                }
                if (jornal.Price <= 0)
                {
                    throw new Exception("You need enter a valid price(more than 1). Pleate try again!");
                }
                jornal.SetDiscount(jornal.Discount);
                var json = JsonConvert.SerializeObject(jornal);
                HttpResponseMessage response = new HttpResponseMessage();
                if (method == "edit/jornal")
                    response = await ApiService.EditItem(Client, method, json);
                else if (method == "create/jornal")
                    response = await ApiService.PostItem(Client, method, json);

                if (response.IsSuccessStatusCode)
                    NavigateTool.Nav(new MainLibraryPage());
                else
                    MessageBox.Show(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
