using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Repairs_f;
using Pronets.EntityRequests.Users_f;
using Pronets.Model;
using Pronets.Viev.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace Pronets.VievModel.Repairs_f
{
    public class ReceiptDocumentInspectorVM : RepairsModel
    {
        #region Properties
        private string noWarranty = "Нет";
        public string NoWarranty
        {
            get { return noWarranty; }
            set
            {
                noWarranty = value;
                RaisedPropertyChanged("NoWarranty");
            }
        }
        private string warrantyPronets = "Наша Гарантия";
        public string WarrantyPronets
        {
            get { return warrantyPronets; }
            set
            {
                warrantyPronets = value;
                RaisedPropertyChanged("WarrantyPronets");
            }
        }
        private string warrantyEltex = "Гарантия Элтекс";
        public string WarrantyEltex
        {
            get { return warrantyEltex; }
            set
            {
                warrantyEltex = value;
                RaisedPropertyChanged("WarrantyEltex");
            }
        }
        Dispatcher _dispatcher;
        private List<v_Repairs> searchRepairs = new List<v_Repairs>(); // список для поиска
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
        private ObservableCollection<Clients> recipients = new ObservableCollection<Clients>();
        public ObservableCollection<Clients> Recipients
        {
            get { return recipients; }

            set
            {
                recipients = value;
                RaisedPropertyChanged("Recipients");
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
        private Clients selectedRecipientItem;
        public Clients SelectedRecipientItem
        {
            get { return selectedRecipientItem; }
            set
            {
                selectedRecipientItem = value;
                RaisedPropertyChanged("SelectedRecipientItem");
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
        private v_Repairs selectedItem;
        public v_Repairs SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        private string loadingStatus;
        public string LoadingStatus
        {
            get { return loadingStatus; }
            set
            {
                loadingStatus = value;
                RaisedPropertyChanged("LoadingStatus");
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
        private bool isAdminOrInspector;
        public bool IsAdminOrInspector
        {
            get { return isAdminOrInspector; }
            set
            {
                isAdminOrInspector = value;
                RaisedPropertyChanged("IsAdminOrInspector");
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
                DocumentId = document.Document_Id;
                TitleName = Id = document.Document_Id.ToString() + " От " + document.Date.ToString(/*"dd.MM.yyyy"*/);
                InspectorName = document.Inspector != null ? document.Inspector : "Отсутствует";
                ClientName = document.Client != null ? document.Client : "Отсутствует";
                NoteOfDocument = document.Note;
                GetStatus();
                GetClient();
                GetRepairsAsync();
                DepartureDate = DateTime.Now;
                _dispatcher = Dispatcher.CurrentDispatcher;
                IsAdminOrInspector = UsersRequest.IsAdminOrInspector();
            }
            else
                MessageBox.Show("Не передан экземпляр класса в конструктор!", "Системаня ошибка!");
        }
        #region Select

        /// <summary>
        /// Выгружает список статусов и БД и устанавливает значение по умолчанию
        /// </summary>
        public void GetStatus()
        {
            Statuses.Clear();
            Statuses = StatusesRequests.FillList();
            if (Statuses != null)
            {
                foreach (var status in statuses)
                {
                    if (status.Status == Document.Status)
                        SelectedStatusItem = status;
                }
            }
        }
        /// <summary>
        /// Выгружает список клиентов из БД
        /// </summary>
        public void GetClient()
        {
            Clients.Clear();
            Recipients.Clear();
            Clients = ClientsRequests.FillList();
            Recipients = ClientsRequests.FillList();
            if (Clients != null && Recipients != null)
            {
                foreach (var client in clients)
                {
                    if (client.ClientName == Document.Client)
                    {
                        SelectedClientItem = client;
                        SelectedRecipientItem = client;
                    }
                }
            }
        }

        /// <summary>
        /// Выгружает список ремонтов(асинхронно)
        /// </summary>
        private async void GetRepairsAsync()
        {
            LoadingStatus = "Выполняется загрузка данных в таблицу!";
            V_Repairs.Clear();
            await Task.Run(() => GetRepairs());
            GetCopyRepairsAsync();
            LoadingStatus = "Загрузка завершена";
        }
        /// <summary>
        /// Выгружает список ремонтов
        /// </summary>
        private void GetRepairs()
        {
            try
            {
                foreach (var repair in RepairsRequest.FillList(Document.Document_Id))
                {
                    _dispatcher.Invoke(new Action(() =>
                    {
                        V_Repairs.Add(repair);
                    }));
                }
            }
            catch (Exception)
            {
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
            string recipient = selectedRecipientItem != null ? selectedRecipientItem.ClientName : null;
            var result = MessageBox.Show("Вы действительно хотете записать в базу?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
                        DepartureDate = SelectedStatusItem.Status == "Отправлено заказчику" ? DepartureDate : null

                    };

                    if (SelectedStatusItem.Status == "Отправлено заказчику" || SelectedStatusItem.Status == "Отправлено заказчику(Частично)")
                    {
                        if (RepairsChecked(V_Repairs))
                        {
                            RepairsRequest.EditItemStatusToSendToClient(Document.Document_Id, departureDateNotNull, recipient);
                            editingDocument.Status = "Отправлено заказчику";
                        }
                        else
                        {
                            foreach (var item in V_Repairs)
                            {
                                if (item.IsChecked)
                                {
                                    RepairsRequest.EditItemStatus(item.RepairId, departureDateNotNull, recipient);
                                }
                            }
                            editingDocument.Status = "Отправлено заказчику(Частично)";
                        }
                    }

                    RepairsRequest.EditItemClient(Document.Document_Id, SelectedClientItem.ClientId);
                    ReceiptDocumentRequest.EditItem(editingDocument);

                    GetRepairsAsync();

                    MessageBox.Show("Произведена успешная запись в базу данных!", "Результат");
                }

                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                }

            }
        }
        /// <summary>
        /// проверка на все IsChecked для установки статуса ремонта(Если все без IsChecked => return true)
        /// </summary>
        /// <param name="repairs"></param>
        /// <returns></returns>
        public bool RepairsChecked(ObservableCollection<v_Repairs> repairs)
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
                var result = MessageBox.Show("Вы действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    RepairsRequest.RemoveFromBaseById(SelectedItem.RepairId, out bool ex);
                    if (ex)
                        v_Repairs.RemoveAt(selectedIndex);
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент в списке!", "Ошибка");
        }
        #endregion

        #region Remove Few From Base
        private ICommand removeSelectedCommand;
        public ICommand RemoveSelectedCommand
        {
            get
            {
                if (removeSelectedCommand == null)
                {
                    removeSelectedCommand = new RelayCommand(new Action<object>(RemovRemoveSelected));
                }
                return removeSelectedCommand;
            }
            set
            {
                removeSelectedCommand = value;
                RaisedPropertyChanged("RemoveSelectedCommand");
            }
        }
        private void RemovRemoveSelected(object Parameter)
        {
            var result = MessageBox.Show("Вы действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                foreach (var item in V_Repairs)
                {
                    if (item.IsChecked)
                    {
                        RepairsRequest.RemoveFromBaseById(item.RepairId, out bool ex);
                    }
                }
                GetRepairsAsync();
            }
            else
                MessageBox.Show("Необходимо выбрать элемент в списке!", "Ошибка");
        }
        #endregion

        #region Search

        #region Test search
        //private string _searchString;

        //public string SearchString
        //{
        //    get { return _searchString; }
        //    set
        //    {
        //        if (SetProperty(ref _searchString, value))
        //        {

        //            PropertyInfo prop = typeof(v_Repairs).GetProperty("Serial_Number");
        //            if (prop != null)
        //            {
        //                if (
        //                    v_Repairs.Any(
        //                        p =>
        //                            prop.GetValue(p)
        //                                .ToString()
        //                                .ToLower()
        //                                .Contains(_searchString.ToLower())))
        //                {
        //                    SelectedItem =
        //                        v_Repairs.First(
        //                            p =>
        //                                prop.GetValue(p)
        //                                    .ToString()
        //                                    .ToLower()
        //                                    .Contains(_searchString.ToLower()));
        //                }
        //            }
        //        }
        //    }
        //}
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
                string searchWord = IsChecked != true ? EditChars.ToEnglish(SearchText) : SearchText;
                searchPosition = 0;
                searchRepairs = V_Repairs.Where(r => r.Serial_Number.ToLower().Contains(searchWord.ToLower())).ToList();
                searchCount = searchRepairs.Count;
                if (searchRepairs.Count > 0)
                {
                    SelectedItem = (v_Repairs)searchRepairs[0];
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
            if (searchRepairs.Count > 0)
            {
                string searchWord = IsChecked != true ? EditChars.ToEnglish(SearchText) : SearchText;
                if (!string.IsNullOrWhiteSpace(searchWord) && searchRepairs[0].Serial_Number.ToLower().Contains(searchWord.ToLower()))
                {
                    if (searchPosition < searchRepairs.Count)
                    {
                        SelectedItem = (v_Repairs)searchRepairs[searchPosition];
                        searchPosition++;
                    }
                    else if (searchPosition == searchRepairs.Count)
                    {
                        SelectedItem = (v_Repairs)searchRepairs[0];
                        searchPosition = 1;
                    }
                }
                else
                {
                    searchPosition = 0;
                    searchCount = 0;
                    searchRepairs.Clear();
                }
            }
        }
        #endregion
        #endregion

        #region Copy Repairs
        /// <summary>
        /// <para>Заполняет коллекцию v_RepairsCopy совпадениями по серийным номера</para>
        /// </summary>
        private async void GetCopyRepairsAsync()
        {
            if (V_RepairsCopy != null)
                V_RepairsCopy.Clear();

            await Task.Run(() => GetCopyRepairs());
        }
        private void GetCopyRepairs()
        {
            try
            {
                foreach (var repair in v_Repairs)
                {
                    var copyRepairs = RepairsRequest.GetCopy(repair.RepairId, repair.Serial_Number);
                    if (copyRepairs != null)
                    {
                        foreach (var copyRepair in copyRepairs)
                        {
                            _dispatcher.Invoke(new Action(() =>

                            {
                                V_RepairsCopy.Add(copyRepair);
                            }));
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Refresh command
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
        /// <summary>
        /// Обновляет данные на странице
        /// </summary>
        /// <param name="parametr"></param>
        public void Refresh(object parametr)
        {
            GetStatus();
            GetClient();
            GetRepairsAsync();
        }
        #endregion

        #region Edit Nomenclature
        protected ICommand editNomenclature;
        public ICommand EditNomenclature
        {
            get
            {
                if (editNomenclature == null)
                {
                    editNomenclature = new RelayCommand(new Action<object>(EditingNomenclatureAsync));
                }
                return editNomenclature;
            }
            set
            {
                editNomenclature = value;
                RaisedPropertyChanged("EditNomenclature");
            }
        }
        private async void EditingNomenclatureAsync(object parametr)
        {
            TextVisibility = Visibility.Visible;
            var win = new EditNomenclatureWindow();
            win.ShowDialog();
            Nomenclature nomenclature = (Nomenclature)win.comboBoxNomenclature.SelectedItem;
            if (nomenclature != null)
            {
                await Task.Run(() => EditingNomenclature(nomenclature.Name));
                V_Repairs.Clear();
                await Task.Run(() => GetRepairs());
            }
            TextVisibility = Visibility.Hidden;
        }
        private void EditingNomenclature(string nomenclature)
        {
            foreach (var repair in V_Repairs)
            {
                if (repair.IsChecked)
                {
                    try
                    {
                        RepairsRequest.EditItemNomenclature(repair.RepairId, nomenclature);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(ExceptionMessanger.Message(e));
                    }
                }
            }
        }
        #endregion

        #region Edit Warranty
        protected ICommand editWarranty;
        public ICommand EditWarranty
        {
            get
            {
                if (editWarranty == null)
                {
                    editWarranty = new RelayCommand(new Action<object>(EditingWarrantyAsync));
                }
                return editWarranty;
            }
            set
            {
                editWarranty = value;
                RaisedPropertyChanged("EditWarranty");
            }
        }
        private async void EditingWarrantyAsync(object parametr)
        {
            TextVisibility = Visibility.Visible;
            await Task.Run(() => EditingWarranty(parametr));
            V_Repairs.Clear();
            await Task.Run(() => GetRepairs());
            TextVisibility = Visibility.Hidden;
        }
        private void EditingWarranty(object parametr)
        {
            foreach (var repairCopy in V_RepairsCopy)
            {
                if (repairCopy.IsChecked)
                {
                    var repair = V_Repairs.FirstOrDefault(r => r.Serial_Number == repairCopy.Serial_Number);
                    if (repair != null)
                    {
                        try
                        {
                            RepairsRequest.EditItemWarranty(repair.RepairId, parametr.ToString());
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(ExceptionMessanger.Message(e));
                        }
                    }
                }
            }
        }
        #endregion
    }
}

