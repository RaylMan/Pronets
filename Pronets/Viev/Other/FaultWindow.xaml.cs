using Pronets.Viev.MainWindows.Pages;
using Pronets.Viev.Repairs_f;
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
    /// Логика взаимодействия для FaultWindow.xaml
    /// </summary>
    public partial class FaultWindow : Window
    {
        public FaultWindow(RepairsPage page)
        {
            InitializeComponent();
            DataContext = new FaultWindowVM(page);
        }
        public FaultWindow(RepairsTableEngineer page)
        {
            InitializeComponent();
            DataContext = new FaultWindowVM(page);
        }
    }
}
