using Pronets.Data;
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

namespace Pronets.VievModel.MainWindows.Pages
{
    public class ReceiptDocumentPageVM : VievModelBase
    {
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

        public ReceiptDocumentPageVM()
        {
            ReceiptDocuments.Clear();
            ReceiptDocuments = ReceiptDocumentRequest.v_FillList();
            ReceiptDocuments = new ObservableCollection<v_Receipt_Document>(ReceiptDocuments.OrderByDescending(i => i.Document_Id));
            OpenWindowCommand = new OpenWindowCommand(); // создание экземпляра открытия окна
        }
        #region AddCommand
        private ICommand fillList;
        public ICommand FillListCommand
        {
            get
            {
                if (fillList == null)
                {
                    fillList = new RelayCommand(new Action<object>(FillList));
                }
                return fillList;
            }
            set
            {
                fillList = value;
                RaisedPropertyChanged("FillListCommand");
            }
        }

        public void FillList(object Parameter)
        {
            ReceiptDocuments.Clear();
            ReceiptDocuments = ReceiptDocumentRequest.v_FillList();
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
                var result = MessageBox.Show("Вы Действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    RepairsRequest.RemoveFromBase(SelectedItem.Document_Id, out bool ex);
                    ReceiptDocumentRequest.RemoveFromBase(SelectedItem.Document_Id, out bool ex0);
                    if (ex && ex0)
                        receiptDocuments.RemoveAt(selectedIndex);
                }

            }
            else
                MessageBox.Show("Необходимо выбрать элемент в списке!", "Ошибка");
        }
        #endregion
    }
}
