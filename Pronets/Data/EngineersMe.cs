using System;
using System.Linq;

namespace Pronets.Data
{
    public partial class Engineers
    {
        public bool IsChecked { get; set; }

        public string GetRepairsCountInfo(DateTime firstDate, DateTime secondDate)
        {
            string reportInfo = null;
            var query = from repair in this.Repairs
                        where repair.Repair_Date >= firstDate && repair.Repair_Date <= secondDate
                        select repair;
          
            reportInfo = $"Общее количество: {query.Count()} шт.\n";

            var result = from item in query
                         orderby item.Repair_Category
                         group item by new
                         {
                             item.Repair_Category
                         }
                             into info
                         select new { info.Key.Repair_Category, Count = info.Count() };

            foreach (var info in result)
            {
                reportInfo += $"\t  {info.Repair_Category}: {info.Count} шт.\n";
            }

            return reportInfo;
        }
    }
}
