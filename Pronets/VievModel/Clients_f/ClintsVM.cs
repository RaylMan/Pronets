using Pronets.Data;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Clients_f
{
    public class ClientsVM : VievModelBase
    {
        public OpenWindowCommand OpenWindowCommand { get; set; } //Команда открытия нового окна
        
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PronetsDBEntities"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds;
        #region Properties
        protected ObservableCollection<Clients> clients;
        public ObservableCollection<Clients> Clients
        {
            get { return clients; }

            set
            {
                clients = value;
                RaisedPropertyChanged("Clients");
            }
        }

        protected string clientId;
        public string ClientId
        {
            get { return clientId; }
            set
            {
                clientId = value;
                RaisedPropertyChanged("ClientId");
            }
        }
        protected string clientName;
        public string ClientName
        {
            get { return clientName; }
            set
            {
                clientName = value;
                RaisedPropertyChanged("ClientName");
            }
        }
        protected string inn;
        public string Inn
        {
            get { return inn; }
            set
            {
                inn = value;
                RaisedPropertyChanged("Inn");
            }
        }
        protected string contact_Person;
        public string Contact_Person
        {
            get { return contact_Person; }
            set
            {
                contact_Person = value;
                RaisedPropertyChanged("Contact_Person");
            }
        }
        protected string telephone_1;
        public string Telephone_1
        {
            get { return telephone_1; }
            set
            {
                telephone_1 = value;
                RaisedPropertyChanged("Telephone_1");
            }
        }
        protected string telephone_2;
        public string Telephone_2
        {
            get { return telephone_2; }
            set
            {
                telephone_2 = value;
                RaisedPropertyChanged("Telephone_2");
            }
        }
        protected string telephone_3;
        public string Telephone_3
        {
            get { return telephone_3; }
            set
            {
                telephone_3 = value;
                RaisedPropertyChanged("Telephone_3");
            }
        }
        protected string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisedPropertyChanged("Email");
            }
        }
        protected string adress;
        public string Adress
        {
            get { return adress; }
            set
            {
                adress = value;
                RaisedPropertyChanged("Adress");
            }
        }

        protected Clients selectedItem;
        public Clients SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        protected string searchText;
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


        public ClientsVM()
        {
            OpenWindowCommand = new OpenWindowCommand(); // создание экземпляра открытия окна
           FillList();
        }

        protected void FillList()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select * from Clients", con);
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
                        ClientName = (dr[1] is DBNull) ? null : dr[1].ToString(),
                        Inn = (dr[2] is DBNull) ? null : dr[2].ToString(),
                        Contact_Person = (dr[3] is DBNull) ? null : dr[3].ToString(),
                        Telephone_1 = (dr[4] is DBNull) ? null : dr[4].ToString(),
                        Telephone_2 = (dr[5] is DBNull) ? null : dr[5].ToString(),
                        Telephone_3 = (dr[6] is DBNull) ? null : dr[6].ToString(),
                        Email = (dr[7] is DBNull) ? null : dr[7].ToString(),
                        Adress = (dr[8] is DBNull) ? null : dr[8].ToString()
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
        protected void FillList(object Parameter)
        {
            clients.Clear();
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select * from Clients", con);
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
                        ClientName = (dr[1] is DBNull) ? null : dr[1].ToString(),
                        Inn = (dr[2] is DBNull) ? null : dr[2].ToString(),
                        Contact_Person = (dr[3] is DBNull) ? null : dr[3].ToString(),
                        Telephone_1 = (dr[4] is DBNull) ? null : dr[4].ToString(),
                        Telephone_2 = (dr[5] is DBNull) ? null : dr[5].ToString(),
                        Telephone_3 = (dr[6] is DBNull) ? null : dr[6].ToString(),
                        Email = (dr[7] is DBNull) ? null : dr[7].ToString(),
                        Adress = (dr[8] is DBNull) ? null : dr[8].ToString()
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

        #region Delete Command

        protected ICommand removeItem;
        public ICommand RemoveCommand
        {
            get
            {
                if (removeItem == null)
                {
                    removeItem = new RelayCommand(new Action<object>(RemoveItem));
                }
                return removeItem;
            }
            set
            {
                removeItem = value;
                RaisedPropertyChanged("RemoveCommand");
            }
        }
        public void RemoveItem(object Parameter)
        {
            if (selectedItem != null)
            {
                string sql = "delete from Clients where ClientId=" + SelectedItem.ClientId.ToString();
                var result = MessageBox.Show("Вы Действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        con = new SqlConnection(connectionString);
                        SqlCommand command = new SqlCommand(sql, con);
                        adapter = new SqlDataAdapter(command);
                        adapter.InsertCommand = new SqlCommand(sql, con);
                        con.Open();
                        ds = new DataSet();
                        adapter.Fill(ds, "Clients");
                        clients.RemoveAt(selectedIndex);
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

            }
            else
                MessageBox.Show("Необходимо выбрать элемент!", "Ошибка");
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
            string sql;
            if (selectedItem != null)
            {
                if (SelectedItem.ClientId != 0)
                {
                    sql = "Update Clients set " +
                                   "ClientName ='" + selectedItem.ClientName + "'," +
                                   "Inn ='" + selectedItem.Inn + "'," +
                                   "Contact_Person ='" + selectedItem.Contact_Person + "'," +
                                   "Telephone_1 ='" + selectedItem.Telephone_1 + "'," +
                                   "Telephone_2 ='" + selectedItem.Telephone_2 + "'," +
                                   "Telephone_3 ='" + selectedItem.Telephone_3 + "'," +
                                   "Email ='" + selectedItem.Email + "'," +
                                   "Adress ='" + selectedItem.Adress + "'" +
                                   "where ClientId=" + SelectedItem.ClientId.ToString();
                }
                else
                {
                    sql = "Insert into Clients values(" +
                                   "'" + selectedItem.ClientName + "'," +
                                   "'" + selectedItem.Inn + "'," +
                                   "'" + selectedItem.Contact_Person + "'," +
                                   "'" + selectedItem.Telephone_1 + "'," +
                                   "'" + selectedItem.Telephone_2 + "'," +
                                   "'" + selectedItem.Telephone_3 + "'," +
                                   "'" + selectedItem.Email + "'," +
                                   "'" + selectedItem.Adress + "')";
                }

                var result = MessageBox.Show("Вы Действительно хотете редактировать?", "Редактирование", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {

                    try
                    {
                        con = new SqlConnection(connectionString);
                        SqlCommand command = new SqlCommand(sql, con);
                        adapter = new SqlDataAdapter(command);
                        adapter.InsertCommand = new SqlCommand(sql, con);
                        con.Open();
                        ds = new DataSet();
                        adapter.Fill(ds, "Clients");
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

            }
            else
                MessageBox.Show("Необходимо выбрать элемент!", "Ошибка");
        }
        #endregion

        #region FillList button command
        protected ICommand fillItems;
        public ICommand FillListCommand
        {
            get
            {
                if (fillItems == null)
                {
                    fillItems = new RelayCommand(new Action<object>(FillList));
                }
                return fillItems;
            }
            set
            {
                editItem = value;
                RaisedPropertyChanged("FillListCommand");
            }
        }
        #endregion

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
                editItem = value;
                RaisedPropertyChanged("SearchCommand");
            }
        }

        public void SearchItem(object Parameter)
        {
            clients.Clear();
            string sql = "SELECT * FROM Clients where ClientName = '" + searchText + "' or Inn = '" + searchText + "' or Contact_Person = '"
                + searchText + "' or Telephone_1 = '" + searchText + "' or Telephone_2 = '" + searchText + "' or Telephone_3 = '" + searchText + "' or Email = '"
                + searchText + "' or Adress = '" + searchText + "'";

            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, con);
                adapter = new SqlDataAdapter(command);
                adapter.InsertCommand = new SqlCommand(sql, con);
                con.Open();
                ds = new DataSet();
                adapter.Fill(ds, "Clients");
                if (clients == null)
                    clients = new ObservableCollection<Clients>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clients.Add(new Clients
                    {
                        ClientId = Convert.ToInt32(dr[0]),
                        ClientName = (dr[1] is DBNull) ? null : dr[1].ToString(),
                        Inn = (dr[2] is DBNull) ? null : dr[2].ToString(),
                        Contact_Person = (dr[3] is DBNull) ? null : dr[3].ToString(),
                        Telephone_1 = (dr[4] is DBNull) ? null : dr[4].ToString(),
                        Telephone_2 = (dr[5] is DBNull) ? null : dr[5].ToString(),
                        Telephone_3 = (dr[6] is DBNull) ? null : dr[6].ToString(),
                        Email = (dr[8] is DBNull) ? null : dr[8].ToString(),
                        Adress = (dr[8] is DBNull) ? null : dr[8].ToString()
                    });
                    searchText = string.Empty;
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
    }
}
