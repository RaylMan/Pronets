using Pronets.Data;
using Pronets.EntityRequests.Clients_f;
using Pronets.EntityRequests.Nomenclature_f;
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
        private ObservableCollection<Nomenclature_Types> nomenclatureTypes = new ObservableCollection<Nomenclature_Types>();
        public ObservableCollection<Nomenclature_Types> NomenclatureTypes
        {
            get { return nomenclatureTypes; }

            set
            {
                nomenclatureTypes = value;
                RaisedPropertyChanged("NomenclatureTypes");
            }
        }
        private Nomenclature_Types selectedType;
        public Nomenclature_Types SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                GetContent();

                RaisedPropertyChanged("SelectedType");
            }
        }

        private SortingRepair selectedSortingEquipent;
        public SortingRepair SelectedSortingEquipent
        {
            get { return selectedSortingEquipent; }
            set
            {
                selectedSortingEquipent = value;
                if (selectedSortingEquipent != null && selectedSortingEquipent.NomenclatureName != null)
                    GetRepairByNomenclature();
                RaisedPropertyChanged("SelectedSortingEquipent");
            }
        }
        #endregion

        public EquipmentWindowVM()
        {
            GetContent();
            nomenclatureTypes = Nomenclature_TypesRequest.FillList();
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
        private void GetContent(object parametr) // обновление листа без учета типа оборудования
        {
            pronetsClient = null;
            repairs.Clear();
            sortingEquipments.Clear();
            pronetsClient = ClientsRequests.GetPronetsClient();
            repairs = RepairsRequest.GetPronetsRepairs();

            if (repairs != null && repairs.Count > 0)
            {
                var result = from equip in repairs
                             group equip by new
                             {
                                 equip.Nomenclature1
                             } into n
                             select new { n.Key.Nomenclature1, Count = n.Count() };

                foreach (var item in result)
                {
                    sortingEquipments.Add(new SortingRepair
                    {
                        NomenclatureName = item.Nomenclature1.Name,
                        RepairsCount = item.Count
                    });
                }
            }
        }

        private void GetContent() // вывод в список с учетов типа оборудования
        {
            pronetsClient = null;
            repairs.Clear();
            sortingEquipments.Clear();
            pronetsClient = ClientsRequests.GetPronetsClient();
            repairs = RepairsRequest.GetPronetsRepairs();

            if (repairs != null && repairs.Count > 0)
            {
                var result = from equip in repairs
                             group equip by new
                             {
                                 equip.Nomenclature1
                             } into n
                             select new { n.Key.Nomenclature1, Count = n.Count() };

                foreach (var item in result)
                {
                    if (selectedType == null)
                    {
                        sortingEquipments.Add(new SortingRepair
                        {
                            NomenclatureName = item.Nomenclature1.Name,
                            RepairsCount = item.Count
                        });
                    }
                    else if (item.Nomenclature1.Type == selectedType.Type)
                    {
                        sortingEquipments.Add(new SortingRepair
                        {
                            NomenclatureName = item.Nomenclature1.Name,
                            RepairsCount = item.Count
                        });
                    }
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
                         where repair.Nomenclature == selectedSortingEquipent.NomenclatureName
                         select repair;
            foreach (var item in result)
            {
                repairsByNomenclature.Add(item);
            }
        }
    }
}
