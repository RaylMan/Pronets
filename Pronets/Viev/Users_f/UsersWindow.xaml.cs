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
    /// Логика взаимодействия для UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        private static UsersWindow usersWindowInstance;
        public static UsersWindow UsersWindowInstance
        {
            get
            {
                if (usersWindowInstance == null)
                    usersWindowInstance = new UsersWindow();

                return usersWindowInstance;
            }
        }
        public UsersWindow()
        {
            InitializeComponent();
            Style rowStyle = new Style(typeof(DataGridRow));
            rowStyle.Setters.Add(new EventSetter(DataGridRow.MouseDoubleClickEvent,
                                     new MouseButtonEventHandler(Row_DoubleClick)));
            dataGrid.RowStyle = rowStyle;
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Users user = (Users)dataGrid.SelectedItem;
                UserInfoWindow win = new UserInfoWindow(user);
                win.Show();
            }
            catch (System.InvalidCastException)
            {
                MessageBox.Show("Как вы собираетесь открыть строку без работника?", "Ошибка");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            usersWindowInstance = null;
        }
    }
}
