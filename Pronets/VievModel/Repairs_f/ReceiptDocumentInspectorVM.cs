using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.VievModel.Repairs_f
{
    class ReceiptDocumentInspectorVM : RepairsModel
    {
        #region Properties
        private ObservableCollection<Statuses> statuses;
        public ObservableCollection<Statuses> Statuses
        {
            get { return statuses; }

            set
            {
                statuses = value;
                RaisedPropertyChanged("Statuses");
            }
        }
        
        private Statuses selectedStatusItem;
        public Statuses SelectedStatusItem
        {
            get { return selectedStatusItem; }
            set
            {
                selectedStatusItem = value;
                RaisedPropertyChanged("SelectedStatusItem");
            }
        }
        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = "Приходная накладная №" + value;
                RaisedPropertyChanged("Id");
            }
        }
        private string inspectorName;
        public string InspectorName
        {
            get { return inspectorName; }
            set
            {
                inspectorName = "Приемщик:  " + value;
                RaisedPropertyChanged("InspectorName");
            }
        }
        private string clientName;
        public string ClientName
        {
            get { return clientName; }
            set
            {
                clientName = "Клиент:  " + value;
                RaisedPropertyChanged("ClientName");
            }
        }
        
        #endregion
        public ReceiptDocumentInspectorVM(v_Receipt_Document document)
        {
            var date = DateTime.MinValue;
            if (document.Date != null)
                date = (DateTime)document.Date;
            Id = document.Document_Id.ToString() + " От " + document.Date.ToString(/*"dd.MM.yyyy"*/);
            InspectorName = document.Inspector != null ? document.Inspector : "Отсутствует";
            ClientName = document.Client != null ? document.Client : "Отсутствует";
            statuses = StatusesRequets.FillList();
            repairs = RepairsRequest.FillList(document.Document_Id);
        }
    }
}
