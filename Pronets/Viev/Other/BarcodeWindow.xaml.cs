using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BarcodeLib;
using System.IO;
using Pronets.Model;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using Pronets.VievModel.Other;

namespace Pronets.Viev.Other
{
    /// <summary>
    /// Логика взаимодействия для BarcodeWindow.xaml
    /// </summary>
    public partial class BarcodeWindow : Window
    {
        public BarcodeWindow()
        {
            InitializeComponent();
            DataContext = new BarcodeWindowVM();
        }
        private void BufferItem_Click(object sender, RoutedEventArgs e)
        {
            CopyBufferWindow win = CopyBufferWindow.CopyBufferWindowInstance;
            win.Show();
            win.Focus();
        }
    }
}
