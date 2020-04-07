using Pronets.VievModel.Repairs_f;
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

namespace Pronets.Viev.Repairs_f
{
    /// <summary>
    /// Логика взаимодействия для StatusesWindow.xaml
    /// </summary>
    public partial class StatusesWindow : Window
    {
        private static StatusesWindow instance;
        public static StatusesWindow Instance
        {
            get
            {
                if (instance == null)
                    instance = new StatusesWindow();

                return instance;
            }
        }
        public StatusesWindow()
        {
            InitializeComponent();
            DataContext = new StatusesVM();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            instance = null;
        }
    }
}
