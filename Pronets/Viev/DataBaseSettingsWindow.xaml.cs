using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        public DataBaseSettingsWindow()
        {
            InitializeComponent();
            tbxServer.Text = Properties.Settings.Default.ServerName.ToString();
            tbxBase.Text = Properties.Settings.Default.BaseName.ToString();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ServerName = tbxServer.Text;
            Properties.Settings.Default.BaseName = tbxBase.Text;
            Properties.Settings.Default.ConnectionString = $"metadata=res://*/Data.PronetsDB.csdl|res://*/Data.PronetsDB.ssdl|res://*/Data.PronetsDB.msl;provider=System.Data.SqlClient;provider connection string=&quot;Data Source={tbxServer.Text};initial catalog={tbxBase.Text};integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";
            Properties.Settings.Default.Save();
        }
    }
}
