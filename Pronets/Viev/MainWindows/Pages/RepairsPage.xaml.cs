using Pronets.Data;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Users_f;
using Pronets.Viev.Other;
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
    /// Логика взаимодействия для RepairsPage.xaml
    /// </summary>
    public partial class RepairsPage : Page
    {
        public RepairsPage()
        {
            InitializeComponent();
            DataContext = new RepairsPageVM();
        }
        #region Выделить текст в текстбоксе
        private bool isFocused = false;
        private void TbxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            isFocused = true;
        }

        private void TbxSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (isFocused)
            {
                isFocused = false;
                (sender as TextBox).SelectAll();
            }
        }
        #endregion


        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            tbxDefect.Focus();
        }

        private void BtnOpenDefects_Click(object sender, RoutedEventArgs e)
        {
            FaultWindow win = new FaultWindow(this);
            win.ShowDialog();
        }

        private void BtOpenRepairsTable_Click(object sender, RoutedEventArgs e)
        {
            RepairsTableEngineer win = new RepairsTableEngineer();
            win.Show();
        }
    }
}
