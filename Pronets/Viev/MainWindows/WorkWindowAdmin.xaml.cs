﻿using Pronets.Data;
using Pronets.Viev.Other;
using Pronets.VievModel.MainWindows;
using Pronets.VievModel.MainWindows.Pages;
using Pronets.Viev.Repairs_f;
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
using Pronets.Viev.Users_f;
using Pronets.Viev.Clients_f;
using Pronets.Viev.Nomenclature_f;
using Pronets.Viev.ConvertToSQL;
using Pronets.Viev.Other.DefectiveStatement_fw;

namespace Pronets.Viev.MainWindows
{
    /// <summary>
    /// Логика взаимодействия для WorkWindowAdmin.xaml
    /// </summary>
    public partial class WorkWindowAdmin : Window
    {
        Users user;
        public WorkWindowAdmin(Users loginUser)
        {
            if (loginUser != null)
                this.user = loginUser;
            InitializeComponent();
            DataContext = new WorkWindowAdminVM(new ViewModelsResolver(), user);
            Loaded += MainWindow_Loaded;
        }
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Navigation.Navigation.Service = MainFrame.NavigationService;
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

        private void CalculateRepairs_Click(object sender, RoutedEventArgs e)
        {
            CountOfRepairsWindow win = CountOfRepairsWindow.Instance;
            win.Show();
            win.Focus();
        }

        private void ReportUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersReportWindow win = UsersReportWindow.UsersReportWindowInstance;
            win.Show();
            win.Focus();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            PartsWindow win = PartsWindow.PartsWindowInstance;
            win.Show();
            win.Focus();
        }

        private void Open_ClientsWindow_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow win = ClientsWindow.ClientsWindowInstance;
            win.Show();
            win.Focus();
        }

        private void Open_OvertimeWindow_Click(object sender, RoutedEventArgs e)
        {
            OvertimeWindow win = OvertimeWindow.OvertimeWindowInstance;
            win.Show();
            win.Focus();
        }

        private void Open_Users_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow win = UsersWindow.UsersWindowInstance;
            win.Show();
            win.Focus();
        }

        private void Open_NomenclatureWindow_Click(object sender, RoutedEventArgs e)
        {
            NomenclatureWindow win = NomenclatureWindow.NomenclatureWindowInstance;
            win.Show();
            win.Focus();
        }

        private void Open_AllRepairs_Click(object sender, RoutedEventArgs e)
        {
            AllRepairsWindow win = AllRepairsWindow.Instance;
            win.Show();
            win.Focus();
        }

        private void Open_StatusesWindow_Click(object sender, RoutedEventArgs e)
        {
            StatusesWindow win = StatusesWindow.Instance;
            win.Show();
            win.Focus();
        }

        private void Open_CategoriesWindow_Click(object sender, RoutedEventArgs e)
        {
            RepairCategoriesWindow win = RepairCategoriesWindow.Instance;
            win.Show();
            win.Focus();
        }

        private void Open_Export_Click(object sender, RoutedEventArgs e)
        {
            ConvertToSQLWindow win = ConvertToSQLWindow.Instatnce;
            win.Show();
            win.Focus();
        }

        private void SelfUserReportWindow_Click(object sender, RoutedEventArgs e)
        {
            SelfUserReportWindow win = new SelfUserReportWindow(user);
            win.ShowDialog();
        }

        private void barcode_Click(object sender, RoutedEventArgs e)
        {
            BarcodeWindow win = BarcodeWindow.Instance;
            win.Show();
            win.Focus();
        }

        private void docuwiki_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://192.168.91.100:88/");
        }

        private void Open_DefectiveStatementWindow_Click(object sender, RoutedEventArgs e)
        {
            DefectiveStatementWindow win = DefectiveStatementWindow.Instance;
            win.Show();
            win.Focus();
        }
    }
}