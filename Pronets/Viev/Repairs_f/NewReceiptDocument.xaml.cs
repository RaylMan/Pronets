﻿using Pronets.Data;
using Pronets.Model;
using Pronets.Navigation;
using Pronets.VievModel.Repairs_f;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace Pronets.Viev.Repairs_f
{
    /// <summary>
    /// Логика взаимодействия для NewReceiptDocument.xaml
    /// </summary>
    public partial class NewReceiptDocument : Window
    {
        public NewReceiptDocumentVM vm => (NewReceiptDocumentVM)DataContext;
        public NewReceiptDocument()
        {
            InitializeComponent();
            DataContext = new NewReceiptDocumentVM();
        }
        public NewReceiptDocument(string clientName)
        {
            InitializeComponent();
            DataContext = new NewReceiptDocumentVM(clientName);
        }
        public NewReceiptDocument(int documentId)
        {
            InitializeComponent();
            DataContext = new NewReceiptDocumentVM(documentId);
        }
        private void ComboBoxNomenclature_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var rows = GetDataGridRows(repairsGrid);
                if (rows != null)
                {
                    foreach (DataGridRow r in rows)
                    {
                        try
                        {
                            FrameworkElement elmcbmx = repairsGrid.Columns[1].GetCellContent(r);
                            FrameworkElement elmchbx = repairsGrid.Columns[0].GetCellContent(r);
                            ComboBox cbx = ItemTemplateFind.FindChild<ComboBox>(elmcbmx, "cbxGridNom");
                            CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                            if (checkBox.IsChecked == true)
                            {
                                cbx.SelectedItem = comboBoxNomenclature.SelectedItem;
                            }
                        }
                        catch (System.ArgumentNullException ex)
                        {
                            string error = $" {ex.Message} \n\t {ex.InnerException}\n\t {ex.HResult} \n\t {ex.TargetSite} \n\t {ex.StackTrace} \n\t {ex.Data} \n\t {ex.Source}";
                            Logging.NewLog(error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = $" {ex.Message} \n\t {ex.InnerException}\n\t {ex.HResult} \n\t {ex.TargetSite} \n\t {ex.StackTrace} \n\t {ex.Data} \n\t {ex.Source}";
                Logging.NewLog(error);
            }
        }


        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            if (grid.ItemsSource != null)
            {
                grid.SelectedIndex = 0;
                ScrollCarret(0);
                var itemsSource = grid.ItemsSource as IEnumerable;
                if (null == itemsSource) yield return null;
                foreach (var item in itemsSource)
                {
                    ScrollCarret(grid.SelectedIndex++);
                    var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (null != row) yield return row;
                }
            }
        }
        /// <summary>
        /// Прокрутка датагрид, чтобы установились все чекбоксы
        /// </summary>
        /// <param name="index">Индекс выделенной строки</param>
        private void ScrollCarret(int index)
        {
            if (repairsGrid.SelectedItem != null)
                repairsGrid.ScrollIntoView(repairsGrid.SelectedItem);
        }

        private void ComboBoxWarranty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var rows = GetDataGridRows(repairsGrid);
                if (rows != null)
                {
                    foreach (DataGridRow r in rows)
                    {
                        FrameworkElement elmcbmx = repairsGrid.Columns[3].GetCellContent(r);
                        FrameworkElement elmchbx = repairsGrid.Columns[0].GetCellContent(r);
                        ComboBox cbx = ItemTemplateFind.FindChild<ComboBox>(elmcbmx, "cbxGridWar");
                        CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                        if (checkBox.IsChecked == true)
                        {
                            cbx.SelectedIndex = comboBoxWarranty.SelectedIndex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = $" {ex.Message} \n\t {ex.InnerException}\n\t {ex.HResult} \n\t {ex.TargetSite} \n\t {ex.StackTrace} \n\t {ex.Data} \n\t {ex.Source}";
                Logging.NewLog(error);
            }
        }

        private void AllChecked_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var rows = GetDataGridRows(repairsGrid);
                if (rows != null)
                {
                    foreach (DataGridRow r in rows)
                    {
                        FrameworkElement elmchbx = repairsGrid.Columns[0].GetCellContent(r);
                        CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                        checkBox.IsChecked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string error = $" {ex.Message} \n\t {ex.InnerException}\n\t {ex.HResult} \n\t {ex.TargetSite} \n\t {ex.StackTrace} \n\t {ex.Data} \n\t {ex.Source}";
                Logging.NewLog(error);
            }
        }
        private void AllChecked_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                var rows = GetDataGridRows(repairsGrid);
                if (rows != null)
                {
                    foreach (DataGridRow r in rows)
                    {
                        FrameworkElement elmchbx = repairsGrid.Columns[0].GetCellContent(r);
                        CheckBox checkBox = ItemTemplateFind.FindChild<CheckBox>(elmchbx, "chkbx");
                        checkBox.IsChecked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string error = $" {ex.Message} \n\t {ex.InnerException}\n\t {ex.HResult} \n\t {ex.TargetSite} \n\t {ex.StackTrace} \n\t {ex.Data} \n\t {ex.Source}";
                Logging.NewLog(error);
            }
        }
    }
}
