using Pronets.Data;
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

namespace Pronets.Viev.Clients_f
{
    /// <summary>
    /// Логика взаимодействия для ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        private static ClientsWindow clientsWindowInstance;
        public static ClientsWindow ClientsWindowInstance
        {
            get
            {
                if (clientsWindowInstance == null)
                    clientsWindowInstance = new ClientsWindow();

                return clientsWindowInstance;
            }
        }
        public ClientsWindow()
        {
            InitializeComponent();
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Clients client = (Clients)dataGrid.SelectedItem;
            ClientInfoWindow win = new ClientInfoWindow(client);
            win.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            clientsWindowInstance = null;
        }
    }
}
