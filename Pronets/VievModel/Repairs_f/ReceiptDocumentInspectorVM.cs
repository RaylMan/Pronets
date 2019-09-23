using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Clients_f;
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
        private ObservableCollection<Clients> clients;
        public ObservableCollection<Clients> Clients
        {
            get { return clients; }

            set
            {
                clients = value;
                RaisedPropertyChanged("Clients");
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
        private Clients selectedClientItem;
        public Clients SelectedClientItem
        {
            get { return selectedClientItem; }
            set
            {
                selectedClientItem = value;
                RaisedPropertyChanged("SelectedClientItem");
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
        private string titleName;
        public string TitleName
        {
            get { return titleName; }
            set
            {
                titleName = "Приходная накладная №" + value;
                RaisedPropertyChanged("TitleName");
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
                clientName = value;
                RaisedPropertyChanged("ClientName");
            }
        }
        private Repairs selectedItem;
        public Repairs SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
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
                TitleName = Id = document.Document_Id.ToString() + " От " + document.Date.ToString(/*"dd.MM.yyyy"*/);
                InspectorName = document.Inspector != null ? document.Inspector : "Отсутствует";
                ClientName = document.Client != null ? document.Client : "Отсутствует";
                NoteOfDocument = document.Note;
                GetStatus();
                GetClient();
                v_Repairs = RepairsRequest.FillList(document.Document_Id);
                DepartureDate = DateTime.Now;
            }
            else
                MessageBox.Show("Не передан экземпляр класса в конструктор!", "Системаня ошибка!");
        }
        #region Select
        //Устанавливает значение по умолчанию Combobox "статус документа" в соответствии с БД
        public void GetStatus()
        {
            statuses = StatusesRequests.FillList();
            foreach (var status in statuses)
            {
                if (status.Status == Document.Status)
                    SelectedStatusItem = status;
            }
        }
        public void GetClient()
        {
            clients = ClientsRequests.FillList();
            foreach (var client in clients)
            {
                if (client.ClientName == Document.Client)
                    SelectedClientItem = client;
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
            DateTime departureDateNotNull = DepartureDate ?? DateTime.Now;
            var result = MessageBox.Show("Вы Действительно хотете записать в базу?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    ReceiptDocument editingDocument = new ReceiptDocument
                    {
                        DocumentId = Document.Document_Id,
                        ClientId = SelectedClientItem.ClientId,
                        Note = NoteOfDocument,
                        Status = SelectedStatusItem.Status,
                        DepartureDate = SelectedStatusItem.Status == "Отправлен заказчику" ? DepartureDate : null

                    };

                    if (SelectedStatusItem.Status == "Отправлен заказчику" || SelectedStatusItem.Status == "Отправлен заказчику(Частично)")
                    {
                        if (RepairsChecked(V_Repairs))
                        {
                            RepairsRequest.EditItemStatusToSendToClient(Document.Document_Id, departureDateNotNull);
                            editingDocument.Status = "Отправлен заказчику";
                        }
                        else
                        {
                            foreach (var item in V_Repairs)
                            {
                                if (item.IsChecked)
                                {
                                    RepairsRequest.EditItemStatus(item.RepairId, departureDateNotNull);
                                }
                            }
                            editingDocument.Status = "Отправлен заказчику(Частично)";
                        }
                    }
                    RepairsRequest.EditItemClient(Document.Document_Id, SelectedClientItem.ClientId);
                    ReceiptDocumentRequest.EditItem(editingDocument);

                    V_Repairs.Clear(); // обновить таблицу реомнтов
                    foreach (var repair in RepairsRequest.FillList(Document.Document_Id))
                    {
                        V_Repairs.Add(repair);
                    }

                    MessageBox.Show("Произведена успешная запись в базу данных!", "Результат");
                }

                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
               
            }
        }
        public bool RepairsChecked(ObservableCollection<v_Repairs> repairs) // проверка на все IsChecked для установки статуса ремонта
        {
            int count = 0;
            if (repairs != null)
            {
                foreach (var item in repairs)
                {
                    if (item.IsChecked)
                        count++;
                }
            }
            return count == repairs.Count || count == 0 ? true : false;
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
                    RepairsRequest.RemoveFromBase(SelectedItem, out bool ex);
                    if (ex)
                        repairs.RemoveAt(selectedIndex);
                }

            }
            else
                MessageBox.Show("Необходимо выбрать элемент в списке!", "Ошибка");
        }
        #endregion
    }
}

