using Pronets.Data;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Repairs_f;
using Pronets.EntityRequests.Users_f;
using Pronets.Viev.Other;
using Pronets.Viev.Repairs_f;
using Pronets.VievModel.MainWindows.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
using System.Windows.Threading;

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
            RepairsTableEngineer win = RepairsTableEngineer.Instance;
            win.Show();
            win.Focus();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            tbxSearch.Focus();
        }

        /// <summary>
        /// Установка цвета фона комбобокс и текстбокса с информацией в соответствии статусу
        /// </summary>
        private void SetStatusColor()
        {
            LinearGradientBrush gradientLime = new LinearGradientBrush(Color.FromRgb(200, 245, 80), Color.FromRgb(0, 255, 0), new Point(0.5, 0), new Point(0.5, 1));
            LinearGradientBrush gradientRed = new LinearGradientBrush(Color.FromRgb(250, 160, 160), Color.FromRgb(255, 99, 71), new Point(0.5, 0), new Point(0.5, 1));
            LinearGradientBrush gradientYellow = new LinearGradientBrush(Color.FromRgb(240, 240, 240), Color.FromRgb(249, 241, 76), new Point(0.5, 0), new Point(0.5, 1));
            LinearGradientBrush gradientGrey = new LinearGradientBrush(Color.FromRgb(237, 237, 237), Color.FromRgb(112, 112, 112), new Point(0.5, 0), new Point(0.5, 1));
            LinearGradientBrush gradientDefault = new LinearGradientBrush(Color.FromRgb(243, 243, 243), Color.FromRgb(205, 205, 205), new Point(0.5, 0), new Point(0.5, 1));

            if (cbxStatuses.SelectedItem != null)
            {
                Statuses status = (Statuses)cbxStatuses.SelectedItem;
                if (status != null)
                {
                    switch (status.Status)
                    {
                        case "Готово":
                            cbxStatuses.Background = gradientLime;
                            tbxInfo.BorderBrush = gradientLime;
                            break;
                        case "Донор":
                            cbxStatuses.Background = gradientRed;
                            tbxInfo.BorderBrush = gradientRed;
                            break;
                        case "Не смогли починить":
                            cbxStatuses.Background = gradientRed;
                            tbxInfo.BorderBrush = gradientRed;
                            break;
                        case "Восстановлению не подлежит":
                            cbxStatuses.Background = gradientRed;
                            tbxInfo.BorderBrush = gradientRed;
                            break;
                        case "Утеряно":
                            cbxStatuses.Background = gradientRed;
                            tbxInfo.BorderBrush = gradientRed;
                            break;
                        case "В ремонте":
                            cbxStatuses.Background = gradientYellow;
                            tbxInfo.BorderBrush = gradientYellow;
                            break;
                        case "Частично готово":
                            cbxStatuses.Background = gradientYellow;
                            tbxInfo.BorderBrush = gradientYellow;
                            break;
                        case "Отправлено заказчику":
                            cbxStatuses.Background = gradientGrey;
                            tbxInfo.BorderBrush = gradientGrey;
                            break;
                        case "Отправлено заказчику(Частично)":
                            cbxStatuses.Background = gradientGrey;
                            tbxInfo.BorderBrush = gradientGrey;
                            break;
                        case "Отправлено в Элтекс":
                            cbxStatuses.Background = gradientGrey;
                            tbxInfo.BorderBrush = gradientGrey;
                            break;
                        case "Отправлено в Новые Сети":
                            cbxStatuses.Background = gradientGrey;
                            tbxInfo.BorderBrush = gradientRed;
                            break;
                        default:
                            cbxStatuses.Background = gradientDefault;
                            tbxInfo.BorderBrush = gradientDefault;
                            break;
                    }
                }
            }
        }

        private void CbxStatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetStatusColor();
        }

        private void BtnOpenDocument_Click(object sender, RoutedEventArgs e)
        {
            if (Docunents.SelectedItem != null) //IsInspectorOrAdmin()
            {
                v_Repairs repair = (v_Repairs)Docunents.SelectedItem;
                if (repair != null)
                {
                    if (repair.RepairId == -10) return;

                    v_Receipt_Document document = ReceiptDocumentRequest.GetDocument((int)repair.DocumentId);
                    ReceiptDocumentInspector window = new ReceiptDocumentInspector(document);
                    window.Show();
                }
            }
        }

        private void OpenEditRepairWindow(object sender, RoutedEventArgs e)
        {
            if (Docunents.SelectedItem != null)
            {
                v_Repairs repair = (v_Repairs)Docunents.SelectedItem;
                if (repair != null)
                {
                    if (repair.RepairId == -10) return;

                    EditRepairWindow window = new EditRepairWindow((v_Repairs)Docunents.SelectedItem);
                    window.Show();
                }
            }
        }
    }
}
