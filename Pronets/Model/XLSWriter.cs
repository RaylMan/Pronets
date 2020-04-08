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
using System.Windows;

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
            try
            {
                if(filePath != null)
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
                        lstColumns.Append(new Column() { Min = 5, Max = 10, Width = 20, CustomWidth = true });
                        lstColumns.Append(new Column() { Min = 6, Max = 10, Width = 20, CustomWidth = true });
                        if (needToInsertColumns)
                            worksheetPart.Worksheet.InsertAt(lstColumns, 0);


                        #region merge cells
                        SetClientRows(pronetsClient, sheetData, 1, "Исполнитель");
                        SetClientRows(client, sheetData, 7, "Заказчик");

                        MergeCells mergeCells = new MergeCells();

                        //append a MergeCell to the mergeCells for each set of merged cells
                        for (int i = 1; i < 11; i++)
                        {
                            mergeCells.Append(new MergeCell() { Reference = new StringValue($"B{i}:F{i}") });
                        }

                        worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());


                        #endregion

                        SetTableHeaderRow(sheetData);

                        for (int i = 0; i < listRepairs.Count; i++)
                        {
                            uint rowIndex = (uint)(i + 14);
                            Row row = new Row() { RowIndex = UInt32Value.FromUInt32(rowIndex) };


                            Cell header1 = new Cell() { CellReference = $"A{rowIndex}", CellValue = new CellValue(listRepairs[i].Nomenclature), DataType = CellValues.String, StyleIndex = 1 };
                            row.Append(header1);
                            Cell header2 = new Cell() { CellReference = $"B{rowIndex}", CellValue = new CellValue(listRepairs[i].Serial_Number), DataType = CellValues.String, StyleIndex = 1 };
                            row.Append(header2);
                            Cell header3 = new Cell() { CellReference = $"C{rowIndex}", CellValue = new CellValue(listRepairs[i].Identifie_Fault), DataType = CellValues.String, StyleIndex = 1 };
                            row.Append(header3);
                            Cell header4 = new Cell() { CellReference = $"D{rowIndex}", CellValue = new CellValue(listRepairs[i].Warranty), DataType = CellValues.String, StyleIndex = 1 };
                            row.Append(header4);
                            Cell header5 = new Cell() { CellReference = $"E{rowIndex}", CellValue = new CellValue(listRepairs[i].Identifie_Fault), DataType = CellValues.String, StyleIndex = 1 };
                            row.Append(header5);
                            Cell header6 = new Cell() { CellReference = $"F{rowIndex}", CellValue = new CellValue(listRepairs[i].Work_Done), DataType = CellValues.String, StyleIndex = 1 };
                            row.Append(header6);

                            sheetData.Append(row);
                        }



                        sheets.Append(sheet);

                        workbookpart.Workbook.Save();

                        // Close the document.
                        spreadsheetDocument.Close();
                    }
                }
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Закройте файл в который вы хотите сохранить!", "Ошибка");
            }
            catch (System.ArgumentNullException)
            { }
        }
        private void SetTableHeaderRow(SheetData sheetData)
        {
            uint rowIndex = (uint)(13);
            Row row = new Row() { RowIndex = UInt32Value.FromUInt32(rowIndex) };


            Cell header1 = new Cell() { CellReference = $"A{rowIndex}", CellValue = new CellValue("Наименование"), DataType = CellValues.String, StyleIndex = 1 };
            row.AppendChild(header1);
            Cell header2 = new Cell() { CellReference = $"B{rowIndex}", CellValue = new CellValue("Серийный номер"), DataType = CellValues.String, StyleIndex = 1 };
            row.AppendChild(header2);
            Cell header3 = new Cell() { CellReference = $"C{rowIndex}", CellValue = new CellValue("Заявленная неисправность"), DataType = CellValues.String, StyleIndex = 1 };
            row.AppendChild(header3);
            Cell header4 = new Cell() { CellReference = $"D{rowIndex}", CellValue = new CellValue("Гарантия"), DataType = CellValues.String, StyleIndex = 1 };
            row.AppendChild(header4);
            Cell header5 = new Cell() { CellReference = $"E{rowIndex}", CellValue = new CellValue("Выявленная неисправность"), DataType = CellValues.String, StyleIndex = 1 };
            row.AppendChild(header5);
            Cell header6 = new Cell() { CellReference = $"F{rowIndex}", CellValue = new CellValue("Проделанная работа"), DataType = CellValues.String, StyleIndex = 1 };
            row.AppendChild(header6);

            sheetData.AppendChild(row);
        }
        private void SetClientRows(Clients client, SheetData sheetData, uint firstCellIndex, string whoIs)
        {

            Row row = new Row();
            row.RowIndex = firstCellIndex;
            Cell cell = new Cell() { CellReference = $"B{firstCellIndex++}", CellValue = new CellValue(whoIs), DataType = CellValues.String, StyleIndex = 3 };
            row.Append(cell);
            sheetData.Append(row);

            Row row1 = new Row();
            row1.RowIndex = firstCellIndex;
            Cell cell1a = new Cell() { CellReference = $"A{firstCellIndex}", CellValue = new CellValue("Клиент:"), DataType = CellValues.String, StyleIndex = 3 };
            row1.Append(cell1a);
            Cell cell1 = new Cell() { CellReference = $"B{firstCellIndex++}", CellValue = new CellValue(client.ClientName), DataType = CellValues.String };
            row1.Append(cell1);
            
            sheetData.Append(row1);

            Row row2 = new Row();
            row2.RowIndex = firstCellIndex;
            Cell cell2a = new Cell() { CellReference = $"A{firstCellIndex}", CellValue = new CellValue("Адрес:"), DataType = CellValues.String, StyleIndex = 3 };
            row2.Append(cell2a);
            Cell cell2 = new Cell() { CellReference = $"B{firstCellIndex++}", CellValue = new CellValue(client.Adress), DataType = CellValues.String };
            row2.Append(cell2);
            sheetData.Append(row2);

            Row row3 = new Row();
            row3.RowIndex = firstCellIndex;
            Cell cell3a = new Cell() { CellReference = $"A{firstCellIndex}", CellValue = new CellValue("Телефон:"), DataType = CellValues.String, StyleIndex = 3 };
            row3.Append(cell3a);
            Cell cell3 = new Cell() { CellReference = $"B{firstCellIndex++}", CellValue = new CellValue(client.Telephone_1), DataType = CellValues.String };
            row3.Append(cell3);
            sheetData.Append(row3);


            Row row4 = new Row();
            row4.RowIndex = firstCellIndex;
            Cell cell4a = new Cell() { CellReference = $"A{row4.RowIndex}", CellValue = new CellValue("E-mail"), DataType = CellValues.String, StyleIndex = 3};
            row4.Append(cell4a);
            Cell cell4 = new Cell() { CellReference = $"B{row4.RowIndex}", CellValue = new CellValue(client.Email), DataType = CellValues.String };
            row4.Append(cell4);
            sheetData.Append(row4);

        }
        /// <summary>
        /// Формат ячейки:
        /// 1 - 11 шрифт, узкий border
        /// 2 - 11 шрифт, толстый border
        /// </summary>
        /// <returns></returns>
        public static Stylesheet GenerateStyleSheet()
        {
            return new Stylesheet(
                new Fonts(
                    new Font(                                                               // Стиль под номером 0 - Шрифт по умолчанию.
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri" }),
                    new Font(                                                               // Стиль под номером 1 - Жирный шрифт Times New Roman.
                        new Bold(),
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),
                    new Font(                                                               // Стиль под номером 2 - Обычный шрифт Times New Roman.
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),
                    new Font(                                                               // Стиль под номером 3 - Шрифт Times New Roman размером 14.
                        new FontSize() { Val = 14 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Times New Roman" }),
                    new Font(
                        new Bold(),                                                         // Стиль под номером 4 - жирный шрифт Calibri.
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                        new FontName() { Val = "Calibri" })
                ),
                new Fills(
                    new Fill(                                                           // Стиль под номером 0 - Заполнение ячейки по умолчанию.
                        new PatternFill() { PatternType = PatternValues.None }),
                    new Fill(                                                           // Стиль под номером 1 - Заполнение ячейки серым цветом
                        new PatternFill(
                            new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFAAAAAA" } }
                            )
                        { PatternType = PatternValues.Solid }),
                    new Fill(                                                           // Стиль под номером 2 - Заполнение ячейки красным.
                        new PatternFill(
                            new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFFFAAAA" } }
                        )
                        { PatternType = PatternValues.Solid })
                )
                ,
                new Borders(
                    new Border(                                                         // Стиль под номером 0 - Грани.
                        new LeftBorder(),
                        new RightBorder(),
                        new TopBorder(),
                        new BottomBorder(),
                        new DiagonalBorder()),
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
                    new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 },                          // Стиль под номером 0 - The default cell style.  (по умолчанию)
                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true }) { FontId = 0, FillId = 0, BorderId = 2, ApplyFont = true },       // Стиль под номером 1 - Bold 
                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true }) { FontId = 1, FillId = 0, BorderId = 2, ApplyFont = true },       // Стиль под номером 2 - REgular
                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center, WrapText = true }) { FontId = 4, FillId = 0, BorderId = 0, ApplyFont = true },       // Стиль под номером 3 - Times Roman
                    new CellFormat() { FontId = 0, FillId = 2, BorderId = 0, ApplyFill = true },       // Стиль под номером 4 - Yellow Fill
                    new CellFormat(                                                                   // Стиль под номером 5 - Alignment
                        new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                    )
                    { FontId = 0, FillId = 0, BorderId = 0, ApplyAlignment = true },
                    new CellFormat() { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true },      // Стиль под номером 6 - Border
                    new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Right, Vertical = VerticalAlignmentValues.Center, WrapText = true }) { FontId = 2, FillId = 0, BorderId = 2, ApplyFont = true, NumberFormatId = 4 }       // Стиль под номером 7 - Задает числовой формат полю.
                )
            ); // Выход
        }
    }
}
