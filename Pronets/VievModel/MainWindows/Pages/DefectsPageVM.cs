using Pronets.Data;
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

namespace Pronets.VievModel.MainWindows.Pages
{
    public class DefectsPageVM : VievModelBase
    {
        #region Properties
        private ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
        public ObservableCollection<v_Repairs> V_Repairs
        {
            get { return v_Repairs; }

            set
            {
                v_Repairs = value;
                RaisedPropertyChanged("V_Repairs");
            }
        }
        private ObservableCollection<Clients> clients;
        public ObservableCollection<Clients> Clients
        {
            get { return clients; }

            set
            {
                clients = value;
                RaisedPropertyChanged("Clients");
            }
        }
        private Clients selectedClientItem;
        public Clients SelectedClientItem
        {
            get { return selectedClientItem; }
            set
            {
                selectedClientItem = value;
                RaisedPropertyChanged("SelectedClientItem");
            }
        }
        private ObservableCollection<SerialNumbers> serialNumbers = new ObservableCollection<SerialNumbers>();
        public ObservableCollection<SerialNumbers> SerialNumbers
        {
            get { return serialNumbers; }

            set
            {
                serialNumbers = value;
                RaisedPropertyChanged("SerialNumbers");
            }
        }
        private int selectedSerialIndex;
        public int SelectedSerialIndex
        {
            get { return selectedSerialIndex; }
            set
            {
                selectedSerialIndex = value;
                RaisedPropertyChanged("SelectedSerialIndex");
            }
        }
        private int selectedRepairIndex;
        public int SelectedRepairIndex
        {
            get { return selectedRepairIndex; }
            set
            {
                selectedRepairIndex = value;
                RaisedPropertyChanged("SelectedRepairIndex");
            }
        }
        private SerialNumbers selectedSerialItem;
        public SerialNumbers SelectedSerialItem
        {
            get { return selectedSerialItem; }
            set
            {
                selectedSerialItem = value;
                RaisedPropertyChanged("SelectedSerialItem");
            }
        }
        #endregion
        public DefectsPageVM()
        {
            clients = ClientsRequests.FillList();
        }
        #region AddToTable
        private ICommand addToTableCommand;
        public ICommand AddToTableCommand
        {
            get
            {
                if (addToTableCommand == null)
                {
                    addToTableCommand = new RelayCommand(new Action<object>(AddToTable));
                }
                return addToTableCommand;
            }
            set
            {
                addToTableCommand = value;
                RaisedPropertyChanged("AddToTableCommand");
            }
        }
        private void AddToTable(object Parameter)
        {
            V_Repairs.Clear();
            foreach (var serial in serialNumbers)
            {
                foreach (var repair in RepairsRequest.v_FillList(serial.Serial))
                {
                    v_Repairs.Add(repair);
                }
            }
        }
        #endregion

        #region Clear
        private ICommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (clearCommand == null)
                {
                    clearCommand = new RelayCommand(new Action<object>(Clear));
                }
                return clearCommand;
            }
            set
            {
                clearCommand = value;
                RaisedPropertyChanged("ClearCommand");
            }
        }
        private void Clear(object Parameter)
        {
            serialNumbers.Clear();
            v_Repairs.Clear();
        }
        #endregion

        #region Delete Row
        private ICommand removeSerialCommand;
        public ICommand RemoveSerialCommand
        {
            get
            {
                if (removeSerialCommand == null)
                {
                    removeSerialCommand = new RelayCommand(new Action<object>(RemoveSerial));
                }
                return removeSerialCommand;
            }
            set
            {
                removeSerialCommand = value;
                RaisedPropertyChanged("RemoveSerialCommand");
            }
        }
        private void RemoveSerial(object Parameter)
        {
            //if(selectedSerialItem != null)
            //{
            //    var removedItems = V_Repairs.Where(r => r.Serial_Number == SelectedSerialItem.Serial).ToList();
            //    foreach (var repair in removedItems)
            //    {
            //        v_Repairs.Remove(repair);
            //    }
            //    serialNumbers.RemoveAt(SelectedSerialIndex);
            //}
        }
        private ICommand removeRepairCommand;
        public ICommand RemoveRepairCommand
        {
            get
            {
                if (removeRepairCommand == null)
                {
                    removeRepairCommand = new RelayCommand(new Action<object>(RemoveRepair));
                }
                return removeRepairCommand;
            }
            set
            {
                removeRepairCommand = value;
                RaisedPropertyChanged("RemoveRepairCommand");
            }
        }
        private void RemoveRepair(object Parameter)
        {
            V_Repairs.RemoveAt(SelectedRepairIndex);
        }
        #endregion
    }
}
