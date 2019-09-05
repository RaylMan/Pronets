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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pronets.Viev.MainWindows.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddRecipeDocument.xaml
    /// </summary>
    public partial class AddRecipeDocument : Page
    {
        FrameworkElement elmcbmx;
        FrameworkElement elmchbx;
        public AddRecipeDocument()
        {
            InitializeComponent();
        }

        private void EditNomenclature_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < repairsGrid.Items.Count; i++)
            {
                //repairsGrid.Items[i] as  SelectedItem = comboBoxNomenclature.SelectedItem;
                //repairsGrid.SelectedValue = comboBoxNomenclature.SelectedValue;
                //repairsGrid.SelectedValuePath= comboBoxNomenclature.SelectedValuePath;
            }
        }

        private void ComboBoxNomenclature_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = GetDataGridRows(repairsGrid);
            foreach (DataGridRow r in row)
            {
                try
                {
                    elmcbmx = repairsGrid.Columns[1].GetCellContent(r);
                    elmchbx = repairsGrid.Columns[0].GetCellContent(r);
                    ComboBox cbx = ItemTemplateFind.FindChild<ComboBox>(elmcbmx, "cbxGridNom");
                    CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                    if (checkBox.IsChecked == true)
                    {
                        cbx.SelectedIndex = comboBoxNomenclature.SelectedIndex;
                    }
                }
                catch (System.ArgumentNullException)
                {

                }
                
            }
        }

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            if(grid.ItemsSource != null)
            {
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
                elmcbmx = repairsGrid.Columns[3].GetCellContent(r);
                elmchbx = repairsGrid.Columns[0].GetCellContent(r);
                ComboBox cbx = ItemTemplateFind.FindChild<ComboBox>(elmcbmx, "cbxGridWar");
                CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                if (checkBox.IsChecked == true)
                {
                    cbx.SelectedIndex = comboBoxNomenclature.SelectedIndex;
                }
            }
        }

        private void AllChecked_Checked(object sender, RoutedEventArgs e)
        {
            var row = GetDataGridRows(repairsGrid);
            foreach (DataGridRow r in row)
            {
                elmchbx = repairsGrid.Columns[0].GetCellContent(r);
                CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                checkBox.IsChecked = true;
            }

        }

        private void AllChecked_Unchecked(object sender, RoutedEventArgs e)
        {
            var row = GetDataGridRows(repairsGrid);
            foreach (DataGridRow r in row)
            {
                elmchbx = repairsGrid.Columns[0].GetCellContent(r);
                CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                checkBox.IsChecked = false;
            }
        }
    }
}
