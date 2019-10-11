using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pronets.Data;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Repairs_f;

namespace Pronets.VievModel.Other
{
    public class PtintingPurchaseWindowVM : VievModelBase
    {

        #region Properties
        private v_Receipt_Document document;
        private Clients clientPronets;
        private List<int> repairsId = new List<int>();
        private ObservableCollection<v_Repairs> repairsTable = new ObservableCollection<v_Repairs>();
        public ObservableCollection<v_Repairs> RepairsTable
        {
            get { return repairsTable; }

            set
            {
                repairsTable = value;
                RaisedPropertyChanged("RepairsTable");
            }
        }

        private string dateOfDocument1 = DateTime.Now.ToString("dd.MM.yyyy");
        public string DateOfDocument1
        {
            get { return dateOfDocument1; }
            set
            {
                dateOfDocument1 = value;
                RaisedPropertyChanged("DateOfDocument1");
            }
        }

        protected string pronetsInfo;
        public string PronetsInfo
        {
            get { return pronetsInfo; }
            set
            {
                pronetsInfo = value;
                RaisedPropertyChanged("PronetsInfo");
            }
        }
        protected string inn;
        public string Inn
        {
            get { return inn; }
            set
            {
                inn = value;
                RaisedPropertyChanged("Inn");
            }
        }

        protected string telephone_1;
        public string Telephone_1
        {
            get { return telephone_1; }
            set
            {
                telephone_1 = value;
                RaisedPropertyChanged("Telephone_1");
            }
        }

        protected string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisedPropertyChanged("Email");
            }
        }
        protected string adress;
        public string Adress
        {
            get { return adress; }
            set
            {
                adress = value;
                RaisedPropertyChanged("Adress");
            }
        }
        #endregion

        public PtintingPurchaseWindowVM(v_Receipt_Document document)
        {
            this.document = document;
            RepairsTable = RepairsRequest.FillReportList(document.Document_Id);
            GetPronetsInfo();
        }

        private void GetPronetsInfo()
        {
            clientPronets = ClientsRequests.GetPronetsClient();
            Telephone_1 = clientPronets.Telephone_1;
            Email = clientPronets.Email;
            adress = clientPronets.Adress;
            pronetsInfo = $"4. Данные Исполнителя:\n 4.1. Организация:  ООО Пронетс\\Новые сети НСК" +
                $"\n4.2. Отдел: СЦ\n4.4 {Adress}\n" +
                $"4.5 Телефон: {Telephone_1}\n" +
                $"4.6 Email: {Email}";
        }
    }
}


