using Pronets.Data;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using Pronets.Repository;
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
    public class DefectsPageVM : VievModelBase
    {
        #region Properties
       // BaseRepository repo;
        private ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
        public ObservableCollection<v_Repairs> V_Repairs
        {
            get { return v_Repairs; }

            set
            {
                v_Repairs = value;
                RaisedPropertyChanged("V_Repairs");
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
        private ObservableCollection<DocumentType> documentTypes = new ObservableCollection<DocumentType>();
        public ObservableCollection<DocumentType> DocumentTypes
        {
            get { return documentTypes; }
            set
            {
                documentTypes = value;
                RaisedPropertyChanged("DocumentTypes");
            }
        }
        private DocumentType selectedTypeItem;
        public DocumentType SelectedTypeItem
        {
            get { return selectedTypeItem; }
            set
            {
                selectedTypeItem = value;
                RaisedPropertyChanged("SelectedTypeItem");
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
        private ObservableCollection<SerialNumbers> serialNumbers = new ObservableCollection<SerialNumbers>();
        public ObservableCollection<SerialNumbers> SerialNumbers
        {
            get { return serialNumbers; }

            set
            {
                serialNumbers = value;
                RaisedPropertyChanged("SerialNumbers");
            }
        }
        private int selectedSerialIndex;
        public int SelectedSerialIndex
        {
            get { return selectedSerialIndex; }
            set
            {
                selectedSerialIndex = value;
                RaisedPropertyChanged("SelectedSerialIndex");
            }
        }
        private int selectedRepairIndex;
        public int SelectedRepairIndex
        {
            get { return selectedRepairIndex; }
            set
            {
                selectedRepairIndex = value;
                RaisedPropertyChanged("SelectedRepairIndex");
            }
        }
        private SerialNumbers selectedSerialItem;
        public SerialNumbers SelectedSerialItem
        {
            get { return selectedSerialItem; }
            set
            {
                selectedSerialItem = value;
                RaisedPropertyChanged("SelectedSerialItem");
            }
        }
        private bool isDocument;
        public bool IsDocument
        {
            get { return isDocument; }
            set
            {
                isDocument = value;
                RaisedPropertyChanged("IsDocument");
            }
        }

        private string serialsCount;
        public string SerialsCount
        {
            get { return "Количество: " + serialsCount; }
            set
            {
                serialsCount = value;
                RaisedPropertyChanged("SerialsCount");
            }
        }
        private string repairsCount;
        public string RepairsCount
        {
            get { return "Количество: " + repairsCount; }
            set
            {
                repairsCount = value;
                RaisedPropertyChanged("RepairsCount");
            }
        }
        #endregion
        public DefectsPageVM()
        {
            //repo = new BaseRepository();
            //Clients = repo.GetClients();
            Clients = ClientsRequests.FillList();
            SetDocumentTypes();
        }
        private void SetDocumentTypes()
        {
            DocumentTypes.Add(new DocumentType { Type = "Дефектовка" });
            DocumentTypes.Add(new DocumentType { Type = "Отправка клиенту" });
            SelectedTypeItem = DocumentTypes[0];
            GetCounts();
        }
        /// <summary>
        /// Подсчет общего количества строк в таблицах серийных номеров и ремонтов
        /// </summary>
        private void GetCounts()
        {
            SerialsCount = serialNumbers.Count.ToString();
            RepairsCount = V_Repairs.Count.ToString();
        }

        #region AddToTable
        private ICommand addToTableCommand;
        public ICommand AddToTableCommand
        {
            get
            {
                if (addToTableCommand == null)
                {
                    addToTableCommand = new RelayCommand(new Action<object>(AddToTable));
                }
                return addToTableCommand;
            }
            set
            {
                addToTableCommand = value;
                RaisedPropertyChanged("AddToTableCommand");
            }
        }
        public void AddToTable(object Parameter)
        {
            string error = null;
            V_Repairs.Clear();
            if (!IsDocument)
            {
                foreach (var serial in serialNumbers)
                {
                    var repairs = RepairsRequest.v_FillList(serial.Serial);
                    if(repairs != null)
                    {
                        if (repairs.Count > 0)
                        {
                            foreach (var repair in repairs)
                            {
                                V_Repairs.Add(repair);
                            }
                        }
                        else
                            error += $" {serial.Serial},";
                    }
                }
                if (error != null)
                {
                   
                    MessageBox.Show($"В базе данных отсутствуют:{error.Remove(error.Length -1)}", "");
                }
            }
            else
            {
                foreach (var serial in serialNumbers)
                {
                    int.TryParse(serial.Serial, out int documentId);
                    var repairs = RepairsRequest.v_FillList(documentId);
                    if(repairs != null)
                    {
                        foreach (var repair in repairs)
                        {
                            V_Repairs.Add(repair);
                        }
                    }
                }
            }
            GetCounts();
        }

        /// <summary>
        /// Сортирует список ремонтов, вначале все готовые, а затем кооторые не смогли сделать и возвращает коллекцию
        /// </summary>
        /// <param name="repairs"></param>
        /// <returns></returns>
        private ObservableCollection<v_Repairs> GetSortingRepairs(ObservableCollection<v_Repairs> repairs)
        {
            ObservableCollection<v_Repairs> repairsTable = new ObservableCollection<v_Repairs>();
            if (repairs != null)
            {
                foreach (var repair in repairs)
                {
                    if (repair.Status != "Восстановлению не подлежит" ||
                        repair.Status != "Донор" ||
                        repair.Status != "Не смогли починить" ||
                        repair.Status != "Утеряно")
                    {
                        repairsTable.Add(repair);
                    }
                }

                foreach (var repair in repairs)
                {
                    if (repair.Status == "Восстановлению не подлежит" ||
                        repair.Status == "Донор" ||
                        repair.Status == "Не смогли починить" ||
                        repair.Status == "Утеряно")
                    {
                        repairsTable.Add(repair);
                    }
                }
            }
            return repairsTable;
        }
        #endregion

        #region SendingCommand
        private ICommand sendingCommand;
        public ICommand SendingCommand
        {
            get
            {
                if (sendingCommand == null)
                {
                    sendingCommand = new RelayCommand(new Action<object>(SendToClient));
                }
                return sendingCommand;
            }
            set
            {
                sendingCommand = value;
                RaisedPropertyChanged("SendingCommand");
            }
        }
        private void SendToClient(object parametr)
        {
            if (SelectedTypeItem.Type == "Отправка клиенту" && V_Repairs.Count > 0)
            {
                if (selectedClientItem != null)
                {
                    var result = MessageBox.Show("Вы действительно хотите совершить операцию?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (var repair in V_Repairs)
                        {
                            RepairsRequest.EditItemStatus(repair.RepairId, DateTime.Now, selectedClientItem.ClientName);
                        }
                        MessageBox.Show("Успешная операция!");
                    }
                }
                else
                    MessageBox.Show("Необходимо выбрать клиента!", "Ошибка");
            }
            object e = null;
            AddToTable(e);//обновление списка
            GetCounts();
        }
        #endregion

        #region Clear
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
        private void Clear(object Parameter)
        {
            SerialNumbers.Clear();
            V_Repairs.Clear();
            GetCounts();
        }
        #endregion

        #region Delete Row
        private ICommand removeSerialCommand;
        public ICommand RemoveSerialCommand
        {
            get
            {
                if (removeSerialCommand == null)
                {
                    removeSerialCommand = new RelayCommand(new Action<object>(RemoveSerial));
                }
                return removeSerialCommand;
            }
            set
            {
                removeSerialCommand = value;
                RaisedPropertyChanged("RemoveSerialCommand");
            }
        }
        /// <summary>
        /// Удаление группы элементов по серийному номеру или номеру накладной
        /// </summary>
        /// <param name="Parameter"></param>
        private void RemoveSerial(object Parameter)
        {
            if (selectedSerialItem != null)
            {
                if (!IsDocument)
                {
                    var removedItems = V_Repairs.Where(r => r.Serial_Number == SelectedSerialItem.Serial).ToList();
                    foreach (var repair in removedItems)
                    {
                        v_Repairs.Remove(repair);
                    }
                    serialNumbers.RemoveAt(SelectedSerialIndex);
                }
                else
                {
                    int.TryParse(SelectedSerialItem.Serial, out int documentId);
                    var removedItems = V_Repairs.Where(r => r.DocumentId == documentId).ToList();
                    foreach (var repair in removedItems)
                    {
                        v_Repairs.Remove(repair);
                    }
                    serialNumbers.RemoveAt(SelectedSerialIndex);
                }

            }
            GetCounts();
        }
        private ICommand removeRepairCommand;
        public ICommand RemoveRepairCommand
        {
            get
            {
                if (removeRepairCommand == null)
                {
                    removeRepairCommand = new RelayCommand(new Action<object>(RemoveRepair));
                }
                return removeRepairCommand;
            }
            set
            {
                removeRepairCommand = value;
                RaisedPropertyChanged("RemoveRepairCommand");
            }
        }
        /// <summary>
        /// Удаление выделенного элемента по SelectedRepairIndex
        /// </summary>
        /// <param name="Parameter"></param>
        private void RemoveRepair(object Parameter)
        {
            if (SelectedRepairIndex >= 0)
                try
                {
                    V_Repairs.RemoveAt(SelectedRepairIndex);
                }
                catch (Exception) { }
            GetCounts();
        }

        private ICommand removeSelectedRepairCommand;
        public ICommand RemoveSelectedRepairCommand
        {
            get
            {
                if (removeSelectedRepairCommand == null)
                {
                    removeSelectedRepairCommand = new RelayCommand(new Action<object>(RemoveSelectedRepair));
                }
                return removeSelectedRepairCommand;
            }
            set
            {
                removeSelectedRepairCommand = value;
                RaisedPropertyChanged("RemoveSelectedRepairCommand");
            }
        }

        /// <summary>
        /// Удаление выбранных ремонтов по полю IsChecked
        /// </summary>
        /// <param name="Parameter"></param>
        private void RemoveSelectedRepair(object Parameter)
        {
            var removedRepairs = V_Repairs.Where(r => r.IsChecked == true).ToList();
            foreach (var repair in removedRepairs)
            {
                V_Repairs.Remove(repair);
            }
            GetCounts();
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
            Clients = ClientsRequests.FillList();
            GetCounts();
        }
        #endregion
    }
}
