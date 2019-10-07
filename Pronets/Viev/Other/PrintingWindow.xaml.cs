using Pronets.Data;
using Pronets.VievModel.Other;
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

namespace Pronets.Viev.Other
{
    /// <summary>
    /// Логика взаимодействия для PrintingWindow.xaml
    /// </summary>
    public partial class PrintingWindow : Window
    {
        private v_Receipt_Document document;

        public PrintingWindow(v_Receipt_Document document)
        {
            InitializeComponent();
            this.document = document;
            DataContext = new PrintingWindowVM(document);
        }
    }
}
