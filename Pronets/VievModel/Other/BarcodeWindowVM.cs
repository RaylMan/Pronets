using Microsoft.Win32;
using Pronets.Data;
using Pronets.EntityRequests.Nomenclature_f;
using Pronets.EntityRequests.Users_f;
using Pronets.Model;
using Pronets.Model.Excel.Documents;
using Pronets.Model.FromXlsxToSQL;
using Pronets.Model.Labels;
using Pronets.Model.Parsehtml;
using Pronets.Model.Printer;
using Pronets.Model.TCP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Other
{
    public class BarcodeWindowVM : VievModelBase
    {
        #region Properties
        XlsxLabelsDocument doc = new XlsxLabelsDocument();
        private string ipAdress = @"http://192.168.1.1/";
        private ObservableCollection<Nomenclature> nomenclatures = new ObservableCollection<Nomenclature>();
        public ObservableCollection<Nomenclature> Nomenclatures
        {
            get { return nomenclatures; }
            set
            {
                nomenclatures = value;
                RaisedPropertyChanged("Nomenclatures");
            }
        }
        private ObservableCollection<Nomenclature_Types> nomenclature_Types = new ObservableCollection<Nomenclature_Types>();
        public ObservableCollection<Nomenclature_Types> Nomenclature_Types
        {
            get { return nomenclature_Types; }
            set
            {
                nomenclature_Types = value;
                RaisedPropertyChanged("Nomenclature_Types");
            }

        }
        private ObservableCollection<IPrint> printers = new ObservableCollection<IPrint>();
        public ObservableCollection<IPrint> Printers
        {
            get { return printers; }
            set
            {
                printers = value;
                RaisedPropertyChanged("Printers");
            }

        }
        private ObservableCollection<ILabel> labels = new ObservableCollection<ILabel>();
        public ObservableCollection<ILabel> Labels
        {
            get { return labels; }
            set
            {
                labels = value;
                RaisedPropertyChanged("Labels");
            }

        }
        private IPrint selectedPrinter;
        public IPrint SelectedPrinter
        {
            get { return selectedPrinter; }
            set
            {
                selectedPrinter = value;
                RaisedPropertyChanged("SelectedPrinter");
            }
        }
        private ILabel selectedLabel;
        public ILabel SelectedLabel
        {
            get { return selectedLabel; }
            set
            {
                selectedLabel = value;
                SetBorderBrushColor();
                SetNomenclatureType();
                RaisedPropertyChanged("SelectedLabel");
            }
        }

        private Nomenclature_Types selectedType;
        public Nomenclature_Types SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                FillNomenclaturete(selectedType);
                RaisedPropertyChanged("SelectedType");
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
        private string count = "1";
        public string Count
        {
            get { return count; }
            set
            {
                count = value;
                RaisedPropertyChanged("Count");
            }
        }
        private string serialNumber;
        public string SerialNumber
        {
            get { return serialNumber; }
            set
            {
                serialNumber = value;
                RaisedPropertyChanged("SerialNumber");
            }
        }
        private string macAdress;
        public string MacAdress
        {
            get { return macAdress; }
            set
            {
                macAdress = value;
                RaisedPropertyChanged("MacAdress");
            }
        }
        private string ponSerial;
        public string PonSerial
        {
            get { return ponSerial; }
            set
            {
                ponSerial = value;
                RaisedPropertyChanged("PonSerial");
            }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisedPropertyChanged("Status");
            }
        }
        private System.Windows.Media.Brush snBorderColor;
        public System.Windows.Media.Brush SNBorderColor
        {
            get { return snBorderColor; }
            set
            {
                snBorderColor = value;
                RaisedPropertyChanged("SNBorderColor");
            }
        }
        private System.Windows.Media.Brush macBorderColor;
        public System.Windows.Media.Brush MacBorderColor
        {
            get { return macBorderColor; }
            set
            {
                macBorderColor = value;
                RaisedPropertyChanged("MacBorderColor");
            }
        }
        private System.Windows.Media.Brush ponBorderColor;
        public System.Windows.Media.Brush PonBorderColor
        {
            get { return ponBorderColor; }
            set
            {
                ponBorderColor = value;
                RaisedPropertyChanged("PonBorderColor");
            }
        }
        private bool isNTE = false;
        public bool IsNTE
        {
            get { return isNTE; }
            set
            {
                isNTE = value;
                if (isNTE) ipAdress = @"http://192.168.0.1/";
                else ipAdress = @"http://192.168.1.1/";
                RaisedPropertyChanged("IsNTE");
            }
        }
        private bool enabled = true;
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                RaisedPropertyChanged("Enabled");
            }
        }
        #endregion
        public BarcodeWindowVM()
        {
            Init();
        }
        #region Methods
        private void GetPrinters()
        {
            try
            {
                Printers = PrintersRepository.GetPrinters();
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionMessanger.Message(e));
            }
        }

        private void Init()
        {
            GetPrinters();
            Labels = LabelRepository.GetLabels();
            Nomenclature_Types = Nomenclature_TypesRequest.FillList();
            SelectedPrinter = PrintersRepository.GetDefaultPrinterName();
            SelectedLabel = Labels[0];
            SetNomenclatureType();
        }
        private void FillNomenclaturete(Nomenclature_Types type)
        {
            if (type != null)
            {
                Nomenclatures.Clear();
                Nomenclatures = NomenclatureRequest.GetNomenclaturesByType(type);
                if (Nomenclatures.Count > 0) SelectedNomenclature = Nomenclatures[0];
            }
        }
        private void SetBorderBrushColor()
        {
            if (selectedLabel != null)
            {
                SNBorderColor = selectedLabel.SNBorderColor;
                MacBorderColor = selectedLabel.MacBorderColor;
                PonBorderColor = selectedLabel.PonBorderColor;
            }
        }
        private void SetNomenclatureType()
        {
            if (selectedLabel != null)
            {
                var type = Nomenclature_Types.FirstOrDefault(t => t.Type == selectedLabel.NomenclatureType);
                if (type != null)
                {
                    SelectedType = type;
                    Nomenclatures = NomenclatureRequest.GetNomenclaturesByType(type);
                    if (Nomenclatures.Count > 0) SelectedNomenclature = Nomenclatures[0];
                }
            }
        }
        #endregion

        #region PrintCommand
        private ICommand printCommand;
        public ICommand PrintCommand
        {
            get
            {
                if (printCommand == null)
                {
                    printCommand = new RelayCommand(new Action<object>(Print));
                }
                return printCommand;
            }
            set
            {
                printCommand = value;
                RaisedPropertyChanged("PrintCommand");
            }
        }
        public async void Print(object Parameter)
        {
            if (int.TryParse(count.Replace(" ", ""), out int numCount))
            {
                if (SelectedLabel != null)
                {
                    if (selectedNomenclature != null)
                    {
                        TextVisibility = Visibility.Visible;
                        try
                        {
                            Status = "Производится печать";
                            var printLabel = selectedLabel.GetZPLCodeLabel(selectedNomenclature.Name.ToUpper(), SerialNumber?.ToUpper().Replace(" ", ""), MacAdress?.ToUpper(), PonSerial?.ToUpper().Replace(" ", ""));
                            for (int i = 0; i < numCount; i++)
                            {
                                Status = "Подключение к серверу и печать.";
                                await Task.Factory.StartNew(() => SelectedPrinter.Print(printLabel));//TCPClient.Print(printLabel)
                            }
                            Status = "Печать завершена!";
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(ExceptionMessanger.Message(e));
                            Status = "Ошибка!";
                        }
                        TextVisibility = Visibility.Hidden;
                    }
                    else MessageBox.Show("Неоходимо выбрать модель!", "Ошибка");
                }
                else MessageBox.Show("Неоходимо выбрать этикетку!", "Ошибка");
            }
            else MessageBox.Show("Неоходимо ввести количество!", "Ошибка");
        }
        #endregion

        #region PrintFromFileCommand
        private ICommand printFromFileCommand;
        public ICommand PrintFromFileCommand
        {
            get
            {
                if (printFromFileCommand == null)
                {
                    printFromFileCommand = new RelayCommand(new Action<object>(PrintFromFile));
                }
                return printFromFileCommand;
            }
            set
            {
                printFromFileCommand = value;
                RaisedPropertyChanged("PrintFromFileCommand");
            }
        }
        public async void PrintFromFile(object Parameter)
        {
            if (selectedLabel != null)
            {
                string FilePath = null;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = ".xlsx Files (*.xlsx)|*.xlsx";

                if (openFileDialog.ShowDialog() == true)
                {
                    Status = "Производится печать";
                    FilePath = openFileDialog.FileName;
                    await Task.Factory.StartNew(() => PrintFromTable(selectedLabel, FilePath));
                }
            }
            else MessageBox.Show("Необходимо выбрать тип этикетки");
        }
        void PrintFromTable(ILabel label, string filepath)
        {
            TextVisibility = Visibility.Visible;
            try
            {
                List<DeviceForLabel> devices = doc.GetDevices(filepath);
                if (devices != null)
                {
                    foreach (var item in devices)
                    {
                        if (!string.IsNullOrWhiteSpace(item.Nomenclature) && item.Nomenclature != "0")
                        {
                            var printLabel = label.GetZPLCodeLabel(item.Nomenclature.ToUpper(), item.SerialNumber.ToUpper(), item.MacAdress.ToUpper(), item.PonSerial.ToUpper());
                            Status = "Подключение к серверу и печать.";
                            SelectedPrinter.Print(printLabel);
                        }
                    }
                }
                Status = "Печать завершена!";
            }
            catch (Exception e)
            {
                Status = "Ошибка!";

               MessageBox.Show(ExceptionMessanger.Message(e));
            }
            TextVisibility = Visibility.Hidden;
        }
        #endregion

        #region Create Example table
        private ICommand createExampleCommand;
        public ICommand CreateExampleCommand
        {
            get
            {
                if (createExampleCommand == null)
                {
                    createExampleCommand = new RelayCommand(new Action<object>(CreateExample));
                }
                return createExampleCommand;
            }
            set
            {
                createExampleCommand = value;
                RaisedPropertyChanged("CreateExampleCommand");
            }
        }
        public void CreateExample(object Parameter)
        {

            string FilePath = null;
            SaveFileDialog showDialog = new SaveFileDialog();
            showDialog.Filter = ".xlsx Files (*.xlsx)|*.xlsx";
            showDialog.FileName = $"Таблица для наклеек";
            if (showDialog.ShowDialog() == true)
            {
                FilePath = showDialog.FileName;
            }
            if (FilePath != null)
            {
                try
                {
                    doc.GenerateFile(FilePath);
                }
                catch (IOException e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }

            }
        }
        #endregion

        #region FillFromBufferCommand
        private ICommand fillFromBufferCommand;
        public ICommand FillFromBufferCommand
        {
            get
            {
                if (fillFromBufferCommand == null)
                {
                    fillFromBufferCommand = new RelayCommand(new Action<object>(FillFromBuffer));
                }
                return fillFromBufferCommand;
            }
            set
            {
                fillFromBufferCommand = value;
                RaisedPropertyChanged("FillFromBufferCommand");
            }
        }
        public async void FillFromBuffer(object Parameter)
        {
            try
            {
                var user = await Task<Users>.Factory.StartNew(UsersRequest.GetDefauldUser);
                SerialNumber = user.BufferSerial;
                MacAdress = user.BufferMac;
                PonSerial = user.BufferPonMac;
            }
            catch (Exception e)
            {
                MessageBox.Show(ExceptionMessanger.Message(e));
            }
        }
        #endregion

        #region FillFromDeviceCommand
        private ICommand fillFromDeviceCommand;
        public ICommand FillFromDeviceCommand
        {
            get
            {
                if (fillFromDeviceCommand == null)
                {
                    fillFromDeviceCommand = new RelayCommand(new Action<object>(FillFromDevice));
                }
                return fillFromDeviceCommand;
            }
            set
            {
                fillFromDeviceCommand = value;
                RaisedPropertyChanged("FillFromDeviceCommand");
            }
        }
        public async void FillFromDevice(object Parameter)
        {
            Enabled = false;
            TextVisibility = Visibility.Visible;
            await Task.Factory.StartNew(FillInfo);
            TextVisibility = Visibility.Hidden;
            Enabled = true;
        }
        private void FillInfo()
        {
            try
            {
                string sHTML = "";
                HTTPClient client = new HTTPClient("session");
                HttpWebResponse httpWebResponse = client.Request(ipAdress);//@"http://192.168.1.1/"
                if (httpWebResponse != null) //&& httpWebResponse.StatusCode == HttpStatusCode.OK
                {
                    httpWebResponse.Close();
                    try
                    {
                        httpWebResponse = client.Request_Post($"{ipAdress}login", "username=user&password=user");
                        if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.OK)
                        {
                            Stream stream = httpWebResponse.GetResponseStream();
                            using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.GetEncoding(1251)))
                            {
                                sHTML = reader.ReadToEnd();
                            }
                        }
                    }
                    catch (System.IO.IOException)
                    {
                        GetRevCHTML(client, httpWebResponse);
                    }
                }
                if (!string.IsNullOrWhiteSpace(sHTML))
                {
                    GetRevCHTML(client, httpWebResponse);
                }
                if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    httpWebResponse.Close();
                    //папка входящие, доступна после авторизации
                    httpWebResponse = client.Request($"{ipAdress}info.html");
                    if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        Stream stream = httpWebResponse.GetResponseStream();
                        using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.GetEncoding(1251)))
                        {
                            sHTML = reader.ReadToEnd();
                        }
                    }
                }
                if (sHTML.Length > 0)
                {
                    var nomenclature = GetNomenclature(HTMLParser.GetNomenclature(sHTML).Result);
                    if (nomenclature != null)
                        SelectedNomenclature = nomenclature;
                    SerialNumber = HTMLParser.GetSerial(sHTML).Result;
                    PonSerial = IsNTE ? HTMLParser.GetPonSerialNTE(sHTML).Result.Replace(":", "") : HTMLParser.GetPonSerial(sHTML).Result.Replace("454C5458", "ELTX");
                    MacAdress = HTMLParser.GetMac(sHTML).Result;
                }
                else
                    MessageBox.Show("Нет соединения с оборудованием");
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}\nПопробуйте еще раз", "Ошибка");
            }
        }
        /// <summary>
        /// Авторизация и получение страницы на NTP-RG-1402G-W RevC
        /// </summary>
        /// <param name="client"></param>
        /// <param name="httpWebResponse"></param>
        /// <returns></returns>
        private string GetRevCHTML(HTTPClient client, HttpWebResponse httpWebResponse)
        {
            httpWebResponse = client.Request_Post($"{ipAdress}login?username=user&password=user", "username=user&password=user");
            if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                Stream stream = httpWebResponse.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.GetEncoding(1251)))
                {
                    return reader.ReadToEnd();
                }
            }
            return "";
        }

        private Nomenclature GetNomenclature(string name)
        {
            if (name != null)
                return nomenclatures.FirstOrDefault(n => n.Name == name);
            return null;
        }
        #endregion
    }
}
