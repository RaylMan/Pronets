using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model.Printer
{
    public interface IPrint
    {
        string Name { get;}
        void Print(string command);
    }
}
