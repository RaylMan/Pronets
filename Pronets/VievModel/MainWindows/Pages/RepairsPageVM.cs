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
        private Repairs repair = new Data.Repairs();
        private Clients client;
        private Users user;
        private ObservableCollection<Defects> defects = new ObservableCollection<Defects>();
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
        private ObservableCollection<Users> users = new ObservableCollection<Users>();
        public ObservableCollection<Users> Users
        {
            get { return users; }

            set
            {
                users = value;
                RaisedPropertyChanged("Users");
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

        private Repairs selectedRepair;
        public Repairs SelectedRepair
        {
            get
            { return selectedRepair; }
            set
            {
                selectedRepair = (Repairs)value;
                RaisedPropertyChanged("SelectedRepair");
                if (SelectedRepair != null)
                {
                    repair.Client = selectedRepair.Client;
                    repair.Date_Of_Receipt = selectedRepair.Date_Of_Receipt;
                    repair.Inspector = selectedRepair.Inspector;
                    repair.Departure_Date = selectedRepair.Departure_Date;
                    user = UsersRequest.GetUser(selectedRepair.Inspector);
                    client = ClientsRequests.GetClient(selectedRepair.Client);
                    RepairId = repair.RepairId = selectedRepair.RepairId;
                    DocumentId = repair.DocumentId = selectedRepair.DocumentId;
                    Nomenclature = repair.Nomenclature = selectedRepair.Nomenclature;
                    Serial_Number = repair.Serial_Number = selectedRepair.Serial_Number;
                    InspectorName = user.LastName;
                    Warranty = repair.Warranty = selectedRepair.Warranty;
                    Claimed_Malfunction = repair.Claimed_Malfunction = selectedRepair.Claimed_Malfunction;
                    ClientName = this.client.ClientName;
                    Identifie_Fault = repair.Identifie_Fault = selectedRepair.Identifie_Fault;
                    Work_Done = repair.Work_Done = selectedRepair.Work_Done;
                    Note = repair.Note = selectedRepair.Note;
                    Repair_Date = repair.Repair_Date = selectedRepair.Repair_Date != null ? selectedRepair.Repair_Date : DateTime.Now;
                    GetStatus();
                    GetUser();
                    GetCategory();
                }
            }
        }

        protected Users selectedUser;
        public Users SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                RaisedPropertyChanged("SelectedUser");
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
            defects = DefectsRequests.FillList();
            Date_Of_Receipt = DateTime.Now;
            categories = RepairCategoriesRequests.FillList();
            users = UsersRequest.FillList();
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
                repairs.Clear();
                string engWord = IsChecked != true ? EditChars.ToEnglish(SearchText) : SearchText;
                foreach (var repair in RepairsRequest.SearchItem(engWord))
                {
                    repairs.Add(repair);
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
                if (SelectedUser != null)
                {
                    repair.Engineer = SelectedUser != null ? SelectedUser.UserId : 0;
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
        public void GetUser()
        {
            foreach (var user in users)
            {
                if (user.UserId == selectedRepair.Engineer)
                    SelectedUser = user;
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