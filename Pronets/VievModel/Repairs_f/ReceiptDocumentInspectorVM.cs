using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
        private v_Receipt_Document Document { get; set; }

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
        private string noteOfDocument;
        public string NoteOfDocument
        {
            get { return noteOfDocument; }
            set
            {
                noteOfDocument = value;
                RaisedPropertyChanged("NoteOfDocument");
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
            if (document != null)
            {
                this.Document = document;
                var date = DateTime.MinValue;
                if (document.Date != null)
                    date = (DateTime)document.Date;
                Id = document.Document_Id.ToString() + " От " + document.Date.ToString(/*"dd.MM.yyyy"*/);
                InspectorName = document.Inspector != null ? document.Inspector : "Отсутствует";
                ClientName = document.Client != null ? document.Client : "Отсутствует";
                NoteOfDocument = document.Note;
                statuses = StatusesRequets.FillList();
                GetStatus();
                repairs = RepairsRequest.FillList(document.Document_Id);
            }
            else
                MessageBox.Show("Не передан экземпляр класса в конструктор!", "Системаня ошибка!");
        }
        #region Select Status
        //Устанавливает значение по умолчанию Combobox "статус документа" в соответствии с БД
        public void GetStatus()
        {
            foreach (var status in statuses)
            {
                if (status.Status == Document.Status)
                    SelectedStatusItem = status;
            }
        }
        #endregion
        #region EditCommand
        private ICommand editItem;
        public ICommand EditCommand
        {
            get
            {
                if (editItem == null)
                {
                    editItem = new RelayCommand(new Action<object>(EditDocument));
                }
                return editItem;
            }
            set
            {
                editItem = value;
                RaisedPropertyChanged("EditCommand");
            }
        }

        public void EditDocument(object Parameter)
        {
            var result = MessageBox.Show("Вы Действительно хотете записать в базу?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    ReceiptDocument editingDocument = new ReceiptDocument
                    {
                        DocumentId = Document.Document_Id,
                        Note = NoteOfDocument,
                        Status = SelectedStatusItem.Status,
                    };
                    #region add datagrid not edit
                    //string sn, cm, nm, wt;
                    //for (int i = 0; i < repairs.Count; i++)
                    //{
                    //    nm = repairs[i].Nomenclature1 != null ? repairs[i].Nomenclature1.Name : "Отсутствует";
                    //    //sn = repairs[i].Serial_Number != null ? repairs[i].Serial_Number : "Отсутствует";
                    //    //cm = repairs[i].Claimed_Malfunction != null ? repairs[i].Claimed_Malfunction : "Отсутствует";
                    //    wt = repairs[i].Warrantys != null ? repairs[i].Warrantys.Warranty : "нет";

                    //    repairs[i].DocumentId = documentId;
                    //    repairs[i].Nomenclature = nm;
                    //    //repairs[i].Serial_Number = sn;
                    //    //repairs[i].Claimed_Malfunction = cm;
                    //    repairs[i].Client = selectClientItem.ClientId;
                    //    repairs[i].Date_Of_Receipt = date_Of_Receipt;
                    //    repairs[i].Inspector = selectUserItem.UserId;
                    //    repairs[i].Warranty = wt;
                    //}
                    //repairs.GetHashCode();
                    #endregion
                    ReceiptDocumentRequest.EditItem(editingDocument);
                    MessageBox.Show("Произведена успешная запись в базу данных!", "Результат");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        #endregion
    }
}
