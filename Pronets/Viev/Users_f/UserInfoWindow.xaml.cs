using Pronets.Data;
using Pronets.VievModel.Users_f;
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

namespace Pronets.Viev.Users_f
{
    /// <summary>
    /// Логика взаимодействия для UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        Users user;
        public UserInfoWindow(Users user)
        {
            
            InitializeComponent();
            if (user != null)
                this.user = user;
            DataContext = new UserInfoWindowVM(user);
        }


        private void ChbxAllCategory_Checked(object sender, RoutedEventArgs e)
        {
            cmbCategory.SelectedItem = null;
            chbxAllCategory.IsChecked = true;
        }

        private void CmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chbxAllCategory.IsChecked = false;
        }
    }
}
