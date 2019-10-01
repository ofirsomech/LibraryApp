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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RelayCommand = GalaSoft.MvvmLight.Command.RelayCommand;

namespace Client.ViewModels
{
    class MainLibraryViewModel : ViewModelBase
    {
        public HttpClient Client { get; set; }

        public Visibility EditVisibility { get; set; } = Visibility.Visible;
        public string DeleteAndRentText { get; set; }



        private ObservableCollection<AbstractItem> _items;
        public ObservableCollection<AbstractItem> items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        public static AbstractItem SelectedIndex { get; set; }


        public static List<CategoryBook> Categories { get; set; }
        public User ActiveUser { get; set; }

        private bool _canEdit;
        public bool CanEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                _canEdit = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// Remove (Book/Jurnal)
        /// </summary>

        public ICommand OpenCreateBookCommand { get; set; }
        public ICommand OpenCreateJornalCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DisconectedCommand { get; set; }
        public ICommand OrderByPriceCommand { get; set; }
        public ICommand OrderByTitleCommand { get; set; }
        public ICommand OrderByPublisherCommand { get; set; }
        public ICommand OrderByPrintDateCommand { get; set; }

        public MainLibraryViewModel()
        {
            Client = new HttpClient();
            GetAllAbstractItems();
            ActiveUser = Consts.ActiveUser;
            if (ActiveUser.Type == UserTypes.User)
            {
                EditVisibility = Visibility.Collapsed;
                //DeleteVisibility = Visibility.Collapsed;
                DeleteAndRentText = "Buy";
            }
            else if (ActiveUser.Type == UserTypes.Admin)
            {
                DeleteAndRentText = "Delete";
            }
            OpenCreateBookCommand = new RelayCommand(CreateBookHendler);
            OpenCreateJornalCommand = new RelayCommand(CreateJornalHendler);
            EditCommand = new RelayCommand(EditItemHandler);
            DeleteCommand = new RelayCommand(DeleteItemHandler);
            DisconectedCommand = new RelayCommand(DisconectedHandler);
            SearchCommand = new RelayCommand(SearchItemsHandler);
            OrderByPriceCommand = new RelayCommand(OrderByPrice);
            OrderByTitleCommand = new RelayCommand(OrderByTitle);
            OrderByPublisherCommand = new RelayCommand(OrderByPublisher);
            OrderByPrintDateCommand = new RelayCommand(OrderByPrintDate);
        }

        private async void GetAllAbstractItems()
        {
            items = await Tools.Consts.GetAllAvialiabeItems(Client, items, "book", "jornal", "printDate");
        }
        private async void OrderByPrice()
        {
            items = await Consts.GetAllAvialiabeItems(Client, items, "book", "jornal", "price");
        }
        private async void OrderByPublisher()
        {
            items = await Consts.GetAllAvialiabeItems(Client, items, "book", "jornal", "publisher");
        }
        private async void OrderByTitle()
        {
            items = await Consts.GetAllAvialiabeItems(Client, items, "book", "jornal", "title");
        }
        private async void OrderByPrintDate()
        {
            items = await Consts.GetAllAvialiabeItems(Client, items, "book", "jornal", "printDate");
        }



        private void DisconectedHandler()
        {
            ActiveUser = null;
            NavigateTool.DisconectedNav();
        }

        private async void DeleteItemHandler()
        {

            try
            {
                if (SelectedIndex == null || String.IsNullOrEmpty(SelectedIndex.Title))
                {
                    throw new Exception("You need select item before delete!");
                }

                var json = JsonConvert.SerializeObject(SelectedIndex);

                var response = await Consts.DeleteItemAsync(Client, SelectedIndex.ISBN, json);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"{SelectedIndex.Title} was deleted!");
                    items = await Consts.GetAllAvialiabeItems(Client, items, "book", "jornal", "printDate");
                }
                else
                    MessageBox.Show("Cant delete it , try again!");
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }



        }

        private void SearchItemsHandler()
        {
            NavigateTool.Nav(new SearchPage());
        }

        private void CreateBookHendler()
        {
            SelectedIndex = new Book();
            NavigateTool.Nav(new CreateBook());
        }
        private void CreateJornalHendler()
        {
            SelectedIndex = new Jornal();
            NavigateTool.Nav(new CreateJornalView());
        }

        private void EditItemHandler()
        {
            try
            {
                if (SelectedIndex == null || String.IsNullOrEmpty(SelectedIndex.Title))
                {
                    throw new Exception("You need select item before edit!");
                }
                if (SelectedIndex.GetType() == typeof(Book))
                    NavigateTool.Nav(new CreateBook());
                if (SelectedIndex.GetType() == typeof(Jornal))
                    NavigateTool.Nav(new CreateJornalView());
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }
    }
}
