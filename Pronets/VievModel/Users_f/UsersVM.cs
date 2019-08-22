using Pronets.Data;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Users_f
{
    public class UsersVM : VievModelBase
    {
        public OpenWindowCommand OpenWindowCommand { get; private set; }
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PronetsDBEntities"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds;
        #region UsersVM Properties
        protected ObservableCollection<Users> users;
        public ObservableCollection<Users> Users
        {
            get { return users; }

            set
            {
                users = value;
                RaisedPropertyChanged("Users");
            }
        }


        protected string userId;
        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                RaisedPropertyChanged("UserId");
            }
        }
        protected string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                RaisedPropertyChanged("Login");
            }
        }
        protected string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisedPropertyChanged("Password");
            }
        }
        protected string position;
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                RaisedPropertyChanged("Position");
            }
        }
        protected string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                RaisedPropertyChanged("LastName");
            }
        }
        protected string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                RaisedPropertyChanged("FirstName");
            }
        }
        protected string patronymic;
        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                patronymic = value;
                RaisedPropertyChanged("Patronymic");
            }
        }
        protected DateTime birthday;
        public DateTime Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                RaisedPropertyChanged("Birthday");
            }
        }
        protected string telephone;
        public string Telephone
        {
            get { return telephone; }
            set
            {
                telephone = value;
                RaisedPropertyChanged("Telephone");
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
        protected Users selectedItem;
        public Users SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        #endregion


        public UsersVM()
        {
            OpenWindowCommand = new OpenWindowCommand();
            FillList();
            //FillUsers();
        }
        protected ObservableCollection<Users> users1;
        public ObservableCollection<Users> Users1
        {
            get { return users1; }

            set
            {
                users1 = value;
                RaisedPropertyChanged("Users1");
            }
        }
        //private void FillUsers()
        //{
        //    using (var context = new PronetsDBEntities())
        //    {
        //        foreach (Users user in context.Users)
        //        {
        //            users1.Add(new Users
        //            {
        //                UserId = user.UserId,
        //                Login = user.Login,
        //                Password = user.Password,
        //                Position = user.Position,
        //                FirstName = user.FirstName,
        //                LastName = user.LastName,
        //                Patronymic = user.Patronymic,
        //                Birthday = user.Birthday,
        //                Telephone = user.Telephone,
        //                Adress = user.Adress
        //            });
        //        }

        //    }
        //}
        protected void FillList()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select * from Users", con);
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
                        Login = dr[1].ToString(),
                        Password = dr[2].ToString(),
                        Position = dr[3].ToString(),
                        FirstName = (dr[4] is DBNull) ? null : dr[4].ToString(),
                        LastName = (dr[5] is DBNull) ? null : dr[5].ToString(),
                        Patronymic = (dr[6] is DBNull) ? null : dr[6].ToString(),
                        Birthday = (dr[7] is DBNull) ? DateTime.MinValue : Convert.ToDateTime(dr[7]),
                        Telephone = (dr[8] is DBNull) ? null : dr[8].ToString(),
                        Adress = (dr[9] is DBNull) ? null : dr[9].ToString()
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
                string sql = "delete from Users where UserId=" + SelectedItem.UserId.ToString();
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
                        adapter.Fill(ds, "Users");
                        users.RemoveAt(selectedIndex);
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
                MessageBox.Show("Необходимо выбрать элемент", "Ошибка");
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
                if (SelectedItem.UserId != 0)
                {
                    sql = "Update Users set " +
                                   "Login ='" + selectedItem.Login + "'," +
                                   "Password ='" + selectedItem.Password + "'," +
                                   "Position ='" + selectedItem.Position + "'," +
                                   "FirstName ='" + selectedItem.FirstName + "'," +
                                   "LastName ='" + selectedItem.LastName + "'," +
                                   "Patronymic ='" + selectedItem.Patronymic + "'," +
                                   "Birthday = '" + selectedItem.Birthday.ToString("yyyy/MM/dd") + "'," +
                                   "Telephone ='" + selectedItem.Telephone + "'," +
                                   "Adress ='" + selectedItem.Adress + "'" +
                                   "where UserID=" + SelectedItem.UserId.ToString();
                }
                else
                {
                    sql = "Insert into Users values(" +
                                   "'" + selectedItem.Login + "'," +
                                   "'" + selectedItem.Password + "'," +
                                   "'" + selectedItem.Position + "'," +
                                   "'" + selectedItem.FirstName + "'," +
                                   "'" + selectedItem.LastName + "'," +
                                   "'" + selectedItem.Patronymic + "'," +
                                   "'" + selectedItem.Birthday.ToString("yyyy/MM/dd") + "'," +
                                   "'" + selectedItem.Telephone + "'," +
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
                        adapter.Fill(ds, "Users");
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
                MessageBox.Show("Необходимо выбрать элемент", "Ошибка");
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
                fillItems = value;
                RaisedPropertyChanged("FillListCommand");
            }
        }
        void FillList(object Parameter)
        {
            users.Clear();
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select * from Users", con);
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
                        Login = dr[1].ToString(),
                        Password = dr[2].ToString(),
                        Position = dr[3].ToString(),
                        FirstName = (dr[4] is DBNull) ? null : dr[4].ToString(),
                        LastName = (dr[5] is DBNull) ? null : dr[5].ToString(),
                        Patronymic = (dr[6] is DBNull) ? null : dr[6].ToString(),
                        Birthday = (dr[7] is DBNull) ? DateTime.MinValue : Convert.ToDateTime(dr[7]),
                        Telephone = (dr[8] is DBNull) ? null : dr[8].ToString(),
                        Adress = (dr[9] is DBNull) ? null : dr[9].ToString()
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
            users.Clear();
            string sql = "SELECT * FROM Users where Login = '" + searchText + "' or Password = '" + searchText + "' or Position = '"
                + searchText + "' or FirstName = '" + searchText + "' or LastName = '" + searchText + "' or Patronymic = '" + searchText + "' or Telephone = '"
                + searchText + "' or Adress = '" + searchText + "'";

            try
            {
                con = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, con);
                adapter = new SqlDataAdapter(command);
                adapter.InsertCommand = new SqlCommand(sql, con);
                con.Open();
                ds = new DataSet();
                adapter.Fill(ds, "Users");
                if (users == null)
                    users = new ObservableCollection<Users>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    users.Add(new Users
                    {
                        UserId = Convert.ToInt32(dr[0]),
                        Login = dr[1].ToString(),
                        Password = dr[2].ToString(),
                        Position = dr[3].ToString(),
                        FirstName = (dr[4] is DBNull) ? null : dr[4].ToString(),
                        LastName = (dr[5] is DBNull) ? null : dr[5].ToString(),
                        Patronymic = (dr[6] is DBNull) ? null : dr[6].ToString(),
                        Birthday = (dr[7] is DBNull) ? DateTime.MinValue : Convert.ToDateTime(dr[7]),
                        Telephone = (dr[8] is DBNull) ? null : dr[8].ToString(),
                        Adress = (dr[9] is DBNull) ? null : dr[9].ToString()
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
