using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Pronets.Model.Barcode
{
    public class EltexDevice : AbstractDevice
    {
        public EltexDevice(string model, string serialNumber, string ponSerialNumber, string wanMacAdress) : base (model, serialNumber, ponSerialNumber, wanMacAdress)
        {
            base.Manufacturer = "Eltex";
        }
    }
}
