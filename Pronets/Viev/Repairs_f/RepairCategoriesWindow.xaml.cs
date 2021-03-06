﻿using System;
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

namespace Pronets.Viev.Repairs_f
{
    /// <summary>
    /// Логика взаимодействия для RepairCategoriesWindow.xaml
    /// </summary>
    public partial class RepairCategoriesWindow : Window
    {
        private static RepairCategoriesWindow instance;
        public static RepairCategoriesWindow Instance
        {
            get
            {
                if (instance == null)
                    instance = new RepairCategoriesWindow();

                return instance;
            }
        }
        public RepairCategoriesWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
