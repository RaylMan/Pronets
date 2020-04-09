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
        public bool Connected { get; private set; }
        public ZebraPrintLabel()
        {
        }
        public void Connect()
        {
            usbPrinter = UsbDiscoverer.GetZebraUsbPrinters(new ZebraPrinterFilter()).FirstOrDefault();
            if (usbPrinter == null) throw new ArgumentException("Принтер не найден!");
            connection = usbPrinter.GetConnection();
            Connected = true;
        }
        public bool Close()
        {
            if (connection == null) return false;
            if (connection.Connected)
            {
                connection.Close();
                return true;
            }
            return false;   
        }
        public void Print(string zplCommand)
        {
            if(Connected)
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
            else throw new ArgumentException("Отсутствует подключение к принтеру");
        }
    }
}
