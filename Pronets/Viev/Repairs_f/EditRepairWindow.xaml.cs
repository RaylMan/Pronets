using Pronets.Data;
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
    /// Логика взаимодействия для EditRepairWindow.xaml
    /// </summary>
    public partial class EditRepairWindow : Window
    {
        private static EditRepairWindow instance;
        public static EditRepairWindow GetInstance(v_Repairs repair)
        {
            if (instance == null)
                instance = new EditRepairWindow(repair);

            return instance;
        }
        public EditRepairWindow(v_Repairs repair)
        {
            InitializeComponent();
            if (repair != null)
            {
                DataContext = new EditRepairWindowVM(repair);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            instance = null;
        }
    }
}
