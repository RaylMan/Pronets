using Pronets.Model.Printer;
using System;
using System.Net.Sockets;
using System.Text;

namespace Pronets.Model.TCP
{
    public class TCPClient : IPrint
    {
        private string host = Properties.Settings.Default.ServerHost.ToString();
        private const int port = 8888;
        static TcpClient client;
        static NetworkStream stream;

        public string Name => "Принтер на сервере";

        public void Print(string message)
        {
            client = new TcpClient();
            try
            {
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                Random r = new Random();
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Disconnect();
            }
        }
        private void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
