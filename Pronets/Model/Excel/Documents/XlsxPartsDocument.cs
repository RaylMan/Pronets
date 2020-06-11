using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model.Excel.Documents
{
    /// <summary>
    /// Генерирует файл *.xlsx с заказом запчастей
    /// </summary>
    public class XlsxPartsDocument : AbstractExcelGenerator
    {
        protected override string SheetName  => "Заказ запчастей";
        private ObservableCollection<PartsOrder> partsOrder;
        public XlsxPartsDocument(ObservableCollection<PartsOrder> partsOrder)
        {
            this.partsOrder = partsOrder;
        }
        protected override List<string> GetColumnsNames()
        {
            return new List<string> { "Номенклатура", "Кол-во", "Оборудование" };
        }
        protected override void CreateBody(SheetData sheetData, WorksheetPart worksheetPart)
        {
            for (int i = 0; i < partsOrder.Count; i++)
            {
                uint rowIndex = (uint)(i + lastRowIndex);
                Row row = new Row() { RowIndex = UInt32Value.FromUInt32(rowIndex) };

                Cell header1 = new Cell() { CellReference = $"A{rowIndex}", CellValue = new CellValue(partsOrder[i].PartName), DataType = CellValues.String, StyleIndex = 1 };
                row.Append(header1);
                Cell header2 = new Cell() { CellReference = $"B{rowIndex}", CellValue = new CellValue(partsOrder[i].Count.ToString()), DataType = CellValues.String, StyleIndex = 1 };
                row.Append(header2);
                Cell header3 = new Cell() { CellReference = $"C{rowIndex}", CellValue = new CellValue(partsOrder[i].Equipment), DataType = CellValues.String, StyleIndex = 1 };
                row.Append(header3);

                sheetData.Append(row);
            }
        }
        protected override void CreateHeader(SheetData sheetData, WorksheetPart worksheetPart)
        {
            base.DefaultHeader(sheetData, worksheetPart);
        }
        protected override Columns GenerateColumns(SheetData sheetData, WorksheetPart worksheetPart)
        {
            Columns columns = new Columns();

            columns.Append(new Column() { Min = 1, Max = 10, Width = 30, CustomWidth = true });
            columns.Append(new Column() { Min = 2, Max = 10, Width = 10, CustomWidth = true });
            columns.Append(new Column() { Min = 3, Max = 10, Width = 30, CustomWidth = true });

            return columns;
        }
        protected override void CreateFooter(SheetData sheetData, WorksheetPart worksheetPart)
        {

        }
    }
}
