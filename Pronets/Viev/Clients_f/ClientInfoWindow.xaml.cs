using Pronets.Data;
using Pronets.Viev.Repairs_f;
using Pronets.VievModel.Clients_f;
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
    /// Логика взаимодействия для ClientInfoWindow.xaml
    /// </summary>
    
    public partial class ClientInfoWindow : Window
    {
        private Clients client;

        public ClientInfoWindow(Clients client)
        {
            InitializeComponent();
            if (client != null)
                this.client = client;
            DataContext = new ClientInfoWIndowVM(client);
        }
    }
}
