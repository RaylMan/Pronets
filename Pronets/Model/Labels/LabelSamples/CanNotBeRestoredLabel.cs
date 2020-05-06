using System.Windows.Media;

namespace Pronets.Model.Labels.LabelSamples
{
    class CanNotBeRestoredLabel : ILabel
    {
        public string LabelName => "Восстановлению не подлежит";

        public Brush SNBorderColor => Brushes.Red;

        public Brush MacBorderColor => Brushes.Red;

        public Brush PonBorderColor => Brushes.Red;

        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial)
        {
            return "^XA" +
                    "^CI28" +
                    "^LH0, 0" +
                    "^FO 0,50" +
                    "^FB370,4,10,C,0^A0,40,32" +
                    "^FDВосстановлению\\&" +
                    "не подлежит\\&" +
                    "^FS" +
                    "^FO0,95" +
                    "^XZ";
        }
    }
}
