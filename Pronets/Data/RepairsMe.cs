using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Data
{
    public partial class Repairs
    {
        public bool IsChecked { get; set; }
        /// <summary>
        /// Запрет на запись в БД
        /// </summary>
        public bool NotMapped { get; set; }
        public virtual Warrantys Warrantys { get; set; }
    }
}
