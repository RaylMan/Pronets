using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.Linq;
using System.Data.Sql;
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

namespace Pronets.Viev
{
    /// <summary>
    /// Логика взаимодействия для DataBaseSettingsWindow.xaml
    /// </summary>
    public partial class DataBaseSettingsWindow : Window
    {
        private static DataBaseSettingsWindow windowInstance;
        public static DataBaseSettingsWindow WindowInstance
        {
            get
            {
                if (windowInstance == null)
                    windowInstance = new DataBaseSettingsWindow();

                return windowInstance;
            }
        }
        public DataBaseSettingsWindow()
        {
            InitializeComponent();
            tbxServer.Text = Properties.Settings.Default.ServerName.ToString();
            tbxBase.Text = Properties.Settings.Default.BaseName.ToString();
            tbxLogin.Text = Properties.Settings.Default.ServerLogin.ToString();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ServerName = tbxServer.Text;
            Properties.Settings.Default.BaseName = tbxBase.Text;
            Properties.Settings.Default.ServerLogin = tbxLogin.Text;
            Properties.Settings.Default.ServerPassword = tbxPassword.Password;
            Properties.Settings.Default.Save();
            MessageBox.Show("Настройки сохранены!");
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            windowInstance = null;
        }
    }
}
