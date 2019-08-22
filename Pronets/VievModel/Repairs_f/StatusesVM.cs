using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Repairs_f
{
    public class StatusesVM : VievModelBase
    {
        public static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PronetsDBEntities"].ConnectionString;
        public SqlConnection con = new SqlConnection(connectionString);
        public SqlDataAdapter adapter;
        public SqlCommand cmd;
        public DataSet ds;
        private ObservableCollection<Statuses> statuses;
        public ObservableCollection<Statuses> Statuses
        {
            get { return this.statuses; }

            set
            {
                statuses = value;
                RaisedPropertyChanged("Statuses");
            }
        }
        public StatusesVM()
        {
            FillList();
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
        private void FillList()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select * from Statuses", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Statuses");
                if (statuses == null)
                    statuses = new ObservableCollection<Statuses>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    statuses.Add(new Statuses
                    {
                        Status = dr[0].ToString()
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


        private ICommand addItem;
        public ICommand AddCommand
        {
            get
            {
                if (addItem == null)
                {
                    addItem = new RelayCommand(new Action<object>(AddType));
                }
                return addItem;
            }
            set
            {
                addItem = value;
                RaisedPropertyChanged("AddCommand");

            }
        }
        private void AddType(object Parameter)
        {
            if (status != null && status.Length > 0)
            {
                string sql = "INSERT Statuses VALUES ('" + status + "')";

                try
                {
                    con = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand(sql, con);
                    adapter = new SqlDataAdapter(command);
                    adapter.InsertCommand = new SqlCommand(sql, con);
                    con.Open();
                    ds = new DataSet();
                    adapter.Fill(ds, "Statuses");
                    statuses.Add(
                        new Statuses
                        {
                            Status = status
                        });
                    Status = string.Empty;
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
            else
                MessageBox.Show("Необходимо ввести название!", "Ошибка");

        }

        private Statuses selectedItem;
        public Statuses SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }


        private ICommand removeItem;
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


        private void RemoveItem(object Parameter)
        {
            if (selectedItem != null)
            {
                string sql = "delete from Statuses where Status='" + SelectedItem.Status.ToString() + "'";

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
                        adapter.Fill(ds, "Statuses");
                        statuses.RemoveAt(selectedIndex);
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
                MessageBox.Show("Необходимо выбрать элемент в списке!", "Ошибка");
        }
    }
}

