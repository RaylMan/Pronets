using System;
using System.Text.RegularExpressions;

namespace Pronets.Model
{
    public static class Validations
    {
        public static bool IsValidIP(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip)) return false;
            
            Regex ipaddr = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            MatchCollection result = ipaddr.Matches(ip);
            if (result.Count > 0)
                return true;

            return false;
        }
    }
}
