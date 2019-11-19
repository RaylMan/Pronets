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
    class ClientInfoWIndowVM : VievModelBase
    {
        #region Repairs Properties
        private Repairs repair;
        private ObservableCollection<v_Repairs> v_repairs = new ObservableCollection<v_Repairs>();
        public ObservableCollection<v_Repairs> V_Repairs
        {
            get { return v_repairs; }

            set
            {
                v_repairs = value;
                RaisedPropertyChanged("V_Repairs");
            }
        }
        private int? repairId;
        public int? RepairId
        {
            get { return repairId; }
            set
            {
                repairId = value;
                RaisedPropertyChanged("RepairId");
            }
        }
        private int? documentId;
        public int? DocumentId
        {
            get { return documentId; }
            set
            {
                documentId = value;
                RaisedPropertyChanged("DocumentId");
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
        private int client_Id;
        public int Client_Id
        {
            get { return client_Id; }
            set
            {
                client_Id = value;
                RaisedPropertyChanged("Client_Id");
            }
        }
        private string client_Name;
        public string Client_Name
        {
            get { return clientName; }
            set
            {
                clientName = value;
                RaisedPropertyChanged("Client_Name");
            }
        }

        private DateTime? date_Of_Receipt;
        public DateTime? Date_Of_Receipt
        {
            get { return date_Of_Receipt; }
            set
            {
                date_Of_Receipt = value;
                RaisedPropertyChanged("Date_Of_Receipt");
            }
        }
        private DateTime? departure_Date;
        public DateTime? Departure_Date
        {
            get { return departure_Date; }
            set
            {
                departure_Date = value;
                RaisedPropertyChanged("Departure_Date");
            }
        }
        private int inspectorId;
        public int InspectorId
        {
            get { return inspectorId; }
            set
            {
                inspectorId = value;
                RaisedPropertyChanged("InspectorId");
            }
        }
        private string inspector;
        public string Inspector
        {
            get { return inspector; }
            set
            {
                inspector = value;
                RaisedPropertyChanged("Inspector");
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
        private string identifie_Fault;
        public string Identifie_Fault
        {
            get { return identifie_Fault; }
            set
            {
                identifie_Fault = value;
                RaisedPropertyChanged("Identifie_Fault");
            }
        }
        private string work_Done;
        public string Work_Done
        {
            get { return work_Done; }
            set
            {
                work_Done = value;
                RaisedPropertyChanged("Work_Done");
            }
        }
        private int engineerId;
        public int EngineerId
        {
            get { return engineerId; }
            set
            {
                engineerId = value;
                RaisedPropertyChanged("EngineerId");
            }
        }
        private string engineer;
        public string Engineer
        {
            get { return engineer; }
            set
            {
                engineer = value;
                RaisedPropertyChanged("Engineer");
            }
        }

        private string repair_Category;
        public string Repair_Category
        {
            get { return repair_Category; }
            set
            {
                repair_Category = value;
                RaisedPropertyChanged("Repair_Category");
            }
        }

        private DateTime? repair_Date;
        public DateTime? Repair_Date
        {
            get { return repair_Date; }
            set
            {
                repair_Date = value;
                RaisedPropertyChanged("Repair_Date");
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
        private string note;
        public string Note
        {
            get { return note; }
            set
            {
                note = value;
                RaisedPropertyChanged("Note");
            }
        }

        private v_Repairs selectedRepairItem;
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
        #region Clients properties
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
        private string inn;
        public string Inn
        {
            get { return inn; }
            set
            {
                inn = value;
                RaisedPropertyChanged("Inn");
            }
        }
        private string contact_Person;
        public string Contact_Person
        {
            get { return contact_Person; }
            set
            {
                contact_Person = value;
                RaisedPropertyChanged("Contact_Person");
            }
        }
        private string telephone_1;
        public string Telephone_1
        {
            get { return telephone_1; }
            set
            {
                telephone_1 = value;
                RaisedPropertyChanged("Telephone_1");
            }
        }
        private string telephone_2;
        public string Telephone_2
        {
            get { return telephone_2; }
            set
            {
                telephone_2 = value;
                RaisedPropertyChanged("Telephone_2");
            }
        }
        private string telephone_3;
        public string Telephone_3
        {
            get { return telephone_3; }
            set
            {
                telephone_3 = value;
                RaisedPropertyChanged("Telephone_3");
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisedPropertyChanged("Email");
            }
        }
        private string adress;
        public string Adress
        {
            get { return adress; }
            set
            {
                adress = value;
                RaisedPropertyChanged("Adress");
            }
        }

        private Clients selectedItem;
        public Clients SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
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
        private ObservableCollection<Warrantys> warrantysList = new ObservableCollection<Warrantys>();
        public ObservableCollection<Warrantys> WarrantysList
        {
            get { return warrantysList; }

            set
            {
                warrantysList = value;
                RaisedPropertyChanged("WarrantysList");
            }
        }
        private Warrantys selectedWarranty;
        public Warrantys SelectedWarranty
        {
            get { return selectedWarranty; }
            set
            {
                selectedWarranty = value;
                RaisedPropertyChanged("SelectedWarranty");
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
        private bool allWarranrys = true;
        public bool AllWarranrys
        {
            get { return allWarranrys; }
            set
            {
                allWarranrys = value;
                RaisedPropertyChanged("AllWarranrys");
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
            GetContent();
        }
        private void GetContent()
        {
            NomenclaturesList.Clear();
            V_Repairs.Clear();
            Statuses.Clear();
            ReceiptDocuments.Clear();
            WarrantysList.Clear();

            if (clientInstance != null)
            {
                TitleName = "Информация о клиенте " + clientInstance.ClientName;
                Name = $"{clientInstance.ClientName} [{clientInstance.ClientId}]";
                ClientName = clientInstance.ClientName;
                Contact_Person = clientInstance.Contact_Person;
                Email = clientInstance.Email;
                Inn = clientInstance.Inn;
                Telephone_1 = clientInstance.Telephone_1;
                Telephone_2 = clientInstance.Telephone_2;
                Telephone_3 = clientInstance.Telephone_3;
                Adress = clientInstance.Adress;
                NomenclaturesList = NomenclatureRequest.FillList();
                V_Repairs = RepairsRequest.FillListClient(clientInstance.ClientId);
                Statuses = StatusesRequests.FillList();
                ReceiptDocuments = ReceiptDocumentRequest.FillListClient(clientInstance.ClientId);
                AddDocumentName();
                RepairsCount = v_repairs.Count.ToString();
                WarrantysList.Add(new Warrantys { Warranty = "Нет" });
                WarrantysList.Add(new Warrantys { Warranty = "Гарантия Элтекс" });
                WarrantysList.Add(new Warrantys { Warranty = "Наша Гарантия" });
            }
            else
                MessageBox.Show("Откройте заново страницу", "Ошибка");

        }
        #region Sorting
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
        private void SortRepairs(object Parameter)
        {
            int count = 0; //переменная для проверки количества не отмеченых статусов ремонта
            v_repairs.Clear();
            if (statuses != null)
            {
                foreach (var status in statuses)
                {
                    if (status.IsSelected)
                    {
                        if (!AllWarranrys && !AllNomenclature && !AllDocuments && selectedDocument != null && selectedNomenclature != null && selectedWarranty != null)
                        {
                            var repairs = RepairsRequest.SortList(clientInstance.ClientId, selectedDocument.DocumentId, selectedNomenclature.Name, selectedWarranty.Warranty, status.Status);
                            FillRepairs(repairs);

                        }

                        else if (!AllWarranrys && !AllNomenclature && AllDocuments && selectedNomenclature != null && selectedWarranty != null)
                        {
                            var repairs = RepairsRequest.SortListWarNom(clientInstance.ClientId, selectedWarranty.Warranty, selectedNomenclature.Name, status.Status);
                            FillRepairs(repairs);
                        }

                        else if (!AllWarranrys && AllNomenclature && AllDocuments && selectedWarranty != null)
                        {
                            var repairs = RepairsRequest.SortList(selectedWarranty.Warranty, clientInstance.ClientId, status.Status);
                            FillRepairs(repairs);
                        }
                        else if (AllWarranrys && !AllNomenclature && !AllDocuments && selectedDocument != null && selectedNomenclature != null)
                        {
                            var repairs = RepairsRequest.SortListDocNom(clientInstance.ClientId, selectedDocument.DocumentId, selectedNomenclature.Name, status.Status);
                            FillRepairs(repairs);
                        }
                        else if (AllWarranrys && AllNomenclature && !AllDocuments && selectedDocument != null)
                        {
                            var repairs = RepairsRequest.SortList(clientInstance.ClientId, selectedDocument.DocumentId, status.Status);
                            FillRepairs(repairs);
                        }
                        else if (AllWarranrys && !AllNomenclature && AllDocuments && selectedNomenclature != null)
                        {
                            var repairs = RepairsRequest.SortList(clientInstance.ClientId, selectedNomenclature.Name, status.Status);
                            FillRepairs(repairs);
                        }
                        else if (!AllWarranrys && AllNomenclature && !AllDocuments && selectedDocument != null && selectedWarranty != null)
                        {
                            var repairs = RepairsRequest.SortList(clientInstance.ClientId, selectedWarranty.Warranty, selectedDocument.DocumentId, status.Status);
                            FillRepairs(repairs);
                        }

                        else
                        {
                            var repairs = RepairsRequest.SortList(clientInstance.ClientId, status.Status);
                            FillRepairs(repairs);
                        }

                    }
                    else
                        count++;

                }
                if (count == Statuses.Count) // если не выбран статус ремонта в listbox
                {
                    string status = null;
                    if (!AllWarranrys && !AllNomenclature && !AllDocuments && selectedDocument != null && selectedNomenclature != null && selectedWarranty != null)
                    {
                        var repairs = RepairsRequest.SortList(clientInstance.ClientId, selectedDocument.DocumentId, selectedNomenclature.Name, selectedWarranty.Warranty, status);
                        FillRepairs(repairs);
                    }

                    else if (!AllWarranrys && !AllNomenclature && AllDocuments && selectedNomenclature != null && selectedWarranty != null)
                    {
                        var repairs = RepairsRequest.SortListWarNom(clientInstance.ClientId, selectedWarranty.Warranty, selectedNomenclature.Name, status);
                        FillRepairs(repairs);
                    }

                    else if (!AllWarranrys && AllNomenclature && AllDocuments && selectedWarranty != null)
                    {
                        var repairs = RepairsRequest.SortList(selectedWarranty.Warranty, clientInstance.ClientId, status);
                        FillRepairs(repairs);
                    }
                    else if (AllWarranrys && !AllNomenclature && !AllDocuments && selectedDocument != null && selectedNomenclature != null)
                    {
                        var repairs = RepairsRequest.SortListDocNom(clientInstance.ClientId, selectedDocument.DocumentId, selectedNomenclature.Name, status);
                        FillRepairs(repairs);
                    }
                    else if (AllWarranrys && AllNomenclature && !AllDocuments && selectedDocument != null)
                    {
                        var repairs = RepairsRequest.SortList(clientInstance.ClientId, selectedDocument.DocumentId, status);
                        FillRepairs(repairs);
                    }
                    else if (AllWarranrys && !AllNomenclature && AllDocuments && selectedNomenclature != null)
                    {
                        var repairs = RepairsRequest.SortList(clientInstance.ClientId, selectedNomenclature.Name, status);
                        FillRepairs(repairs);
                    }
                    else if (!AllWarranrys && AllNomenclature && !AllDocuments && selectedDocument != null && selectedWarranty != null)
                    {
                        var repairs = RepairsRequest.SortList(clientInstance.ClientId, selectedWarranty.Warranty, selectedDocument.DocumentId, status);
                        FillRepairs(repairs);
                    }
                    else
                    {

                    }
                }
            }
            RepairsCount = v_repairs.Count.ToString();
        }
        private void FillRepairs(ObservableCollection<v_Repairs> repairs)
        {
            if(repairs != null)
            {
                foreach (var item in repairs)
                {
                    v_repairs.Add(item);
                }
            }
        }
        #endregion

        #region Other
        private void AddDocumentName()
        {
            if(receiptDocuments != null)
            {
                foreach (var item in receiptDocuments)
                {
                    DateTime updatedTime = item.Date ?? DateTime.MinValue;
                    item.DocumentName = $"№:{item.DocumentId}  От: {updatedTime.ToString("dd/MM/yyyy")}";
                }
            }
        }
        #endregion

        #region Edit Command

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

                var result = MessageBox.Show("Вы действительно хотите произвести запись?", "Запиьс в базу данных", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
        private ICommand searchRepairCommand;
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
                searchRepairCommand = value;
                RaisedPropertyChanged("SearchRepairCommand");
            }
        }

        public void SearchRepair(object Parameter)
        {
            if (SearchText != null && SearchText != "")
            {
                v_repairs.Clear();
                string engWord = IsCheckedSearch != true ? EditChars.ToEnglish(SearchText) : SearchText;
                var repairs = RepairsRequest.SearchItem(engWord, clientInstance.ClientId);
                if(repairs != null)
                {
                    foreach (var repair in repairs)
                    {
                        v_repairs.Add(repair);
                    }
                }
                
            }
        }
        #endregion

        #region RefreshCommand
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
        private void Refresh(object parametr)
        {
            GetContent();
        }

        #endregion
    }
}
