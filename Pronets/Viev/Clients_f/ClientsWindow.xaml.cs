﻿using Pronets.Data;
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

namespace Pronets.Viev.Clients_f
{
    /// <summary>
    /// Логика взаимодействия для ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        public ClientsWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clients client = (Clients)dataGrid.SelectedItem;
                ClientInfoWindow win = new ClientInfoWindow(client);
                win.Show();
                this.Close();
            }
            catch (System.InvalidCastException)
            {
                MessageBox.Show("Как вы собираетесь открыть строку без клиента?", "Ошибка");
            }
        }
    }
}
