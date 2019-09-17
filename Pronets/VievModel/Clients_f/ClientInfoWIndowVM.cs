using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Nomenclature_f;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Clients_f
{
    class ClientInfoWIndowVM : ClientsVM
    {
        #region Repairs Properties
        protected Repairs repair;
        protected ObservableCollection<v_Repairs> v_repairs = new ObservableCollection<v_Repairs>();
        public ObservableCollection<v_Repairs> V_Repairs
        {
            get { return v_repairs; }

            set
            {
                v_repairs = value;
                RaisedPropertyChanged("Repairs");
            }
        }
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
            get { return clientName; }
            set
            {
                clientName = value;
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

        protected v_Repairs selectedRepairItem;
        public v_Repairs SelectedRepairItem
        {
            get { return selectedRepairItem; }
            set
            {
                selectedRepairItem = value;
                RaisedPropertyChanged("SelectedRepairItem");
            }
        }
        #endregion
        #region Other Properties
        private Clients clientInstance;

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
        private ObservableCollection<ReceiptDocument> receiptDocuments = new ObservableCollection<ReceiptDocument>();
        public ObservableCollection<ReceiptDocument> ReceiptDocuments
        {
            get { return receiptDocuments; }

            set
            {
                receiptDocuments = value;
                RaisedPropertyChanged("ReceiptDocuments");
            }
        }
        private ReceiptDocument selectedDocument;
        public ReceiptDocument SelectedDocument
        {
            get { return selectedDocument; }
            set
            {
                selectedDocument = value;
                RaisedPropertyChanged("SelectedDocument");
            }
        }
        private string documentName;
        public string DocumentName
        {
            get { return documentName; }
            set
            {
                documentName = value;
                RaisedPropertyChanged("DocumentName");
            }
        }
        private ObservableCollection<Nomenclature> nomenclaturesList = new ObservableCollection<Nomenclature>();
        public ObservableCollection<Nomenclature> NomenclaturesList
        {
            get { return nomenclaturesList; }

            set
            {
                nomenclaturesList = value;
                RaisedPropertyChanged("NomenclaturesList");
            }
        }
        private Nomenclature selectedNomenclature;
        public Nomenclature SelectedNomenclature
        {
            get { return selectedNomenclature; }
            set
            {
                selectedNomenclature = value;
                RaisedPropertyChanged("SelectedNomenclature");
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisedPropertyChanged("IsSelected");
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
        private string titleName;
        public string TitleName
        {
            get { return titleName; }
            set
            {
                titleName = value;
                RaisedPropertyChanged("TitleName");
            }
        }

        private bool allDocuments = true;
        public bool AllDocuments
        {
            get { return allDocuments; }
            set
            {
                allDocuments = value;
                RaisedPropertyChanged("AllDocuments");
            }
        }
        private bool allNomenclature = true;
        public bool AllNomenclature
        {
            get { return allNomenclature; }
            set
            {
                allNomenclature = value;
                RaisedPropertyChanged("AllNomenclature");
            }
        }

        private bool isCheckedSearch;
        public bool IsCheckedSearch
        {
            get { return isCheckedSearch; }
            set
            {
                isCheckedSearch = value;
                RaisedPropertyChanged("IsCheckedSearch");
            }
        }

        private string repairsCount;
        public string RepairsCount
        {
            get { return repairsCount; }
            set
            {
                repairsCount = " Количество " + value;
                RaisedPropertyChanged("RepairsCount");
            }
        }

        #endregion


        public ClientInfoWIndowVM(Clients client)
        {
            clientInstance = client;
            TitleName = "Информация о клиенте " + client.ClientName;
            Name = $"{client.ClientName} [{client.ClientId}]";
            ClientName = client.ClientName;
            Contact_Person = client.Contact_Person;
            Email = client.Email;
            Inn = client.Inn;
            Telephone_1 = client.Telephone_1;
            Telephone_2 = client.Telephone_2;
            Telephone_3 = client.Telephone_3;
            Adress = client.Adress;
            nomenclaturesList = NomenclatureRequest.FillList();
            v_repairs = RepairsRequest.FillListClient(client.ClientId);
            statuses = StatusesRequests.FillList();
            receiptDocuments = ReceiptDocumentRequest.FillListClient(client.ClientId);
            AddDocumentName();
            RepairsCount = v_repairs.Count.ToString();
        }

        #region Sort by status 
        private ICommand sortCommand;
        public ICommand SortCommand
        {
            get
            {
                if (sortCommand == null)
                {
                    sortCommand = new RelayCommand(new Action<object>(SortRepairs));
                }
                return sortCommand;
            }
            set
            {
                sortCommand = value;
                RaisedPropertyChanged("Sort");
            }
        }
        protected void SortRepairs(object Parameter)
        {
            v_repairs.Clear();
            foreach (var status in statuses)
            {
                if (status.IsSelected)
                {
                    if (!AllDocuments && !AllNomenclature && selectedDocument != null && selectedNomenclature != null)
                    {
                        foreach (var item in RepairsRequest.FillList(clientInstance.ClientId, status.Status, selectedDocument.DocumentId, selectedNomenclature.Name))
                        {
                            v_repairs.Add(item);
                        }
                    }
                    else if (AllDocuments && !AllNomenclature && selectedNomenclature != null)
                    {
                        foreach (var item in RepairsRequest.FillList(clientInstance.ClientId, status.Status, selectedNomenclature.Name))
                        {
                            v_repairs.Add(item);
                        }
                    }
                    else if (!AllDocuments && AllNomenclature && selectedDocument != null)
                    {
                        foreach (var item in RepairsRequest.FillList(clientInstance.ClientId, status.Status, selectedDocument.DocumentId))
                        {
                            v_repairs.Add(item);
                        }
                    }
                    else
                    {
                        foreach (var item in RepairsRequest.FillList(clientInstance.ClientId, status.Status))
                        {
                            v_repairs.Add(item);
                        }
                    }

                }

            }
            if (v_repairs.Count == 0)
                V_Repairs = RepairsRequest.FillListClient(clientInstance.ClientId);
        }
        #endregion

        #region Other
        private void AddDocumentName()
        {
            foreach (var item in receiptDocuments)
            {
                DateTime updatedTime = item.Date ?? DateTime.MinValue;
                item.DocumentName = $"№:{item.DocumentId}  От: {updatedTime.ToString("dd/MM/yyyy")}";
            }

        }
        #endregion

        #region Edit Command

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
            Clients modifiedClient = null;
            if (ClientName != null)
            {
                modifiedClient = new Clients
                {
                    ClientId = clientInstance.ClientId,
                    ClientName = ClientName,
                    Inn = Inn,
                    Contact_Person = Contact_Person,
                    Telephone_1 = Telephone_1,
                    Telephone_2 = Telephone_2,
                    Telephone_3 = Telephone_3,
                    Email = Email,
                    Adress = Adress
                };

                var result = MessageBox.Show("Вы Действительно хотите произвести запись?", "Запиьс в базу данных", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (modifiedClient != null)
                    {
                        ClientsRequests.EditItem(modifiedClient);
                    }
                }
            }
            else
                MessageBox.Show("Необходимо ввести имя клиента!", "Ошибка");
        }
        #endregion

        #region Search Command
        protected ICommand searchRepairCommand;
        public ICommand SearchRepairCommand
        {
            get
            {
                if (searchRepairCommand == null)
                {
                    searchRepairCommand = new RelayCommand(new Action<object>(SearchRepair));
                }
                return searchRepairCommand;
            }
            set
            {
                searchItem = value;
                RaisedPropertyChanged("SearchRepairCommand");
            }
        }

        public void SearchRepair(object Parameter)
        {
            if (SearchText != null && SearchText != "")
            {
                v_repairs.Clear();
                string engWord = IsCheckedSearch != true ? EditChars.ToEnglish(SearchText) : SearchText;
                foreach (var repair in RepairsRequest.SearchItem(engWord))
                {
                    v_repairs.Add(repair);
                }
            }
        }
        #endregion
    }
}
