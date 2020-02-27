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

namespace Pronets.Viev.Other
{
    /// <summary>
    /// Логика взаимодействия для UserOvertimeWindow.xaml
    /// </summary>
    public partial class UserOvertimeWindow : Window
    {
        private static UserOvertimeWindow userOvertimeWindowInstance;
        public static UserOvertimeWindow GetUserOvertimeWindowInstance(Users user)
        {
            if (userOvertimeWindowInstance == null)
                userOvertimeWindowInstance = new UserOvertimeWindow(user);

            return userOvertimeWindowInstance;
        }

        private Users user;
        public UserOvertimeWindow(Users user)
        {
            InitializeComponent();
            if (user != null)
            {
                this.user = user;
                DataContext = new UserOvertimeWindowVM(user);
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            tbxHours.Text = "0";
            checkBox.IsChecked = true;
        }

        private void TbxHours_SelectionChanged(object sender, RoutedEventArgs e)
        {
            checkBox.IsChecked = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            userOvertimeWindowInstance = null;
        }
    }
}
