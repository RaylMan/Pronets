using Pronets.EntityRequests.Repairs_f;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ObservableCollection<v_Repairs>> GetRepairsFromDate(DateTime firstDate, DateTime secondDate)
        {
            ObservableCollection<v_Repairs> repairs = new ObservableCollection<v_Repairs>();
            var query = from repair in this.Repairs
                        where repair.Repair_Date >= firstDate && repair.Repair_Date <= secondDate
                        select repair;
            List<Repairs> rep = new List<Repairs>(query);
            if(query != null)
            {
                try
                {
                    return await RepairsRequest.GetViewOfRepairs(rep);
                }
                catch
                {
                    throw;
                }
              
            }
            return repairs;
        }
    }
}
