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
    /// Логика взаимодействия для CopyBufferWindow.xaml
    /// </summary>
    public partial class CopyBufferWindow : Window
    {
        private static CopyBufferWindow instance;
        public static CopyBufferWindow CopyBufferWindowInstance
        {
            get
            {
                if (instance == null)
                    instance = new CopyBufferWindow();
                return instance;
            }
        }
        public CopyBufferWindow()
        {
            InitializeComponent();
            DataContext = new CopyBufferWindowVM();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            instance = null;
        }
    }
}
