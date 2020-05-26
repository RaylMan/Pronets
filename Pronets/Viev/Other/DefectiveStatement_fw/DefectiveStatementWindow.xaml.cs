using Pronets.Data;
using Pronets.EntityRequests.DefectiveStatements_f;
using Pronets.VievModel.Other.DefectiveStatement_f;
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

namespace Pronets.Viev.Other.DefectiveStatement_fw
{
    /// <summary>
    /// Логика взаимодействия для DefectiveStatementWindow.xaml
    /// </summary>
    public partial class DefectiveStatementWindow : Window
    {
        private static DefectiveStatementWindow instance;
        public static DefectiveStatementWindow Instance
        {
            get
            {
                if (instance == null)
                    instance = new DefectiveStatementWindow();

                return instance;
            }
        }
        public DefectiveStatementWindow()
        {
            InitializeComponent();
            DataContext = new DefectiveStatementWindowVM();
        }
        public DefectiveStatementWindow(int documentId)
        {
            InitializeComponent();
            DataContext = new DefectiveStatementWindowVM(documentId);
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            instance = null;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row = (v_DefectiveStatements)datagrid.SelectedItem;
                var idsList = DefectiveStatementsRequests.GetRepairsId(row.Id);
                var win = new PrintingWindow(idsList, row.ClientId);
                win.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Ошибка");
            }
        }
    }
}
