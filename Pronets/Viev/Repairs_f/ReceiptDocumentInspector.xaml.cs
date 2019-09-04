using Pronets.Data;
using Pronets.VievModel.Repairs_f;
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

namespace Pronets.Viev.Repairs_f
{
    /// <summary>
    /// Логика взаимодействия для ReceiptDocument.xaml
    /// </summary>
    public partial class ReceiptDocumentInspector : Window
    {
        private v_Receipt_Document document;
        public ReceiptDocumentInspector(v_Receipt_Document document)
        {
            InitializeComponent();
            if (document != null)
                this.document = document;
            DataContext = new ReceiptDocumentInspectorVM(document);
        }
    }
}
