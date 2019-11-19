using Pronets.Data;
using Pronets.EntityRequests.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Other
{
    public class PartsWindowVM : VievModelBase
    {
        #region Receipt Of Parts Properties
        ReceiptOfParts document = new ReceiptOfParts();
        private ObservableCollection<ReceiptOfParts> receiptOfParts = new ObservableCollection<ReceiptOfParts>();
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
        private int selectedReceiptIndex;
        public int SelectedReceiptIndex
        {
            get { return selectedReceiptIndex; }
            set
            {
                selectedReceiptIndex = value;
                RaisedPropertyChanged("SelectedReceiptIndex");
            }
        }
        private ReceiptOfParts selectedDocument;
        public ReceiptOfParts SelectedDocument
        {
            get { return selectedDocument; }
            set
            {
                selectedDocument = value;
                if (selectedDocument != null)
                {
                    document = selectedDocument;
                    SelectedStatus = document.Status;
                    OrderTitleName = $"Номер заказа: {document.Id}";
                    partsOrder.Clear();
                    foreach (var order in PartsOrderRequest.FillList(selectedDocument.Id))
                    {
                        partsOrder.Add(order);
                    }
                }
                RaisedPropertyChanged("SelectedDocument");
            }
        }
        #endregion

        #region Part order properties
        private ObservableCollection<PartsOrder> partsOrder = new ObservableCollection<PartsOrder>();
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
        private int selectedOrderIndex;
        public int SelectedOrderIndex
        {
            get { return selectedOrderIndex; }
            set
            {
                selectedOrderIndex = value;
                RaisedPropertyChanged("SelectedOrderIndex");
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

        private string orderTitleName;
        public string OrderTitleName
        {
            get { return orderTitleName; }
            set
            {
                orderTitleName = value;
                RaisedPropertyChanged("OrderTitleName");
            }
        }
        #endregion

        #region Parts properties
        private List<Parts> searchParts = new List<Parts>();
        private ObservableCollection<Parts> parts = new ObservableCollection<Parts>();
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
        private string searchText;
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

        #region Other properties
        private List<string> statuses;
        public List<string> Statuses
        {
            get
            {
                return statuses = new List<string>
                    {
                        "Ожидает заказа", "Заказано", "Принято", "Отказ"
                    };
            }
            set
            {
                statuses = value;
                RaisedPropertyChanged("Statuses");
            }
        }
        private string selectedStatus;
        public string SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                RaisedPropertyChanged("SelectedStatus");
            }
        }
        #endregion

        public PartsWindowVM()
        {
            ReceiptOfParts = ReceiptOfPartsRequest.FillList();
            Parts = PartsRequest.FillList();
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

                var result = MessageBox.Show("Вы действительно хотете добавить?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
            if (SelectedPart != null)
            {
                var result = MessageBox.Show("Вы действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PartsRequest.RemoveFromBase(SelectedPart, out bool ex);
                    if (ex)
                        Parts.RemoveAt(SelectedIndex);
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент в таблице!", "Ошибка");
        }
        #endregion

        #region Add to Order
        private ICommand doubleClickCommand;
        public ICommand DoubleClickCommand
        {
            get
            {
                if (doubleClickCommand == null)
                {
                    doubleClickCommand = new RelayCommand(new Action<object>(AddToOrder));
                }
                return doubleClickCommand;
            }
            set
            {
                addPart = value;
                RaisedPropertyChanged("DoubleClickCommand");
            }
        }
        public void AddToOrder(object Parameter)
        {
            if (document != null && document.Id != 0)
            {
                PartsOrder.Add(new PartsOrder
                {
                    PartName = selectedPart.Part_Name
                });
            }
            else
            {
                MessageBox.Show("Необходимо выбрать документ", "Ошибка");
            }
        }

        #endregion

        #region Search
        int searchCount = 0; // общеек количество совпадения поиска
        int searchPosition = 0; // номер элемента поиска для выделения строки(FindNext)
        protected ICommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(new Action<object>(Search));
                }
                return searchCommand;
            }
            set
            {
                searchCommand = value;
                RaisedPropertyChanged("SearchCommand");
            }
        }
        public void Search(object Parameter)
        {
            if (!String.IsNullOrWhiteSpace(SearchText))
            {

                searchPosition = 0;
                searchParts = Parts.Where(p => p.Part_Name.ToLower().Contains(SearchText.ToLower())).ToList();
                searchCount = searchParts.Count;
                if (searchParts.Count > 0)
                {
                    SelectedPart = (Parts)searchParts[0];
                    searchPosition++;
                }
            }
        }

        #endregion
        #region Search next
        protected ICommand searchNextCommand;
        public ICommand SearchNextCommand
        {
            get
            {
                if (searchNextCommand == null)
                {
                    searchNextCommand = new RelayCommand(new Action<object>(SearchNext));
                }
                return searchNextCommand;
            }
            set
            {
                searchNextCommand = value;
                RaisedPropertyChanged("SearchNextCommand");
            }
        }
        public void SearchNext(object Parameter)
        {
            if (searchParts.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(SearchText) && searchParts[0].Part_Name.ToLower().Contains(SearchText.ToLower()))
                {
                    if (searchPosition < searchParts.Count)
                    {
                        SelectedPart = (Parts)searchParts[searchPosition];
                        searchPosition++;
                    }
                    else if (searchPosition == searchParts.Count)
                    {
                        SelectedPart = (Parts)searchParts[0];
                        searchPosition = 1;
                    }
                }
                else
                {
                    searchPosition = 0;
                    searchCount = 0;
                    searchParts.Clear();
                }
            }
        }
        #endregion
        #endregion

        #region Parts Order
        #region Add Part
        private ICommand addPartOrder;
        public ICommand AddPartOrderCommand
        {
            get
            {
                if (addPartOrder == null)
                {
                    addPartOrder = new RelayCommand(new Action<object>(AddPartOrder));
                }
                return addPartOrder;
            }
            set
            {
                addPart = value;
                RaisedPropertyChanged("AddPartOrderCommand");
            }
        }
        public void AddPartOrder(object Parameter)
        {
            if (partsOrder != null && document != null && document.Id != 0)
            {

                var result = MessageBox.Show("Вы действительно хотете сохранить?\nПроверьте правильность данных!", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var order in PartsOrder)
                    {
                        if (order.OrderId == 0)
                        {
                            order.DocumentId = document.Id;
                            PartsOrderRequest.AddToBase(order);
                        }
                    }
                    if (SelectedStatus == "Принято")
                    {
                        document.Status = selectedStatus;
                        document.Date_Arrival = DateTime.Now.Date;
                    }
                    else
                        document.Status = selectedStatus;
                    ReceiptOfPartsRequest.EditItem(document);
                    ReceiptOfParts.Clear();
                    ReceiptOfParts = ReceiptOfPartsRequest.FillList();
                }
            }
        }
        #endregion

        #region RemoveCommand
        protected ICommand removeOrder;
        public ICommand RemoveOrderCommand
        {
            get
            {
                if (removeOrder == null)
                {
                    removeOrder = new RelayCommand(new Action<object>(RemoveOrder));
                }
                return removeOrder;
            }
            set
            {
                removeOrder = value;
                RaisedPropertyChanged("RemoveOrderCommand");
            }
        }
        public void RemoveOrder(object Parameter)
        {
            if (SelectedOrder != null)
            {
                var result = MessageBox.Show("Вы действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedOrder.DocumentId != 0)
                    {
                        PartsOrderRequest.RemoveFromBase(SelectedOrder, out bool ex);
                        if (ex)
                           PartsOrder.RemoveAt(SelectedOrderIndex);
                    }
                    else
                        PartsOrder.RemoveAt(SelectedOrderIndex);
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент в таблице!", "Ошибка");
        }
        #endregion
        #endregion

        #region Recipe of order

        #region Add document
        private ICommand addRecipe;
        public ICommand AddRecipeCommand
        {
            get
            {
                if (addRecipe == null)
                {
                    addRecipe = new RelayCommand(new Action<object>(AddRecipe));
                }
                return addRecipe;
            }
            set
            {
                addPart = value;
                RaisedPropertyChanged("AddRecipeCommand");
            }
        }
        public void AddRecipe(object Parameter)
        {
            ReceiptOfParts recipe = new ReceiptOfParts
            {
                Order_Date = DateTime.Now.Date,
                Status = "Ожидает заказа"
            };
            ReceiptOfPartsRequest.AddToBase(recipe);
            ReceiptOfParts.Clear();
            ReceiptOfParts = ReceiptOfPartsRequest.FillList();
        }
        #endregion

        #region RemoveCommand
        protected ICommand removeDocument;
        public ICommand RemoveDocumentCommand
        {
            get
            {
                if (removeDocument == null)
                {
                    removeDocument = new RelayCommand(new Action<object>(RemoveDocument));
                }
                return removeDocument;
            }
            set
            {
                removeDocument = value;
                RaisedPropertyChanged("RemoveDocumentCommand");
            }
        }
        public void RemoveDocument(object Parameter)
        {
            if (SelectedDocument != null)
            {
                var result = MessageBox.Show("Вы действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    PartsOrderRequest.RemoveFromBase(SelectedDocument.Id, out bool ex1);
                    if (ex1)
                    {
                        ReceiptOfPartsRequest.RemoveFromBase(SelectedDocument, out bool ex);
                        if (ex)
                            ReceiptOfParts.RemoveAt(SelectedReceiptIndex);
                        PartsOrder.Clear();
                        document = null;
                    }

                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент в таблице!", "Ошибка");
        }
        #endregion

        #endregion
    }
}
