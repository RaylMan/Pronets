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

namespace Pronets.Model
{
    public class XLSWriter
    {
        private string responsiblePerson = null;
        private string chiefEngineer = null;
        private Clients pronetsClient;
        private Clients client;
        public XLSWriter(Clients pronetsClient, Clients client, string responsiblePerson, string chiefEngineer)
        {
            this.responsiblePerson = responsiblePerson;
            this.chiefEngineer = chiefEngineer;
            this.pronetsClient = pronetsClient;
            this.client = client;
            
        }

        public void GenerateFile(string filePath, ObservableCollection<v_Repairs> listRepairs)
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

                // Добавляем в документ набор стилей
                wbsp.Stylesheet = GenerateStyleSheet();
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
                    Name = "Дефектная ведомость"
                };

                Columns lstColumns = worksheetPart.Worksheet.GetFirstChild<Columns>();
                Boolean needToInsertColumns = false;
                if (lstColumns == null)
                {
                    lstColumns = new Columns();
                    needToInsertColumns = true;
                }
                lstColumns.Append(new Column() { Min = 1, Max = 10, Width = 20, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 2, Max = 10, Width = 20, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 3, Max = 10, Width = 20, CustomWidth = true });
                lstColumns.Append(new Column() { Min = 4, Max = 10, Width = 20, CustomWidth = true });

                if (needToInsertColumns)
                    worksheetPart.Worksheet.InsertAt(lstColumns, 0);


                SetClientRows(pronetsClient, sheetData, 1, "Исполнитель");
                SetClientRows(client, sheetData, 7, "Заказчик");

                MergeCells mergeCells = new MergeCells();

                //append a MergeCell to the mergeCells for each set of merged cells
                for (int i = 1; i < 11; i++)
                {
                    mergeCells.Append(new MergeCell() { Reference = new StringValue($"A{i}:D{i}") });
                }

                worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());

                for (int i = 0; i < listRepairs.Count; i++)
                {
                    uint rowIndex = (uint)(i + 12);
                    Row row = new Row() { RowIndex = UInt32Value.FromUInt32(rowIndex) };

                    Cell header1 = new Cell() { CellReference = $"A{rowIndex}", CellValue = new CellValue(listRepairs[i].Nomenclature), DataType = CellValues.String, StyleIndex = 1 };
                    row.Append(header1);
                    Cell header2 = new Cell() { CellReference = $"B{rowIndex}", CellValue = new CellValue(listRepairs[i].Serial_Number), DataType = CellValues.String, StyleIndex = 1 };
                    row.Append(header2);
                    Cell header3 = new Cell() { CellReference = $"C{rowIndex}", CellValue = new CellValue(listRepairs[i].Identifie_Fault), DataType = CellValues.String, StyleIndex = 1 };
                    row.Append(header3);
                    Cell header4 = new Cell() { CellReference = $"D{rowIndex}", CellValue = new CellValue(listRepairs[i].Work_Done), DataType = CellValues.String, StyleIndex = 1 };
                    row.Append(header4);
                    sheetData.Append(row);
                }



                sheets.Append(sheet);

                workbookpart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Close();
            }
        }
        /// <summary>
        /// Заполняет строки информации о клиенте(5 строк)
        /// </summary>
        /// <param name="client">Экземпляр клиента</param>
        /// <param name="sheetData"></param>
        /// <param name="firstCellIndex">инекс строки с которой начнется зполнение</param>
        /// <param name="whoIs">Передать "Исполнитель" или "Приемщик" в зависимости от клиента</param>
        private void SetClientRows(Clients client, SheetData sheetData, uint firstCellIndex, string whoIs)
        {

            Row row = new Row();
            row.RowIndex = firstCellIndex;
            Cell cell = new Cell() { CellReference = $"A{firstCellIndex++}", CellValue = new CellValue(whoIs), DataType = CellValues.String };
            row.AppendChild(cell);
            sheetData.AppendChild(row);

            Row row1 = new Row();
            row1.RowIndex = firstCellIndex;
            Cell cell1 = new Cell() { CellReference = $"A{firstCellIndex++}", CellValue = new CellValue(client.ClientName), DataType = CellValues.String };
            row1.AppendChild(cell1);
            sheetData.AppendChild(row1);

            Row row2 = new Row();
            row2.RowIndex = firstCellIndex;
            Cell cell2 = new Cell() { CellReference = $"A{firstCellIndex++}", CellValue = new CellValue(client.Adress), DataType = CellValues.String };
            row2.AppendChild(cell2);
            sheetData.AppendChild(row2);

            Row row3 = new Row();
            row3.RowIndex = firstCellIndex;
            Cell cell3 = new Cell() { CellReference = $"A{firstCellIndex++}", CellValue = new CellValue(client.Telephone_1), DataType = CellValues.String };
            row3.AppendChild(cell3);
            sheetData.AppendChild(row3);


            Row row4 = new Row();
            row4.RowIndex = firstCellIndex;
            Cell cell4 = new Cell() { CellReference = $"A{firstCellIndex++}", CellValue = new CellValue(client.Email), DataType = CellValues.String };
            row4.AppendChild(cell4);
            sheetData.AppendChild(row4);

        }
        /// <summary>
        /// Формат ячейки:
        /// 1 - 11 шрифт, узкий border
        /// 2 - 11 шрифт, толстый border
        /// </summary>
        /// <returns></returns>
        private Stylesheet GenerateStyleSheet()
        {
            return new Stylesheet(
                new Fonts(
                    new Font(                                                               // Стиль под номером 0 - Обычный шрифт Times New Roman.
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),
                    new Font(                                                               // Стиль под номером 1 - Шрифт Times New Roman размером 14.
                        new FontSize() { Val = 14 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" })
                    ),
                new Borders(
                    new Border(                                                         // Стиль под номером 1 - Грани
                        new LeftBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Medium },
                        new RightBorder(
                            new Color() { Indexed = (UInt32Value)64U }
                        )
                        { Style = BorderStyleValues.Medium },
                        new TopBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Medium },
                        new BottomBorder(
                            new Color() { Indexed = (UInt32Value)64U }
                        )
                        { Style = BorderStyleValues.Medium },
                        new DiagonalBorder()),
                    new Border(                                                         // Стиль под номером 2 - Грани.
                        new LeftBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new RightBorder(
                            new Color() { Indexed = (UInt32Value)64U }
                        )
                        { Style = BorderStyleValues.Thin },
                        new TopBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new BottomBorder(
                            new Color() { Indexed = (UInt32Value)64U }
                        )
                        { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                    ),
                 new CellFormats(
                     new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true }) { FontId = 0, BorderId = 2, ApplyFont = true },
                     new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true }) { FontId = 0, BorderId = 1, ApplyFont = true }

                     )
                );
        }
    }
}
