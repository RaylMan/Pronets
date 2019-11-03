using Pronets.VievModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model
{
    public static class DefectsTextModel
    {
        public static event PropertyChangedEventHandler PropertyChanged;
        public static void RaisedPropertyChanged([CallerMemberName]string PropertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(PropertyChanged, new PropertyChangedEventArgs(PropertyName));
        }
        private static string defect;
        public static string Defect
        {
            get { return defect; }
            set
            {
                defect = value;
                RaisedPropertyChanged("Defect");
            }
        }
        private static string work;
        public static string Work
        {
            get { return work; }
            set
            {
                work = value;
                RaisedPropertyChanged("Work");
            }
        }
    }
}
