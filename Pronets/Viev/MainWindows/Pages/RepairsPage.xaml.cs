using Pronets.Data;
using Pronets.EntityRequests.Other;
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
    /// Логика взаимодействия для RepairsPage.xaml
    /// </summary>
    public partial class RepairsPage : Page
    {
        public RepairsPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbxDocimentId.Text != null && tbxDocimentId.Text != "")
            {
                Int32.TryParse(tbxDocimentId.Text, out int id);
                v_Receipt_Document document = ReceiptDocumentRequest.GetDocument(id);
                ReceiptDocumentInspector doc = new ReceiptDocumentInspector(document);
                doc.Show();
            }
            else
                MessageBox.Show("Не выбран элемент", "Ошибка");
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
    }
}
