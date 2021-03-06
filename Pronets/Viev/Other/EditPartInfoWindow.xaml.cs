﻿using Pronets.Data;
using Pronets.EntityRequests.Other;
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
    /// Логика взаимодействия для EditPartInfoWindow.xaml
    /// </summary>
    public partial class EditPartInfoWindow : Window
    {
        public EditPartInfoWindow(Parts part, PartsWindowVM partsWindowVM)
        {
            InitializeComponent();
            if (!part.IsNew)
            {
                tbxName.IsReadOnly = true;
            }
            DataContext = new EditPartInfoWindowVM(part, this, partsWindowVM);
        }
    }
}
