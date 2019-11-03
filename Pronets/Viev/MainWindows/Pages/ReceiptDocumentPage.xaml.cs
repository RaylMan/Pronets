using Pronets.Data;
using Pronets.Viev.Repairs_f;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pronets.Viev.MainWindows.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReceiptDocumentPage.xaml
    /// </summary>
    public partial class ReceiptDocumentPage : Page
    {
        NewReceiptDocument win = null;
        public ReceiptDocumentPage()
        {
            InitializeComponent();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            win = new NewReceiptDocument();
            win.Show();
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            v_Receipt_Document document = (v_Receipt_Document)docunents.SelectedItem;
            ReceiptDocumentInspector doc = new ReceiptDocumentInspector(document);
            doc.Show();
        }


        private void CmbStatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxAllStatus.IsChecked = false;
        }

        private void CmbClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxAllClients.IsChecked = false;
        }

        private void CbxAllClients_Checked(object sender, RoutedEventArgs e)
        {
            cmbClients.SelectedItem = null;
            cbxAllClients.IsChecked = true;
        }

        private void CbxAllStatus_Checked(object sender, RoutedEventArgs e)
        {
            cmbStatuses.SelectedItem = null;
            cbxAllStatus.IsChecked = true;
        }
    }
}
