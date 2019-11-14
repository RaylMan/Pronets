using Pronets.Navigation;
using Pronets.Viev.Other;
using Pronets.VievModel.Repairs_f;
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
    /// Логика взаимодействия для RepairsTableEngineer.xaml
    /// </summary>
    public partial class RepairsTableEngineer : Window
    {
        public RepairsTableEngineer()
        {
            InitializeComponent();
            DataContext = new RepairsTableEngineerVM();
        }

        private void BtnDefect_Click(object sender, RoutedEventArgs e)
        {
            FaultWindow win = new FaultWindow(this);
            win.Show();
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
        /// <summary>
        /// Прокрутка датагрид, чтобы установились все чекбоксы
        /// </summary>
        /// <param name="index">Индекс выделенной строки</param>
        private void ScrollCarret(int index)
        {
            if (Docunents1.SelectedItem != null)
                Docunents1.ScrollIntoView(Docunents1.SelectedItem);
        }
    }
}
