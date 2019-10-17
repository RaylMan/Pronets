using Pronets.Data;
using Pronets.Viev.Other;
using Pronets.VievModel.MainWindows;
using Pronets.VievModel.MainWindows.Pages;
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
    /// Логика взаимодействия для WorkWindowInspector.xaml
    /// </summary>
    public partial class WorkWindowInspector : Window
    {
        Users user;

        public WorkWindowInspector(Users loginUser)
        {
            if (loginUser != null)
                this.user = loginUser;
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        public WorkWindowInspector()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            Navigation.Navigation.Service = MainFrame.NavigationService;
            DataContext = new WorkWindowAdminVM(new ViewModelsResolver(), user);
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
            UserOvertimeWindow overtimeWindow = new UserOvertimeWindow(user);
            overtimeWindow.Show();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(user);
            settingsWindow.Show();
        }
    }
}
