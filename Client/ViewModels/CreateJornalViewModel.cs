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
    class CreateJornalViewModel
    {
        public Jornal Jornal { get; set; }

        public ObservableCollection<CategoryBook> BooksCategories { get; set; } = new ObservableCollection<CategoryBook>(Enum.GetValues(typeof(CategoryBook)).Cast<CategoryBook>().ToList());

        public ICommand CreateJornalCommand { get; set; }

        public CreateJornalViewModel()
        {
            if (MainLibraryViewModel.SelectedIndex != null)
                Jornal = (Jornal)MainLibraryViewModel.SelectedIndex;
            else
                Jornal = new Jornal();
            Jornal.PrintDate = DateTime.Now;
            CreateJornalCommand = new GalaSoft.MvvmLight.Command.RelayCommand(CreateJornalHandler);
        }

        public CreateJornalViewModel(Jornal jornal)
        {
            Jornal = jornal;
        }


        async void CreateJornalHandler()
        {
            HttpClient client = new HttpClient();
            var jornal = this.Jornal;
            jornal.SetDiscount();
            var json = JsonConvert.SerializeObject(jornal);

            var response = await Consts.PostItem(client, "create/jornal", json);
            if (response.IsSuccessStatusCode)
                NavigateTool.Nav(new MainLibraryPage());
            else
                MessageBox.Show("Cant create jornal , try again!");
        }

    }
}
