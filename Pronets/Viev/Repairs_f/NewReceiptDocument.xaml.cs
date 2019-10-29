using Pronets.Navigation;
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

namespace Pronets.Viev.Repairs_f
{
    /// <summary>
    /// Логика взаимодействия для NewReceiptDocument.xaml
    /// </summary>
    public partial class NewReceiptDocument : Window
    {
        
        public NewReceiptDocument()
        {
            InitializeComponent();
        }
       
        private void ComboBoxNomenclature_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = GetDataGridRows(repairsGrid);
            foreach (DataGridRow r in row)
            {
                try
                {
                    FrameworkElement elmcbmx = repairsGrid.Columns[1].GetCellContent(r);
                    FrameworkElement elmchbx = repairsGrid.Columns[0].GetCellContent(r);
                    ComboBox cbx = ItemTemplateFind.FindChild<ComboBox>(elmcbmx, "cbxGridNom");
                    CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                    if (checkBox.IsChecked == true)
                    {
                        cbx.SelectedItem = comboBoxNomenclature.SelectedItem;
                    }
                }
                catch (System.ArgumentNullException)
                {

                }

            }

        }

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            if (grid.ItemsSource != null)
            {
                //for (int i = 0; i < repairsGrid.Items.Count; i++)
                //{
                //    DataGridRow row = (DataGridRow)repairsGrid.ItemContainerGenerator
                //                                               .ContainerFromIndex(i);
                //    yield return row;
                //}
                var itemsSource = grid.ItemsSource as IEnumerable;
                if (null == itemsSource) yield return null;
                foreach (var item in itemsSource)
                {
                    var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (null != row) yield return row;
                }
            }
        }

        private void ComboBoxWarranty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var row = GetDataGridRows(repairsGrid);
            foreach (DataGridRow r in row)
            {
                FrameworkElement elmcbmx = repairsGrid.Columns[3].GetCellContent(r);
                FrameworkElement elmchbx = repairsGrid.Columns[0].GetCellContent(r);
                ComboBox cbx = ItemTemplateFind.FindChild<ComboBox>(elmcbmx, "cbxGridWar");
                CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                if (checkBox.IsChecked == true)
                {
                    cbx.SelectedIndex = comboBoxWarranty.SelectedIndex;
                }
            }
        }

        private void AllChecked_Checked(object sender, RoutedEventArgs e)
        {
            var row = GetDataGridRows(repairsGrid);
            foreach (DataGridRow r in row)
            {
                FrameworkElement elmchbx = repairsGrid.Columns[0].GetCellContent(r);
                CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                checkBox.IsChecked = true;
            }
        }

        private void AllChecked_Unchecked(object sender, RoutedEventArgs e)
        {
            var row = GetDataGridRows(repairsGrid);
            foreach (DataGridRow r in row)
            {
                FrameworkElement elmchbx = repairsGrid.Columns[0].GetCellContent(r);
                CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                checkBox.IsChecked = false;
            }
        }
    }
}
