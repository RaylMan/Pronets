using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pronets.Data;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;

namespace Pronets.VievModel.Other
{
    public class PtintingPurchaseWindowVM : VievModelBase
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

        private ObservableCollection<FontSizes> fontSizes = new ObservableCollection<FontSizes>();

        public ObservableCollection<FontSizes> FontSizes
        {
            get { return fontSizes; }

            set
            {
                fontSizes = value;
                RaisedPropertyChanged("FontSizes");
            }
        }

        private FontSizes selectedSize;

        public FontSizes SelectedSize
        {
            get { return selectedSize; }

            set
            {
                selectedSize = value;
                FontSize = SelectedSize.FontSize;
                TitleFontSize = SelectedSize.FontSize + 5;
                RaisedPropertyChanged("SelectedSize");
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
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisedPropertyChanged("Title");
            }
        }

        private string pronetsInfo;
        public string PronetsInfo
        {
            get { return pronetsInfo; }
            set
            {
                pronetsInfo = value;
                RaisedPropertyChanged("PronetsInfo");
            }
        }
        private string clientInfo;
        public string ClientInfo
        {
            get { return clientInfo; }
            set
            {
                clientInfo = value;
                RaisedPropertyChanged("ClientInfo");
            }
        }
        private string inn;
        public string Inn
        {
            get { return inn; }
            set
            {
                inn = value;
                RaisedPropertyChanged("Inn");
            }
        }

        private string telephone_1;
        public string Telephone_1
        {
            get { return telephone_1; }
            set
            {
                telephone_1 = value;
                RaisedPropertyChanged("Telephone_1");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisedPropertyChanged("Email");
            }
        }
        private string adress;
        public string Adress
        {
            get { return adress; }
            set
            {
                adress = value;
                RaisedPropertyChanged("Adress");
            }
        }

        private int fontSize;
        public int FontSize
        {
            get { return fontSize; }
            set
            {
                fontSize = value;
                RaisedPropertyChanged("FontSize");
            }
        }

        private int titleFontSize;
        public int TitleFontSize
        {
            get { return titleFontSize; }
            set
            {
                titleFontSize = value;
                RaisedPropertyChanged("TitleFontSize");
            }
        }
        #endregion

        public PtintingPurchaseWindowVM(v_Receipt_Document document, int clientId)
        {
            this.document = document;
            RepairsTable = RepairsRequest.FillReportList(document.Document_Id);
            if (clientId > 0)
                client = ClientsRequests.GetClient(clientId);
            GetInfo();
            SetFontSizes();
        }

        private void SetFontSizes()
        {
            for (int i = 6; i < 25; i++)
            {
                fontSizes.Add(new FontSizes { FontSize = i });
            }
            foreach (var item in fontSizes)
            {
                if (item.FontSize == 10)
                    selectedSize = item;
            }
        }

        private void GetInfo()
        {
            var date = document.Date ?? DateTime.Now; //привести из DateTime?(nullable) в DateTime
            title = $"Акт приема №{document.Document_Id} \nот {date.ToString("dd MMMM yyyy")}";
            clientPronets = ClientsRequests.GetPronetsClient();
            Telephone_1 = clientPronets.Telephone_1;
            Email = clientPronets.Email;
            adress = clientPronets.Adress;
            pronetsInfo = $"Организация:  ООО \"Пронетс\"" +
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


