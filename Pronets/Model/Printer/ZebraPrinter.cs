using System;
using System.Runtime.CompilerServices;
using System.Text;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace Pronets.Model.Printer
{
    public class ZebraPrinter : IDisposable, IPrint
    {
        Connection connection;

        public string Name => "Сетевой принтер";

        public ZebraPrinter(IConnection con)
        {
            if (con == null) throw new ArgumentException("Не передан объект подключения к принтеру!");
            this.connection = con.GetConnection;
        }
        public void Print(string zplData)
        {
            try
            {
                connection.Open();
                connection.Write(Encoding.UTF8.GetBytes(zplData));
            }
            catch (ConnectionException e)
            {
                throw new ArgumentException("Нет подключения к принтеру.\nПодключите принтер или установите IP адрес принтера:\nЛичные данные -> Настройки -> IP Адрес принтера");
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection.Connected)
                    connection.Close();
            }
        }
        public void Dispose()
        {
            if (connection.Connected)
                connection.Close();
        }
    }
}
