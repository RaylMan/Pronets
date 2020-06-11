using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Nomenclature_f;
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

namespace Pronets.VievModel.Repairs_f
{
    public class EditRepairWindowVM : VievModelBase
    {
        #region Properties

        #region Repairs Properties
        protected int? repairId;
        public int? RepairId
        {
            get { return repairId; }
            set
            {
                repairId = value;
                RaisedPropertyChanged("RepairId");
            }
        }
        protected int? documentId;
        public int? DocumentId
        {
            get { return documentId; }
            set
            {
                documentId = value;
                RaisedPropertyChanged("DocumentId");
            }
        }
        protected string nomenclature;
        public string Nomenclature
        {
            get { return nomenclature; }
            set
            {
                nomenclature = value;
                RaisedPropertyChanged("Nomenclature");
            }
        }
        protected string serial_Number;
        public string Serial_Number
        {
            get { return serial_Number; }
            set
            {
                serial_Number = value;
                RaisedPropertyChanged("Serial_Number");
            }
        }
        protected string claimed_Malfunction;
        public string Claimed_Malfunction
        {
            get { return claimed_Malfunction; }
            set
            {
                claimed_Malfunction = value;
                RaisedPropertyChanged("Claimed_Malfunction");
            }
        }
        protected int? client;
        public int? Client
        {
            get { return client; }
            set
            {
                client = value;
                RaisedPropertyChanged("Client");
            }
        }
        protected int client_Id;
        public int Client_Id
        {
            get { return client_Id; }
            set
            {
                client_Id = value;
                RaisedPropertyChanged("Client_Id");
            }
        }
        protected string client_Name;
        public string Client_Name
        {
            get { return client_Name; }
            set
            {
                client_Name = value;
                RaisedPropertyChanged("Client_Name");
            }
        }


        protected DateTime? date_Of_Receipt;
        public DateTime? Date_Of_Receipt
        {
            get { return date_Of_Receipt; }
            set
            {
                date_Of_Receipt = value;
                RaisedPropertyChanged("Date_Of_Receipt");
            }
        }
        protected DateTime? departure_Date;
        public DateTime? Departure_Date
        {
            get { return departure_Date; }
            set
            {
                departure_Date = value;
                RaisedPropertyChanged("Departure_Date");
            }
        }
        //protected int? inspector;
        //public int? Inspector
        //{
        //    get { return inspector; }
        //    set
        //    {
        //        inspector = value;
        //        RaisedPropertyChanged("Inspector");
        //    }
        //}
        protected int inspectorId;
        public int InspectorId
        {
            get { return inspectorId; }
            set
            {
                inspectorId = value;
                RaisedPropertyChanged("InspectorId");
            }
        }
        protected string inspector;
        public string Inspector
        {
            get { return inspector; }
            set
            {
                inspector = value;
                RaisedPropertyChanged("Inspector");
            }
        }
        protected string warranty;
        public string Warranty
        {
            get { return warranty; }
            set
            {
                warranty = value;
                RaisedPropertyChanged("Warranty");
            }
        }
        protected string identifie_Fault;
        public string Identifie_Fault
        {
            get { return identifie_Fault; }
            set
            {
                identifie_Fault = value;
                RaisedPropertyChanged("Identifie_Fault");
            }
        }
        protected string work_Done;
        public string Work_Done
        {
            get { return work_Done; }
            set
            {
                work_Done = value;
                RaisedPropertyChanged("Work_Done");
            }
        }
        protected string repair_Category;
        public string Repair_Category
        {
            get { return repair_Category; }
            set
            {
                repair_Category = value;
                RaisedPropertyChanged("Repair_Category");
            }
        }
        protected int engineerId;
        public int EngineerId
        {
            get { return engineerId; }
            set
            {
                engineerId = value;
                RaisedPropertyChanged("EngineerId");
            }
        }
        protected string engineer;
        public string Engineer
        {
            get { return engineer; }
            set
            {
                engineer = value;
                RaisedPropertyChanged("Engineer");
            }
        }

