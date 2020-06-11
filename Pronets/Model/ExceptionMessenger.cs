using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Model
{
    public class ExceptionMessanger
    {
        public static string Message(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return Message(ex.InnerException);
            }
            return ex.Message;
        }
    }
}
