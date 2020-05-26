using Pronets.Data;
using Pronets.Navigation;
using Pronets.Viev.Other;
using Pronets.Viev.Other.DefectiveStatement_fw;
using Pronets.VievModel.Repairs_f;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для ReceiptDocument.xaml
    /// </summary>
    public partial class ReceiptDocumentInspector : Window
    {
        List<int> repairsIds = new List<int>();
        private v_Receipt_Document document;
        public ReceiptDocumentInspectorVM vm => (ReceiptDocumentInspectorVM)DataContext;
        public ReceiptDocumentInspector(v_Receipt_Document document)
        {
            InitializeComponent();
            if (document != null)
                this.document = document;
            DataContext = new ReceiptDocumentInspectorVM(document);
        }

        public object My_dataGrid { get; private set; }

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

        private void AllChecked_Checked(object sender, RoutedEventArgs e)
        {
            var row = GetDataGridRows(Docunents1);
            foreach (DataGridRow r in row)
            {
                FrameworkElement elmchbx = Docunents1.Columns[0].GetCellContent(r);
                CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                checkBox.IsChecked = true;
            }

        }

        private void AllChecked_Unchecked(object sender, RoutedEventArgs e)
        {
            var row = GetDataGridRows(Docunents1);
            foreach (DataGridRow r in row)
            {
                FrameworkElement elmchbx = Docunents1.Columns[0].GetCellContent(r);
                CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                checkBox.IsChecked = false;
            }
        }

        /// <summary>
        /// Открывает окно дефектовки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDefectsDoc_Click(object sender, RoutedEventArgs e)
        {
            SetRepairsId();
            if (vm.SelectedClientItem != null)
            {
                PrintingWindow win = new PrintingWindow(repairsIds, vm.SelectedClientItem.ClientId);
                win.Show();
            }
        }
        /// <summary>
        /// Открывает окно приходной накладной
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPurchaseDoc_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedClientItem != null)
            {
                PtintingPurchaseWindow win = new PtintingPurchaseWindow(document, vm.SelectedClientItem.ClientId);
                win.Show();
            }
        }
        /// <summary>
        /// Открывает окно изменения ремонта, по выбранному элементу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenEditRepairWindow(object sender, RoutedEventArgs e)
        {
            if (Docunents1.SelectedItem != null)
            {
                EditRepairWindow window = new EditRepairWindow((v_Repairs)Docunents1.SelectedItem);
                window.Show();
            }
        }

        /// <summary>
        /// Открывает окно добавления ремонтаов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRepairs_Click(object sender, RoutedEventArgs e)
        {
            if (vm.DocumentId > 0)
            {
                NewReceiptDocument win = new NewReceiptDocument((int)vm.DocumentId);
                win.Show();
            }
        }
        /// <summary>
        /// Заполняет таблицу номерами ремонта из датагрид
        /// </summary>
        private void SetRepairsId()
        {
            repairsIds.Clear();
            int count = 0;
            foreach (var repair in vm.V_Repairs)
            {
                if (repair.IsChecked == true)
                {
                    repairsIds.Add(repair.RepairId);
                    count++;
                }
            }
            if (count == 0) //если не установлены чекбоксы, вся таблица передается в окно дефектовки
            {
                foreach (var repair in vm.V_Repairs)
                {
                    repairsIds.Add(repair.RepairId);
                }
            }
        }

        private void Docunents1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Docunents1.SelectedItem != null)
                Docunents1.ScrollIntoView(Docunents1.SelectedItem);
        }

        private void DefectiveStatements_Click(object sender, RoutedEventArgs e)
        {
            DefectiveStatementWindow win = new DefectiveStatementWindow(document.Document_Id);
            win.Show();
            win.Focus();
        }
    }
}
