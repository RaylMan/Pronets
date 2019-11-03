using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Repairs_f;
using Pronets.EntityRequests.Users_f;
using Pronets.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Pronets.VievModel.MainWindows.Pages
{
    class RepairsPageVM : RepairsModel
    {
        #region Properties
        Dispatcher _dispatcher;
        private v_Repairs v_Repair = new Data.v_Repairs();
        private Engineers engineer;
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
        private ObservableCollection<Engineers> engineers = new ObservableCollection<Engineers>();
        public ObservableCollection<Engineers> Engineers
        {
            get { return engineers; }

            set
            {
                engineers = value;
                RaisedPropertyChanged("Engineers");
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
        private string repairsInfo;
        public string RepairsInfo
        {
            get { return repairsInfo; }
            set
            {
                repairsInfo = value;
                RaisedPropertyChanged("RepairsInfo");
            }
        }
        private string inspectorName;
        public string InspectorName
        {
            get { return inspectorName; }
            set
            {
                inspectorName = value;
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

        private v_Repairs selectedRepair;
        public v_Repairs SelectedRepair
        {
            get
            { return selectedRepair; }
            set
            {
                selectedRepair = (v_Repairs)value;
                RaisedPropertyChanged("SelectedRepair");
                if (SelectedRepair != null)
                {
                    GetRepairInfo();
                    GetStatus();
                    GetCategory();
                    GetRepairInfo();
                }
            }
        }
        private Engineers selectedEngineer;
        public Engineers SelectedEngineer
        {
            get { return selectedEngineer; }
            set
            {
                selectedEngineer = value;
                RaisedPropertyChanged("SelectedEngineer");
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

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                RaisedPropertyChanged("IsChecked");
            }
        }

        #endregion

        public RepairsPageVM()
        {
            Repair_Date = DateTime.Now;
            _dispatcher = Dispatcher.CurrentDispatcher;
            GetContentAsync();
            GetEngineer();
        }
        private async void GetContentAsync()
        {
            await Task.Run(() => GetContent());
        }
        private void GetContent()
        {
            _dispatcher.Invoke(new Action(() =>
            {
                Repair_Categories = RepairCategoriesRequests.FillList();
                Statuses = StatusesRequests.FillList();
            }));
        }

        #region Search Command
        private ICommand searchItem;
        public ICommand SearchCommand
        {
            get
            {
                if (searchItem == null)
                {
                    searchItem = new RelayCommand(new Action<object>(SearchItemAsync));
                }
                return searchItem;
            }
            set
            {
                searchItem = value;
                RaisedPropertyChanged("SearchCommand");
            }
        }
        public async void SearchItemAsync(object Parameter)
        {
            V_Repairs.Clear();
            await Task.Run(() => SearchItem());
        }
        public async void SearchItemAsync()
        {
            V_Repairs.Clear();
            await Task.Run(() => SearchItem());
        }
        public void SearchItem()
        {
            if (SearchText != null && SearchText != "")
            {
                string engWord = IsChecked != true ? EditChars.ToEnglish(SearchText) : SearchText;
                foreach (var repair in RepairsRequest.SearchItem(engWord))
                {
                    _dispatcher.Invoke(new Action(() =>
                    {
                        V_Repairs.Add(repair);
                    }));
                }
                if (V_Repairs.Count > 0)
                    SelectedIndex = 0;
                //SearchText = string.Empty;
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
                    editItem = new RelayCommand(new Action<object>(EditItem));
                }
                return editItem;
            }
            set
            {
                editItem = value;
                RaisedPropertyChanged("EditCommand");
            }
        }

        public void EditItem(object Parameter)
        {
            if (SelectedRepair != null)
            {
                repair = RepairsRequest.GetRepair(SelectedRepair.RepairId);
                repair.Engineer = engineer != null ? engineer.Id : 0;
                repair.Identifie_Fault = Identifie_Fault;
                repair.Work_Done = Work_Done;
                repair.Note = Note;
                repair.Repair_Date = Repair_Date;
                repair.Repair_Category = SelectedCategory != null ? SelectedCategory.Category : null;
                repair.Status = SelectedStatus != null ? SelectedStatus.Status : "Готово";

                var result = MessageBox.Show("Вы действительно хотите редактировать?", "Редактирование", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedStatus.Status != "Принято")
                    {
                        if (repair != null)
                        {
                            RepairsRequest.EditItem(repair);
                            ReceiptDocumentRequest.SetStatus((int)repair.DocumentId, "В ремонте");
                            SearchItemAsync();
                        }
                    }
                    else
                        MessageBox.Show("Установите статус ремонта!", "Ошибка");
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент!", "Ошибка");
        }
        #endregion

        #region Selectitems
        private void GetRepairInfo()
        {
            RepairsInfo = $"№ ремонта: {selectedRepair.RepairId}\n" +
               $"№ документа: {selectedRepair.DocumentId}\n" +
               $"Наименование: {selectedRepair.Nomenclature}\n" +
               $"Серийный номер: {selectedRepair.Serial_Number}\n" +
               $"Клиент: {selectedRepair.Client_Name}\n" +
               $"Заявленная неисправность: {selectedRepair.Claimed_Malfunction}\n" +
               $"Выявленная неисправность: {selectedRepair.Identifie_Fault}\n" +
               $"Проделанная работа: {selectedRepair.Work_Done}\n" +
               $"Заметка: {selectedRepair.Note}\n" +
               $"Дата приемки: {selectedRepair.Date_Of_Receipt}\n" +
               $"Приемщик: {selectedRepair.Inspector}\n" +
               $"Получатель: {selectedRepair.Recipient}\n" +
               $"Дата отправки: {selectedRepair.Departure_Date}\n" +
               $"Гарантия: {selectedRepair.Warranty}\n" +
               $"Инженер: {selectedRepair.Engineer}\n" +
               $"Дата ремонта: {selectedRepair.Repair_Date}\n" +
               $"Категория ремонта: {selectedRepair.Repair_Category}\n" +
               $"Статус ремонта: {selectedRepair.Status}" ;

        }
        //Устанавливает значение по умолчанию Combobox "статус документа" в соответствии с БД
        public void GetStatus()
        {
            foreach (var status in statuses)
            {
                if (status.Status == selectedRepair.Status)
                    SelectedStatus = status;
            }
        }
        public void GetEngineer()
        {
            var user = UsersRequest.GetUser(Properties.Settings.Default.DefaultUserId);
            engineer = UsersRequest.GetEngineer(user.LastName);
        }
        public void GetCategory()
        {
            foreach (var category in Repair_Categories)
            {
                if (category.Category == selectedRepair.Repair_Category)
                    SelectedCategory = category;
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
            GetContentAsync();
        }
        #endregion

        #region Info Command
        private ICommand getInfoCommand;
        public ICommand GetInfoCommand
        {
            get
            {
                if (getInfoCommand == null)
                {
                    getInfoCommand = new RelayCommand(new Action<object>(GetInfo));
                }
                return getInfoCommand;
            }
            set
            {
                getInfoCommand = value;
                RaisedPropertyChanged("GetInfoCommand");
            }
        }
        private void GetInfo(object parametr)
        {
            string info = $"№ ремонта: {selectedRepair.RepairId}\n" +
                $"№ документа: {selectedRepair.DocumentId}\n" +
                $"Наименование: {selectedRepair.Nomenclature}\n" +
                $"Серийный номер: {selectedRepair.Serial_Number}\n" +
                $"Клиент: {selectedRepair.Client_Name}\n" +
                $"Дата приемки: {selectedRepair.Date_Of_Receipt}\n" +
                $"Приемщик: {selectedRepair.Inspector}\n" +
                $"Получатель: {selectedRepair.Recipient}\n" +
                $"Дата отправки: {selectedRepair.Departure_Date}\n" +
                $"Гарантия: {selectedRepair.Warranty}\n" +
                $"Заявленная неисправность: {selectedRepair.Claimed_Malfunction}\n" +
                $"Выявленная неисправность: {selectedRepair.Identifie_Fault}\n" +
                $"Проделанная работа: {selectedRepair.Work_Done}\n" +
                $"Инженер: {selectedRepair.Engineer}\n" +
                $"Дата ремонта: {selectedRepair.Repair_Date}\n" +
                $"Категория ремонта: {selectedRepair.Repair_Category}\n" +
                $"Статус ремонта: {selectedRepair.Status}\n" +
                $"Заметка: {selectedRepair.Note}";
            MessageBox.Show(info, "Информация о ремонте");
        }
        #endregion
    }

}