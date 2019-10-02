using Client.Tools;
using Client.Views;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
    class CreateBookViewModel : ViewModelBase
    {
        public string HeaderText { get; set; }
        public Book Book { get; set; }

        public HttpClient Client { get; set; }

        public ObservableCollection<CategoryBook> BooksCategories { get; set; } = new ObservableCollection<CategoryBook>(Enum.GetValues(typeof(CategoryBook)).Cast<CategoryBook>().ToList());

        public ICommand BookCommand { get; set; }
        public ICommand CloseCommand { get; set; }


        public CreateBookViewModel()
        {
            Client = new HttpClient();
            if (MainLibraryViewModel.SelectedItem.Id != 0)
            {
                HeaderText = $"Edit {MainLibraryViewModel.SelectedItem.Title}";
                Book = (Book)MainLibraryViewModel.SelectedItem;
                BookCommand = new GalaSoft.MvvmLight.Command.RelayCommand(EditBookHendler);

            }
            else if (MainLibraryViewModel.SelectedItem.Id == 0)
            {
                HeaderText = $"Create Book";

                Book = new Book();
                Book.PrintDate = DateTime.Now;
                BookCommand = new GalaSoft.MvvmLight.Command.RelayCommand(CreateBookHandler);
            }
            CloseCommand = new GalaSoft.MvvmLight.Command.RelayCommand(NavigateTool.Close);
        }


        private void CreateBookHandler()
        {
            SubmitHanler("create/book");
        }

        private void EditBookHendler()
        {
            SubmitHanler($"edit/book");
            MainLibraryViewModel.SelectedItem = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        private async void SubmitHanler(string method)
        {
            try
            {
                var book = this.Book;
                if (book.Price <= 0 &&
                    book.Copies <= 0 &&
                    String.IsNullOrWhiteSpace(book.Title) && String.IsNullOrEmpty(book.Title) &&
                    String.IsNullOrWhiteSpace(book.Publisher) && String.IsNullOrEmpty(book.Publisher))
                {
                    throw new Exception("You need enter a valid title, publisher , price and copies. Pleate try again!");
                }
                if (String.IsNullOrWhiteSpace(book.Title) && String.IsNullOrEmpty(book.Title))
                {
                    throw new Exception("You need enter a valid title");

                }
                if (String.IsNullOrWhiteSpace(book.Publisher) && String.IsNullOrEmpty(book.Publisher))
                {
                    throw new Exception("You need enter a valid publisher name. Pleate try again!");
                }
                if (book.Price <= 0)
                {
                    throw new Exception("You need enter a valid price(more than 1). Pleate try again!");
                }
                if (book.Copies <= 0)
                {
                    throw new Exception("You need enter a valid copies(more than 1). Pleate try again!");
                }
                if (book.Discount<0 || book.Discount > 100) 
                {
                    throw new Exception("You need enter a valid discount(between 0-100). Pleate try again!");

                }
                book.SetDiscount(book.Discount);
                var json = JsonConvert.SerializeObject(book);
                HttpResponseMessage response = new HttpResponseMessage();
                if (method == "edit/book")
                    response = await ApiService.EditItem(Client, method, json);
                else if (method == "create/book")
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

