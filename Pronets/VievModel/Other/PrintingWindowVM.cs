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
using System.Windows.Input;

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
        private string chiefEngineer;
        public string ChiefEngineer
        {
            get { return chiefEngineer; }
            set
            {
                chiefEngineer = value;
                RaisedPropertyChanged("ChiefEngineer");
            }
        }
        private string responsiblePerson;
        public string ResponsiblePerson
        {
            get { return responsiblePerson; }
            set
            {
                responsiblePerson = value;
                RaisedPropertyChanged("ResponsiblePerson");
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

        public PrintingWindowVM(v_Receipt_Document document, int clientId)
        {
            this.document = document;
            RepairsTable = RepairsRequest.FillReportList(document.Document_Id);
            if (clientId > 0)
                client = ClientsRequests.GetClient(clientId);
            GetInfo();
            SetFontSizes();
            fontSize = 10;
            titleFontSize = fontSize + 5;
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
            SetFontSizes();
            fontSize = 10;
            titleFontSize = fontSize + 5;
        }

        private void SetFontSizes()
        {
            for(int i = 6; i < 25; i++)
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
            string respPers = Properties.Settings.Default.ResponsiblePerson ?? "";
            string engineer = Properties.Settings.Default.ChiefEngineer ?? "";
            responsiblePerson = $"С актом ознакомлен          Исполнительный директор _____________ {respPers}";
            chiefEngineer = $"Ответственное лицо          Главный инженер _____________________ {engineer}";
        }
        #region AddRecipientCommand
        private ICommand addRecipientCommand;
        public ICommand AddRecipientCommand
        {
            get
            {
                if (addRecipientCommand == null)
                {
                    addRecipientCommand = new RelayCommand(new Action<object>(AddRecipient));
                }
                return addRecipientCommand;
            }
            set
            {
                addRecipientCommand = value;
                RaisedPropertyChanged("AddRecipientCommand");
            }
        }
        public void AddRecipient(object Parameter)
        {
            if(repairsTable.Count > 0 && client != null)
            {
                foreach (var repair in repairsTable)
                {
                    RepairsRequest.SetRepairRecipient(repair.RepairId, client.ClientName);
                }
            }
        }
        #endregion
    }
}
