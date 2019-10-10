using Pronets.Data;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.VievModel.Other
{
    public class PrintingWindowVM : RepairsModel
    {
        #region Properties
        private v_Receipt_Document document;
        private Clients clientPronets;
        private Clients client;
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

        private string dateOfDocument = "3. Дата заполнения: " + DateTime.Now.ToString("dd.MM.yyyy");
        public string DateOfDocument
        {
            get { return dateOfDocument; }
            set
            {
                dateOfDocument = value;
                RaisedPropertyChanged("DateOfDocument");
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
        protected string clientInfo;
        public string ClientInfo
        {
            get { return clientInfo; }
            set
            {
                clientInfo = value;
                RaisedPropertyChanged("ClientInfo");
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

        public PrintingWindowVM(v_Receipt_Document document, int clientId)
        {
            this.document = document;
            RepairsTable = RepairsRequest.FillReportList(document.Document_Id);
            if (clientId > 0)
                client = ClientsRequests.GetClient(clientId);
            GetInfo();
        }
        public PrintingWindowVM(List<int> repairsId, int clientId)
        {
            this.repairsId = repairsId;
            foreach (var Id in repairsId)
            {
                repairsTable.Add(RepairsRequest.v_GetRepair(Id));
            }
            if (clientId > 0)
                client = ClientsRequests.GetClient(clientId);
            GetInfo();
        }

        private void GetInfo()
        {
            clientPronets = ClientsRequests.GetPronetsClient();
            Telephone_1 = clientPronets.Telephone_1;
            Email = clientPronets.Email;
            adress = clientPronets.Adress;
            pronetsInfo = $"Организация:  ООО \"Пронетс\"" +
                $"\nОтдел: СЦ" +
                $"\nАдрес: {Adress}" +
                $"\nТелефон: {Telephone_1}" +
                $"\nE-mail: {Email}";
            if (client != null)
            {
                clientInfo = $"Организация:  {client.ClientName}" +
                                    $"\nАдрес: {client.Adress}" +
                                    $"\nТелефон: {client.Telephone_1}" +
                                    $"\nE-mail: {client.Email}";
            }
        }
    }
}
