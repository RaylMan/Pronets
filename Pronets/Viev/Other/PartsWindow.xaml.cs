using Pronets.Data;
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
    /// Логика взаимодействия для PartsWindow.xaml
    /// </summary>
    public partial class PartsWindow : Window
    {
        private static PartsWindow partsWindowInstance;
        public static PartsWindow PartsWindowInstance
        {
            get
            {
                if (partsWindowInstance == null)
                    partsWindowInstance = new PartsWindow();
                return partsWindowInstance;
            }
        }
        public PartsWindow()
        {
            InitializeComponent();
            DataContext = new PartsWindowVM();
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

        private void Window_Closed(object sender, EventArgs e)
        {
            partsWindowInstance = null;
        }

        private void EditPartMetuItem_Click(object sender, RoutedEventArgs e)
        {
            if(grdParts.SelectedItem != null)
            {
                EditPartInfoWindow win = new EditPartInfoWindow((Parts)grdParts.SelectedItem, (PartsWindowVM)this.DataContext);
                win.ShowDialog();
            }
        }

        private void btnNewPart_Click(object sender, RoutedEventArgs e)
        {
            var newPart = new Parts
            {
                IsNew = true
            };
            EditPartInfoWindow win = new EditPartInfoWindow(newPart, (PartsWindowVM)this.DataContext);
            win.ShowDialog();
        }
    }
}
