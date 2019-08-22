using Pronets.Data;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Nomenclature_f
{
    class NomenclatureVM : VievModelBase
    {
        public OpenWindowCommand OpenWindowCommand { get; private set; }
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PronetsDBEntities"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds;
        #region Property
        private ObservableCollection<Nomenclature> nomenclature;
        public ObservableCollection<Nomenclature> Nomenclature
        {
            get { return this.nomenclature; }

            set
            {
                nomenclature = value;
                RaisedPropertyChanged("Nomenclature");
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
        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                RaisedPropertyChanged("Type");
            }
        }
        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                RaisedPropertyChanged("Price");
            }
        }
        protected Nomenclature selectedItem;
        public Nomenclature SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }

        private ObservableCollection<Nomenclature_Types> nomenclature_Types;
        public ObservableCollection<Nomenclature_Types> Nomenclature_Types
        {
            get { return nomenclature_Types; }

            set
            {
                nomenclature_Types = value;
                RaisedPropertyChanged("Nomenclature_Types");
            }
        }
        private Nomenclature_Types selItem;
        public Nomenclature_Types SelItem
        {
            get { return selItem; }
            set
            {
                selItem = value;
                RaisedPropertyChanged("SelItem");
            }
        }

        #endregion
        public NomenclatureVM()
        {
            OpenWindowCommand = new OpenWindowCommand();
            FillList();
            FillCombobox();
        }
        void FillList()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select * from Nomenclature", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Nomenclature");
                if (nomenclature == null)
                    nomenclature = new ObservableCollection<Nomenclature>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    nomenclature.Add(new Nomenclature
                    {
                        Name = (dr[0] is DBNull) ? null : dr[0].ToString(),
                        Type = dr[1].ToString(),
                        Price = Convert.ToDecimal(dr[2])
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
        #region Combobox
        public void FillCombobox()
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select * from Nomenclature_Types", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Nomenclature_Types");
                if (nomenclature_Types == null)
                    nomenclature_Types = new ObservableCollection<Nomenclature_Types>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    nomenclature_Types.Add(new Nomenclature_Types
                    {
                        Type = dr[0].ToString()
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
            if (name != null && name != " " && name != "" && selItem != null)
            {
                sql = "Insert into Nomenclature values(" +
                      "'" + Name + "'," +
                       "'" + selItem.Type.ToString() + "'," +
                       +Price + ")";


                var result = MessageBox.Show("Вы Действительно хотете добавить?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
                        adapter.Fill(ds, "Nomenclature");
                        nomenclature.Add(new Nomenclature
                        {
                            Name = Name,
                            Type = selItem.Type.ToString(),
                            Price = Price
                        });

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
                        Name = string.Empty;
                        Price = 0;
                    }

                }
            }
            else
                MessageBox.Show("1) Введите название номенклатуры;\n2) Выберете тип оборудования.", "Ошибка");
        }
        #endregion

        #region FillLost button
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
            nomenclature.Clear();
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select * from Nomenclature", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Nomenclature");
                if (nomenclature == null)
                    nomenclature = new ObservableCollection<Nomenclature>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    nomenclature.Add(new Nomenclature
                    {
                        Name = (dr[0] is DBNull) ? null : dr[0].ToString(),
                        Type = dr[1].ToString(),
                        Price = Convert.ToDecimal(dr[2])
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

        #region FillComboBox
        protected ICommand fillComboItems;
        public ICommand FillComboBoxCommand
        {
            get
            {
                if (fillComboItems == null)
                {
                    fillComboItems = new RelayCommand(new Action<object>(FillComboBox));
                }
                return fillComboItems;
            }
            set
            {
                fillItems = value;
                RaisedPropertyChanged("FillComboBoxCommand");
            }
        }
        public void FillComboBox(object Parametr)
        {
            try
            {
                nomenclature_Types.Clear();
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select * from Nomenclature_Types", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Nomenclature_Types");
                if (nomenclature_Types == null)
                    nomenclature_Types = new ObservableCollection<Nomenclature_Types>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    nomenclature_Types.Add(new Nomenclature_Types
                    {
                        Type = dr[0].ToString()
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

        #region RemoveCommand
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
                string sql = "delete from Nomenclature where Name='" + SelectedItem.Name.ToString() + "'";
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
                        adapter.Fill(ds, "Nomenclature");
                        nomenclature.RemoveAt(selectedIndex);
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
                MessageBox.Show("Необходимо выбрать элемент в таблице!", "Ошибка");
        }
        #endregion
    }
}
