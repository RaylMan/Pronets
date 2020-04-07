using Pronets.Data;
using Pronets.VievModel.Other;
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

namespace Pronets.Viev.Other
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private Users user;
        private static SettingsWindow settingsWindowInstance;
        public static SettingsWindow GetSettingsWindowInstance(Users user)
        {
            if (settingsWindowInstance == null)
                settingsWindowInstance = new SettingsWindow(user);

            return settingsWindowInstance;
        }
        public SettingsWindow(Users user)
        {
            InitializeComponent();
            this.user = user;
            DataContext = new SettingsWindowVM(user);
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            settingsWindowInstance = null;
        }
    }
}