        protected DateTime? repair_Date;
        public DateTime? Repair_Date
        {
            get { return repair_Date; }
            set
            {
                repair_Date = value;
                RaisedPropertyChanged("Repair_Date");
            }
        }
        protected string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisedPropertyChanged("Status");
            }
        }
        protected string note;
        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                RaisedPropertyChanged("Note");
            }
        }
        #endregion

        #region Nomenclatures properties
        private ObservableCollection<Nomenclature> nomenclatures = new ObservableCollection<Nomenclature>();
        public ObservableCollection<Nomenclature> Nomenclatures
        {
            get { return nomenclatures; }

            set
            {
                nomenclatures = value;
                RaisedPropertyChanged("Nomenclatures");
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
        #endregion

        #region Status, warrantys properties and Repair_Categories
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
        #endregion

        #region Client and users properties
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
        private Clients selectedRecipient;
        public Clients SelectedRecipient
        {
            get { return selectedRecipient; }
            set
            {
                selectedRecipient = value;
                RaisedPropertyChanged("SelectedRecipient");
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
        private Users selectedUser;
        public Users SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                RaisedPropertyChanged("SelectedUser");
            }
        }
        #endregion

        private bool isAdmin;
        public bool IsAdmin
        {
            get { return isAdmin; }
            set
            {
                isAdmin = value;
                RaisedPropertyChanged("IsAdmin");
            }
        }
        private Repairs defaultRepair;
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisedPropertyChanged("Title");
            }
        }


        #endregion

        public EditRepairWindowVM(v_Repairs repair)
        {
            if (repair != null)
            {
                try
                {
                    Title = $"Изменить ремонт №{repair.RepairId}";
                    GetContent(repair.RepairId);
                    IsAdmin = UsersRequest.IsAdmin() || UsersRequest.IsInspector();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
        }
        #region Get Content
        private void GetContent(int repairId)
        {
            defaultRepair = RepairsRequest.GetRepair(repairId);
            if (defaultRepair != null)
            {
                RepairId = defaultRepair.RepairId;
                DocumentId = defaultRepair.DocumentId;
                GetNomenclature(defaultRepair.Nomenclature);
                Serial_Number = defaultRepair.Serial_Number;
                Claimed_Malfunction = defaultRepair.Claimed_Malfunction;
                GetClient((int)defaultRepair.Client);
                Date_Of_Receipt = defaultRepair.Date_Of_Receipt;
                GetRecipient(defaultRepair.Recipient);
                Departure_Date = defaultRepair.Departure_Date;
                GetUser((int)defaultRepair.Inspector);
                GetWarrantys(defaultRepair.Warranty);
                Identifie_Fault = defaultRepair.Identifie_Fault;
                Work_Done = defaultRepair.Work_Done;
                GetEngineer((int)defaultRepair.Engineer);
                Repair_Date = defaultRepair.Repair_Date;
                GetRepair_Categories(defaultRepair.Repair_Category);
                GetStatuses(defaultRepair.Status);
                Note = defaultRepair.Note;
            }
        }
        private void GetWarrantys(string warranty)
        {
            Warrantys.Clear();
            Warrantys.Add(new Warrantys { Warranty = "Нет" });
            Warrantys.Add(new Warrantys { Warranty = "Гарантия Элтекс" });
            Warrantys.Add(new Warrantys { Warranty = "Наша Гарантия" });
            foreach (var item in warrantys)
            {
                if (item.Warranty == warranty)
                {
                    SelectedWarrantyItem = item;
                }
            }
        }
        private void GetNomenclature(string nomenclature)
        {
            Nomenclatures.Clear();
            Nomenclatures = NomenclatureRequest.FillList();
            foreach (var item in Nomenclatures)
            {
                if (item.Name == nomenclature)
                    SelectedNomenclatureItem = item;
            }
        }
        private void GetStatuses(string status)
        {
            Statuses.Clear();
            Statuses = StatusesRequests.FillList();
            foreach (var item in Statuses)
            {
                if (item.Status == status)
                    SelectedStatusItem = item;
            }
        }
        private void GetRepair_Categories(string category)
        {
            Repair_Categories.Clear();
            Repair_Categories = RepairCategoriesRequests.FillList();
            foreach (var item in Repair_Categories)
            {
                if (item.Category == category)
                    SelectedCategory = item;
            }
        }
        private void GetEngineer(int id)
        {
            Engineers.Clear();
            Engineers = UsersRequest.FillListEngineers();
            foreach (var item in Engineers)
            {
                if (item.Id == id)
                    SelectedEngineer = item;
            }
        }
        private void GetUser(int id)
        {
            Users.Clear();
            Users = UsersRequest.FillList();
            foreach (var item in Users)
            {
                if (item.UserId == id)
                    SelectedUser = item;
            }
        }
        private void GetClient(int id)
        {
            Clients.Clear();
            Clients = ClientsRequests.FillList();
            foreach (var item in Clients)
            {
                if (item.ClientId == id)
                    SelectedClientItem = item;
            }
        }
        private void GetRecipient(string name)
        {
            Recipients.Clear();
            Recipients = ClientsRequests.FillList();
            foreach (var item in Recipients)
            {
                if (item.ClientName == name)
                    SelectedRecipient = item;
            }
        }
        #endregion

        #region SaveCommand
        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(new Action<object>(Save));
                }
                return saveCommand;
            }
            set
            {
                saveCommand = value;
                RaisedPropertyChanged("SaveCommand");
            }
        }

        public void Save(object Parameter)
        {
            var result = MessageBox.Show("Вы действительно хотете изменить текущий ремонт?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (SelectedNomenclatureItem != null &&
                              SelectedClientItem != null &&
                              SelectedUser != null &&
                              SelectedWarrantyItem != null &&
                              SelectedStatusItem != null
                             )
                    {
                        string recipient = SelectedRecipient != null ? SelectedRecipient.ClientName : null;
                        int? engineer = SelectedEngineer != null ? SelectedEngineer.Id : defaultRepair.Engineer;
                        string category = SelectedCategory != null ? SelectedCategory.Category : null;

                        Repairs editingRepair = new Repairs
                        {
                            RepairId = defaultRepair.RepairId,
                            DocumentId = defaultRepair.DocumentId,
                            Nomenclature = SelectedNomenclatureItem.Name,
                            Serial_Number = this.Serial_Number,
                            Claimed_Malfunction = this.Claimed_Malfunction,
                            Client = SelectedClientItem.ClientId,
                            Date_Of_Receipt = this.Date_Of_Receipt,
                            Recipient = recipient,
                            Departure_Date = this.Departure_Date,
                            Inspector = SelectedUser.UserId,
                            Warranty = SelectedWarrantyItem.Warranty,
                            Identifie_Fault = this.Identifie_Fault,
                            Work_Done = this.Work_Done,
                            Repair_Category = category,
                            Engineer = engineer,
                            Repair_Date = this.Repair_Date,
                            Status = SelectedStatusItem.Status,
                            Note = this.Note
                        };
                        RepairsRequest.EditItem(editingRepair);
                    }
                    else
                    {
                        MessageBox.Show("Необходимо установить:\n" +
                        "- Номеклатура\n" +
                        "- Гарантия\n" +
                        "- Приемщик\n" +
                        "- Клиент\n" +
                        "- Статус ремонта\n", "Ошибка");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                }
            }
        }
        #endregion
    }
}
