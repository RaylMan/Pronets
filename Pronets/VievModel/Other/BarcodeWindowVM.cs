using Microsoft.Win32;
using Pronets.Data;
using Pronets.EntityRequests.Nomenclature_f;
using Pronets.Model.FromXlsxToSQL;
using Pronets.Model.Labels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Other
{
    public class BarcodeWindowVM : VievModelBase
    {
        #region Properties
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
        private ILabel selectedLabel;
        public ILabel SelectedLabel
        {
            get { return selectedLabel; }
            set
            {
                selectedLabel = value;
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
        #endregion
        public BarcodeWindowVM()
        {
            DefaultType();
            Labels.Add(new EltexONTLabelGpon());
            Labels.Add(new EltexONTLabelGepon());
            Labels.Add(new HuaweiLabel());
        }
        void TestLabel()
        {
            selectedLabel = labels[0];
            selectedNomenclature = nomenclatures.FirstOrDefault(n => n.Name.Contains("NTU-RG"));
            SerialNumber = "GP21073997";
            MacAdress = "A8:F9:4B:CA:45:64";
            PonSerial = "ELTX5C0483E0";
        }
        private void DefaultType()
        {
            Nomenclature_Types = Nomenclature_TypesRequest.FillList();
            var defaultType = Nomenclature_Types.FirstOrDefault(t => t.Type == "ONT");
            SelectedType = defaultType;
        }
        private void FillNomenclaturete(Nomenclature_Types type)
        {
            if (type != null)
            {
                Nomenclatures.Clear();
                Nomenclatures = NomenclatureRequest.GetNomenclaturesByType(type);
            }
        }
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
        public void Print(object Parameter)
        {
            if (selectedLabel != null)
            {
                try
                {
                    ZebraPrintLabel printer = new ZebraPrintLabel();
                    var printLabel = selectedLabel.GetZPLCodeLabel(selectedNomenclature.Name, SerialNumber, MacAdress, PonSerial);
                    printer.Print(printLabel);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            else MessageBox.Show("Неоходимо выбрать этикетку!", "Ошибка");
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
                    FilePath = openFileDialog.FileName;
                    await Task.Factory.StartNew(() => PrintFromTable(selectedLabel, FilePath));
                }
            }
        }
        void PrintFromTable(ILabel label, string filepath)
        {
            ZebraPrintLabel printer = new ZebraPrintLabel();
            List<DeviceForLabel> devices = XlsxLabels.GetDevices(filepath);
            if(devices != null)
            {
                foreach (var item in devices)
                {
                    var printLabel = label.GetZPLCodeLabel(item.Nomenclature, item.SerialNumber, item.MacAdress, item.PonSerial);
                    printer.Print(printLabel);
                }
            }
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
                XlsxLabels.GenerateExampleFile(FilePath);
                
        }
        #endregion
    }
}
