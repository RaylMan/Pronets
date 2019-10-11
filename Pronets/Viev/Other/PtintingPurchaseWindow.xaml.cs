using Pronets.Data;
using Pronets.Model;
using Pronets.VievModel.Other;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Printing;
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
    /// Логика взаимодействия для PtintingPurchaseWindow.xaml
    /// </summary>
    public partial class PtintingPurchaseWindow : Window
    {
        public PtintingPurchaseWindowVM vm => (PtintingPurchaseWindowVM)DataContext;
        int clientId = 0;
        private v_Receipt_Document document;
       

        public PtintingPurchaseWindow(v_Receipt_Document document, int clientId)
        {
            InitializeComponent();
            this.document = document;
            this.clientId = clientId;
            DataContext = new PtintingPurchaseWindowVM(document, clientId);
            GetTable();
        }
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument doc = flowDocument;
            Print(doc, "Акт приема", 10);
        }
        private void PrintDocument(PrintDialog pd, FlowDocument document, double scale, string title)
        {
            DocumentPaginator dp = ((IDocumentPaginatorSource)document).DocumentPaginator;
            PrintTicket pt = new PrintTicket();
            pt.PageOrientation = PageOrientation.Landscape;
            pd.PrintTicket = pd.PrintQueue.MergeAndValidatePrintTicket(pd.PrintQueue.DefaultPrintTicket, pt).ValidatedPrintTicket;
            FittedDocumentPaginator fdp = new FittedDocumentPaginator(dp, scale);

            pd.PrintDocument(fdp, title);
        }
        private void Print(FlowDocument document, string title, double documentWidth)
        {
            var pd = new PrintDialog();

            if (pd.ShowDialog() == true)
            {
                // Set the printing margins.
                Thickness pageMargins = document.PagePadding;
                document.PagePadding = new Thickness(50.0);

                // In our case, the document's width is NaN so we have to navigate the visual tree to get the ActualWidth, which is represented by 'documentWidth'.
                double scale = documentWidth / pd.PrintableAreaWidth;

                if (scale < 1.0)
                {
                    // The report fits on the page just fine. Don't scale.
                    scale = 1.0;
                }

                double invScale = 1 / scale;

                document.PageHeight = pd.PrintableAreaHeight * scale;
                document.PageWidth = pd.PrintableAreaWidth * scale;
                document.ColumnGap = 0;
                document.ColumnWidth = pd.PrintableAreaWidth;
                DocumentPaginator dp = ((IDocumentPaginatorSource)document).DocumentPaginator;
                FittedDocumentPaginator fdp = new FittedDocumentPaginator(dp, invScale);

                pd.PrintDocument(fdp, title);

                // Restore the original values so the GUI isn't altered.
                document.PageHeight = Double.NaN;
                document.PageWidth = Double.NaN;
                document.PagePadding = pageMargins;
            }
        }


        private void GetTable()
        {
            System.Data.DataTable dataTable = vm.ToDataTable<v_Repairs>(vm.RepairsTable);
            var brushConverter = new BrushConverter();
            var rowGroup = new TableRowGroup();
            Table1.RowGroups.Add(rowGroup);
            var header = new TableRow();
            rowGroup.Rows.Add(header);
            Table1.CellSpacing = 1;

            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.ColumnName == "Nomenclature" ||
                    column.ColumnName == "Serial_Number" ||
                    column.ColumnName == "Claimed_Malfunction" ||
                    column.ColumnName == "Warranty" )
                {
                    string columnName = "";
                    switch (column.ColumnName)
                    {
                        case "Nomenclature":
                            columnName = "Наименование";
                            break;
                        case "Serial_Number":
                            columnName = "Серийный номер";
                            break;
                        case "Claimed_Malfunction":
                            columnName = "Заявленная Неисправность";
                            break;
                        case "Warranty":
                            columnName = "Гарантия";
                            break;
                    }
                    var tableColumn = new TableColumn();

                    //configure width and such
                    Table1.Columns.Add(tableColumn);
                    var cell = new TableCell(new Paragraph(new Run(columnName)))
                    {
                        Background = (Brush)brushConverter.ConvertFrom("#FFFFFF"),
                        TextAlignment = TextAlignment.Center,
                        Padding = new Thickness(2)
                    };
                    header.Cells.Add(cell);
                }
            }

            foreach (DataRow row in dataTable.Rows)
            {
                var tableRow = new TableRow();
                rowGroup.Rows.Add(tableRow);
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName == "Nomenclature" ||
                   column.ColumnName == "Serial_Number" ||
                   column.ColumnName == "Claimed_Malfunction" ||
                   column.ColumnName == "Warranty" )
                    {
                        var value = row[column].ToString();//mayby some formatting is in order
                        var cell = new TableCell(new Paragraph(new Run(value)))
                        {
                            Background = (Brush)brushConverter.ConvertFrom("#FFFFFF"),
                            TextAlignment = TextAlignment.Center,
                            Padding = new Thickness(2)
                        };
                        tableRow.Cells.Add(cell);
                    }
                }
            }
        }
    }
}
