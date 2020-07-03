using System.Windows.Media;

namespace Pronets.Model.Labels
{
    class HuaweiLabel : ILabel
    {
        public string LabelName => "Huawei";
        public string NomenclatureType => "GPON";
        public Brush SNBorderColor => Brushes.Green;

        public Brush MacBorderColor => Brushes.Green;

        public Brush PonBorderColor => Brushes.Red;

        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial = null)
        {
            return "^XA" +
                    "^LH0,10" +
                    "^FO0,10" +
                    "^FB370,1,0,C,0" +
                    $"^A0,35,25^FDHuawei {nomenclature}^FS" +
                    "^FO75,45" +
                    "^BY1" +
                    "^BCN,50,N,N,N" +
                    $"^FD{serialNumber}\\^FS" +
                    "^FO0,105" +
                    "^FB370,2,10,C,40" +
                    $"^A0,18,15^FDS/N: {serialNumber}\\&MAC: {macAdress}^FS" +
                    "^XZ";
        }
        string huaweiAlignCenter = "^XA" +
                                    "^LH0,10" +
                                    "^FO0,10" +
                                    "^FB370,1,0,C,0" +
                                    "^A0,35,20^FDHuawei {nomenclature}^FS" +
                                    "^FO75,45" +
                                    "^BY1" +
                                    "^BCN,50,N,N,N" +
                                    "^FD{serialNumber}\\^FS" +
                                    "^FO0,105" +
                                    "^FB370,2,10,C,40" +
                                    "^A0,18,15^FDS/N{serialNumber}\\&MAC: {macAdress}^FS" +
                                    "^XZ";
        string huaweiNoAlign = "^XA" +
                    "^LH10,10" +
                    "^FO100,10" +
                    "^A0,35,20^FDHuawei {nomenclature}^FS" +
                    "^FO65,45" +
                    "^BY1" +
                    "^BCN,50,N,N,N" +
                    "^FD{serialNumber}^FS" +
                    "^FO100,105" +
                    "^A0,18,15^FDS/N{serialNumber}^FS" +
                    "^FO110,135" +
                    "^A0,18,15^FDMAC: {macAdress}^FS" +
                    "^XZ";
    }
}
