using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model
{
    public static class Logging
    {
        public static void NewLog(string message)
        {
            using (StreamWriter sr = new StreamWriter("ErrLog.txt", true))
            {
                sr.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}
