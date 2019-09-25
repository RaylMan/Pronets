using Pronets.Data;
using Pronets.Navigation;
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
        private v_Receipt_Document document;
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
                var itemsSource = grid.ItemsSource as IEnumerable;
                if (null == itemsSource) yield return null;
                foreach (var item in itemsSource)
                {
                    var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (null != row) yield return row;
                }
            }
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
    }
}
