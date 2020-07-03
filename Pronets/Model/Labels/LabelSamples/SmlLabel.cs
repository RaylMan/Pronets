using System.Windows.Media;

namespace Pronets.Model.Labels.LabelSamples
{
    class SmlLabel : ILabel
    {
        public string LabelName => "Приставки SML"; 
        public string NomenclatureType => "Приставка";
        public Brush SNBorderColor => Brushes.Green;

        public Brush MacBorderColor => Brushes.Green;

        public Brush PonBorderColor => Brushes.Red;

        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial)
        {
            var barcodeMac = macAdress.Replace(":", "");
            return $@"^XA
                    ^LH10, 10
                    ^FO0,10
                    ^FB350,1,0,C,0
                    ^A0,35,20^FD{nomenclature}^FS
                    ^FO45,45
                    ^BY1
                    ^BCN,45,N,N,N
                    ^FD{serialNumber}^FS
                    ^FO345,95,1
                    ^FB350,1,0,C,0
                    ^A0,18,15^FDS/N   {serialNumber}^FS
                    ^FO 85,113
                    ^BY1
                    ^BCN,45,N,N,N
                    ^FD{barcodeMac}^FS
                    ^FO345,162,1
                    ^FB350,1,0,C,0
                    ^A0,18,15^FDMAC  {macAdress}^FS
                    ^XZ";
        }
    }
}
