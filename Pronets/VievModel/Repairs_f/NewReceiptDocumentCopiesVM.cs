using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pronets.VievModel.Repairs_f
{
    public class NewReceiptDocumentCopiesVM : VievModelBase
    {
        private ObservableCollection<Repairs> repairs;
        private ObservableCollection<v_Repairs> copyRepairs;
        public ObservableCollection<v_Repairs> CopyRepairs
        {
            get { return copyRepairs; }
            set
            {
                copyRepairs = value;
                RaisedPropertyChanged("CopyRepairs");
            }
        }
        private v_Repairs selectedRepair;
        public v_Repairs SelectedRepair
        {
            get { return selectedRepair; }
            set
            {
                selectedRepair = value;
                RaisedPropertyChanged("SelectedRepair");
            }
        }
        public NewReceiptDocumentCopiesVM(ObservableCollection<v_Repairs> copyRepairs, ObservableCollection<Repairs> repairs)
        {
            this.copyRepairs = copyRepairs;
            this.repairs = repairs;
        }

        #region RemoveSelectedCommand
        private ICommand removeSelectedCommand;
        public ICommand RemoveSelectedCommand
        {
            get
            {
                if (removeSelectedCommand == null)
                {
                    removeSelectedCommand = new RelayCommand(new Action<object>(RemoveSelected));
                }
                return removeSelectedCommand;
            }
            set
            {
                removeSelectedCommand = value;
                RaisedPropertyChanged("RemoveSelectedCommand");
            }
        }
        public void RemoveSelected(object Parameter)
        {
            ObservableCollection<v_Repairs> bufer = new ObservableCollection<v_Repairs>();
            foreach (var repair in copyRepairs)
            {
                if (repair.IsChecked)
                {
                    var query = repairs.Where(r => r.Serial_Number == repair.Serial_Number).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            repairs.Remove(item);
                        }
                    }
                    bufer.Add(repair);
                }
            }
            foreach (var item in bufer)
            {
                copyRepairs.Remove(item);
            }
        }
        #endregion
        #region RemoveCommand
        private ICommand removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                {
                    removeCommand = new RelayCommand(new Action<object>(Remove));
                }
                return removeCommand;
            }
            set
            {
                removeCommand = value;
                RaisedPropertyChanged("RemoveCommand");
            }
        }
        public void Remove(object Parameter)
        {
            if (SelectedRepair != null)
            {
                var query = repairs.FirstOrDefault(r => r.Serial_Number == SelectedRepair.Serial_Number);
                if (query != null)
                {
                    repairs.Remove(query);
                    CopyRepairs.RemoveAt(SelectedIndex);
                }
            }
        }
        #endregion
    }
}
