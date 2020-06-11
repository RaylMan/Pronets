using Pronets.Model.TCP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model.Printer
{
    public static class PrintersRepository
    {
        private static ObservableCollection<IPrint> printers = new ObservableCollection<IPrint>();
        public static ObservableCollection<IPrint> GetPrinters()
        {
            printers.Clear();
            try
            {
                printers.Add(new TCPClient());
                printers.Add(new ZebraPrinter(new ZebraTCPConnection(Properties.Settings.Default.PrinterServerHost.ToString())));
            }
            catch
            {
                throw;
            }

            return printers;
        }
        public static IPrint GetDefaultPrinter()
        {
            var printerName = Properties.Settings.Default.DefaultPrinterName.ToString();
            if (string.IsNullOrWhiteSpace(printerName)) return null;
            
            return printers.FirstOrDefault(p => p.Name == printerName);
        }
    }
}
