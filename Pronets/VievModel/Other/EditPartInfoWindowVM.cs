using Pronets.Data;
using Pronets.EntityRequests.Nomenclature_f;
using Pronets.EntityRequests.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Other
{
    public class EditPartInfoWindowVM : VievModelBase
    {
        #region Properties
        private Parts part;
        private PartsWindowVM partsWindowVM;


        private ObservableCollection<Nomenclature> nomenclatures = new ObservableCollection<Nomenclature>();
        public ObservableCollection<Nomenclature> Nomenclatures
        {
            get { return nomenclatures; }
            set
            {
                nomenclatures = value;
                RaisedPropertyChanged("Nomenclatures");
            }
        }
        private Nomenclature selectedNomenclature;
        public Nomenclature SelectedNomenclature
        {
            get { return selectedNomenclature; }
            set
            {
                selectedNomenclature = value;
                RaisedPropertyChanged("SelectedNomenclature");
            }
        }
        private string part_Name;
        public string Part_Name
        {
            get { return part_Name; }
            set
            {
                part_Name = value;
                RaisedPropertyChanged("Part_Name");
            }
        }
        private decimal part_Price;
        public decimal Part_Price
        {
            get { return part_Price; }
            set
            {
                part_Price = value;
                RaisedPropertyChanged("Part_Price");
            }
        }
        private string part_Info;
        public string Part_Info
        {
            get { return part_Info; }
            set
            {
                part_Info = value;
                RaisedPropertyChanged("Part_Info");
            }
        }


        #endregion
        public EditPartInfoWindowVM(Parts part, PartsWindowVM partsWindowVM)
        {
            this.part = part;
            this.partsWindowVM = partsWindowVM;
            GetContetnt();
        }
        private void GetContetnt()
        {
            nomenclatures = NomenclatureRequest.FillList();
            if (part != null)
            {
                Part_Name = part.Part_Name;
                part_Info = part.Part_Info;
                SelectedNomenclature = (Nomenclature)nomenclatures.FirstOrDefault(n => n.Name == part.Equipment);
            }
        }
        #region Edit Part
        private ICommand savePart;
        public ICommand SavePartCommand
        {
            get
            {
                if (savePart == null)
                {
                    savePart = new RelayCommand(new Action<object>(Save));
                }
                return savePart;
            }
            set
            {
                savePart = value;
                RaisedPropertyChanged("SavePartCommand");
            }
        }
        public void Save(object Parameter)
        {
            if (string.IsNullOrWhiteSpace(Part_Name))
            {
                MessageBox.Show("Введите название запчасти", "Ошибка");
                return;
            }
            if (selectedNomenclature == null)
            {
                MessageBox.Show("Неоходимо выбрать номенклатуру", "Ошибка");
                return;
            }
            part.Part_Name = Part_Name;
            part.Part_Info = Part_Info;
            part.Equipment = SelectedNomenclature.Name;
            if (part.IsNew)
            {
                PartsRequest.AddToBase(part, out bool ex);
                if (ex) MessageBox.Show("Успешно добавлено в базу данных", "Запись");
            }
            else
            {
                PartsRequest.EditPart(part);
                MessageBox.Show("Успешно изменено в базе данных", "Изменение");
            }
            partsWindowVM.FillPartsList();
        }
        #endregion
    }
}
