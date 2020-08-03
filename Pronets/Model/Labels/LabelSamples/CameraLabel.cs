using System.Windows.Media;

namespace Pronets.Model.Labels.LabelSamples
{
    class CameraLabel : ILabel
    {
        public string LabelName => "Камера";
        public string NomenclatureType => "Другое";
        public Brush SNBorderColor => Brushes.Green;
        public Brush MacBorderColor => Brushes.Green;
        public Brush PonBorderColor => Brushes.Red;
        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial)
        {
            return $@"^XA
                    ^LH0,10
                    ^FO10,10
                    ^FB350,1,10,C,40
                    ^A0,22,22^FDLogin: admin^FS
                    ^FO10,40
                    ^FB350,1,10,C,40
                    ^A0,22,22^FDPassword: Pro12345^FS
                    ^FO10,70
                    ^FB350,1,10,C,40
                    ^A0,22,22^FDMAC {macAdress}^FS
                    ^FO50,100
                    ^BY2
                    ^BCN,45,N,N,N
                    ^FD{serialNumber}^FS
                    ^FO10,155
                    ^FB350,1,10,C,40
                    ^A0,18,15^FDS/N {serialNumber}^FS
                    ^XZ";
        }
    }
}
