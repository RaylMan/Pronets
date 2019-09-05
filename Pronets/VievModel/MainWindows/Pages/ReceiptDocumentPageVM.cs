using Pronets.Data;
using Pronets.EntityRequests.Other;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pronets.VievModel.MainWindows.Pages
{
    class ReceiptDocumentPageVM : VievModelBase
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
            receiptDocuments.Clear();
            receiptDocuments = ReceiptDocumentRequest.v_FillList();
            OpenWindowCommand = new OpenWindowCommand();
        }
    }
}
