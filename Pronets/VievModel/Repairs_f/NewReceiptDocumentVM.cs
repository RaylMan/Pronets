using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Nomenclature_f;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Repairs_f;
using Pronets.EntityRequests.Users_f;
using Pronets.Model;
using Pronets.Navigation.WindowsNavigation;
using Pronets.Viev.Repairs_f;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Repairs_f
{
    public class NewReceiptDocumentVM : VievModelBase
    {
        #region Property
        Users defaultUser;
        bool isOldDocument = false;
        public OpenWindowCommand OpenWindowCommand { get; set; }
        private ObservableCollection<Repairs> repairs = new ObservableCollection<Repairs>();
        public ObservableCollection<Repairs> Repairs
        {
            get { return this.repairs; }

            set
            {
                repairs = value;
                RaisedPropertyChanged("Repairs");
            }
        }


        private ObservableCollection<Nomenclature> nomenclatures = new ObservableCollection<Nomenclature>();
        public ObservableCollection<Nomenclature> Nomenclatures
        {
            get
            { return this.nomenclatures; }

            set
            {
                nomenclatures = value;
                RaisedPropertyChanged("Nomenclatures");
            }

        }
        private ObservableCollection<Warrantys> warrantys = new ObservableCollection<Warrantys>();
        public ObservableCollection<Warrantys> Warrantys
        {
            get { return this.warrantys; }

            set
            {
                warrantys = value;
                RaisedPropertyChanged("Warrantys");
            }

        }

        private ObservableCollection<Users> users = new ObservableCollection<Users>();
        public ObservableCollection<Users> Users
        {
            get { return this.users; }

            set
            {
                users = value;
                RaisedPropertyChanged("Users");
            }
        }
        private ObservableCollection<Clients> clients = new ObservableCollection<Clients>();
        public ObservableCollection<Clients> Clients
        {
            get { return this.clients; }

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

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisedPropertyChanged("Name");
            }
        }
        private string nomenclature;
        public string Nomenclature
        {
            get { return nomenclature; }
            set
            {
                nomenclature = value;
                RaisedPropertyChanged("Nomenclature");
            }
        }
        private string serial_Number;
        public string Serial_Number
        {
            get { return serial_Number; }
            set
            {
                serial_Number = value;
                RaisedPropertyChanged("Serial_Number");
            }
        }
        private string claimed_Malfunction;
        public string Claimed_Malfunction
        {
            get { return claimed_Malfunction; }
            set
            {
                claimed_Malfunction = value;
                RaisedPropertyChanged("Claimed_Malfunction");
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
        private string textDocumentId;
        public string TextDocumentId
        {
            get { return textDocumentId; }
            set
            {
                textDocumentId = "Приходная накладная № " + value;
                RaisedPropertyChanged("TextDocumentId");
            }
        }
        private int userId;
        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                RaisedPropertyChanged("UserId");
            }
        }
        private string warranty;
        public string Warranty
        {
            get { return warranty; }
            set
            {
                warranty = value;
                RaisedPropertyChanged("Warranty");
            }
        }
        private DateTime date_Of_Receipt;
        public DateTime Date_Of_Receipt
        {
            get { return date_Of_Receipt; }
            set
            {
                date_Of_Receipt = value;
                RaisedPropertyChanged("Date_Of_Receipt");
            }
        }

        private Users selectUserItem;
        public Users SelectUserItem
        {
            get { return selectUserItem; }
            set
            {
                selectUserItem = value;
                RaisedPropertyChanged("SelectUserItem");
            }
        }
        private Clients selectClientItem;
        public Clients SelectClientItem
        {
            get { return selectClientItem; }
            set
            {
                selectClientItem = value;
                RaisedPropertyChanged("SelectClientItem");
            }
        }
        private Nomenclature selectedNomenclatureItem;
        public Nomenclature SelectedNomenclatureItem
        {
            get { return selectedNomenclatureItem; }
            set
            {
                selectedNomenclatureItem = value;
                RaisedPropertyChanged("SelectedNomenclatureItem");
            }
        }
        private Nomenclature nomenclature1;
        public Nomenclature Nomenclature1
        {
            get { return nomenclature1; }
            set
            {
                nomenclature1 = value;
                RaisedPropertyChanged("Nomenclature1");
            }
        }
        private Statuses selectedStatus;
        public Statuses SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                RaisedPropertyChanged("SelectedStatus");
            }
        }
        private Warrantys selectedWarrantyItem;
        public Warrantys SelectedWarrantyItem
        {
            get { return selectedWarrantyItem; }
            set
            {
                selectedWarrantyItem = value;
                RaisedPropertyChanged("SelectedWarrantyItem");
            }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                if (value == isChecked) return;
                isChecked = value;
                RaisedPropertyChanged();
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


        #endregion
        public NewReceiptDocumentVM()
        {
            GetContent();
        }
        public NewReceiptDocumentVM(string clientName)
        {
            GetContent();
            GetClient(clientName);
        }
        public NewReceiptDocumentVM(int documentId)
        {
            DocumentId = documentId;
            isOldDocument = true;
            GetContent();
            GetClient();
        }
        #region methods

        private void GetContent()
        {
            users = UsersRequest.FillList();
            clients = ClientsRequests.FillList();
            nomenclatures = NomenclatureRequest.FillList();
            date_Of_Receipt = DateTime.Now;
            repairs = new ObservableCollection<Repairs>();
            warrantys = new ObservableCollection<Warrantys>();
            warrantys.Add(new Warrantys { Warranty = "Нет" });
            warrantys.Add(new Warrantys { Warranty = "Гарантия Элтекс" });
            warrantys.Add(new Warrantys { Warranty = "Наша Гарантия" });
            GetDefaultUser();
            SetStatus();
            // GetRepairsFromDocument();
            OpenWindowCommand = new OpenWindowCommand();
        }
        private void SetStatus()
        {
            Statuses.Clear();
            Statuses = StatusesRequests.FillList();

            foreach (var status in Statuses)
            {
                if (status.Status == "Принято")
                    SelectedStatus = status;
            }
        }
        private void GetDefaultUser()
        {
            defaultUser = UsersRequest.GetUser(Properties.Settings.Default.DefaultUserId);
        }
        private void GetRepairsFromDocument()
        {
            if (isOldDocument)
            {
                Repairs = RepairsRequest.FillListDocument(DocumentId);
            }
        }
        private void GetClient()
        {
            foreach (var item in clients)
            {
                if (item.ClientName == ReceiptDocumentRequest.GetDocument(DocumentId).Client)
                    SelectClientItem = item;
            }
        }
        private void GetClient(string clientName)
        {
            foreach (var item in clients)
            {
                if (item.ClientName == clientName)
                    SelectClientItem = item;
            }
        }

        private ObservableCollection<v_Repairs> GetCopyRepairs(ObservableCollection<Repairs> repairs)
        {
            ObservableCollection<v_Repairs> copyRepairs = new ObservableCollection<v_Repairs>();
            foreach (var repair in repairs)
            {
                var query = RepairsRequest.GetCopy(repair.RepairId, repair.Serial_Number);
                if (query != null)
                {
                    foreach (var item in query)
                    {
                        copyRepairs.Add(item);
                    }
                }
            }
            return copyRepairs;
        }
        #endregion

        #region AddCommand
        private ICommand addItem;
        public ICommand AddCommand
        {
            get
            {
                if (addItem == null)
                {
                    addItem = new RelayCommand(new Action<object>(AddRepairAsync));
                }
                return addItem;
            }
            set
            {
                addItem = value;
                RaisedPropertyChanged("AddCommand");
            }
        }
        public async void AddRepairAsync(object Parameter)
        {
            await Task.Factory.StartNew(AddRepair);
        }
        public void AddRepair(object Parameter)
        {
            if (!isOldDocument)
                AddRepair();
            else
                AddRepairInOldDocument();
        }
        /// <summary>
        /// Добавляет в базу данных новый документ и ремонты к нему
        /// </summary>
        public void AddRepair()
        {
            try
            {
                string textError = "В базе данных уже приняты: ";
                if (selectClientItem == null && defaultUser == null && SelectedStatus == null)
                {
                    MessageBox.Show("Необходимо выбрать клиента и статус!", "Ошибка");
                    return;
                }
                if (!IsAllHaveNomenclature(out string error))
                {
                    MessageBox.Show($"Установите номенклатуру: {error}", "Ошибка");
                    return;
                }

                var result = MessageBox.Show("Вы действительно хотете записать в базу?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var defaultEngineer = UsersRequest.GetEngineer("Не выбран");
                    ReceiptDocument newReceiptDocument = new ReceiptDocument
                    {
                        ClientId = selectClientItem.ClientId,
                        InspectorId = defaultUser.UserId,
                        Date = DateTime.Now,
                        Status = SelectedStatus.Status,
                        Note = NoteOfDocument
                    };
                    ReceiptDocumentRequest.AddToBase(newReceiptDocument);
                    DocumentId = ReceiptDocumentRequest.GetDocumentID();
                    string nm, wt;
                    for (int i = 0; i < repairs.Count; i++)
                    {
                        if (selectClientItem.ClientName == "Пронетс" && RepairsRequest.IsPronetsOldRepair(repairs[i].Serial_Number))
                        {
                            textError += $"{repairs[i].Serial_Number}, ";
                            repairs[i].NotMapped = true;
                        }


                        nm = repairs[i].Nomenclature1 != null ? repairs[i].Nomenclature1.Name : "Отсутствует";
                        wt = repairs[i].Warrantys != null ? repairs[i].Warrantys.Warranty : "Нет";

                        repairs[i].DocumentId = DocumentId;
                        repairs[i].Nomenclature = nm;
                        repairs[i].Client = selectClientItem.ClientId;
                        repairs[i].Status = SelectedStatus.Status;
                        repairs[i].Date_Of_Receipt = date_Of_Receipt;
                        repairs[i].Engineer = defaultEngineer.Id;
                        repairs[i].Inspector = defaultUser.UserId;
                        repairs[i].Warranty = wt;
                    }

                    RepairsRequest.AddToBase(repairs);
                    if (textError.Length > 28)
                    {
                        MessageBox.Show(textError);
                    }
                    MessageBox.Show("Произведена успешная запись в базу данных!", "Результат");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionMessanger.Message(e));
            }
            
        }


        /// <summary>
        /// Добавляет в базу данных ремонты к существующему документу
        /// </summary>
        public void AddRepairInOldDocument()
        {
            string textError = "В базе данных уже приняты: ";
            try
            {
                if (selectClientItem != null && defaultUser != null)
                {
                    if (IsAllHaveNomenclature(out string error))
                    {
                        var result = MessageBox.Show("Вы действительно хотете записать в базу?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            var defaultEngineer = UsersRequest.GetEngineer("Не выбран");

                            string nm, wt;
                            for (int i = 0; i < repairs.Count; i++)
                            {
                                if (selectClientItem.ClientName == "Пронетс" && RepairsRequest.IsPronetsOldRepair(repairs[i].Serial_Number))
                                {
                                    textError += $"{repairs[i].Serial_Number}, ";
                                    repairs[i].NotMapped = true;
                                }

                                nm = repairs[i].Nomenclature1 != null ? repairs[i].Nomenclature1.Name : "Отсутствует";
                                wt = repairs[i].Warrantys != null ? repairs[i].Warrantys.Warranty : "Нет";

                                repairs[i].DocumentId = DocumentId;
                                repairs[i].Nomenclature = nm;
                                repairs[i].Client = selectClientItem.ClientId;
                                repairs[i].Status = "Принято";
                                repairs[i].Date_Of_Receipt = date_Of_Receipt;
                                repairs[i].Engineer = defaultEngineer.Id;
                                repairs[i].Inspector = defaultUser.UserId;
                                repairs[i].Warranty = wt;
                            }

                            RepairsRequest.AddToBase(repairs);
                            if (textError.Length > 28)
                            {
                                MessageBox.Show(textError + "\nВ базу данных не внесены.");
                            }
                            
                            MessageBox.Show("Произведена успешная запись в базу данных!", "Результат");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Установите номенклатуру: {error}", "Ошибка");
                    }
                }
                else
                    MessageBox.Show("Необходимо выбрать клиента!", "Ошибка");
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionMessanger.Message(e));
            }
        }
        /// <summary>
        /// Проверка на неустановленное поле номенклатура
        /// </summary>
        /// <returns>Возращает строку с серийными номерами, у которых нет номенклатуры</returns>
        private bool IsAllHaveNomenclature(out string error)
        {
            bool isHave = true;
            error = null;
            foreach (var repair in Repairs)
            {
                if (repair.Nomenclature1 == null)
                {
                    error += $" {repair.Serial_Number},";
                    isHave = false;
                }
            }
            return isHave;
        }
        #endregion
        #region FindCopiesCommand
        private ICommand findCopiesCommand;
        public ICommand FindCopiesCommand
        {
            get
            {
                if (findCopiesCommand == null)
                {
                    findCopiesCommand = new RelayCommand(new Action<object>(FindCopies));
                }
                return findCopiesCommand;
            }
            set
            {
                findCopiesCommand = value;
                RaisedPropertyChanged("FindCopiesCommand");
            }
        }
        private void FindCopies(object parametr)
        {
            var copyRepairs = GetCopyRepairs(repairs);
            if (copyRepairs.Count > 0)
            {
                NewReceiptDocumentCopies win = new NewReceiptDocumentCopies(copyRepairs, repairs);
                win.Show();
            }
            else MessageBox.Show("Повторных ремонтов не найдено!");
        }
        #endregion

        #region Delete Command
        private ICommand deleteItem;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteItem == null)
                {
                    deleteItem = new RelayCommand(new Action<object>(DeleteRepair));
                }
                return deleteItem;
            }
            set
            {
                deleteItem = value;
                RaisedPropertyChanged("DeleteCommand");
            }
        }

        public void DeleteRepair(object Parameter)
        {
            if (selectedIndex >= 0 && selectedIndex < Repairs.Count)
            {
                Repairs.RemoveAt(SelectedIndex);
            }
        }
        #endregion

        #region Add row
        private ICommand addRowCommand;
        public ICommand AddRowCommand
        {
            get
            {
                if (addRowCommand == null)
                {
                    addRowCommand = new RelayCommand(new Action<object>(AddRow));
                }
                return addRowCommand;
            }
            set
            {
                addRowCommand = value;
                RaisedPropertyChanged("AddRowCommand");
            }
        }

        public void AddRow(object Parameter)
        {
            repairs.Add(new Repairs());
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
            clients.Clear();
            nomenclatures.Clear();
            Clients = ClientsRequests.FillList();
            Nomenclatures = NomenclatureRequest.FillList();
        }
        #endregion

        #region Clear command
        private ICommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (clearCommand == null)
                {
                    clearCommand = new RelayCommand(new Action<object>(Clear));
                }
                return clearCommand;
            }
            set
            {
                clearCommand = value;
                RaisedPropertyChanged("ClearCommand");
            }
        }
        /// <summary>
        /// Обновляет данные на странице
        /// </summary>
        /// <param name="parametr"></param>
        public void Clear(object parametr)
        {
            repairs.Clear();
            NoteOfDocument = string.Empty;
        }
        #endregion
    }
}
