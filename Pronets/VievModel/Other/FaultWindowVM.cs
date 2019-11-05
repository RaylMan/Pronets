using Pronets.Data;
using Pronets.EntityRequests.Repairs_f;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pronets.VievModel.Other
{
    class FaultWindowVM : VievModelBase
    {
        private ObservableCollection<Defects> defects = new ObservableCollection<Defects>();
        public ObservableCollection<Defects> Defects
        {
            get { return defects; }
            set
            {
                defects = value;
                RaisedPropertyChanged("Defects");
            }
        }
        private Defects selectedDefect;
        public Defects SelectedDefect
        {
            get { return selectedDefect; }
            set
            {
                selectedDefect = value;
                RaisedPropertyChanged("SelectedDefect");
            }
        }


        public FaultWindowVM()
        {
            Defects = DefectsRequests.FillList();
        }

        #region Send command
        private ICommand sendCommand;
        public ICommand SendCommand
        {
            get
            {
                if (sendCommand == null)
                {
                    sendCommand = new RelayCommand(new Action<object>(Send));
                }
                return sendCommand;
            }
            set
            {
                sendCommand = value;
                RaisedPropertyChanged("SendCommand");
            }
        }
        /// <summary>
        /// Обновляет данные на странице
        /// </summary>
        /// <param name="parametr"></param>
        public void Send(object parametr)
        {
           
        }
        #endregion
    }
}
