using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pronets.Model.FromXlsxToSQL
{
    public class XslxExporter
    {
        public DataTable ReadAsDataTable(string fileName, string sheetID)
        {
            DataTable dataTable = new DataTable();

            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(fileName, false))
            {
                try
                {
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string relationshipId = sheets.First().Id.Value;
                    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(sheetID);
                    worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(sheetID);
                    Worksheet workSheet = worksheetPart.Worksheet;
                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Descendants<Row>();
                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        try
                        {
                            dataTable.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                        }
                        catch (System.Data.DuplicateNameException)
                        {

                            MessageBox.Show("Удалите все данные после последней колонки(Дата или Статус ремонта)\n" +
                                "Удалите все объедененные ячейки", "Ошибка");
                            return dataTable;
                        }

                    }

                    foreach (Row row in rows)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                        {
                            Cell cell = row.Descendants<Cell>().ElementAt(i);
                            int actualCellIndex = CellReferenceToIndex(cell);
                            try
                            {
                                dataRow[actualCellIndex] = GetCellValue(spreadSheetDocument, cell);
                            }
                            catch (System.IndexOutOfRangeException)
                            {
                                MessageBox.Show("Удалите все данные после последней колонки(Дата или Статус ремонта)\n" +
                                "Удалите все объедененные ячейки", "Ошибка");
                                return dataTable;
                            }

                        }

                        dataTable.Rows.Add(dataRow);
                    }
                }
                catch (System.InvalidCastException)
                {
                    MessageBox.Show("Удалите все данные после последней колонки(Дата или Статус ремонта)\n" +
                            "Удалите все объедененные ячейки", "Ошибка");
                }
            }
            dataTable.Rows.RemoveAt(0);

            return dataTable;
        }

        private string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value;
            if (cell.CellValue != null)
                value = cell.CellValue.InnerXml;
            else
                value = "0";
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }
        private int CellReferenceToIndex(Cell cell) //Проверка на пустые ячейки в листе
        {
            int index = 0;
            string reference = cell.CellReference.ToString().ToUpper();
            foreach (char ch in reference)
            {
                if (Char.IsLetter(ch))
                {
                    int value = (int)ch - (int)'A';
                    index = (index == 0) ? value : ((index + 1) * 26) + value;
                }
                else
                    return index;
            }
            return index;
        }
        public bool IsDigitsOnly(string str) //Возвражает true, если в строке только цифры
        {
            if (str != null)
            {
                foreach (char c in str)
                {
                    if (c < '0' || c > '9')
                        return false;
                }
            }
            return true;
        }

        public DateTime ConvToDate(string date) //Конвертирует строку в дату ConvToDate(IsDigitsOnly(date)) 
        {
            if (date != null && IsDigitsOnly(date) && date != "" && date != "")
            {
                double doubleDate = Convert.ToDouble(date);
                DateTime convDate;
                try
                {
                    convDate = DateTime.FromOADate(doubleDate);
                }
                catch (System.ArgumentException)
                {
                    return convDate = DateTime.MinValue;
                }

                return convDate;
            }
            return DateTime.MinValue;
        }
    }
}
