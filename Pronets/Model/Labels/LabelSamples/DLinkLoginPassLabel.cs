using System.Windows.Media;


namespace Pronets.Model.Labels.LabelSamples
{
    public class DLinkLoginPassLabel : ILabel
    {
        public string LabelName => "D-Link Login/Password";
        public string NomenclatureType => "Станционное оборудование ";
        public Brush SNBorderColor => Brushes.Red;

        public Brush MacBorderColor => Brushes.Red;

        public Brush PonBorderColor => Brushes.Red;

        public string GetZPLCodeLabel(string nomenclature = null, string serialNumber = null, string macAdress = null, string ponSerial = null)
        {
            return "^XA " +
                   "^LH0, 0" +
                   "^FO20,20" +
                   "^A0,30,22^FDLogin: admin^FS" +
                   "^FO20,55" +
                   "^A0,30,22^FDPassword: admin^FS" +
                   "^FO20,110" +
                   "^A0,30,22^FDLogin: admin^FS" +
                   "^FO20,145" +
                   "^A0,30,22^FDPassword: admin^FS" +
                   "^FO202,20" +
                   "^A0,30,22^FDLogin: admin^FS" +
                   "^FO202,55" +
                   "^A0,30,22^FDPassword: admin^FS" +
                   "^FO202,110" +
                   "^A0,30,22^FDLogin: admin^FS" +
                   "^FO202,145" +
                   "^A0,30,22^FDPassword: admin^FS" +
                   "^FO0,95" +
                   "^GB370,0,1^FS" +
                   "^FO182,0" +
                   "^GB0,200,1^FS" +
                   "^XZ";
           
        }
    }
}
