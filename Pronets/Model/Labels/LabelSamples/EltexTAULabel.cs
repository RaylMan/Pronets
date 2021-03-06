﻿using System.Windows.Media;

namespace Pronets.Model.Labels.LabelSamples
{
    class EltexTAULabel : ILabel
    {
        public string LabelName => "Eltex TAU"; 
        public string NomenclatureType => "VoIP";
        public Brush SNBorderColor => Brushes.Green;

        public Brush MacBorderColor => Brushes.Green;

        public Brush PonBorderColor => Brushes.Red;

        public string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial)
        {
            return "^XA" +
                    "^LH10, 10" +
                    "^FO0,10" +
                    "^FB216,1,0,C,0" +
                    $"^A0,35,28^FD{nomenclature}^FS" +
                    "^FO216,1" +
                    "^A0,15,16^FDhttp://192.168.1.1^FS" +
                    "^FO216,15" +
                    "^A0,15,16^FDUsername: admin^FS" +
                    "^FO216,30" +
                    "^A0,15,16^FDPassword: password^FS" +
                    "^FO345,45,1" +
                    "^BY2" +
                    "^BCN,45,N,N,N" +
                    $"^FD{serialNumber}^FS" +
                    "^FO10,95" +
                    "^A0,18,15^FD^FS" +
                    "^FO345,95,1" +
                    $"^A0,18,15^FDS/N {serialNumber}^FS" +
                    "^FO 345,113,1" +
                    "^BY2" +
                    "^BCN,45,N,N,N" +
                    $"^FD{macAdress}^FS" +
                    "^FO345,162,1" +
                    $"^A0,18,15^FDWAN MAC {macAdress}^FS" +
                    "^XZ";
        }
    }
}
