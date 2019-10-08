using Pronets.Data;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.VievModel.Other
{
    class PrintingWindowVM : RepairsModel
    {
        private v_Receipt_Document document;
        private string dateOfDocument = "Дата заполнения: " + DateTime.Now.ToString("dd.MM.yyyy");
        public string DateOfDocument
        {
            get { return dateOfDocument; }
            set
            {
                dateOfDocument = value;
                RaisedPropertyChanged("DateOfDocument");
            }
        }
        public PrintingWindowVM(v_Receipt_Document document)
        {
            this.document = document;
            V_Repairs = RepairsRequest.FillList(document.Document_Id);
            Client_Name = document.Client;
        }
    }
}
