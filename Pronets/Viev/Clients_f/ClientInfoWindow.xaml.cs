using Pronets.Data;
using Pronets.Navigation;
using Pronets.Viev.Repairs_f;
using Pronets.VievModel.Clients_f;
using System;
using System.Collections;
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

namespace Pronets.Viev.Clients_f
{
    /// <summary>
    /// Логика взаимодействия для ClientInfoWindow.xaml
    /// </summary>
    
    public partial class ClientInfoWindow : Window
    {
        private Clients client;

        public ClientInfoWindow(Clients client)
        {
            InitializeComponent();
            if (client != null)
                this.client = client;
            DataContext = new ClientInfoWIndowVM(client);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxAllDocument.IsChecked = false;
        }

        private void CmbDucuments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxAllNomenclature.IsChecked = false;
        }
        private void CmbWarranty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxAllWarrantys.IsChecked = false;
        }
       
        private void CbxAllDocument_Checked(object sender, RoutedEventArgs e)
        {
            cmbDucuments.SelectedItem = null;
            cbxAllDocument.IsChecked = true;
        }

        private void CbxAllNomenclature_Checked(object sender, RoutedEventArgs e)
        {
            cmbNomenclature.SelectedItem = null;
            cbxAllNomenclature.IsChecked = true;
        }

        private void CbxAllWarrantys_Checked(object sender, RoutedEventArgs e)
        {
            cmbWarranty.SelectedItem = null;
            cbxAllWarrantys.IsChecked = true;
        }
        #region Выделить текст в текстбоксе
        private bool isFocused = false;
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


        #endregion

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F5)
            {
                var vm = (ClientInfoWIndowVM)DataContext;
                vm.Refresh(sender);
            }
        }
    }
}
