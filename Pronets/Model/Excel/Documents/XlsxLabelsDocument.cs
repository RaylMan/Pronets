using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Pronets.Model.FromXlsxToSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pronets.Model.Excel.Documents
{
    public class XlsxLabelsDocument : AbstractExcelGenerator
    {
        XslxReader reader = new XslxReader();
        protected override string SheetName => "Список устройств";
        protected override List<string> GetColumnsNames()
        {
            return new List<string> { "Наименование", "Serial", "Mac", "Pon-Serial" };
        }
        protected override Columns GenerateColumns(SheetData sheetData, WorksheetPart worksheetPart)
        {
            return base.DefaultColumnsGenerator(4, 20);
        }

        protected override void CreateBody(SheetData sheetData, WorksheetPart worksheetPart)
        {
        }

        protected override void CreateFooter(SheetData sheetData, WorksheetPart worksheetPart)
        {
        }

        protected override void CreateHeader(SheetData sheetData, WorksheetPart worksheetPart)
        {
            base.DefaultHeader(sheetData, worksheetPart);
        }
        /// <summary>
        /// Возвращает из сгенерированного файла таблицу устройств
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        public List<DeviceForLabel> GetDevices(string path)
        {
            XslxExporter exporter = new XslxExporter();
            List<DeviceForLabel> devices = new List<DeviceForLabel>();
            try
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    foreach (var item in reader.ReadAsDataTable(path, "rId1").AsEnumerable())
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
            }
            catch (Exception)
            {
                throw;
            }
            return devices;
        }
    }
}
