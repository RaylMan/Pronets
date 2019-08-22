using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Pronets.Data;
using Pronets.Viev.MainWindows;

namespace Pronets.Viev
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PronetsDBEntities"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds;
        public List<Users> userLogin = new List<Users>();
        public StartWindow()
        {
            InitializeComponent();
        }
        public void Select()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT * FROM[dbo].[Users] WHERE[Login] = '" + tbxLogin.Text + "' AND[password] = '" + tbxPassword.Password + "'", con);
                adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds, "Users");


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    userLogin.Add(new Users
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
        public DataTable Select(string selectSQL)
        {
            DataTable dt_user = new DataTable("UserBase");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dt_user);
            return dt_user;
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            WorkWindowAdmin workWindowAdmin = new WorkWindowAdmin();
            workWindowAdmin.Show();
            this.Close();
            Properties.Settings.Default.ProgOpen++;
            Properties.Settings.Default.Save();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Select();
            if (userLogin.Count > 0) // если такая запись существует       
            {
                WorkWindowAdmin workWindowAdmin = new WorkWindowAdmin();
                //WorkWindowAdminVM workWindowAdminVM = new WorkWindowAdminVM();
                //workWindowAdminVM.user1 = userLogin;
                workWindowAdmin.Show();
                this.Close();
            }

            else
                MessageBox.Show("Введен не правильный логин или пароль");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click(sender, e);
        }
    }
}