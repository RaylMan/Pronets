using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model.Labels
{
    class HuaweiLabel : ILabel
    {
        public string LabelName => "Huawei";

        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial = null)
        {
            return "^XA" +
                    "^LH10,10" +
                    "^FO100,10" +
                    $"^A0,35,20^FDHuawei {nomenclature}^FS" +
                    "^FO65,45" +
                    "^BY1" +
                    "^BCN,50,N,N,N" +
                    $"^FD{serialNumber}^FS" +
                    "^FO100,105" +
                    $"^A0,18,15^FDS/N{serialNumber}^FS" +
                    "^FO110,135" +
                    $"^A0,18,15^FDMAC: {macAdress}^FS" +
                    "^XZ";
        }
    }
}
