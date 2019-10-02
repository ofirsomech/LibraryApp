using Client.Tools;
using GalaSoft.MvvmLight;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    class SearchViewModel : ViewModelBase
    {
        public HttpClient Client { get; set; }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChange(Text);
            }
        }

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
        public ICommand FilterTextChangedCommand { get; set; }


        public ICommand SearchTypeCommand { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChange(Text);
            }
        }

        public SearchViewModel()
        {
            Client = new HttpClient();
            SearchTypeCommand = new RelayCommand(executemethod, canexecutemethod);
            FilterTextChangedCommand = new RelayCommand(executemethod, canexecutemethod);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void OnPropertyChange(string textSearch)
        {
            try
            {
            if (Name == "By Name")
            {
                var lst = new List<AbstractItem>();
                items = new ObservableCollection<AbstractItem>(lst);
                if (Text == "")
                {
                    items.Clear();
                    return;
                }
                if (String.IsNullOrEmpty(textSearch))
                {
                    return;
                }
                items = await GetItemsByName(Client, items, textSearch);
            }
            else if(Name =="By ISBN")
            {
                items = new ObservableCollection<AbstractItem>();
                if (Text == "")
                {
                    items.Clear();
                    return;
                }
                if (String.IsNullOrEmpty(textSearch))
                {
                    return;
                }
                items = await GetItemsByISBN(Client, items, textSearch);
            }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        private async Task<ObservableCollection<AbstractItem>> GetItemsByISBN(HttpClient client, ObservableCollection<AbstractItem> items, string propertyname)
        {
            items = await ApiService.GetAllAvialiabeItems(client, items, "book", "jornal" , "printDate");
            ObservableCollection<AbstractItem> toReturn = new ObservableCollection<AbstractItem>();

            foreach (var item in items)
            {
                if (item.ISBN.ToString().Contains(this.Text))
                    toReturn.Add(item);
            }

            return toReturn;
        }

        private async Task<ObservableCollection<AbstractItem>> GetItemsByName(HttpClient client, ObservableCollection<AbstractItem> items, string propertyname)
        {
            items = await ApiService.GetAllAvialiabeItems(client, items, "book", "jornal" , "printDate");
            ObservableCollection<AbstractItem> toReturn = new ObservableCollection<AbstractItem>();

            foreach (var item in items)
            {
                if (item.Title.Contains(this.Text))
                    toReturn.Add(item);
            }

            return toReturn;
        }

        private bool canexecutemethod(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void executemethod(object parameter)
        {
            Name = (string)parameter;
        }
    }
}

