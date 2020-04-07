﻿
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
    /// Логика взаимодействия для SelfUserReportWindow.xaml
    /// </summary>
    /// 
    public partial class SelfUserReportWindow : Window
    {
        public SelfUserReportWindow(Users user)
        {
            InitializeComponent();
            DataContext = new SelfUserReportWindowVM(user);
        }
    }
}
