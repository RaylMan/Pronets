using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model.Labels.LabelSamples
{
    public class DLinkLoginPassLabel : ILabel
    {
        public string LabelName => "D-Link Login/Password";

        public string GetZPLCodeLabel(string nomenclature = null, string serialNumber = null, string macAdress = null, string ponSerial = null)
        {
            return "^XA"+
                    "^CI28" +
                    "^LH0, 0" +
                    "^FO 0,20" +
                    "^FB370,4,10,C,0^A0,30,22" +
                    "^FDНа устройстве прописан\\&" +
                    "сервисный аккаунт\\&" +
                    "\"service_pronets\"\\&" +
                    "Данный аккаунт не удалять" +
                    "^FS" +
                    "^FO0,95" +
                    "^XZ";
        }
    }
}
