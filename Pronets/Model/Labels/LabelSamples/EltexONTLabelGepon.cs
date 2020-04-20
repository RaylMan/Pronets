using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model.Labels
{
    class EltexONTLabelGepon : ILabel
    {
        public string LabelName { get { return "Eltex GEPON"; } }
       
        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial)
        {
            return "^XA" +
                   "^LH10, 10" +
                   "^FO0,10" +
                   "^FB230,1,0,C,0" +
                   $"^A0,32,19^FD{nomenclature}^FS" +
                   "^FO230,1" +
                   "^A0,15,15^FDhttp://192.168.0.1^FS" +
                   "^FO230,15" +
                   "^A0,15,15^FDUsername: user ^FS" +
                   "^FO230,30" +
                   "^A0,15,15^FDPassword: user ^FS" +
                   "^FO345,45,1" +
                   "^BY2" +
                   "^BCN,45,N,N,N" +
                   $"^FD{serialNumber}^FS" +
                   "^FO10,95" +
                   $"^A0,18,15^FDWAN MAC {macAdress}^FS" +
                   "^FO345,95,1" +
                   $"^A0,18,15^FDS/N {serialNumber}^FS" +
                   "^FO 345,113,1" +
                   "^BY2" +
                   "^BCN,45,N,N,N" +
                   $"^FD{ponSerial}^FS" +
                   "^FO345,162,1" +
                   $"^A0,18,15^FDPON SERIAL {ponSerial}^FS" +
                   "^XZ";
        }
    }
}
