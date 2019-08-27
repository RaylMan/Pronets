﻿using Pronets.Data;
using Pronets.EntityRequests.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.VievModel.MainWindows.Pages
{
    class ReceiptDocumentPageVM : VievModelBase
    {
        private ObservableCollection<ReceiptDocument> receiptDocuments;
        public ObservableCollection<ReceiptDocument> ReceiptDocuments
        {
            get { return this.receiptDocuments; }

            set
            {
                receiptDocuments = value;
                RaisedPropertyChanged("ReceiptDocuments");
            }

        }
        private int clientId;
        public int ClientId
        {
            get { return clientId; }
            set
            {
                clientId = value;
                RaisedPropertyChanged("ClientId");
            }
        }
        private int documentId;
        public int DocumentId
        {
            get { return documentId; }
            set
            {
                documentId = value;
                RaisedPropertyChanged("DocumentId");
            }
        }
        private int inspectorId;
        public int InspectorId
        {
            get { return inspectorId; }
            set
            {
                inspectorId = value;
                RaisedPropertyChanged("InspectorId");
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
        public string LNote
        {
            get { return note; }
            set
            {
                note = value;
                RaisedPropertyChanged("Note");
            }
        }
        private string clientName;
        public string ClientName
        {
            get { return clientName; }
            set
            {
                clientName = value;
                RaisedPropertyChanged("ClientName");
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                RaisedPropertyChanged("LastName");
            }
        }


        public ReceiptDocumentPageVM()
        {
            receiptDocuments = ReceiptDocumentRequest.FillList();
        }
        #region Open Document
        
        #endregion
    }
}