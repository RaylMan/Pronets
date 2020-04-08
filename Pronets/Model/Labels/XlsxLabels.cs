using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Pronets.Model.FromXlsxToSQL;

namespace Pronets.Model.Labels
{
    public class XlsxLabels
    {
        public static List<DeviceForLabel> GetDevices(string path)
        {
            XslxExporter exporter = new XslxExporter();
            List<DeviceForLabel> devices = new List<DeviceForLabel>();
            if (!string.IsNullOrWhiteSpace(path))
            {
                foreach (var item in exporter.ReadAsDataTable(path, "rId1").AsEnumerable())
                {
                    devices.Add(new DeviceForLabel
                    {
                        Nomenclature = Regex.Replace(Convert.ToString(item["Наименование"]), " {2,}", " "),
                        SerialNumber = Regex.Replace(Convert.ToString(item["Serial"]), " {2,}", " "),
                        MacAdress = Regex.Replace(Convert.ToString(item["Mac"]), " {2,}", " "),
                        PonSerial = Regex.Replace(Convert.ToString(item["Pon-Serial"]), " {2,}", " ")
                    });
                }
            }
            return devices;
        }
        public static void GenerateExampleFile(string filePath)
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
                wbsp.Stylesheet = XLSWriter.GenerateStyleSheet();
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
                    Name = "Список устройств"
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




                SetTableHeaderRow(sheetData);

                sheets.Append(sheet);

                workbookpart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Close();
            }

        }
        private static void SetTableHeaderRow(SheetData sheetData)
        {
            uint rowIndex = (uint)(1);
            Row row = new Row() { RowIndex = UInt32Value.FromUInt32(rowIndex) };


            Cell header1 = new Cell() { CellReference = $"A{rowIndex}", CellValue = new CellValue("Наименование"), DataType = CellValues.String, StyleIndex = 1 };
            row.AppendChild(header1);
            Cell header2 = new Cell() { CellReference = $"B{rowIndex}", CellValue = new CellValue("Serial"), DataType = CellValues.String, StyleIndex = 1 };
            row.AppendChild(header2);
            Cell header3 = new Cell() { CellReference = $"C{rowIndex}", CellValue = new CellValue("Mac"), DataType = CellValues.String, StyleIndex = 1 };
            row.AppendChild(header3);
            Cell header4 = new Cell() { CellReference = $"D{rowIndex}", CellValue = new CellValue("Pon-Serial"), DataType = CellValues.String, StyleIndex = 1 };
            row.AppendChild(header4);


            sheetData.AppendChild(row);
        }
        private void SetClientRows(object pronetsClient, SheetData sheetData, int v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
}
