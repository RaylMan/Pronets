using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Pronets.Model.FromXlsxToSQL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pronets.VievModel.ConvertToSQL
{
    public class ConvertToSQLWindowVM : VievModelBase
    {
        XslxExporter exporter = new XslxExporter();
        private ObservableCollection<WorkList> workList = new ObservableCollection<WorkList>();
        public ObservableCollection<WorkList> WorkList
        {
            get { return workList; }
            set
            {
                workList = value;
                RaisedPropertyChanged("WorkList");
            }
        }
        private ObservableCollection<SheetsId> sheets = new ObservableCollection<SheetsId>();
        public ObservableCollection<SheetsId> Sheets
        {
            get { return sheets; }
            set
            {
                sheets = value;
                RaisedPropertyChanged("Sheets");
            }
        }
        private SheetsId selectedSheet;
        public SheetsId SelectedSheet
        {
            get { return selectedSheet; }
            set
            {
                selectedSheet = value;
                GetTableFromSheet();
                RaisedPropertyChanged("SelectedSheet");
            }
        }
        private string path;
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                RaisedPropertyChanged("Path");
            }
        }
        public ConvertToSQLWindowVM()
        {
            
            
        }
       
        void GetTableFromSheet()
        {
            workList.Clear();
            if (SelectedSheet != null && !string.IsNullOrWhiteSpace(path))
            {
                foreach (var item in exporter.ReadAsDataTable(path, SelectedSheet.SheetID).AsEnumerable())
                {
                    if(!string.IsNullOrWhiteSpace(item["SN"].ToString()))
                    workList.Add(new WorkList {
                        Name = Regex.Replace(Convert.ToString(item["Наименование оборудования"]), " {2,}", " "),
                        SerialNumber = Regex.Replace(Convert.ToString(item["SN"]), " {2,}", " "),
                        Claimed_Malfunction = Regex.Replace(Convert.ToString(item["Заявленная Неисправность"]), " {2,}", " "),
                        Client = Regex.Replace(Convert.ToString(item["Клиент"]), " {2,}", " "),
                        DateOfReceipt = exporter.ConvToDate(Convert.ToString(item["Дата сдачи в СЦ"])),
                        Warranty = Regex.Replace(Convert.ToString(item["Гарантия"]), " {2,}", " "),
                        IdentifyFault = Regex.Replace(Convert.ToString(item["Выявленная неисправность "]), " {2,}", " "),
                        WorkDone = Regex.Replace(Convert.ToString(item["Проделанный ремонт"]), " {2,}", " "),
                        Engineer = Regex.Replace(Convert.ToString(item["ФИО Мастера"]), " {2,}", " ").Split(' ').First(),
                        Date = exporter.ConvToDate(Convert.ToString(item["Дата"]))
                    });
                }
            }
            //Thread.Sleep(10);
        }
        #region OpenCommand
        private ICommand openCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (openCommand == null)
                {
                    openCommand = new RelayCommand(new Action<object>(Open));
                }
                return openCommand;
            }
            set
            {
                openCommand = value;
                RaisedPropertyChanged("OpenCommand");
            }
        }
        public void Open(object Parameter)
        {
            if(!string.IsNullOrWhiteSpace(path))
            {
                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(path, false))
                {
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheetsXls = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    List<SheetsId> listSheets = new List<SheetsId>();

                    foreach (var sheet in sheetsXls)
                    {
                        sheets.Add(new SheetsId() { SheetID = sheet.Id, SheetName = sheet.Name });
                    }
                }
            }
        }
        #endregion

        #region ExportCommand
        private ICommand exportCommand;
        public ICommand ExportCommand
        {
            get
            {
                if (exportCommand == null)
                {
                    exportCommand = new RelayCommand(new Action<object>(Export));
                }
                return exportCommand;
            }
            set
            {
                exportCommand = value;
                RaisedPropertyChanged("ExportCommand");
            }
        }
        public void Export(object Parameter)
        {
           
        }
        #endregion
    }
}
