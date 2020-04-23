using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Repairs_f;
using Pronets.EntityRequests.Users_f;
using Pronets.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Pronets.VievModel.Repairs_f
{
    public class RepairsTableEngineerVM : VievModelBase
    {
        #region Properties
        private Engineers engineer;
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
        private ObservableCollection<Repair_Categories> repair_Categories = new ObservableCollection<Repair_Categories>();
        public ObservableCollection<Repair_Categories> Repair_Categories
        {
            get { return repair_Categories; }

            set
            {
                repair_Categories = value;
                RaisedPropertyChanged("Repair_Categories");
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
        private Statuses selectedStatus;
        public Statuses SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                SetCategoryFromStatus();
                RaisedPropertyChanged("SelectedStatus");
            }
        }
        private Repair_Categories selectedCategory;
        public Repair_Categories SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                RaisedPropertyChanged("SelectedCategory");
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
        private string workDone;
        public string WorkDone
        {
            get { return workDone; }
            set
            {
                workDone = value;
                RaisedPropertyChanged("WorkDone");
            }
        }
        private string identifieFault;
        public string IdentifieFault
        {
            get { return identifieFault; }
            set
            {
                identifieFault = value;
                RaisedPropertyChanged("IdentifieFault");
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
        #endregion
        public RepairsTableEngineerVM()
        {
            GetCounts();
            GetContent();
            GetEngineer();
        }
        private void GetCounts()
        {
            SerialsCount = serialNumbers.Count.ToString();
            RepairsCount = V_Repairs.Count.ToString();
        }

        private void SetCategoryFromStatus()
        {
            if (SelectedStatus != null)
            {
                if (SelectedStatus.Status == "Восстановлению не подлежит" ||
                    SelectedStatus.Status == "Донор" ||
                    SelectedStatus.Status == "Не смогли починить" ||
                    SelectedStatus.Status == "В ремонте")
                {
                    SetDefaultCategory();
                }
            }
        }
        /// <summary>
        /// Установка значения Selected Category на Диагностика
        /// </summary>
        private void SetDefaultCategory()
        {
            foreach (var category in Repair_Categories)
            {
                if (category.Category == "Диагностика")
                    SelectedCategory = category;
            }
        }
        private void SetDefaultStatus()
        {
            foreach (var status in Statuses)
            {
                if (status.Status == "Готово")
                    SelectedStatus = status;
            }
        }
        private void GetContent()
        {
            IdentifieFault = "Не выявлено";
            WorkDone = "Диагностика";
            Repair_Categories.Clear();
            Statuses.Clear();
            Repair_Categories = RepairCategoriesRequests.FillList();
            Statuses = StatusesRequests.FillList();
            SetDefaultCategory();
            SetDefaultStatus();
        }
        public void GetEngineer()
        {
            var user = UsersRequest.GetUser(Properties.Settings.Default.DefaultUserId);
            this.engineer = UsersRequest.GetEngineer(user.LastName);
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
        private void AddToTable(object Parameter)
        {
            string error = null;
            V_Repairs.Clear();
            if (!IsDocument)
            {
                foreach (var serial in serialNumbers)
                {
                    if (!string.IsNullOrWhiteSpace(serial.Serial))
                    {
                        string engSerial = EditChars.ToEnglish(serial.Serial);
                        var repairs = RepairsRequest.v_FillList(engSerial);
                        if (repairs != null)
                        {
                            if (repairs.Count > 0)
                            {
                                foreach (var repair in repairs)
                                {
                                    V_Repairs.Add(repair);
                                }
                            }
                            else
                                error += $" {engSerial},";
                        }
                    }
                }
                if (error != null)
                {
                    MessageBox.Show($"В базе данных отсутствуют:{error.Remove(error.Length - 1)}", "");
                }
            }
            else
            {
                foreach (var serial in serialNumbers)
                {
                    int.TryParse(serial.Serial, out int documentId);
                    var repairs = RepairsRequest.v_FillList(documentId);
                    if (repairs != null)
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

        #region Fill Command
        private ICommand fillCommand;
        public ICommand FillCommand
        {
            get
            {
                if (fillCommand == null)
                {
                    fillCommand = new RelayCommand(new Action<object>(Fill));
                }
                return fillCommand;
            }
            set
            {
                fillCommand = value;
                RaisedPropertyChanged("FillCommand");
            }
        }
        /// <summary>
        /// Заполение выбранных ремонтов полями выявленная неисправность и проделанная работа
        /// </summary>
        /// <param name="Parameter"></param>
        private void Fill(object Parameter)
        {
            if (RepairsChecked(V_Repairs))
            {
                foreach (var repair in v_Repairs)
                {
                    repair.Identifie_Fault = IdentifieFault;
                    repair.Work_Done = WorkDone;
                }
            }
            else
            {
                foreach (var repair in v_Repairs)
                {
                    if (repair.IsChecked)
                    {
                        repair.Identifie_Fault = IdentifieFault;
                        repair.Work_Done = WorkDone;
                    }
                }
            }
            CollectionViewSource.GetDefaultView(V_Repairs).Refresh();

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

        #region EditCommand
        private ICommand editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new RelayCommand(new Action<object>(Edit));
                }
                return editCommand;
            }
            set
            {
                editCommand = value;
                RaisedPropertyChanged("EditCommand");
            }
        }

        private void Edit(object Parameter)
        {
            if (V_Repairs.Count > 0)
            {
                if (SelectedStatus.Status != null && SelectedCategory != null)
                {
                    if (IsAllHaveDefect())
                    {
                        var result = MessageBox.Show("Вы действительно хотите редактировать?", "Редактирование", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            foreach (var repair in V_Repairs)
                            {
                                Repairs editingRepair = RepairsRequest.GetRepair(repair.RepairId);
                                editingRepair.Engineer = engineer.Id;
                                editingRepair.Identifie_Fault = repair.Identifie_Fault;
                                editingRepair.Work_Done = repair.Work_Done;
                                editingRepair.Repair_Date = DateTime.Now.Date;
                                editingRepair.Repair_Category = SelectedCategory != null ? SelectedCategory.Category : null;
                                editingRepair.Status = SelectedStatus != null ? SelectedStatus.Status : "Готово";

                                RepairsRequest.EditItem(editingRepair);
                                ReceiptDocumentRequest.SetStatus((int)repair.DocumentId, "В ремонте");
                            }
                            object e = null;
                            AddToTable(e);
                        }
                    }
                    else
                        MessageBox.Show("Необходимо заполнить поля \"Неисправность\" и \"Проделанный ремонт\"\nВозможно вы забыли нажать кнопку \"Заполнить\"", "Ошибка");
                }
                else
                    MessageBox.Show("Установите статус и категорию ремонта!", "Ошибка");
            }

        }

        private bool IsAllHaveDefect()
        {
            bool isAllHaveDefect = true;
            if (V_Repairs != null)
            {
                foreach (var repair in V_Repairs)
                {
                    if (string.IsNullOrWhiteSpace(repair.Identifie_Fault) || string.IsNullOrWhiteSpace(repair.Work_Done))
                    {
                        isAllHaveDefect = false;
                    }
                }
            }
            return isAllHaveDefect;
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
            GetContent();
        }
        #endregion
    }
}


