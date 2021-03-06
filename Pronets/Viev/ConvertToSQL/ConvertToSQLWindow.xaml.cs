﻿using Microsoft.Win32;
using Pronets.Data;
using Pronets.Navigation;
using Pronets.VievModel.ConvertToSQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pronets.Viev.ConvertToSQL
{
    /// <summary>
    /// Логика взаимодействия для ConvertToSQLWindow.xaml
    /// </summary>
    public partial class ConvertToSQLWindow : Window
    {
        private static ConvertToSQLWindow instance;
        public static ConvertToSQLWindow Instatnce
        {
            get
            {
                if (instance == null)
                    instance = new ConvertToSQLWindow();

                return instance;
            }
        }
        Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        public ConvertToSQLWindow()
        {
            InitializeComponent();
            DataContext = new ConvertToSQLWindowVM();
        }

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            grid.SelectedIndex = 0;
            ScrollCarret(0);
            if (grid.ItemsSource != null)
            {
                var itemsSource = grid.ItemsSource as IEnumerable;
                if (null == itemsSource) yield return null;
                foreach (var item in itemsSource)
                {
                    ScrollCarret(grid.SelectedIndex++);
                    DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (null != row) yield return row;
                }
            }
        }
        private void ScrollCarret(int index)
        {
            if (baseFromExcel.SelectedItem != null)
                baseFromExcel.ScrollIntoView(baseFromExcel.SelectedItem);
        }

        private void AllChecked_Checked(object sender, RoutedEventArgs e)
        {
            txbOperation.Visibility = Visibility.Visible;
            dispatcher.Invoke(new Action(() =>
            {
                var row = GetDataGridRows(baseFromExcel);
                foreach (DataGridRow r in row)
                {
                    FrameworkElement elmchbx = baseFromExcel.Columns[0].GetCellContent(r);
                    CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                    checkBox.IsChecked = true;
                }
            }));
            txbOperation.Visibility = Visibility.Hidden;
        }

        private void AllChecked_Unchecked(object sender, RoutedEventArgs e)
        {
            txbOperation.Visibility = Visibility.Visible;
            dispatcher.Invoke(new Action(() =>
            {
                var row = GetDataGridRows(baseFromExcel);
                foreach (DataGridRow r in row)
                {
                    FrameworkElement elmchbx = baseFromExcel.Columns[0].GetCellContent(r);
                    CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                    checkBox.IsChecked = false;
                }
            }));
            txbOperation.Visibility = Visibility.Hidden;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            tbxPath.Text = fileDialog.FileName;
        }

        private void CmbDocuments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxIsNewDocument.IsChecked = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cmbDocuments.SelectedItem = null;
            cbxIsNewDocument.IsChecked = true;
        }
        private void CmbNoemclature_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var row = GetDataGridRows(baseFromExcel);
            //foreach (DataGridRow r in row)
            //{
            //    try
            //    {
            //        FrameworkElement elmchbx = baseFromExcel.Columns[0].GetCellContent(r);
            //        FrameworkElement elmtbx = baseFromExcel.Columns[1].GetCellContent(r);
            //        CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
            //        TextBlock tbx = ItemTemplateFind.FindChild<TextBlock>(elmtbx, "textBlock");
                   
                    
            //        if (checkBox.IsChecked == true)
            //        {
            //            Nomenclature nom = (Nomenclature)cmbNoemclature.SelectedItem;
            //            if(nom != null)
            //            tbx.Text = nom.Name;
            //        }
            //    }
            //    catch (System.ArgumentNullException)
            //    {

            //    }
            //}
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            instance = null;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                var vm = (ConvertToSQLWindowVM)DataContext;
                vm.Refresh(sender);
            }
        }
    }
}
