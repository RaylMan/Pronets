using Pronets.Data;
using Pronets.EntityRequests.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Other
{
    public class PartsWindowVM : VievModelBase
    {
        #region Receipt Of Parts Properties
        private ObservableCollection<ReceiptOfParts> receiptOfParts;
        public ObservableCollection<ReceiptOfParts> ReceiptOfParts
        {
            get { return receiptOfParts; }

            set
            {
                receiptOfParts = value;
                RaisedPropertyChanged("ReceiptOfParts");
            }
        }
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisedPropertyChanged("Id");
            }
        }
        private DateTime order_Date;
        public DateTime Order_Date
        {
            get { return order_Date; }
            set
            {
                order_Date = value;
                RaisedPropertyChanged("Order_Date");
            }
        }
        private DateTime date_Arrival;
        public DateTime Date_Arrival
        {
            get { return date_Arrival; }
            set
            {
                date_Arrival = value;
                RaisedPropertyChanged("Date_Arrival");
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
        private ReceiptOfParts selectedDocument;
        public ReceiptOfParts SelectedDocument
        {
            get { return selectedDocument; }
            set
            {
                selectedDocument = value;
                RaisedPropertyChanged("SelectedDocument");
            }
        }
        #endregion

        #region Part order properties
        private ObservableCollection<PartsOrder> partsOrder;
        public ObservableCollection<PartsOrder> PartsOrder
        {
            get { return partsOrder; }

            set
            {
                partsOrder = value;
                RaisedPropertyChanged("PartsOrder");
            }
        }
        private int orderId;
        public int OrderId
        {
            get { return orderId; }
            set
            {
                orderId = value;
                RaisedPropertyChanged("OrderId");
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
        private string partName;
        public string PartName
        {
            get { return partName; }
            set
            {
                partName = value;
                RaisedPropertyChanged("PartName");
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
        private PartsOrder selectedOrder;
        public PartsOrder SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                RaisedPropertyChanged("SelectedOrder");
            }
        }
        #endregion

        #region Parts properties
        private ObservableCollection<Parts> parts;
        public ObservableCollection<Parts> Parts
        {
            get { return parts; }

            set
            {
                parts = value;
                RaisedPropertyChanged("Parts");
            }
        }
        private string part_Name;
        public string Part_Name
        {
            get { return part_Name; }
            set
            {
                part_Name = value;
                RaisedPropertyChanged("Part_Name");
            }
        }
        private decimal part_Price;
        public decimal Part_Price
        {
            get { return part_Price; }
            set
            {
                part_Price = value;
                RaisedPropertyChanged("Part_Price");
            }
        }
        private Parts selectedPart;
        public Parts SelectedPart
        {
            get { return selectedPart; }
            set
            {
                selectedPart = value;
                RaisedPropertyChanged("SelectedPart");
            }
        }
        #endregion

        public PartsWindowVM()
        {
            receiptOfParts = ReceiptOfPartsRequest.FillList();
            partsOrder = PartsOrderRequest.FillList();
            parts = PartsRequest.FillList();
        }
        #region Parts
        #region Add Part
        private ICommand addPart;
        public ICommand AddPartCommand
        {
            get
            {
                if (addPart == null)
                {
                    addPart = new RelayCommand(new Action<object>(AddPart));
                }
                return addPart;
            }
            set
            {
                addPart = value;
                RaisedPropertyChanged("AddPartCommand");
            }
        }
        public void AddPart(object Parameter)
        {
            if (!string.IsNullOrWhiteSpace(Part_Name))
            {
                Parts part = new Parts
                {
                    Part_Name = part_Name,
                    Part_Price = part_Price
                };

                var result = MessageBox.Show("Вы Действительно хотете добавить?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    PartsRequest.AddToBase(part, out bool ex);
                    if (ex) //если ex == true, нет копии в базе, происходит запись в таблицу viev
                    {
                        parts.Add(part);
                    }
                    Part_Name = string.Empty;
                    Part_Price = 0;
                }
            }
            else
                MessageBox.Show("Введите название запчасти", "Ошибка");
        }
        #endregion

        #region RemoveCommand
        protected ICommand removePart;
        public ICommand RemovePartCommand
        {
            get
            {
                if (removePart == null)
                {
                    removePart = new RelayCommand(new Action<object>(RemovePart));
                }
                return removePart;
            }
            set
            {
                removePart = value;
                RaisedPropertyChanged("RemovePartCommand");
            }
        }
        public void RemovePart(object Parameter)
        {
            if (selectedPart != null)
            {
                var result = MessageBox.Show("Вы Действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PartsRequest.RemoveFromBase(selectedPart, out bool ex);
                    if (ex)
                        parts.RemoveAt(SelectedIndex);
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент в таблице!", "Ошибка");
        }
        #endregion

        #endregion
    }
}
