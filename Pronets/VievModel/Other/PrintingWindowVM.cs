using Pronets.Data;
using Pronets.VievModel.MainWindows.Pages;
using Pronets.VievModel.Repairs_f;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.VievModel.Other
{
    public class PrintingWindowVM : VievModelBase
    {
        private v_Receipt_Document document;
        public PrintingWindowVM(v_Receipt_Document document)
        {
            this.document = document;
        }
    }
}
