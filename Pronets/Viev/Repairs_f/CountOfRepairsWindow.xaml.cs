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
    /// Логика взаимодействия для CountOfRepairsWindow.xaml
    /// </summary>
    public partial class CountOfRepairsWindow : Window
    {
        private static CountOfRepairsWindow instance;
        public static CountOfRepairsWindow Instance
        {
            get
            {
                if (instance == null)
                    instance = new CountOfRepairsWindow();

                return instance;
            }
        }
        public CountOfRepairsWindow()
        {
            InitializeComponent();
            DataContext = new CountOfRepairsWindowVM();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            instance = null;
        }
    }
}
