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
using Pronets.EntityRequests.Users_f;
using Pronets.Viev.MainWindows;

namespace Pronets.Viev
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public Users user;
        public StartWindow()
        {
            InitializeComponent();
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
            user = UsersRequest.Login(tbxLogin.Text, tbxPassword.Password);
            if (user != null)
            {
                WorkWindowAdmin workWindowAdmin = new WorkWindowAdmin(user);

                workWindowAdmin.Show();
                this.Close();
            }
            else
            {
                tbxLogin.Clear();
                tbxPassword.Clear();
                MessageBox.Show("Введен не правильный логин или пароль");
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click(sender, e);
        }
    }
}