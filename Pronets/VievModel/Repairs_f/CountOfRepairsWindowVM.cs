using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pronets.VievModel.Repairs_f
{
    public class CountOfRepairsWindowVM : VievModelBase
    {
        #region Properties
        private ObservableCollection<SortingRepair> sortingEquipments = new ObservableCollection<SortingRepair>();
        public ObservableCollection<SortingRepair> SortingEquipments
        {
            get { return sortingEquipments; }

            set
            {
                sortingEquipments = value;
                RaisedPropertyChanged("SortingEquipments");
            }
        }
        private ObservableCollection<Repairs> repairs = new ObservableCollection<Repairs>();
        public ObservableCollection<Repairs> Repairs
        {
            get { return repairs; }

            set
            {
                repairs = value;
                RaisedPropertyChanged("Repairs");
            }
        }
        private ObservableCollection<Statuses> statuses = new ObservableCollection<Statuses>();
        public ObservableCollection<Statuses> Statuses
        {
            get { return statuses; }

            set
            {
                statuses = value;
                RaisedPropertyChanged("Statuses");
            }
        }
        private ObservableCollection<Clients> clients = new ObservableCollection<Clients>();
        public ObservableCollection<Clients> Clients
        {
            get { return clients; }

            set
            {
                clients = value;
                RaisedPropertyChanged("Clients");
            }
        }
        private Clients selectedClient;
        public Clients SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                RaisedPropertyChanged("SelectedClient");
            }
        }
        private Statuses selectedStatus;
        public Statuses SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                RaisedPropertyChanged("SelectedStatus");
            }
        }
        private SortingRepair selectedSortingEquipent;
        public SortingRepair SelectedSortingEquipent
        {
            get { return selectedSortingEquipent; }
            set
            {
                selectedSortingEquipent = value;
                RaisedPropertyChanged("SelectedSortingEquipent");
            }
        }
        private string totalAmount;
        public string TotalAmount
        {
            get { return totalAmount; }
            set
            {
                totalAmount = value;
                RaisedPropertyChanged("TotalAmount");
            }
        }
        private DateTime firstDate;
        public DateTime FirstDate
        {
            get { return firstDate; }
            set
            {
                firstDate = value;
                RaisedPropertyChanged("FirstDate");
            }
        }
        private DateTime secondDate;
        public DateTime SecondDate
        {
            get { return secondDate; }
            set
            {
                secondDate = value;
                RaisedPropertyChanged("SecondDate");
            }
        }
        #endregion
        public CountOfRepairsWindowVM()
        {
            GetContent();
        }

        #region Methods
        /// <summary>
        /// Выгружет из БД Список клиентов и статусов, устанавливает даты, клиента и статус по умолчанию
        /// </summary>
        private void GetContent()
        {
            FirstDate = DateTime.Now.Date;
            SecondDate = DateTime.Now.Date.AddHours(23);
            GetDefaultStatus();
            GetPronetsClient();
            CalculateRepairs(new object());
        }
        private void GetPronetsClient()
        {
            Clients.Clear();
            Clients = ClientsRequests.FillList();
            foreach (var item in Clients)
            {
                if (item.ClientName == "Пронетс")
                    SelectedClient = item;
            }
        }
        private void GetDefaultStatus()
        {
            Statuses.Clear();
            Statuses = StatusesRequests.FillList();
            foreach (var item in Statuses)
            {
                if (item.Status == "Готово")
                    SelectedStatus = item;
            }
        }
        #endregion

        #region CalculateRepairsCommand
        private ICommand calculateRepairsCommand;
        public ICommand CalculateRepairsCommand
        {
            get
            {
                if (calculateRepairsCommand == null)
                {
                    calculateRepairsCommand = new RelayCommand(new Action<object>(CalculateRepairs));
                }
                return calculateRepairsCommand;
            }
            set
            {
                calculateRepairsCommand = value;
                RaisedPropertyChanged("CalculateRepairsCommand");
            }
        }

        public void CalculateRepairs(object parametr)
        {
            SortingEquipments.Clear();
            Repairs.Clear();
            if (SelectedClient != null && SelectedStatus != null)
            {
                repairs = RepairsRequest.CalculateRepairs(SelectedClient.ClientId, SelectedStatus.Status, firstDate, secondDate);
                if (repairs != null && repairs.Count > 0)
                {
                    var result = from equip in repairs
                                 group equip by new
                                 {
                                     equip.Nomenclature

                                 } into n
                                 select new { n.Key.Nomenclature, Count = n.Count() };
                    foreach (var item in result)
                    {
                        SortingEquipments.Add(new SortingRepair
                        {
                            NomenclatureName = item.Nomenclature,
                            RepairsCount = item.Count
                        });
                    }
                }
            }
            GetTotalAmount();

        }
        private void GetTotalAmount() //Общее количество ремонтов
        {
            int count = 0;
            foreach (var item in SortingEquipments)
            {
                count += item.RepairsCount;
            }
            TotalAmount = $"Общее количество: {count} шт.";
        }
        #endregion
    }
}
