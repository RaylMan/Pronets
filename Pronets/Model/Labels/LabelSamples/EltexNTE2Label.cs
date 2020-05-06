using System.Windows.Media;

namespace Pronets.Model.Labels.LabelSamples
{
    public class EltexNTE2Label : ILabel
    {
        public string LabelName => "Eltex NTE-2";

        public Brush SNBorderColor => Brushes.Green;

        public Brush MacBorderColor => Brushes.Green;

        public Brush PonBorderColor => Brushes.Green;

        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial)
        {
           return "^XA" +
                    "^LH10, 10" +
                    "^FO345,10,1" +
                    "^FB230,1,0,R,0" +
                    $"^A0,35,20^FD{nomenclature}^FS" +
                    "^FO345,45,1" +
                    "^BY2" +
                    "^BCN,45,N,N,N" +
                    $"^FD{serialNumber}^FS" +
                    "^FO345,95,1" +
                    $"^A0,18,15^FDS/N {serialNumber}^FS" +
                    "^FO 345,113,1" +
                    "^BY2" +
                    "^BCN,45,N,N,N" +
                    $"^FD{ponSerial}^FS" +
                    "^FO345,162,1" +
                    $"^A0,18,15^FDPON MAC {ponSerial}^FS" +
                    "^XZ";
        }
    }
}
