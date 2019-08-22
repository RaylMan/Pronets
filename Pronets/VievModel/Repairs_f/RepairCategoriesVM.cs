using Pronets.Data;
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

namespace Pronets.VievModel.Repairs_f
{
    class RepairCategoriesVM : VievModelBase
    {
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PronetsDBEntities"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds;
        #region Property
        private ObservableCollection<Repair_Categories> repair_Categories;
        public ObservableCollection<Repair_Categories> Repair_Categories
        {
            get { return this.repair_Categories; }

            set
            {
                repair_Categories = value;
                RaisedPropertyChanged("Repair_Categories");
            }
        }
        private string category;
        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                RaisedPropertyChanged("Category");
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
        protected Repair_Categories selectedItem;
        public Repair_Categories SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        #endregion
        public RepairCategoriesVM()
        {
            FillList();
        }
        void FillList()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select * from Repair_Categories", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Repair_Categories");
                if (repair_Categories == null)
                    repair_Categories = new ObservableCollection<Repair_Categories>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    repair_Categories.Add(new Repair_Categories
                    {
                        Category = (dr[0] is DBNull) ? null : dr[0].ToString(),
                        Price = Convert.ToDecimal(dr[1])
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
            if (category != null && category != "" && category != "")
            {
                sql = "Insert into Repair_Categories values(" +
                          "'" + Category + "'," +
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
                        adapter.Fill(ds, "Repair_Categories");
                        repair_Categories.Add(new Repair_Categories
                        {
                            Category = Category,
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
                        Category = string.Empty;
                        Price = 0;
                    }


                }
            }
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
            repair_Categories.Clear();
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select * from Repair_Categories", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Repair_Categories");
                if (repair_Categories == null)
                    repair_Categories = new ObservableCollection<Repair_Categories>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    repair_Categories.Add(new Repair_Categories
                    {
                        Category = (dr[0] is DBNull) ? null : dr[0].ToString(),
                        Price = Convert.ToDecimal(dr[1])
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
                string sql = "delete from Repair_Categories where Category='" + SelectedItem.Category.ToString() + "'";
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
                        adapter.Fill(ds, "Repair_Categories");
                        repair_Categories.RemoveAt(selectedIndex);
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
