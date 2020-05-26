using Pronets.Data;
using Pronets.EntityRequests.Users_f;
using Pronets.Viev.MainWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

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
                tbxPassword.Password = Properties.Settings.Default.Password;
            }

        }
        private async void Login()
        {
            SaveLogin();
            try
            {
                string login = tbxLogin.Text;
                string pwd = tbxPassword.Password;

                await Task.Factory.StartNew(() =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        button.Content = "Загрузка";
                        button.IsEnabled = false;
                        btnSettings.IsEnabled = false;
                    });
                    try
                    {
                        user = UsersRequest.LoginWork(login, pwd);
                        if (user == null) //true => нет совпадений
                        {
                            user = new Users();
                            user.UserId = -1;
                        }
                    }
                    catch (Exception)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            button.Content = "Войти";
                            button.IsEnabled = true;
                            btnSettings.IsEnabled = true;
                        });
                        MessageBox.Show("Ошибка подключения к серверу!", "Ошибка");
                    }

                    //this.Dispatcher.Invoke(() =>
                    //{
                    //    button.Content = "Войти";
                    //    button.IsEnabled = true;
                    //    btnSettings.IsEnabled = true;
                    //});
                });

                if (user != null)
                {
                    if (user.UserId != -1)
                    {
                        GetDefaultUser();
                        if (user.Position == "Администратор" || user.Position == "Директор")
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
                        this.Dispatcher.Invoke(() =>
                        {
                            button.Content = "Войти";
                            button.IsEnabled = true;
                            btnSettings.IsEnabled = true;
                        });
                        if (!Properties.Settings.Default.SaveLogin)
                            tbxLogin.Clear();
                        tbxPassword.Clear();
                        MessageBox.Show("Введен не правильный логин или пароль");
                    }
                }
            }
            catch (Exception ex)
            {
                string er = $"{Properties.Settings.Default.DefaultUserId}\n" +
                    $"{user.UserId}\n" +
                    $"{ex.HelpLink}\n" +
                    $"{ex.HResult}\n" +
                    $"{ex.InnerException}\n" +
                    $"{ex.Message}\n" +
                    $"{ex.Source}\n" +
                    $"{ex.StackTrace}\n" +
                    $"{ex.TargetSite}\n";
                MessageBox.Show(er);
                this.Dispatcher.Invoke(() =>
                {
                    button.Content = "Войти";
                    button.IsEnabled = true;
                    btnSettings.IsEnabled = true;
                });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click(sender, e);
        }
        /// <summary>
        /// Открывает окно настроек подключения к SQL server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            DataBaseSettingsWindow win = DataBaseSettingsWindow.WindowInstance;
            win.Show();
            win.Focus();
        }
        /// <summary>
        /// Сохраняет логин в настройки, для автоматического заполнения tbxLogin и tbxPassword при открытии окна.
        /// </summary>
        private void SaveLogin()
        {
            if (cbxSaveLogin.IsChecked == true)
            {
                Properties.Settings.Default.Login = tbxLogin.Text;
                Properties.Settings.Default.Password = tbxPassword.Password;
                Properties.Settings.Default.SaveLogin = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Login = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.SaveLogin = false;
                Properties.Settings.Default.Save();
            }
        }
        private void GetDefaultUser()
        {
            if (user != null)
            {
                Properties.Settings.Default.DefaultLastName = user.LastName;
                Properties.Settings.Default.DefaultUserId = user.UserId;
                Properties.Settings.Default.Save();
            }
        }
    }
}
