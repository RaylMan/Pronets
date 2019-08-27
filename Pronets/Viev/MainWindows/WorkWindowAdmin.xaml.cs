using Pronets.Data;
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
            progOpenLabel.Content = Properties.Settings.Default.ProgOpen.ToString();
            Loaded += MainWindow_Loaded;
        }
        public WorkWindowAdmin()
        {
            InitializeComponent();
            progOpenLabel.Content = Properties.Settings.Default.ProgOpen.ToString();
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
        private void ChangeUser_Click(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //System.Windows.Application.Current.Shutdown();
        }
    }
}