using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Clients_f
{
    class AddClientVM : ClientsVM
    {
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PronetsDBEntities"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds;
        public AddClientVM()
        {
        }

        #region AddCommand
        private ICommand addItem;
        public ICommand AddCommand
        {
            get
            {
                if (addItem == null)
                {
                    addItem = new RelayCommand(new Action<object>(AddItem));
                }
                return addItem;
            }
            set
            {
                addItem = value;
                RaisedPropertyChanged("AddCommand");
            }
        }
        public void AddItem(object Parameter)
        {
            string sql;

            sql = "Insert into Clients values(" +
                           "'" + ClientName + "'," +
                           "'" + Inn + "'," +
                           "'" + Contact_Person + "'," +
                           "'" + Telephone_1 + "'," +
                           "'" + Telephone_2 + "'," +
                           "'" + Telephone_3 + "'," +
                           "'" + Email + "'," +
                           "'" + Adress + "')";

            var result = MessageBox.Show("Вы Действительно хотете добавить клиента?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    con = new SqlConnection(connectionString);
                    cmd = new SqlCommand(sql, con);
                    adapter = new SqlDataAdapter(cmd);
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
                    ClientName = string.Empty;
                    Inn = string.Empty;
                    Contact_Person = string.Empty;
                    Telephone_1 = string.Empty;
                    Telephone_2 = string.Empty;
                    Telephone_3 = string.Empty;
                    Email = string.Empty;
                    Adress = string.Empty;
                }

            }
        }
        #endregion
    }
}
