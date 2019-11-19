using Pronets.Viev.Other;
using Pronets.VievModel.MainWindows.Pages;
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
    /// Логика взаимодействия для DefectsPage.xaml
    /// </summary>
    public partial class DefectsPage : Page
    {
        List<int> repairsId = new List<int>();
        public DefectsPageVM vm => (DefectsPageVM)DataContext;
        public DefectsPage()
        {
            InitializeComponent();
            DataContext = new DefectsPageVM();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedTypeItem.Type == "Дефектовка")
            {
                repairsId.Clear();
                foreach (var repair in vm.V_Repairs)
                {
                    if (repair != null)
                        repairsId.Add(repair.RepairId);
                }
                if (vm.SelectedClientItem != null)
                {
                    PrintingWindow win = new PrintingWindow(repairsId, vm.SelectedClientItem.ClientId);
                    win.Show();
                }
                else
                {
                    MessageBox.Show("Необходимо выбрать клиента", "Ошибка");
                }
            }
        }

        //private void SerialsGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        //{
        //    object parametr = null;
        //    vm.AddToTable(parametr);
        //}
    }
}
