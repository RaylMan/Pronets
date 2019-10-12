using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pronets.Viev.Repairs_f
{
    /// <summary>
    /// Логика взаимодействия для AllRepairsWindow.xaml
    /// </summary>
    public partial class AllRepairsWindow : Window
    {
        public AllRepairsWindow()
        {
            InitializeComponent();
        }
        private bool isFocused;
        private void TbxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            isFocused = true;
        }

        private void TbxSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (isFocused)
            {
                isFocused = false;
                (sender as TextBox).SelectAll();
            }
        }

        private void Docunents1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Docunents1.SelectedItem != null)
                Docunents1.ScrollIntoView(Docunents1.SelectedItem);
        }
    }
}
