using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Nomenclature_f;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Pronets.VievModel.MainWindows.Pages
{
    public class EquipmentWindowVM : RepairsModel
    {
        #region Properties
        Dispatcher _dispatcher;
        private Clients pronetsClient;

        public OpenWindowCommand OpenWindowCommand { get; set; }

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
        private ObservableCollection<v_Repairs> repairsByNomenclature = new ObservableCollection<v_Repairs>();
        public ObservableCollection<v_Repairs> RepairsByNomenclature
        {
            get { return repairsByNomenclature; }

            set
            {
                repairsByNomenclature = value;
                RaisedPropertyChanged("RepairsByNomenclature");
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

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisedPropertyChanged("IsSelected");
            }
        }

        private SortingRepair selectedSortingEquipent;
        public SortingRepair SelectedSortingEquipent
        {
            get { return selectedSortingEquipent; }
            set
            {
                selectedSortingEquipent = value;
                if (selectedSortingEquipent != null)
                    GetRepairByNomenclatureAsync();
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
        #endregion

        public EquipmentWindowVM()
        {

            _dispatcher = Dispatcher.CurrentDispatcher;
            GetStatuses();
            GetContentAsync();
            OpenWindowCommand = new OpenWindowCommand();
        }
        #region refresh page
        protected ICommand refresh;
        public ICommand RefreshCommand
        {
            get
            {
                if (refresh == null)
                {
                    refresh = new RelayCommand(new Action<object>(GetContentAsync));
                }
                return refresh;
            }
            set
            {
                refresh = value;
                RaisedPropertyChanged("RefreshCommand");
            }
        }
        private async void GetContentAsync()
        {
            V_Repairs.Clear();
            RepairsByNomenclature.Clear();
            pronetsClient = null;
            Repairs.Clear();
            SortingEquipments.Clear();
            await Task.Run(() => GetContent());
        }
        private async void GetContentAsync(object parametr)
        {
            V_Repairs.Clear();
            RepairsByNomenclature.Clear();
            pronetsClient = null;
            Repairs.Clear();
            SortingEquipments.Clear();
            await Task.Run(() => GetContent());
        }
        private void GetContent() // обновление листа без учета типа оборудования
        {

            pronetsClient = ClientsRequests.GetPronetsClient();
            foreach (var status in Statuses)
            {
                if (status.IsSelected == true)
                {
                    foreach (var item in RepairsRequest.GetPronetsRepairs(status.Status, pronetsClient.ClientId))
                    {
                        _dispatcher.Invoke(new Action(() =>
                        {
                            Repairs.Add(item);
                        }));
                    }
                }
            }

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
                    _dispatcher.Invoke(new Action(() =>
                    {
                        SortingEquipments.Add(new SortingRepair
                        {
                            NomenclatureName = item.Nomenclature,
                            RepairsCount = item.Count
                        });
                    }));
                }
            }
            GetTotalAmount();
        }
        #endregion

        #region Search
        private string searchString;

        public string SearchString
        {
            get { return searchString; }
            set
            {
                if (SetProperty(ref searchString, value))
                {

                    PropertyInfo prop = typeof(SortingRepair).GetProperty("NomenclatureName");
                    if (prop != null)
                    {
                        if (
                            sortingEquipments.Any(
                                p =>
                                    prop.GetValue(p)
                                        .ToString()
                                        .ToLower()
                                        .Contains(searchString.ToLower())))
                        {
                            SelectedSortingEquipent =
                                sortingEquipments.First(
                                    p =>
                                        prop.GetValue(p)
                                            .ToString()
                                            .ToLower()
                                            .Contains(searchString.ToLower()));
                        }
                    }
                }
            }
        }
        #endregion

        private async void GetRepairByNomenclatureAsync()
        {
            v_Repairs.Clear();
            repairsByNomenclature.Clear();
            await Task.Run(() => GetRepairByNomenclature());
        }
        private void GetRepairByNomenclature()
        {
            foreach (var status in statuses)
            {
                if (status.IsSelected == true)
                {
                    foreach (var item in RepairsRequest.FillListPronets(status.Status))
                    {
                        _dispatcher.Invoke(new Action(() =>
                        {
                            V_Repairs.Add(item);
                        }));
                    }
                }
            }
            var result = from repair in V_Repairs
                         where repair.Nomenclature == selectedSortingEquipent.NomenclatureName
                         select repair;
            foreach (var item in result)
            {
                _dispatcher.Invoke(new Action(() =>
                {
                    RepairsByNomenclature.Add(item);
                }));
            }
        }

        private void GetStatuses()
        {
            statuses = StatusesRequests.FillList();
            if (statuses != null)
            {
                foreach (var status in Statuses)
                {
                    if (status.Status == "Готово" ||
                        status.Status == "На складе (без ремонта)" ||
                        status.Status == "Принято")
                    {
                        status.IsSelected = true;
                    }
                }
            }
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
    }
}
