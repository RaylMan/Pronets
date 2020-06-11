using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model.Excel
{
    public abstract class AbstractExcelGenerator
    {
        protected uint lastRowIndex = 1;
        protected abstract string SheetName { get; }
        /// <summary>
        /// Генерация и создание файла *.xlsx
        /// </summary>
        /// <param name="filePath"></param>
        public void GenerateFile(string filePath)
        {
            try
            {
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
                {
                    // Add a WorkbookPart to the document.
                    WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                    workbookpart.Workbook = new Workbook();

                    // Add a WorksheetPart to the WorkbookPart.
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);
                    WorkbookStylesPart wbsp = workbookpart.AddNewPart<WorkbookStylesPart>();

                    //// Добавляем в документ набор стилей
                    wbsp.Stylesheet = ExcelStyles.GenerateStylesSheet();
                    wbsp.Stylesheet.Save();
                    // Add Sheets to the Workbook.
                    Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                        AppendChild<Sheets>(new Sheets());

                    // Append a new worksheet and associate it with the workbook.
                    Sheet sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                        SheetId = 1,
                        Name = SheetName
                    };

                    Columns lstColumns = GenerateColumns(sheetData, worksheetPart);
                    if (lstColumns == null) throw new ArgumentNullException("В листе отсутствуют колонки, реализуйте метод GenerateColumns()");

                    worksheetPart.Worksheet.InsertAt(lstColumns, 0);


                    CreateHeader(sheetData, worksheetPart);

                    CreateBody(sheetData, worksheetPart);

                    CreateFooter(sheetData, worksheetPart);

                    sheets.Append(sheet);
                    workbookpart.Workbook.Save();
                    spreadsheetDocument.Close();
                }
            }
            catch
            {
                throw;
            }
        }
        protected abstract List<string> GetColumnsNames();
        /// <summary>
        /// Создание колонок в файле
        /// </summary>
        /// <param name="sheetData"></param>
        /// <param name="worksheetPart"></param>
        /// <returns></returns>
        protected abstract Columns GenerateColumns(SheetData sheetData, WorksheetPart worksheetPart);
        /// <summary>
        /// Создание шапки документа
        /// </summary>
        /// <param name="sheetData"></param>
        /// <param name="worksheetPart"></param>
        protected abstract void CreateHeader(SheetData sheetData, WorksheetPart worksheetPart);
        /// <summary>
        /// Создание тела документа
        /// </summary>
        /// <param name="sheetData"></param>
        /// <param name="worksheetPart"></param>
        protected abstract void CreateBody(SheetData sheetData, WorksheetPart worksheetPart);
        /// <summary>
        /// Создание нижней шапки документа
        /// </summary>
        /// <param name="sheetData"></param>
        /// <param name="worksheetPart"></param>
        protected abstract void CreateFooter(SheetData sheetData, WorksheetPart worksheetPart);
        /// <summary>
        /// Создание колонок со стилями по умолчанию.
        /// </summary>
        /// <param name="columnsCount">Кол-во колонок</param>
        /// <param name="columnsWidth">Ширина колонок</param>
        /// <returns></returns>
        protected Columns DefaultColumnsGenerator(int columnsCount, int columnsWidth = 30)
        {
            Columns columns = new Columns();

            for (int i = 1; i <= columnsCount; i++)
            {
                columns.Append(new Column() { Min = (uint)i, Max = 10, Width = columnsWidth, CustomWidth = true });
            }
            return columns;
        }
        /// <summary>
        /// Создание шапки таблицы со стилями по умолчанию. Название колонок описывается в функции GetColumnsNames
        /// </summary>
        /// <param name="sheetData"></param>
        /// <param name="worksheetPart"></param>
        protected void DefaultHeader(SheetData sheetData, WorksheetPart worksheetPart)
        {
            var columnsNames = GetColumnsNames();
            if (columnsNames == null) throw new ArgumentNullException("Отсутствуют заголовки таблицы.\nРеализуйте функцию GetColumnsNames");
            char ch = 'a';
            uint rowIndex = lastRowIndex++;
            Row row = new Row() { RowIndex = UInt32Value.FromUInt32(rowIndex) };
            foreach (var colName in columnsNames)
            {
                Cell cell = new Cell() { CellReference = $"{ch.ToString().ToUpper()}{rowIndex}", CellValue = new CellValue(colName), DataType = CellValues.String, StyleIndex = 2 };
                row.AppendChild(cell);
                ch++;
            }
            sheetData.AppendChild(row);
        }
    }
}
