using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Users_f
{
    public class AddUserVM : UsersVM
    {
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PronetsDBEntities"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds;
        #region Properties

        private ObservableCollection<Positions> positions;
        public ObservableCollection<Positions> Positions
        {
            get { return positions; }

            set
            {
                positions = value;
                RaisedPropertyChanged("Positions");
            }
        }
        private Positions selItem;
        public Positions SelItem
        {
            get { return selItem; }
            set
            {
                selItem = value;
                RaisedPropertyChanged("SelItem");
            }
        }
        #endregion
        public AddUserVM()
        {
            FillCombobox();
        }

        #region Combobox and Datetimepicker
        public void FillCombobox()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select * from Positions", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Positions");
                if (positions == null)
                    positions = new ObservableCollection<Positions>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    positions.Add(new Positions
                    {
                        Position = dr[0].ToString()
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
            if (selItem != null)
            {

                sql = "Insert into Users values(" +
                               "'" + Login + "'," +
                               "'" + Password + "'," +
                               "'" + selItem.Position.ToString() + "'," +
                               "'" + FirstName + "'," +
                               "'" + LastName + "'," +
                               "'" + Patronymic + "'," +
                               "'" + Birthday.ToString("yyyy/MM/dd") + "'," +
                               "'" + Telephone + "'," +
                               "'" + Adress + "')";


                var result = MessageBox.Show("Вы Действительно хотете добавить работника?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
                        Login = string.Empty;
                        Password = string.Empty;
                        FirstName = string.Empty;
                        LastName = string.Empty;
                        Patronymic = string.Empty;
                        Birthday = DateTime.MinValue;
                        Telephone = string.Empty;
                        Adress = string.Empty;
                    }
                }

            }
            else
                MessageBox.Show("Необходимо выбрать уровень доступа!", "Ошибка");
        }
        #endregion
    }

}
