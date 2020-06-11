using Pronets.Data;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Users_f;
using Pronets.VievModel.Repairs_f;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private static AllRepairsWindow instance;
        public static AllRepairsWindow Instance
        {
            get
            {
                if (instance == null)
                    instance = new AllRepairsWindow();

                return instance;
            }
        }
        public AllRepairsWindow()
        {
            InitializeComponent();
            DataContext = new AllRepairsWindowVM();
        }
        public AllRepairsWindow(ObservableCollection<v_Repairs> repairs)
        {
            InitializeComponent();
            DataContext = new AllRepairsWindowVM(repairs);
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

        private void BtnOpenDocument_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UsersRequest.IsAdmin())
                {
                    v_Repairs repair = (v_Repairs)Docunents1.SelectedItem;
                    if (repair != null)
                    {
                        v_Receipt_Document document = ReceiptDocumentRequest.GetDocument((int)repair.DocumentId);
                        ReceiptDocumentInspector window = new ReceiptDocumentInspector(document);
                        window.Show();
                    }
                }
                else
                    MessageBox.Show("Нет доступа!", "Открыть документ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Ошибка");
            }
        }

        private void OpenEditRepairWindow(object sender, RoutedEventArgs e)
        {
            if (Docunents1.SelectedItem != null)
            {
                EditRepairWindow window = new EditRepairWindow((v_Repairs)Docunents1.SelectedItem);
                window.Show();

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            instance = null;
        }
    }
}
