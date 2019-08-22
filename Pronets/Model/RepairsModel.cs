using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using Pronets.Data;
using Pronets.VievModel;
namespace Pronets.Model
{
    public class RepairsModel : VievModelBase, IEnumerable<Repairs>
    {
        private ObservableCollection<Repairs> repairs1;
        public ObservableCollection<Repairs> Repairs1
        {
            get { return this.repairs1; }

            set
            {
                repairs1 = value;
                RaisedPropertyChanged("Repairs");
            }
        }
        public IEnumerator<Repairs> GetEnumerator()
        {
            return repairs1.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Repairs1.GetEnumerator();
        }

    }
}