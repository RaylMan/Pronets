﻿using Pronets.Data;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Users_f;
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
            HideButton();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbxDocimentId.Text != null && tbxDocimentId.Text != "")
            {
                Int32.TryParse(tbxDocimentId.Text, out int id);
                v_Receipt_Document document = ReceiptDocumentRequest.GetDocument(id);
                ReceiptDocumentInspector doc = new ReceiptDocumentInspector(document);
                doc.Show();
            }
            else
                MessageBox.Show("Не выбран элемент", "Ошибка");
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            tbxDefect.Focus();
        }
        /// <summary>
        /// Делает кнопку открыть приходный документ неактивной
        /// для работников с уровнем доступа Инженер
        /// </summary>
        private void HideButton()
        {
            int.TryParse(Properties.Settings.Default.DefaultUserId.ToString(), out int userId);
            var user = UsersRequest.GetUser(userId);
            if (user != null && user.Position == "Инженер")
            {
                btnOpen.IsEnabled = false;
            }
        }
    }
}
