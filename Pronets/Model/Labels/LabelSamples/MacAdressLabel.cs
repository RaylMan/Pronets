using System.Windows.Media;

namespace Pronets.Model.Labels.LabelSamples
{
    public class MacAdressLabel : ILabel
    {
        public string LabelName => "MAC адрес";

        public Brush SNBorderColor => Brushes.Red;

        public Brush MacBorderColor => Brushes.Green;

        public Brush PonBorderColor => Brushes.Red;

        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial)
        {
            return $@"^XA
                    ^LH0,10
                    ^FO10,45
                    ^FB350,1,10,C,40
                    ^BY2
                    ^BCN,70,Y,N,N
                    ^FD{macAdress}^FS
                    ^XZ";
        }
    }
}
