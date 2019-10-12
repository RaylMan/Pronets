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
    /// Логика взаимодействия для PartsWindow.xaml
    /// </summary>
    public partial class PartsWindow : Window
    {
        public PartsWindow()
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
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdParts.SelectedItem != null)
                grdParts.ScrollIntoView(grdParts.SelectedItem);
        }
    }
}
