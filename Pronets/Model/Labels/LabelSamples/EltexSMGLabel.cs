using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pronets.Model.Labels.LabelSamples
{
    class EltexSMGLabel : ILabel
    {
        public string LabelName { get { return "Eltex SMG"; } }
        public string NomenclatureType => "Станционное оборудование ";
        public Brush SNBorderColor => Brushes.Green;

        public Brush MacBorderColor => Brushes.Green;

        public Brush PonBorderColor => Brushes.Red;
        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial)
        {
            return "^XA" +
                   "^LH10, 10" +
                   "^FO0,10" +
                   "^FB230,1,0,C,0" +
                   $"^A0,32,19^FD{nomenclature}^FS" +
                   "^FO200,1" +
                   "^A0,15,16^FDhttp://192.168.1.2^FS" +
                   "^FO200,16" +
                   "^A0,15,16^FDUsername: admin ^FS" +
                   "^FO200,30" +
                   "^A0,15,16^FDPassword: rootpasswd ^FS" +
                   "^FO345,45,1" +
                   "^BY2" +
                   "^BCN,45,N,N,N" +
                   $"^FD{serialNumber}^FS" +
                   "^FO345,95,1" +
                   $"^A0,18,15^FDS/N {serialNumber}^FS" +
                   "^FO 345,113,1" +
                   "^BY2" +
                   "^BCN,45,N,N,N" +
                   $"^FD{macAdress}^FS" +
                   "^FO345,162,1" +
                   $"^A0,18,15^FDWAN MAC {macAdress}^FS" +
                   "^XZ";
        }
    }
}
