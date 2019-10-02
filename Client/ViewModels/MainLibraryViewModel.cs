using BL.Services.Classes;
using BL.Services.Interfaces;

using Client.Tools;
using Client.Views;
using GalaSoft.MvvmLight;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using RelayCommand = GalaSoft.MvvmLight.Command.RelayCommand;

namespace Client.ViewModels
{
    class MainLibraryViewModel : ViewModelBase
    {
        public HttpClient Client { get; set; }
        public IManageTime ManageTime { get; set; }

        public Visibility EditVisibility { get; set; } = Visibility.Visible;
        public string DeleteAndRentText { get; set; }

        public string GreetingNameText { get; set; }
        public string GreetingTimeText { get; set; }


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

        public static AbstractItem SelectedItem { get; set; }

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
        public ICommand SelectemItemCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DisconectedCommand { get; set; }
        public ICommand OrderByPriceCommand { get; set; }
        public ICommand OrderByTitleCommand { get; set; }
        public ICommand OrderByPublisherCommand { get; set; }
        public ICommand OrderByPrintDateCommand { get; set; }

        public MainLibraryViewModel()
        {
            Client = new HttpClient();
            ManageTime = new ManageTime();
            GetAllAbstractItems();
            ActiveUser = Consts.ActiveUser;
            GreetingNameText = $"Hello , {ActiveUser.Username}";
            GreetingTimeText = ManageTime.GetGreeting();
            if (ActiveUser.Type == UserTypes.User)
            {
                EditVisibility = Visibility.Collapsed;
                DeleteAndRentText = "Buy";
                SelectemItemCommand = new RelayCommand(BuyItemHandler);

            }
            else if (ActiveUser.Type == UserTypes.Admin)
            {
                DeleteAndRentText = "Delete";
                SelectemItemCommand = new RelayCommand(DeleteItemHandler);

            }
            OpenCreateBookCommand = new RelayCommand(CreateBookHendler);
            OpenCreateJornalCommand = new RelayCommand(CreateJornalHendler);
            EditCommand = new RelayCommand(EditItemHandler);
            DisconectedCommand = new RelayCommand(DisconectedHandler);
            SearchCommand = new RelayCommand(SearchItemsHandler);
            OrderByPriceCommand = new RelayCommand(OrderByPrice);
            OrderByTitleCommand = new RelayCommand(OrderByTitle);
            OrderByPublisherCommand = new RelayCommand(OrderByPublisher);
            OrderByPrintDateCommand = new RelayCommand(OrderByPrintDate);
        }

        private async void GetAllAbstractItems()
        {
            items = await ApiService.GetAllAvialiabeItems(Client, items, "book", "jornal", "printDate");

        }
        private async void OrderByPrice()
        {
            items = await ApiService.GetAllAvialiabeItems(Client, items, "book", "jornal", "price");
        }
        private async void OrderByPublisher()
        {
            items = await ApiService.GetAllAvialiabeItems(Client, items, "book", "jornal", "publisher");
        }
        private async void OrderByTitle()
        {
            items = await ApiService.GetAllAvialiabeItems(Client, items, "book", "jornal", "title");
        }
        private async void OrderByPrintDate()
        {
            items = await ApiService.GetAllAvialiabeItems(Client, items, "book", "jornal", "printDate");
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
                var success = await ApiService.SelectetItemHandler(SelectedItem, Client, "delete");
                if (success)
                {
                    MessageBox.Show($"{SelectedItem.Title} was deleted!");
                    items = await ApiService.GetAllAvialiabeItems(Client, items, "book", "jornal", "printDate");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                throw;
            }

        }
        private async void BuyItemHandler()
        {
            try
            {
                var success = await ApiService.SelectetItemHandler(SelectedItem, Client, "buy");
                if (success)
                {
                    MessageBox.Show($"You bought {SelectedItem.Title} successfully");
                    items = await ApiService.GetAllAvialiabeItems(Client, items, "book", "jornal", "printDate");
                }
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }

        private void SearchItemsHandler()
        {
            try
            {
                NavigateTool.Nav(new SearchPage());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void CreateBookHendler()
        {
            try
            {
                SelectedItem = new Book();
                NavigateTool.Nav(new CreateBook());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void CreateJornalHendler()
        {
            try
            {
                SelectedItem = new Jornal();
                NavigateTool.Nav(new CreateJornalView());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        private void EditItemHandler()
        {
            try
            {
                if (SelectedItem == null || String.IsNullOrEmpty(SelectedItem.Title))
                {
                    throw new Exception("You need select item before edit!");
                }
                if (SelectedItem.GetType() == typeof(Book))
                    NavigateTool.Nav(new CreateBook());
                if (SelectedItem.GetType() == typeof(Jornal))
                    NavigateTool.Nav(new CreateJornalView());
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
        }

    }
}
