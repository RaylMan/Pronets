using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Pronets.Data;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Users_f;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pronets.Model.Excel.Documents
{
    class XlsxDefectiveStatementsDocument : AbstractExcelGenerator
    {
        private Clients pronetsClient;
        private Clients client;
        ObservableCollection<v_Repairs> listRepairs;
        protected override string SheetName => "Дефектная ведомость";
        public XlsxDefectiveStatementsDocument(Clients client, ObservableCollection<v_Repairs> listRepairs)
        {
            this.pronetsClient = ClientsRequests.GetPronetsClient();
            this.client = client;
            this.listRepairs = listRepairs;
        }
        protected override void CreateBody(SheetData sheetData, WorksheetPart worksheetPart)
        {
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
        }
        protected override void CreateFooter(SheetData sheetData, WorksheetPart worksheetPart)
        {

        }

        protected override void CreateHeader(SheetData sheetData, WorksheetPart worksheetPart)
        {
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


            char ch = 'a';
            uint rowIndex = (uint)(13);
            Row row = new Row() { RowIndex = UInt32Value.FromUInt32(rowIndex) };

            foreach (var colName in GetColumnsNames())
            {
                Cell cell = new Cell() { CellReference = $"{ch.ToString().ToUpper()}{rowIndex}", CellValue = new CellValue(colName), DataType = CellValues.String, StyleIndex = 1 };
                row.AppendChild(cell);
                ch++;
            }
            sheetData.AppendChild(row);
        }

        protected override Columns GenerateColumns(SheetData sheetData, WorksheetPart worksheetPart)
        {
            return base.DefaultColumnsGenerator(6, 20);
        }

        protected override List<string> GetColumnsNames()
        {
            return new List<string>() { "Наименование", "Серийный номер", "Заявленная неисправность", "Гарантия", "Выявленная неисправность", "Проделанная работа" };
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
            Cell cell4a = new Cell() { CellReference = $"A{row4.RowIndex}", CellValue = new CellValue("E-mail"), DataType = CellValues.String, StyleIndex = 3 };
            row4.Append(cell4a);
            Cell cell4 = new Cell() { CellReference = $"B{row4.RowIndex}", CellValue = new CellValue(client.Email), DataType = CellValues.String };
            row4.Append(cell4);
            sheetData.Append(row4);

        }
    }
}
