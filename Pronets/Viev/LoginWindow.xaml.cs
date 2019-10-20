using Pronets.Data;
using Pronets.EntityRequests.Users_f;
using Pronets.Viev.MainWindows;
using System;
using System.Collections.Generic;
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

namespace Pronets.Viev
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        Users user;
        public LoginWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.SaveLogin)
            {
                cbxSaveLogin.IsChecked = Properties.Settings.Default.SaveLogin;
                tbxLogin.Text = Properties.Settings.Default.Login;
            }
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            WorkWindowAdmin workWindowAdmin = new WorkWindowAdmin(UsersRequest.Login("admin", "password"));
            workWindowAdmin.Show();
            this.Close();
            Properties.Settings.Default.Save();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveLogin();
            user = UsersRequest.Login(tbxLogin.Text, tbxPassword.Password);
            if (user != null)
            {
                if (user.Position == "Администратор" && user.Position == "Директор")
                {
                    WorkWindowAdmin workWindowAdmin = new WorkWindowAdmin(user);
                    workWindowAdmin.Show();
                    this.Close();
                }
                else if (user.Position == "Инженер")
                {
                    WorkWindowEngineer workWindowEngineer = new WorkWindowEngineer(user);
                    workWindowEngineer.Show();
                    this.Close();
                }
                else if (user.Position == "Приемщик")
                {
                    WorkWindowInspector workWindowInspector = new WorkWindowInspector(user);
                    workWindowInspector.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Неверные права доступа у пользователя!\nОбратитесь к администратору!", "Ошибка");
            }
            else
            {
                if (!Properties.Settings.Default.SaveLogin)
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

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            DataBaseSettingsWindow win = new DataBaseSettingsWindow();
            win.Show();
        }
        private void SaveLogin()
        {
            if (cbxSaveLogin.IsChecked == true)
            {
                Properties.Settings.Default.Login = tbxLogin.Text;
                Properties.Settings.Default.SaveLogin = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Login = "";
                Properties.Settings.Default.SaveLogin = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
