using Pronets.Data;
using Pronets.EntityRequests.Other;
using Pronets.Navigation;
using Pronets.VievModel.Repairs_f;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Pronets.Viev.Repairs_f
{
    /// <summary>
    /// Логика взаимодействия для NewReceiptDocumentCopies.xaml
    /// </summary>
    public partial class NewReceiptDocumentCopies : Window
    {
        public NewReceiptDocumentCopies(ObservableCollection<v_Repairs> copyRepairs, ObservableCollection<Repairs> repairs)
        {
            InitializeComponent();
            DataContext = new NewReceiptDocumentCopiesVM(copyRepairs, repairs);
        }

        private void Docunents1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Docunents1.SelectedItem != null)
                Docunents1.ScrollIntoView(Docunents1.SelectedItem);
        }

        private void OpenDocument(object sender, RoutedEventArgs e)
        {
            v_Repairs repair = (v_Repairs)Docunents1.SelectedItem;
            if (repair != null)
            {
                v_Receipt_Document document = ReceiptDocumentRequest.GetDocument((int)repair.DocumentId);
                ReceiptDocumentInspector window = new ReceiptDocumentInspector(document);
                window.Show();
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AllChecked_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = GetDataGridRows(Docunents1);
                foreach (DataGridRow r in row)
                {
                    FrameworkElement elmchbx = Docunents1.Columns[0].GetCellContent(r);
                    CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                    checkBox.IsChecked = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void AllChecked_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = GetDataGridRows(Docunents1);
                foreach (DataGridRow r in row)
                {
                    FrameworkElement elmchbx = Docunents1.Columns[0].GetCellContent(r);
                    CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                    checkBox.IsChecked = false;
                }
            }
            catch (Exception)
            {
            }
        }
        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            if (grid.ItemsSource != null)
            {
                grid.SelectedIndex = 0;
                ScrollCarret(0);
                var itemsSource = grid.ItemsSource as IEnumerable;
                if (null == itemsSource) yield return null;
                foreach (var item in itemsSource)
                {
                    ScrollCarret(grid.SelectedIndex++);
                    var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (null != row) yield return row;
                }
            }
        }
        private void ScrollCarret(int index)
        {
            if (Docunents1.SelectedItem != null)
                Docunents1.ScrollIntoView(Docunents1.SelectedItem);
        }
    }
}
