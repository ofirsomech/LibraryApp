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

namespace Client.ViewModels
{
    class CreateBookViewModel
    {
        public Book Book { get; set; }
        public HttpClient Client { get; set; }

        public ObservableCollection<CategoryBook> BooksCategories { get; set; } = new ObservableCollection<CategoryBook>(Enum.GetValues(typeof(CategoryBook)).Cast<CategoryBook>().ToList());

        public ICommand CreateBookCommand { get; set; }

        public CreateBookViewModel()
        {
            Client = new HttpClient();
            if(MainLibraryViewModel.SelectedIndex != null)
            {
                Book = (Book)MainLibraryViewModel.SelectedIndex;
            }
            else
            {
                Book = new Book();
            }
            CreateBookCommand = new GalaSoft.MvvmLight.Command.RelayCommand(CreateBookHandler);
            Book.PrintDate = DateTime.Now;
        }
        public CreateBookViewModel(Book book)
        {
            Book = book;
        }



        async void CreateBookHandler()
        {
            var book = this.Book;
            book.SetDiscount();
            var json = JsonConvert.SerializeObject(book);

            var response =await Consts.PostItem(Client, "create/book", json);
            if(response.IsSuccessStatusCode)
            NavigateTool.Nav(new MainLibraryPage());
            else
                MessageBox.Show(response.Content.ReadAsStringAsync().Result);
        }

    }
}
