using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using Pronets.Data;
using System.Windows;
using System.Windows.Input;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Users_f;
using Pronets.EntityRequests.Nomenclature_f;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Viev.MainWindows.Pages;
using Pronets.Model;
using Pronets.Navigation.WindowsNavigation;
using System.Linq;

namespace Pronets.VievModel.MainWindows.Pages
{
    public class AddRecipeDocumentVM : VievModelBase
    {
        #region Property
        public OpenWindowCommand OpenWindowCommand { get; set; }
        private ObservableCollection<Repairs> repairs;
        public ObservableCollection<Repairs> Repairs
        {
            get { return this.repairs; }

            set
            {
                repairs = value;
                RaisedPropertyChanged("Repairs");
            }
        }


        private ObservableCollection<Nomenclature> nomenclatures;
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
        private ObservableCollection<Warrantys> warrantys;
        public ObservableCollection<Warrantys> Warrantys
        {
            get { return this.warrantys; }

            set
            {
                warrantys = value;
                RaisedPropertyChanged("Warrantys");
            }

        }

        private ObservableCollection<Users> users;
        public ObservableCollection<Users> Users
        {
            get { return this.users; }

            set
            {
                users = value;
                RaisedPropertyChanged("Users");
            }
        }
        private ObservableCollection<Clients> clients;
        public ObservableCollection<Clients> Clients
        {
            get { return this.clients; }

            set
            {
                clients = value;
                RaisedPropertyChanged("Clients");
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
        #endregion
        public AddRecipeDocumentVM()
        {
            clients = ClientsRequests.FillList();
            users = UsersRequest.FillList();
            nomenclatures = NomenclatureRequest.FillList();
            GetDocumentId();
            date_Of_Receipt = DateTime.Now;
            repairs = new ObservableCollection<Repairs>();
            warrantys = new ObservableCollection<Warrantys>();
            warrantys.Add(new Warrantys { Warranty = "Нет" });
            warrantys.Add(new Warrantys { Warranty = "Гарантия Элтекс" });
            warrantys.Add(new Warrantys { Warranty = "Наша Гарантия" });
            OpenWindowCommand = new OpenWindowCommand();
        }
        #region Document Id
        void GetDocumentId()
        {
            DocumentId = ReceiptDocumentRequest.GetDocumentID();
            DocumentId++;
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
                    addItem = new RelayCommand(new Action<object>(AddRepair));
                }
                return addItem;
            }
            set
            {
                addItem = value;
                RaisedPropertyChanged("AddCommand");
            }
        }

        public void AddRepair(object Parameter)
        {
            if (selectClientItem != null && selectUserItem != null)
            {
                var result = MessageBox.Show("Вы Действительно хотете записать в базу?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ReceiptDocument newReceiptDocument = new ReceiptDocument
                    {
                        ClientId = selectClientItem.ClientId,
                        InspectorId = selectUserItem.UserId,
                        Date = date_Of_Receipt,
                        Status = "Принято"
                    };
                    
                    string sn, cm, nm, wt;
                    for (int i = 0; i < repairs.Count; i++)
                    {
                        nm = repairs[i].Nomenclature1 != null ? repairs[i].Nomenclature1.Name : "Отсутствует";
                        //sn = repairs[i].Serial_Number != null ? repairs[i].Serial_Number : "Отсутствует";
                        //cm = repairs[i].Claimed_Malfunction != null ? repairs[i].Claimed_Malfunction : "Отсутствует";
                        wt = repairs[i].Warrantys != null ? repairs[i].Warrantys.Warranty : "нет";

                        repairs[i].DocumentId = documentId;
                        repairs[i].Nomenclature = nm;
                        //repairs[i].Serial_Number = serial_Number;
                        //repairs[i].Claimed_Malfunction = cm;
                        repairs[i].Client = selectClientItem.ClientId;
                        repairs[i].Date_Of_Receipt = date_Of_Receipt;
                        repairs[i].Inspector = selectUserItem.UserId;
                        repairs[i].Warranty = wt;
                    }
                    repairs.GetHashCode();
                    ReceiptDocumentRequest.AddToBase(newReceiptDocument);
                    RepairsRequest.AddToBase(repairs);
                    repairs.Clear();
                    GetDocumentId();
                    MessageBox.Show("Произведена успешная запись в базу данных!", "Результат");
                }
            }
            else
                MessageBox.Show("Необходимо выбрать клиента и приемщика!", "Ошибка");
        }
        #endregion

        #region test
        private ICommand testCommand;
        public ICommand TestCommand
        {
            get
            {
                if (testCommand == null)
                {
                    testCommand = new RelayCommand(new Action<object>(Test));
                }
                return testCommand;
            }
            set
            {
                testCommand = value;
                RaisedPropertyChanged("TestCommand");
            }
        }

        public void Test(object Parameter)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                var rep = db.Repairs.Where(r => r.Status == null).ToList();
                rep.ForEach(s => s.Status = "Принято");
                db.SaveChanges();

            }
           
            MessageBox.Show("test succesful", "Ошибка");
        }
        #endregion
    }
}
