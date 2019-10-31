using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Nomenclature_f;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Repairs_f;
using Pronets.EntityRequests.Users_f;
using Pronets.Model.FromXlsxToSQL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Pronets.VievModel.ConvertToSQL
{
    public class ConvertToSQLWindowVM : VievModelBase
    {

        #region Properties
        Dispatcher _dispatcher;
        XslxExporter exporter = new XslxExporter();
        private ObservableCollection<BaseFromExcel> baseFromExcel = new ObservableCollection<BaseFromExcel>();
        public ObservableCollection<BaseFromExcel> BaseFromExcel
        {
            get { return baseFromExcel; }
            set
            {
                baseFromExcel = value;
                RaisedPropertyChanged("BaseFromExcel");
            }
        }
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
        private ObservableCollection<Clients> clients = new ObservableCollection<Clients>();
        public ObservableCollection<Clients> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                RaisedPropertyChanged("Clients");
            }
        }

        private ObservableCollection<Nomenclature> nomenclature = new ObservableCollection<Nomenclature>();
        public ObservableCollection<Nomenclature> Nomenclature
        {
            get { return nomenclature; }
            set
            {
                nomenclature = value;
                RaisedPropertyChanged("Nomenclature");
            }
        }
        private ObservableCollection<Statuses> statuses = new ObservableCollection<Statuses>();
        public ObservableCollection<Statuses> Statuses
        {
            get { return statuses; }
            set
            {
                statuses = value;
                RaisedPropertyChanged("Statuses");
            }
        }

        private Nomenclature selectedNomenclature;
        public Nomenclature SelectedNomenclature
        {
            get { return selectedNomenclature; }
            set
            {
                selectedNomenclature = value;
                RaisedPropertyChanged("SelectedNomenclature");
            }
        }

        private Clients selectedClient;
        public Clients SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                RaisedPropertyChanged("SelectedClient");
            }
        }
        private Statuses selectedStatus;
        public Statuses SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                RaisedPropertyChanged("SelectedStatus");
            }
        }

        private SheetsId selectedSheet;
        public SheetsId SelectedSheet
        {
            get { return selectedSheet; }
            set
            {
                selectedSheet = value;
                GetTableFromSheetAsync();
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
        private bool allChecked;
        public bool AllChecked
        {
            get { return allChecked; }
            set
            {
                allChecked = value;
                GetAllChecked();
                RaisedPropertyChanged("AllChecked");
            }
        }
        private int progressValue;
        public int ProgressValue
        {
            get { return progressValue; }
            set
            {
                progressValue = value;
                RaisedPropertyChanged("ProgressValue");
            }
        }
        private int maxValue;
        public int MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                RaisedPropertyChanged("MaxValue");
            }
        }

        #endregion

        public ConvertToSQLWindowVM()
        {
            GetBaseFromExcel();
            Clients = ClientsRequests.FillList();
            Nomenclature = NomenclatureRequest.FillList();
            Statuses = StatusesRequests.FillList();
            _dispatcher = Dispatcher.CurrentDispatcher;
            progressValue = 0;
        }

        public void GetAllChecked()
        {
            if (BaseFromExcel.Count > 0)
            {
                foreach (var item in BaseFromExcel)
                {
                    item.IsSelected = allChecked;
                }
            }
        }
        private string SetStatus(string status)
        {
            string newStatus = "Принято";
            if (!string.IsNullOrWhiteSpace(status) && status != "0")
            {
                foreach (var item in statuses)
                {
                    if (status == item.Status)
                    {
                        newStatus = item.Status;
                    }
                }
            }
            return newStatus;
        }
        void GetBaseFromExcel()
        {
            baseFromExcel.Clear();
            foreach (var item in BaseFromExcelRequest.FillList())
            {
                BaseFromExcel.Add(item);
            }
        }
        private async void GetTableFromSheetAsync()
        {
            workList.Clear();
            await Task.Run(() => GetTableFromSheet());
        }
        private void GetTableFromSheet()
        {
            if (SelectedSheet != null && !string.IsNullOrWhiteSpace(path))
            {
                foreach (var item in exporter.ReadAsDataTable(path, SelectedSheet.SheetID).AsEnumerable())
                {
                    if (!string.IsNullOrWhiteSpace(item["SN"].ToString()) && item["SN"].ToString() != "0")
                        _dispatcher.Invoke(new Action(() =>

                        {
                            WorkList.Add(new WorkList
                            {
                                Name = Regex.Replace(Convert.ToString(item["Наименование оборудования"]), " {2,}", " "),
                                SerialNumber = Regex.Replace(Convert.ToString(item["SN"]), " {2,}", " "),
                                Claimed_Malfunction = Regex.Replace(Convert.ToString(item["Заявленная Неисправность"]), " {2,}", " "),
                                Client = Regex.Replace(Convert.ToString(item["Клиент"]), " {2,}", " "),
                                DateOfReceipt = exporter.ConvToDate(Convert.ToString(item["Дата сдачи в СЦ"])),
                                Warranty = Regex.Replace(Convert.ToString(item["Гарантия"]), " {2,}", " "),
                                IdentifyFault = Regex.Replace(Convert.ToString(item["Выявленная неисправность "]), " {2,}", " "),
                                WorkDone = Regex.Replace(Convert.ToString(item["Проделанный ремонт"]), " {2,}", " "),
                                Engineer = Regex.Replace(Convert.ToString(item["ФИО Мастера"]), " {2,}", " ").Split(' ').First(),
                                Date = exporter.ConvToDate(Convert.ToString(item["Дата"])),
                                Status = SetStatus(Regex.Replace(Convert.ToString(item["Статус"]), " {2,}", " "))
                            });
                        }));
                }
            }
            MessageBox.Show("Загрузка закончена!");
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
            if (!string.IsNullOrWhiteSpace(path))
            {
                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(path, false))
                {
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheetsXls = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    List<SheetsId> listSheets = new List<SheetsId>();

                    foreach (var sheet in sheetsXls)
                    {
                        Sheets.Add(new SheetsId() { SheetID = sheet.Id, SheetName = sheet.Name });
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
                    exportCommand = new RelayCommand(new Action<object>(GetCopyRepairsAsync));
                }
                return exportCommand;
            }
            set
            {
                exportCommand = value;
                RaisedPropertyChanged("ExportCommand");
            }
        }
        private async void GetCopyRepairsAsync(object Parameter)
        {
            await Task.Run(() => Export());
        }
        public void Export(/*object Parameter*/)
        {
            foreach (var item in workList)
            {
                _dispatcher.Invoke(new Action(() =>

                {
                    BaseFromExcel.Add(new Data.BaseFromExcel
                    {
                        Name = item.Name.Length < 50 ? item.Name : item.Name.Substring(0, 50),
                        SerialNumber = item.SerialNumber.Length < 50 ? item.SerialNumber : item.SerialNumber.Substring(0, 50),
                        Claimed_Malfunction = item.Claimed_Malfunction.Length < 200 ? item.Claimed_Malfunction : item.Claimed_Malfunction.Substring(0, 200),
                        Client = item.Client.Length < 50 ? item.Client : item.Client.Substring(0, 50),
                        DateOfReceipt = item.DateOfReceipt,
                        Warranty = item.Warranty.Length < 50 ? item.Warranty : item.Warranty.Substring(0, 50),
                        IdentifyFault = item.IdentifyFault.Length < 200 ? item.IdentifyFault : item.IdentifyFault.Substring(0, 200),
                        WorkDone = item.WorkDone.Length < 200 ? item.WorkDone : item.WorkDone.Substring(0, 200),
                        Engineer = item.Engineer.Length < 50 ? item.Engineer : item.Engineer.Substring(0, 50),
                        Date = item.Date,
                        Status = item.Status.Length < 50 ? item.Status : item.Status.Substring(0, 50),
                    });

                }));

            }
            try
            {
                foreach (var item in baseFromExcel)
                {
                    BaseFromExcelRequest.AddToBase(item);//запись на строну sql
                }
            }
            catch (Exception)
            {

            }
            MessageBox.Show("Загрузка закончена!");
        }
        #endregion

        #region Clear BaseFromExcel
        private ICommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (clearCommand == null)
                {
                    clearCommand = new RelayCommand(new Action<object>(Clear));
                }
                return clearCommand;
            }
            set
            {
                clearCommand = value;
                RaisedPropertyChanged("ClearCommand");
            }
        }
        public void Clear(object Parameter)
        {
            BaseFromExcelRequest.ClearBase();
            baseFromExcel.Clear();
        }
        #endregion


        #region SaveAtRepairsCommand
        private ICommand saveAtRepairsCommand;
        public ICommand SaveAtRepairsCommand
        {
            get
            {
                if (saveAtRepairsCommand == null)
                {
                    saveAtRepairsCommand = new RelayCommand(new Action<object>(SaveAtRepairsAsync));
                }
                return saveAtRepairsCommand;
            }
            set
            {
                saveAtRepairsCommand = value;
                RaisedPropertyChanged("SaveAtRepairsCommand");
            }
        }
        private async void SaveAtRepairsAsync(object Parameter)
        {
            await Task.Run(() => SaveAtRepairs());
            GetBaseFromExcel();
            MessageBox.Show("Успешная запись!", "Запись");
        }
        public void SaveAtRepairs(/*object Parameter*/)
        {
            if (selectedClient != null && selectedStatus != null)
            {
                DateTime defaultDate = new DateTime(2017, 1, 1);
                var result = MessageBox.Show("Вы Действительно хотете записать в базу?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int count = 0;
                    foreach (var item in BaseFromExcel)
                    {
                        if (item.IsSelected == true)
                            count++;
                    }
                    if (count > 0)
                    {
                        var defaultEngineer = UsersRequest.GetEngineer("Не выбран");
                        ReceiptDocument newReceiptDocument = new ReceiptDocument
                        {
                            ClientId = selectedClient.ClientId,
                            InspectorId = Properties.Settings.Default.DefaultUserId,
                            Date = DateTime.Now,
                            Status = SelectedStatus.Status
                        };
                        ReceiptDocumentRequest.AddToBase(newReceiptDocument);
                        int documentId = ReceiptDocumentRequest.GetDocumentID();
                        foreach (var item in BaseFromExcel)
                        {
                            var engineer = UsersRequest.GetEngineer(item.Engineer) ?? defaultEngineer;
                            _dispatcher.Invoke(new Action(() =>
                            {
                                if (item.IsSelected)
                                {
                                    var repair = new Repairs
                                    {
                                        DocumentId = documentId,
                                        Nomenclature = selectedNomenclature != null ? selectedNomenclature.Name : "Отсутствует",
                                        Serial_Number = item.SerialNumber,
                                        Claimed_Malfunction = item.Claimed_Malfunction,
                                        Client = SelectedClient.ClientId,
                                        Status = item.Status,
                                        Date_Of_Receipt = item.DateOfReceipt > defaultDate ? item.DateOfReceipt : defaultDate,
                                        Engineer = engineer.Id,
                                        Inspector = Properties.Settings.Default.DefaultUserId,
                                        Warranty = item.Warranty,
                                        Identifie_Fault = item.IdentifyFault,
                                        Work_Done = item.WorkDone,
                                        Repair_Date = item.Date > defaultDate ? item.DateOfReceipt : defaultDate
                                    };
                                    RepairsRequest.AddToBase(repair);
                                    BaseFromExcelRequest.RemoveFromBase(item);
                                }
                            }));
                        }
                    }
                }
            }
            else
                MessageBox.Show("Выберите клиента, номенклатуру, статус!", "Ошибка");
        }
        #endregion
    }

}


