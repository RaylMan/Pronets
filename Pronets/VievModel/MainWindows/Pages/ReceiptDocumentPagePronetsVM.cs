using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Pronets.VievModel.MainWindows.Pages
{
    public class ReceiptDocumentPagePronetsVM : VievModelBase
    {
        #region Properties
        object e = new object();
        Dispatcher _dispatcher;
        public OpenWindowCommand OpenWindowCommand { get; set; }

        private ObservableCollection<v_Receipt_Document> receiptDocuments = new ObservableCollection<v_Receipt_Document>();
        public ObservableCollection<v_Receipt_Document> ReceiptDocuments
        {
            get { return this.receiptDocuments; }

            set
            {
                receiptDocuments = value;
                RaisedPropertyChanged("ReceiptDocuments");
            }

        }
        private ObservableCollection<Clients> clients = new ObservableCollection<Clients>();
        public ObservableCollection<Clients> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                RaisedPropertyChanged("Clients");
            }
        }
        private ObservableCollection<Statuses> statuses = new ObservableCollection<Statuses>();
        public ObservableCollection<Statuses> Statuses
        {
            get { return statuses; }
            set
            {
                statuses = value;
                RaisedPropertyChanged("Statuses");
            }
        }

        private Clients selectedClientItem;
        public Clients SelectedClientItem
        {
            get { return selectedClientItem; }
            set
            {
                selectedClientItem = value;
                Sort(e);
                RaisedPropertyChanged("SelectedClientItem");
            }
        }
        private Statuses selectedStatusItem;
        public Statuses SelectedStatusItem
        {
            get { return selectedStatusItem; }
            set
            {
                selectedStatusItem = value;
                Sort(e);
                RaisedPropertyChanged("SelectedStatusItem");
            }
        }
        private bool allClients;
        public bool AllClients
        {
            get { return allClients; }
            set
            {
                allClients = value;
               Sort(e);
                RaisedPropertyChanged("AllClients");
            }
        }
        private bool allStatuses;
        public bool AllStatuses
        {
            get { return allStatuses; }
            set
            {
                allStatuses = value;
                Sort(e);
                RaisedPropertyChanged("AllStatuses");
            }
        }

        private int documentId;
        public int DocumentId
        {
            get { return documentId; }
            set
            {
                documentId = value;
                RaisedPropertyChanged("Document_Id");
            }
        }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                RaisedPropertyChanged("Date");
            }
        }
        private DateTime? departureDate;
        public DateTime? DepartureDate
        {
            get { return departureDate; }
            set
            {
                departureDate = value;
                RaisedPropertyChanged("DepartureDate");
            }
        }
        private string note;
        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                RaisedPropertyChanged("Note");
            }
        }
        private string client;
        public string Client
        {
            get { return client; }
            set
            {
                client = value;
                RaisedPropertyChanged("Client");
            }
        }
        private string inspector;
        public string Inspector
        {
            get { return inspector; }
            set
            {
                inspector = value;
                RaisedPropertyChanged("Inspector");
            }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisedPropertyChanged("Status");
            }
        }
        private v_Receipt_Document selectedItem;
        public v_Receipt_Document SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                RaisedPropertyChanged("Count");
            }
        }
        #endregion

        public ReceiptDocumentPagePronetsVM()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            // GetDocumentsAsync(); //Загрузка данных в датагрид происходит в selectedClient, selectedstatus, allclients, allstatuese
            GetContent();
        }

        private void GetContent()
        {
            Statuses.Clear();
            Clients.Clear();
            Statuses = StatusesRequests.FillList();
            Clients = ClientsRequests.FillList();
            AllClients = true;
            AllStatuses = true;
        }
        private async void GetDocumentsAsync(string status)
        {
            ReceiptDocuments.Clear();
            await Task.Run(() => GetDocuments(status));
        }
        private void GetDocuments(string status)
        {
            try
            {
                foreach (var item in ReceiptDocumentRequest.v_FillListPronets(status))
                {
                    _dispatcher.Invoke(new Action(() =>
                    {
                        ReceiptDocuments.Add(item);
                    }));
                }
            }
            catch (Exception) { }

            // ReceiptDocuments = new ObservableCollection<v_Receipt_Document>(ReceiptDocuments.OrderByDescending(i => i.Document_Id));
        }

        #region Sort Command
        private ICommand sortCommand;
        public ICommand SortCommand
        {
            get
            {
                if (sortCommand == null)
                {
                    sortCommand = new RelayCommand(new Action<object>(Sort));
                }
                return sortCommand;
            }
            set
            {
                sortCommand = value;
                RaisedPropertyChanged("SortCommand");
            }
        }
        /// <summary>
        /// Сортировка списка документов
        /// </summary>
        /// <param name="Parameter"></param>
        private void Sort(object Parameter)
        {
            string status = SelectedStatusItem != null ? SelectedStatusItem.Status : null;
            ReceiptDocuments.Clear();
            GetDocumentsAsync(status);
            //ReceiptDocuments = ReceiptDocumentRequest.v_FillListPronets(status);
        }
        #endregion

        #region Remove From Base
        private ICommand removeItem;
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
        private void RemoveItem(object Parameter)
        {
            if (selectedItem != null)
            {
                var result = MessageBox.Show("Вы действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    RepairsRequest.RemoveFromBase(SelectedItem.Document_Id, out bool ex);
                    ReceiptDocumentRequest.RemoveFromBase(SelectedItem.Document_Id, out bool ex0);
                    if (ex && ex0)
                        ReceiptDocuments.RemoveAt(selectedIndex);
                }

            }
            else
                MessageBox.Show("Необходимо выбрать элемент в списке!", "Ошибка");
        }
        #endregion

        #region Refresh Command
        private ICommand refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new RelayCommand(new Action<object>(Refresh));
                }
                return refreshCommand;
            }
            set
            {
                refreshCommand = value;
                RaisedPropertyChanged("RefreshCommand");
            }
        }
        private void Refresh(object parametr)
        {
            GetContent();
        }
        #endregion
    }
}
