using Pronets.Data;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.VievModel.Repairs_f
{
    class AllRepairsWindowVM : RepairsModel
    {
        public OpenWindowCommand OpenWindowCommand { get; set; }
        private v_Repairs selectedItem;
        public v_Repairs SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        public AllRepairsWindowVM()
        {
            v_Repairs = RepairsRequest.v_FillList();
            OpenWindowCommand = new OpenWindowCommand();
        }
    }
}
