using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.Sdk.Printer.Discovery;
using Zebra.Sdk.Comm;

namespace Pronets.Model.Labels
{
    public class ZebraPrintLabel
    {
        private DiscoveredUsbPrinter usbPrinter;
        Connection connection;
        public ZebraPrintLabel()
        {
            GetPrinterConection();
        }
        private void GetPrinterConection()
        {

            usbPrinter = UsbDiscoverer.GetZebraUsbPrinters(new ZebraPrinterFilter()).FirstOrDefault();
            if (usbPrinter == null) throw new ArgumentException("Принтер не найден!");
                connection = usbPrinter.GetConnection();

        }
        public void Print(string zplCommand)
        {
            try
            {
                connection.Open();
                if (connection.Connected)
                {
                    connection.Write(Encoding.UTF8.GetBytes(zplCommand));
                }
                else throw new ArgumentException("Отсутствует подключение к принтеру");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.Connected)
                    connection.Close();
            }
            
        }

        public void PrintOld(string zplCommand)
        {
            foreach (DiscoveredUsbPrinter usbPrinter in UsbDiscoverer.GetZebraUsbPrinters(new ZebraPrinterFilter()))
            {
                Connection c = usbPrinter.GetConnection();
                try
                {
                    c.Open();
                    if (c.Connected)
                    {
                        //ZebraPrinter printer = ZebraPrinterFactory.GetInstance(c);
                        //ZebraPrinterLinkOs linkOsPrinter = ZebraPrinterFactory.CreateLinkOsPrinter(printer);
                        c.Write(Encoding.UTF8.GetBytes(zplCommand));
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    c.Close();
                }
            }
        }

    }
}
