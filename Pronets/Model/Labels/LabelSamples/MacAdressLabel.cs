using System.Windows.Media;

namespace Pronets.Model.Labels.LabelSamples
{
    public class MacAdressLabel : ILabel
    {
        public string LabelName => "MAC адрес";
        public string NomenclatureType => "GPON";
        public Brush SNBorderColor => Brushes.Red;

        public Brush MacBorderColor => Brushes.Green;

        public Brush PonBorderColor => Brushes.Red;

        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial)
        {
            return $@"^XA
                    ^LH0,10
                    ^FO95,45
                    ^BY1
                    ^BCN,70,N,N,N
                    ^FD {macAdress}^FS
                    ^FO10,120
                    ^FB350,1,10,C,40
                    ^A0,14,14^FDMAC {macAdress}^FS
                    ^XZ";
        }
    }
}
