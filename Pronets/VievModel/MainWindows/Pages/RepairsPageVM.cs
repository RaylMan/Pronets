using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Clients_f;
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
using System.Windows.Input;

namespace Pronets.VievModel.MainWindows.Pages
{
    class RepairsPageVM : RepairsModel
    {
        #region Properties
        private v_Repairs v_Repair = new Data.v_Repairs();
        private Clients clientInstance;
        private Users user;
        private Engineers engineer;
        private ObservableCollection<Repair_Categories> categories = new ObservableCollection<Repair_Categories>();
        public ObservableCollection<Repair_Categories> Categories
        {
            get { return categories; }

            set
            {
                categories = value;
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
                    v_Repair.Client_Name = selectedRepair.Client_Name;
                    v_Repair.Date_Of_Receipt = selectedRepair.Date_Of_Receipt;
                    v_Repair.Inspector = selectedRepair.Inspector;
                    v_Repair.Departure_Date = selectedRepair.Departure_Date;
                    user = UsersRequest.GetUser(selectedRepair.InspectorId);
                    clientInstance = ClientsRequests.GetClient(selectedRepair.Client_Id);
                    RepairId = v_Repair.RepairId = selectedRepair.RepairId;
                    DocumentId = v_Repair.DocumentId = selectedRepair.DocumentId;
                    Nomenclature = v_Repair.Nomenclature = selectedRepair.Nomenclature;
                    Serial_Number = v_Repair.Serial_Number = selectedRepair.Serial_Number;
                    InspectorName = user.LastName;
                    Warranty = v_Repair.Warranty = selectedRepair.Warranty;
                    Claimed_Malfunction = v_Repair.Claimed_Malfunction = selectedRepair.Claimed_Malfunction;
                    ClientName = this.clientInstance.ClientName;
                    Identifie_Fault = v_Repair.Identifie_Fault = selectedRepair.Identifie_Fault;
                    Work_Done = v_Repair.Work_Done = selectedRepair.Work_Done;
                    Note = v_Repair.Note = selectedRepair.Note;
                    Repair_Date = v_Repair.Repair_Date = selectedRepair.Repair_Date != null ? selectedRepair.Repair_Date : DateTime.Now;
                    GetStatus();
                    GetEngineer();
                    GetCategory();
                }
            }
        }

        protected Engineers selectedEngineer;
        public Engineers SelectedEngineer
        {
            get { return selectedEngineer; }
            set
            {
                selectedEngineer = value;
                RaisedPropertyChanged("SelectedEngineer");
            }
        }
        protected Statuses selectedStatus;
        public Statuses SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                RaisedPropertyChanged("SelectedStatus");
            }
        }
        protected Repair_Categories selectedCategory;
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
            Date_Of_Receipt = DateTime.Now;
            categories = RepairCategoriesRequests.FillList();
            engineers = UsersRequest.FillListEngineers();
            statuses = StatusesRequests.FillList();
        }

        #region Search Command
        protected ICommand searchItem;
        public ICommand SearchCommand
        {
            get
            {
                if (searchItem == null)
                {
                    searchItem = new RelayCommand(new Action<object>(SearchItem));
                }
                return searchItem;
            }
            set
            {
                searchItem = value;
                RaisedPropertyChanged("SearchCommand");
            }
        }

        public void SearchItem(object Parameter)
        {
            if (SearchText != null && SearchText != "")
            {
                v_Repairs.Clear();
                string engWord = IsChecked != true ? EditChars.ToEnglish(SearchText) : SearchText;
                foreach (var repair in RepairsRequest.SearchItem(engWord))
                {
                    v_Repairs.Add(repair);
                }
                //SearchText = string.Empty;
            }
        }

        
        #endregion

        #region EditCommand
        protected ICommand editItem;
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
                if (SelectedEngineer != null)
                {
                    repair = RepairsRequest.GetRepair(SelectedRepair.RepairId);
                    repair.Engineer = SelectedEngineer != null ? SelectedEngineer.Id : 0;
                    repair.Identifie_Fault = Identifie_Fault;
                    repair.Work_Done = Work_Done;
                    repair.Note = Note;
                    repair.Repair_Date = Repair_Date;
                    repair.Repair_Category = SelectedCategory != null ? SelectedCategory.Category : null;
                    repair.Status = SelectedStatus != null ? SelectedStatus.Status : null;

                    var result = MessageBox.Show("Вы Действительно хотите редактировать?", "Редактирование", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (repair != null)
                        {
                            RepairsRequest.EditItem(repair);
                        }
                    }
                }
                else
                    MessageBox.Show("Необходимо выбрать инженера!", "Ошибка");
            }
            else
                MessageBox.Show("Необходимо выбрать элемент!", "Ошибка");
        }
        #endregion

        #region Selectitems
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
            var defaultEngineer = UsersRequest.GetEngineer("Не выбран");
            foreach (var engineer in engineers)
            {
                if(selectedRepair.EngineerId != defaultEngineer.Id)
                {
                    if (engineer.Id == selectedRepair.EngineerId)
                    {
                        SelectedEngineer = engineer;
                    }
                }
                else
                {
                    foreach (var item in engineers)
                    {
                        if (engineer.LastName == Properties.Settings.Default.DefaultLastName)
                        {
                            SelectedEngineer = engineer;
                        }
                    }
                }
            }
        }
        public void GetCategory()
        {
            foreach (var category in categories)
            {
                if (category.Category == selectedRepair.Repair_Category)
                    SelectedCategory = category;
            }
        }
        #endregion
    }

}