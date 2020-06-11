using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pronets.Model.Labels
{
    public class EltexONTLabelGpon : ILabel
    {
        public string LabelName { get { return "Eltex GPON"; } }

        public Brush SNBorderColor => Brushes.Green;

        public Brush MacBorderColor => Brushes.Green;

        public Brush PonBorderColor => Brushes.Green;

        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial)
        {
            return "^XA" +
                   "^LH10, 10" +
                   "^FO0,10" +
                   "^FB230,1,0,C,0" +
                   $"^A0,32,19^FD{nomenclature}^FS" +
                   "^FO230,1" +
                   "^A0,15,16^FDhttp://192.168.1.1^FS" +
                   "^FO230,16" +
                   "^A0,15,16^FDUsername: user ^FS" +
                   "^FO230,30" +
                   "^A0,15,16^FDPassword: user ^FS" +
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
        string label30x14 = $"^XA" +
                   "^ LH10, 10" +
                   "^FO1,5" +
                   "^A0,25,18^FD{nomenclature}^FS" +
                   "^FO230,1" +
                   "^A0,12,12^FDhttp://192.168.1.1^FS" +
                   "^FO230,12" +
                   "^A0,12,12^FDUsername: user^FS" +
                   "^FO230,22" +
                   "^A0,12,12^FDPassword: user^FS" +
                   "^FO 50,35" +
                   "^BY2" +
                   "^BCN,35,N,N,N" +
                   "^FD{serialNumber}^FS" +
                   "^FO1,75" +
                   "^A0,18,15^FDWAN MAC {macAdress}^FS" +
                   "^FO220,75" +
                   "^A0,18,15^FDS/N {serialNumber}^FS" +
                   "^FO 0,95" +
                   "^BY2" +
                   "^BCN,35,N,N,N" +
                   "^FD{ponSerial}^FS" +
                   "^FO150,135" +
                   "^A0,18,15^FDPON SERIAL {ponSerial}^FS" +
                   "^XZ";
        string label46x24Noalignment = "^XA" +
                    "^LH10,10" +
                    "^FO10,10" +
                    "^A0,35,20^FD{nomenclature}^FS" +
                    "^FO230,1" +
                    "^A0,15,16^FDhttp://192.168.1.1^FS" +
                    "^FO230,15" +
                    "^A0,15,16^FDUsername: user^FS" +
                    "^FO230,30" +
                    "^A0,15,16^FDPassword: user^FS" +
                    "^FO52,45" +
                    "^BY2" +
                    "^BCN,45,N,N,N" +
                    "^FD{serialNumber}^FS" +
                    "^FO10,95" +
                    "^A0,18,15^FDWAN MAC {macAdress}^FS" +
                    "^FO230,95" +
                    "^A0,18,15^FDS/N {serialNumber}^FS" +
                    "^FO9,113" +
                    "^BY2" +
                    "^BCN,45,N,N,N" +
                    "^FD{ponSerial}^FS" +
                    "^FO170,162" +
                    "^A0,18,15^FDPON SERIAL {ponSerial}^FS" +
                    "^XZ";
        string label46x24Center = "^XA" +
                                "^LH10, 10" +
                                "^FO0,10" +
                                "^FB230,1,0,C,0" +
                                "^A0,35,20^FD{nomenclature}^FS" +
                                "^FO230,1" +
                                "^A0,15,16^FD http://192.168.1.1 ^FS" +
                                "^FO230,15" +
                                "^A0,15,16^FD Username: user ^FS" +
                                "^FO230,30" +
                                "^A0,15,16^FD Password: user ^FS" +
                                "^FO345,45,1" +
                                "^BY2" +
                                "^BCN,45,N,N,N" +
                                "^FD{serialNumber}^FS" +
                                "^FO10,95" +
                                "^A0,18,15^FDWAN MAC {macAdress}^FS" +
                                "^FO345,95,1" +
                                "^A0,18,15^FDS/N {serialNumber}^FS" +
                                "^FO 345,113,1  (Штрихкод Пон сериала)" +
                                "^BY2" +
                                "^BCN,45,N,N,N" +
                                "^FD{ponSerial}^FS" +
                                "^FO345,162,1" +
                                "^A0,18,15^FDPON SERIAL {ponSerial}^FS" +
                                "^XZ";
    }
}
