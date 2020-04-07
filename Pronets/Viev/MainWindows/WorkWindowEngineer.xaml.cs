using Pronets.Data;
using Pronets.Viev.Other;
using Pronets.Viev.Repairs_f;
using Pronets.Viev.Users_f;
using Pronets.VievModel.MainWindows;
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

namespace Pronets.Viev.MainWindows
{
    /// <summary>
    /// Логика взаимодействия для WorkWindowEngineer.xaml
    /// </summary>
    public partial class WorkWindowEngineer : Window
    {
        Users user;
        public WorkWindowEngineer(Users loginUser)
        {
           
            if (loginUser != null)
                this.user = loginUser;
            DataContext = new WorkWindowEngineerVM(loginUser);
            InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Overtime_Click(object sender, RoutedEventArgs e)
        {
            UserOvertimeWindow overtimeWindow = UserOvertimeWindow.GetUserOvertimeWindowInstance(user);
            overtimeWindow.Show();
            overtimeWindow.Focus();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = SettingsWindow.GetSettingsWindowInstance(user);
            settingsWindow.Show();
            settingsWindow.Focus();
        }

        private void Open_PartsOrder_Click(object sender, RoutedEventArgs e)
        {
            PartsWindow win = PartsWindow.PartsWindowInstance;
            win.Show();
            win.Focus();
        }

        private void Open_AllRepairsWindow_Click(object sender, RoutedEventArgs e)
        {
            AllRepairsWindow win = AllRepairsWindow.Instance;
            win.Show();
            win.Focus();
        }

        private void CalculateRepairs_Click(object sender, RoutedEventArgs e)
        {
            CountOfRepairsWindow win = CountOfRepairsWindow.Instance;
            win.Show();
            win.Focus();
        }

        private void SelfUserReportWindow_Click(object sender, RoutedEventArgs e)
        {
            SelfUserReportWindow win = new SelfUserReportWindow(user);
            win.ShowDialog();
        }
    }
}
