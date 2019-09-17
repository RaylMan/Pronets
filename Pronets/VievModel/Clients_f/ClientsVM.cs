using Pronets.Data;
using Pronets.EntityRequests.Clients_f;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Clients_f
{
    public class ClientsVM : VievModelBase
    {
        #region Properties
        public OpenWindowCommand OpenWindowCommand { get; set; } //Команда открытия нового окна
        protected ObservableCollection<Clients> clients = new ObservableCollection<Clients>();
        public ObservableCollection<Clients> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                RaisedPropertyChanged("Clients");
            }
        }

        protected int clientId;
        public int ClientId
        {
            get { return clientId; }
            set
            {
                clientId = value;
                RaisedPropertyChanged("ClientId");
            }
        }
        protected string clientName;
        public string ClientName
        {
            get { return clientName; }
            set
            {
                clientName = value;
                RaisedPropertyChanged("ClientName");
            }
        }
        protected string inn;
        public string Inn
        {
            get { return inn; }
            set
            {
                inn = value;
                RaisedPropertyChanged("Inn");
            }
        }
        protected string contact_Person;
        public string Contact_Person
        {
            get { return contact_Person; }
            set
            {
                contact_Person = value;
                RaisedPropertyChanged("Contact_Person");
            }
        }
        protected string telephone_1;
        public string Telephone_1
        {
            get { return telephone_1; }
            set
            {
                telephone_1 = value;
                RaisedPropertyChanged("Telephone_1");
            }
        }
        protected string telephone_2;
        public string Telephone_2
        {
            get { return telephone_2; }
            set
            {
                telephone_2 = value;
                RaisedPropertyChanged("Telephone_2");
            }
        }
        protected string telephone_3;
        public string Telephone_3
        {
            get { return telephone_3; }
            set
            {
                telephone_3 = value;
                RaisedPropertyChanged("Telephone_3");
            }
        }
        protected string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisedPropertyChanged("Email");
            }
        }
        protected string adress;
        public string Adress
        {
            get { return adress; }
            set
            {
                adress = value;
                RaisedPropertyChanged("Adress");
            }
        }

        protected Clients selectedItem;
        public Clients SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        protected string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                RaisedPropertyChanged("SearchText");
            }
        }
        #endregion

        public ClientsVM()
        {
            clients = ClientsRequests.FillList();
            OpenWindowCommand = new OpenWindowCommand(); // создание экземпляра открытия окна
        }

        #region Delete Command

        protected ICommand removeItem;
        public ICommand RemoveCommand
        {
            get
            {
                if (removeItem == null)
                {
                    removeItem = new RelayCommand(new Action<object>(RemoveItem));
                }
                return removeItem;
            }
            set
            {
                removeItem = value;
                RaisedPropertyChanged("RemoveCommand");
            }
        }
        public void RemoveItem(object Parameter)
        {
            if (selectedItem != null)
            {
                var result = MessageBox.Show("Вы Действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ClientsRequests.RemoveFromBase(selectedItem, out bool ex);
                    if (ex)
                        clients.RemoveAt(selectedIndex);
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент!", "Ошибка");
        }
        #endregion

        #region FillList button command
        protected ICommand fillItems;
        public ICommand FillListCommand
        {
            get
            {
                if (fillItems == null)
                {
                    fillItems = new RelayCommand(new Action<object>(FillList));
                }
                return fillItems;
            }
            set
            {
                fillItems = value;
                RaisedPropertyChanged("FillListCommand");
            }
        }
        protected void FillList(object Parameter)
        {
            clients.Clear();
            clients = ClientsRequests.FillList();
        }
        #endregion

        #region Search Command
        protected ICommand searchItem;
        public ICommand SearchCommand
        {
            get
            {
                if (searchItem == null)
                {
                    searchItem = new RelayCommand(new Action<object>(SearchItem));
                }
                return searchItem;
            }
            set
            {
                searchItem = value;
                RaisedPropertyChanged("SearchCommand");
            }
        }

        public void SearchItem(object Parameter)
        {
            if (SearchText != null && SearchText != "")
            {
                clients.Clear();
                foreach (var client in ClientsRequests.SearchItem(searchText))
                {
                    clients.Add(client);
                }
                SearchText = string.Empty;
            }
        }
        #endregion
    }
}
