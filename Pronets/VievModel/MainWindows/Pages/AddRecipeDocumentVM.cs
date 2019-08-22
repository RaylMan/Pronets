using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using Pronets.Data;
using Pronets.Model;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.MainWindows.Pages
{
    public class AddRecipeDocumentVM : VievModelBase
    {
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PronetsDBEntities"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds;

        #region Property
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
            get { return this.nomenclatures; }

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
            FillClients();
            FillUsers();
            FillNomenclature();
            GetDocumentId();
            date_Of_Receipt = DateTime.Now;
            repairs = new ObservableCollection<Repairs>();
            warrantys = new ObservableCollection<Warrantys>();
            warrantys.Add(new Warrantys { Warranty = "Нет" });
            warrantys.Add(new Warrantys { Warranty = "Гарантия Элтекс" });
            warrantys.Add(new Warrantys { Warranty = "Наша Гарантия" });
        }
        #region Document Id
        void GetDocumentId()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("SELECT TOP 1 DocumentId FROM ReceiptDocument ORDER BY DocumentId DESC", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "ReceiptDocument");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DocumentId = Convert.ToInt32(dr[0]);
                    DocumentId++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        #endregion

        #region FillLists
        public void FillClients()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select ClientId, ClientName from Clients", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Clients");
                if (clients == null)
                    clients = new ObservableCollection<Clients>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clients.Add(new Clients
                    {
                        ClientId = Convert.ToInt32(dr[0]),
                        ClientName = dr[1].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        public void FillUsers()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select UserId, LastName from Users", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Users");
                if (users == null)
                    users = new ObservableCollection<Users>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    users.Add(new Users
                    {
                        UserId = Convert.ToInt32(dr[0]),
                        LastName = dr[1].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
            }
        }
        public void FillNomenclature()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select Name from Nomenclature", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Nomenclature");
                if (nomenclatures == null)
                    nomenclatures = new ObservableCollection<Nomenclature>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    nomenclatures.Add(new Nomenclature
                    {
                        Name = dr[0].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ds = null;
                adapter.Dispose();
                con.Close();
                con.Dispose();
            }
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
                    string sql = "INSERT INTO  ReceiptDocument VALUES (" +
                         selectClientItem.ClientId + ", " +
                         selectUserItem.UserId + "," +
                         "'" + date_Of_Receipt.ToString("yyyy/MM/dd") + "')";
                    try
                    {
                        con = new SqlConnection(connectionString);
                        SqlCommand command = new SqlCommand(sql, con);
                        adapter = new SqlDataAdapter(command);
                        adapter.InsertCommand = new SqlCommand(sql, con);
                        con.Open();
                        ds = new DataSet();
                        adapter.Fill(ds, "ReceiptDocument");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        ds = null;
                        adapter.Dispose();
                        con.Close();
                        con.Dispose();
                    }

                    string sn, cm, nm, wt;
                    for (int i = 0; i < repairs.Count; i++)
                    {
                        nm = repairs[i].Nomenclature != null ? repairs[i].Nomenclature1.Name.ToString() : "Пусто";
                        sn = repairs[i].Serial_Number != null ? repairs[i].Serial_Number.ToString() : "";
                        cm = repairs[i].Claimed_Malfunction != null ? repairs[i].Claimed_Malfunction.ToString() : "";
                        wt = repairs[i].Warranty != null ? repairs[i].Warrantys.Warranty.ToString() : "нет";
                        sql = "INSERT INTO Repairs(DocumentId, Nomenclature, Serial_Number, Claimed_Malfunction, Client, Date_Of_Receipt, Inspector, Warranty)  VALUES ("
                             + documentId + "," +
                             "'" + nm + "'," +
                             "'" + sn + "'," +
                             "'" + cm + "'," +
                             +selectClientItem.ClientId + "," +
                             "'" + date_Of_Receipt.ToString("yyyy/MM/dd") + "'," +
                             +selectUserItem.UserId + "," +
                             "'" + wt + "')";
                        try
                        {
                            con = new SqlConnection(connectionString);
                            SqlCommand command = new SqlCommand(sql, con);
                            adapter = new SqlDataAdapter(command);
                            adapter.InsertCommand = new SqlCommand(sql, con);
                            con.Open();
                            ds = new DataSet();
                            adapter.Fill(ds, "Repairs");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            ds = null;
                            adapter.Dispose();
                            con.Close();
                            con.Dispose();
                        }
                    }
                    repairs.Clear();
                    GetDocumentId();
                    MessageBox.Show("Произведена успешная запись в базу данных!", "Результат");
                }
            }
            else
                MessageBox.Show("Необходимо выбрать клиента и приемщика!", "Ошибка");
        }
        #endregion

        #region Edits checked rows
        private ICommand editNomenclature;
        public ICommand EditNomenclatureCommand
        {
            get
            {
                if (editNomenclature == null)
                {
                    editNomenclature = new RelayCommand(new Action<object>(EditingNomenclature));
                }
                return editNomenclature;
            }
            set
            {
                editNomenclature = value;
                RaisedPropertyChanged("EditNomenclatureCommand");
            }
        }
        private ICommand editWarranty;
        public ICommand EditWarrantyCommand
        {
            get
            {
                if (editWarranty == null)
                {
                    editWarranty = new RelayCommand(new Action<object>(EditingWarranty));
                }
                return editWarranty;
            }
            set
            {
                editWarranty = value;
                RaisedPropertyChanged("EditWarrantyCommand");
            }
        }
        void EditingNomenclature(object Parameter)
        {
            for (int i = 0; i < repairs.Count; i++)
            {
                if (repairs[i].IsChecked == true)
                {
                    repairs[i].Nomenclature1 = selectedNomenclatureItem;
                }
            }
        }
        void EditingWarranty(object Parameter)
        {
            for (int i = 0; i < repairs.Count; i++)
            {
                if (repairs[i].IsChecked == true)
                {
                    repairs[i].Warrantys = selectedWarrantyItem;
                }
            }
        }
        #endregion

        #region AllSelected
        private bool? isSelectedAll;
        public bool? IsSelectAll
        {
            get
            {
                return isSelectedAll;
            }

            set
            {
                isSelectedAll = value;
                if (isSelectedAll.HasValue && isSelectedAll.Value)
                {
                    SelectAll();
                }
                else
                {
                    DeselectAll();
                }
                RaisedPropertyChanged("IsSelectAll");
            }
        }

        private void SelectAll()
        {
            foreach (var item in repairs)
            {
                item.IsChecked = true;
            }
        }

        private void DeselectAll()
        {
            foreach (var item in repairs)
            {
                item.IsChecked = false;
            }
        }
        #endregion
    }
}
