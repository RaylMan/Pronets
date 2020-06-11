using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.Sdk.Comm;

namespace Pronets.Model.Printer
{
    class ZebraTCPConnection : IConnection
    {
        private Connection connection;
        public Connection GetConnection => connection;
        public ZebraTCPConnection(string printerAddr)
        {
            if (!Validations.IsValidIP(printerAddr)) throw new ArgumentException("Не верный IP адресс принтера!");
            
            CreateConnection(printerAddr);
        }
        private void CreateConnection(string printerAddr)
        {
            connection = new TcpConnection(printerAddr, TcpConnection.DEFAULT_ZPL_TCP_PORT);
        }
    }
}
