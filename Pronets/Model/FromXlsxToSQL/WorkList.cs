using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model.FromXlsxToSQL
{
    public class WorkList : IComparable<WorkList>
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Claimed_Malfunction { get; set; }
        public string Client { get; set; }
        public DateTime DateOfReceipt { get; set; }
        public string Warranty { get; set; }
        public string IdentifyFault { get; set; }
        public string WorkDone { get; set; }
        public string Engineer { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public int CompareTo(WorkList d)
        {

            return this.Date.CompareTo(d.Name);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            WorkList objAsPart = obj as WorkList;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public bool Equals(WorkList other)
        {
            if (other == null) return false;
            return (this.Date.Equals(other.Name));
        }

    }
}
