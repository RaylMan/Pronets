using Pronets.Viev.MainWindows;
using Pronets.Viev.MainWindows.Pages;
using Pronets.VievModel.Nomenclature_f;
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

namespace Pronets.Viev.Nomenclature_f
{
    /// <summary>
    /// Логика взаимодействия для NomenclatureWindow.xaml
    /// </summary>
    public partial class NomenclatureWindow : Window
    {
        private static NomenclatureWindow nomenclatureWindowInstance;
        public static NomenclatureWindow NomenclatureWindowInstance
        {
            get
            {
                if (nomenclatureWindowInstance == null)
                    nomenclatureWindowInstance = new NomenclatureWindow();

                return nomenclatureWindowInstance;
            }
        }
        public NomenclatureWindow()
        {
            InitializeComponent();
            DataContext = new NomenclatureVM();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            nomenclatureWindowInstance = null;
        }
    }
}
