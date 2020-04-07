using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BarcodeLib;
using System.IO;
using Pronets.Model;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using Pronets.Model.Barcode;
using System.Threading;

namespace Pronets.Viev.Other
{
    /// <summary>
    /// Логика взаимодействия для BarcodeWindow.xaml
    /// </summary>
    public partial class BarcodeWindow : Window
    {
        Barcode barcode = new Barcode();
        EltexDevice testDevice = new EltexDevice("NTU-RG-1402G-WAC-REV-A", "GP21073997", "ELTX5C0483E0", "A8:F9:4B:CA:45:64");
        public BarcodeWindow()
        {
            InitializeComponent();
            TestLabel();
           
            //GetPrintersNames();
        }

        private void btnGenerateBarcode_Click(object sender, RoutedEventArgs e)
        {
            TestLabelGrid();
        }
        void TestLabel()
        {
            pModel.Inlines.Add(new Run(testDevice.Model));
            wanMacText.Inlines.Add(new Run($"WAN MAC {testDevice.WanMacAdress}"));
            serialNumberText.Inlines.Add(new Run($"S/N { testDevice.SerialNumber }"));
            ponSerialText.Inlines.Add(new Run($"PON SERIAL { testDevice.PonSerialNumber }"));
            imgSerial.Source = testDevice.SerialNumberImage;
            imgPonSerial.Source = testDevice.PonSerialNumberImage;
        }
        void TestLabelGrid()
        {
            textModel.Text = testDevice.Model;
            textInfo.Text = $"http://192.168.1.1\nUsername: user\nPassword: user";
            SerialImage.Source = testDevice.GenerateSerialNumberImage(serialRow.ActualHeight);
            textWanMac.Text = $"WAN MAC {testDevice.WanMacAdress}";
            textSerial.Text = $"S/N { testDevice.SerialNumber }";
            PonSerialImage.Source = testDevice.GeneratePonSerialNumberImage(ponRow.ActualHeight);
            textPonSerial.Text = $"PON SERIAL { testDevice.PonSerialNumber }";
        }

        private void SaveImage(Grid grid)
        {
            int gridHeigth = (int)grid.ActualHeight;
            int gridWidth = (int)grid.ActualWidth;
            RenderTargetBitmap bmp = new RenderTargetBitmap(1024, 544, 96, 96, PixelFormats.Pbgra32);//1024 544
            System.Windows.Shapes.Rectangle fillBackground = new System.Windows.Shapes.Rectangle
            {
                Width = bmp.PixelWidth,
                Height = bmp.PixelHeight,
                Fill = System.Windows.Media.Brushes.White
            };

            Grid newGrid = LabelGrid;
            newGrid.Height = bmp.Height;
            newGrid.Width = bmp.Width;

            bmp.Render(newGrid);

            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            using (Stream stm = File.Create("C:\\12.bmp"))
            {
                encoder.Save(stm);
            }
            img.Source = bmp;

            //UsbDiscoverer.GetZebraUsbPrinters(new ZebraPrinterFilter())
            // PrintLabel();
        }

        private void PrintLabel()
        {
            foreach (DiscoveredUsbPrinter usbPrinter in UsbDiscoverer.GetZebraUsbPrinters(new ZebraPrinterFilter()))
            {
                Connection c = usbPrinter.GetConnection();
                Connection connection = new TcpConnection("127.0.0.1", 9100);
                try
                {
                    //c.Open();
                    connection.Open();
                    if (connection.Connected)
                        MessageBox.Show("Подключено");

                    if (c.Connected)
                    {
                        ZebraPrinter printer = ZebraPrinterFactory.GetInstance(c);
                        ZebraPrinterLinkOs linkOsPrinter = ZebraPrinterFactory.CreateLinkOsPrinter(printer);
                        
                        linkOsPrinter.PrintImage("C:\\12.gif", 0, 0, 350, 160, false);
                        //GetSettingsAtFile(c);

                        //printer.PrintImage("C:\\12.bmp", 0, 0, 350, 160, false);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    c.Close();
                }
            }
        }

        void GetSettingsAtFile(Connection c)
        {
            using (StreamWriter sw = new StreamWriter("PrinterSettings.txt", false))
            {
                ZebraPrinter genericPrinter = ZebraPrinterFactory.GetInstance(c);
                ZebraPrinterLinkOs linkOsPrinter = ZebraPrinterFactory.CreateLinkOsPrinter(genericPrinter);

                if (linkOsPrinter != null)
                {
                    sw.WriteLine("Available Settings for myDevice");
                    HashSet<string> availableSettings = linkOsPrinter.GetAvailableSettings();
                    foreach (string setting in availableSettings)
                    {
                        Console.WriteLine($"{setting}: Range = ({linkOsPrinter.GetSettingRange(setting)})");
                    }

                    sw.WriteLine("\nCurrent Setting Values for myDevice");
                    Dictionary<string, string> allSettingValues = linkOsPrinter.GetAllSettingValues();
                    foreach (string settingName in allSettingValues.Keys)
                    {
                        sw.WriteLine($"{settingName}:{allSettingValues[settingName]}");
                    }

                    string darknessSettingId = "print.tone";
                    string newDarknessValue = "10.0";
                    if (availableSettings.Contains(darknessSettingId) &&
                        linkOsPrinter.IsSettingValid(darknessSettingId, newDarknessValue) &&
                        linkOsPrinter.IsSettingReadOnly(darknessSettingId) == false)
                    {
                        linkOsPrinter.SetSetting(darknessSettingId, newDarknessValue);
                    }

                    sw.WriteLine($"\nNew {darknessSettingId} Value = {linkOsPrinter.GetSettingValue(darknessSettingId)}");
                }
            }
        }
        private void Print(FlowDocument document, string title, double documentWidth)
        {
            PrintLabel();

            //var pd = new PrintDialog();

            //if (pd.ShowDialog() == true)
            //{
            //    //LabelGrid.Measure(new System.Windows.Size(pd.PrintableAreaWidth, pd.PrintableAreaHeight));
            //    //LabelGrid.Arrange(new Rect(0, 0, 0, 0));
            //    pd.PrintDocument(new Paginator(LabelGrid), "Вывод этикетки на печать");
            //}
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument doc = flowDocument;
            Print(doc, "Этикетка", 10);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveImage(LabelGrid);
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
