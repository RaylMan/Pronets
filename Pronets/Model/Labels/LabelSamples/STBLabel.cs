using System.Windows.Media;

namespace Pronets.Model.Labels.LabelSamples
{
    public class STBLabel : ILabel
    {
        public string LabelName => "Приставки";
        public string NomenclatureType => "Приставка";

        public Brush SNBorderColor => Brushes.Green;

        public Brush MacBorderColor => Brushes.Green;

        public Brush PonBorderColor => Brushes.Red;

        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial)
        {
            return "^XA" +
                    "^LH10, 10" +
                    "^FO345,10,1" +
                    "^FB230,1,0,R,0" +
                    $"^A0,35,20^FD{nomenclature}    ^FS" +
                    "^FO345,45,1" +
                    "^BY2" +
                    "^BCN,45,N,N,N" +
                    $"^FD{serialNumber}^FS" +
                    "^FO345,95,1" +
                    $"^A0,18,15^FDS/N    {serialNumber}^FS" +
                    "^FO 345,113,1" +
                    "^BY2" +
                    "^BCN,45,N,N,N" +
                    $"^FD{macAdress}^FS" +
                    "^FO345,162,1" +
                    $"^A0,18,15^FDMAC    {macAdress}^FS" +
                    "^XZ";
        }
    }
}
