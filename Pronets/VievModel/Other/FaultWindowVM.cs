using Pronets.Data;
using Pronets.EntityRequests.Repairs_f;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
