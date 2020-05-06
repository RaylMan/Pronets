using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model.Labels
{
    public interface ILabel
    {
        string LabelName { get; }
        System.Windows.Media.Brush SNBorderColor { get; }
        System.Windows.Media.Brush MacBorderColor { get; }
        System.Windows.Media.Brush PonBorderColor { get; }
        /// <summary>
        /// возращает ZPL код этикетки
        /// </summary>
        /// <returns></returns>
        string GetZPLCodeLabel(string nomenclature, string serialNumber, string macAdress, string ponSerial);
    }
}
