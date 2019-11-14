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
using Pronets.Navigation;
using System.Collections;

namespace Pronets.Viev.Other
{
    /// <summary>
    /// Логика взаимодействия для OvertimeWindow.xaml
    /// </summary>
    public partial class OvertimeWindow : Window
    {
        public OvertimeWindow()
        {
            InitializeComponent();
            DataContext = new OvertimeWindowVM();
        }
        private void AllChecked_Checked(object sender, RoutedEventArgs e)
        {
            var row = GetDataGridRows(overtime);
            foreach (DataGridRow r in row)
            {
                FrameworkElement elmchbx = overtime.Columns[0].GetCellContent(r);
                CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                checkBox.IsChecked = true;
            }

        }
        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            grid.SelectedIndex = 0;
            ScrollCarret(0);
            if (grid.ItemsSource != null)
            {
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
            if (overtime.SelectedItem != null)
                overtime.ScrollIntoView(overtime.SelectedItem);
        }
        
        private void AllChecked_Unchecked(object sender, RoutedEventArgs e)
        {
            var row = GetDataGridRows(overtime);
            foreach (DataGridRow r in row)
            {
                FrameworkElement elmchbx = overtime.Columns[0].GetCellContent(r);
                CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                checkBox.IsChecked = false;
            }
        }
    }
}
