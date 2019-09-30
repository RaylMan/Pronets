using Pronets.Data;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pronets.VievModel.MainWindows.Pages
{
    public class EquipmentWindowVM : RepairsModel
    {
        #region Properties
        private Clients pronetsClient;

        public OpenWindowCommand OpenWindowCommand { get; set; }

        private ObservableCollection<SortingEquipments> sortingEquipments = new ObservableCollection<SortingEquipments>();
        public ObservableCollection<SortingEquipments> SortingEquipments
        {
            get { return sortingEquipments; }

            set
            {
                sortingEquipments = value;
                RaisedPropertyChanged("SortingEquipments");
            }
        }
        protected ObservableCollection<v_Repairs> repairsByNomenclature = new ObservableCollection<v_Repairs>();
        public ObservableCollection<v_Repairs> RepairsByNomenclature
        {
            get { return repairsByNomenclature; }

            set
            {
                repairsByNomenclature = value;
                RaisedPropertyChanged("RepairsByNomenclature");
            }
        }

        private SortingEquipments selectedSortingEquipent;
        public SortingEquipments SelectedSortingEquipent
        {
            get { return selectedSortingEquipent; }
            set
            {
                selectedSortingEquipent = value;
                if (selectedSortingEquipent != null && selectedSortingEquipent.Nomenclature != null)
                    GetRepairByNomenclature();
                RaisedPropertyChanged("SelectedSortingEquipent");
            }
        }
        #endregion

        public EquipmentWindowVM()
        {
            GetContent(null);
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
                    refresh = new RelayCommand(new Action<object>(GetContent));
                }
                return refresh;
            }
            set
            {
                refresh = value;
                RaisedPropertyChanged("RefreshCommand");
            }
        }
        private void GetContent(object parametr)
        {
            pronetsClient = null;
            v_Repairs.Clear();
            sortingEquipments.Clear();
            pronetsClient = ClientsRequests.GetPronetsClient();
            v_Repairs = RepairsRequest.FillListPronets();

            if (v_Repairs != null && v_Repairs.Count > 0)
            {
                var result = from equip in v_Repairs
                             group equip by new
                             {
                                 equip.Nomenclature,
                                 equip.Status
                             } into n
                             select new { n.Key.Nomenclature, n.Key.Status, Count = n.Count() };

                foreach (var item in result)
                {
                    sortingEquipments.Add(new SortingEquipments
                    {
                        Nomenclature = item.Nomenclature,
                        Status = item.Status,
                        Count = item.Count
                    });
                }
            }
        }
        #endregion

        private void GetRepairByNomenclature()
        {
            v_Repairs.Clear();
            v_Repairs = RepairsRequest.FillListPronets();

            repairsByNomenclature.Clear();
            var result = from repair in v_Repairs
                         where repair.Nomenclature == selectedSortingEquipent.Nomenclature
                         select repair;
            foreach (var item in result)
            {
                repairsByNomenclature.Add(item);
            }
        }
    }
}
