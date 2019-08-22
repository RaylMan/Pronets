using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Nomenclature_f
{
    public class Nomenclature_TypesVM : VievModelBase
    {
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PronetsDBEntities"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds;
        private ObservableCollection<Nomenclature_Types> nomenclatures_Types;
        public ObservableCollection<Nomenclature_Types> Nomenclatures_Types
        {
            get { return this.nomenclatures_Types; }

            set
            {
                nomenclatures_Types = value;
                RaisedPropertyChanged("Nomenclatures_Types");
            }
        }
        private string nomType;
        public string NomType
        {
            get { return nomType; }
            set
            {
                nomType = value;
                RaisedPropertyChanged("NomType");
            }
        }

        public Nomenclature_TypesVM()
        {
            FillList();
        }

        public void FillList()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select * from Nomenclature_Types", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Nomenclature_Types");
                if (nomenclatures_Types == null)
                    nomenclatures_Types = new ObservableCollection<Nomenclature_Types>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    nomenclatures_Types.Add(new Nomenclature_Types
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
        public void AddType(object Parameter)
        {
            if (nomType != null && nomType.Length > 0)
            {
                string sql = "INSERT Nomenclature_Types VALUES ('" + nomType + "')";

                try
                {

                    con = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand(sql, con);
                    adapter = new SqlDataAdapter(command);
                    adapter.InsertCommand = new SqlCommand(sql, con);
                    con.Open();
                    ds = new DataSet();
                    adapter.Fill(ds, "Nomenclature_Types");
                    nomenclatures_Types.Add(
                        new Nomenclature_Types
                        {
                            Type = nomType
                        });
                    NomType = string.Empty;
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

        private Nomenclature_Types selectedItem;
        public Nomenclature_Types SelectedItem
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


        public void RemoveItem(object Parameter)
        {
            if (selectedItem != null)
            {
                string sql = "delete from Nomenclature_Types where Type='" + SelectedItem.Type.ToString() + "'";
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
                        adapter.Fill(ds, "Nomenclature_Types");
                        nomenclatures_Types.RemoveAt(selectedIndex);
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
