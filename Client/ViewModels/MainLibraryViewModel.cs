﻿using Client.Collection;
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

namespace Client.ViewModels
{
    class MainLibraryViewModel : ViewModelBase
    {
        public HttpClient Client { get; set; }
        ItemsCollection icollection = new ItemsCollection();

        public Visibility EditVisibility { get; set; } = Visibility.Visible;
        public Visibility DeleteVisibility { get; set; } = Visibility.Visible;



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
            if(ActiveUser.Type == UserTypes.User)
            {
                EditVisibility = Visibility.Collapsed;
                DeleteVisibility = Visibility.Collapsed;
            }
            OpenCreateBookCommand = new GalaSoft.MvvmLight.Command.RelayCommand(CreateBookHendler);
            OpenCreateJornalCommand = new GalaSoft.MvvmLight.Command.RelayCommand(CreateJornalHendler);
            EditCommand = new GalaSoft.MvvmLight.Command.RelayCommand(EditItemHandler);
            DeleteCommand = new GalaSoft.MvvmLight.Command.RelayCommand(DeleteItemHandler);
            DisconectedCommand = new GalaSoft.MvvmLight.Command.RelayCommand(DisconectedHandler);
            SearchCommand = new GalaSoft.MvvmLight.Command.RelayCommand(SearchItemsHandler);
            OrderByPriceCommand = new GalaSoft.MvvmLight.Command.RelayCommand(OrderByPrice);
            OrderByTitleCommand = new GalaSoft.MvvmLight.Command.RelayCommand(OrderByTitle);
            OrderByPublisherCommand = new GalaSoft.MvvmLight.Command.RelayCommand(OrderByPublisher);
            OrderByPrintDateCommand = new GalaSoft.MvvmLight.Command.RelayCommand(OrderByPrintDate);
        }

        private async void GetAllAbstractItems()
        {
            items = await Consts.GetAllAvialiabeItems(Client, items, "book", "jornal" , "printDate");
        }
        private async void OrderByPrice()
        {
            items = await Consts.GetAllAvialiabeItems(Client, items, "book", "jornal" , "price");
        }
        private async void OrderByPublisher()
        {
            items = await Consts.GetAllAvialiabeItems(Client, items, "book", "jornal" , "publisher");
        }
        private async void OrderByTitle()
        {
            items = await Consts.GetAllAvialiabeItems(Client, items, "book", "jornal" , "title");
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

            if (SelectedIndex != null)
            {
                var json = JsonConvert.SerializeObject(SelectedIndex);

                var response = await Consts.DeleteItemAsync(Client, SelectedIndex.ISBN, json);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"{SelectedIndex.Title} was deleted!");
                    items =await Consts.GetAllAvialiabeItems(Client, items, "book", "jornal" , "printDate");
                }
                else
                    MessageBox.Show("Cant delete it , try again!");

            }
            else
            {
                MessageBox.Show("You need to choose item !");
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
            if (SelectedIndex.GetType() == typeof(Book))
                NavigateTool.Nav(new CreateBook());
            if (SelectedIndex.GetType() == typeof(Jornal))
                NavigateTool.Nav(new CreateJornalView());
        }




        public List<AbstractItem> searchListByTitle(string strSearchByName)
        {

            return icollection.searchListByTitle(strSearchByName);
        }
        public List<AbstractItem> SearchBySN(Guid sn)
        {
            return icollection.SearchBySN(sn);
        }
        public ObservableCollection<AbstractItem> GetAllItems()
        {
            return items;
        }


    }
}
