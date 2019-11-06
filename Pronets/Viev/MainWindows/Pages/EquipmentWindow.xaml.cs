using Pronets.Data;
using Pronets.Viev.Repairs_f;
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
    /// Логика взаимодействия для EquipmentWindow.xaml
    /// </summary>
    public partial class EquipmentWindow : Page
    {
        public EquipmentWindow()
        {
            InitializeComponent();
            DataContext = new EquipmentWindowVM();
        }
        #region Выделить текст в текстбоксе
        private bool isFocused = false;
        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (isFocused)
            {
                isFocused = false;
                (sender as TextBox).SelectAll();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            isFocused = true;
        }
        #endregion

        private void OpenEditRepairWindow(object sender, RoutedEventArgs e)
        {
            if (repairs.SelectedItem != null)
            {
                EditRepairWindow window = new EditRepairWindow((v_Repairs)repairs.SelectedItem);
                window.Show();
            }
        }
    }
}
