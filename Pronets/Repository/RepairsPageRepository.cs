using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Repository
{
    public class RepairsPageRepository : BaseRepository
    {
        public ObservableCollection<v_Repairs> SearchItem(string word)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            try
            {
                Int32.TryParse(word, out int numericWord);
                var searchItems = from u in db.v_Repairs
                                  where
                                  u.Serial_Number.Contains(word) ||
                                  u.DocumentId == numericWord ||
                                  u.RepairId == numericWord
                                  select u;
                v_Repairs = new ObservableCollection<v_Repairs>(searchItems);
            }
            catch (Exception e)
            {
                throw;
            }
            return v_Repairs;
        }
        public Repairs GetRepair(int repairId)
        {
            Repairs repair = new Repairs();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    repair = db.Repairs.Where(r => r.RepairId == repairId).FirstOrDefault();
                }
                catch
                {
                    throw;
                }
            }
            return repair;
        }
        public void EditRepair(Repairs repair)
        {
            try
            {
                var result = db.Repairs.SingleOrDefault(r => r.RepairId == repair.RepairId);
                if (result != null)
                {
                    result.Identifie_Fault = repair.Identifie_Fault;
                    result.Work_Done = repair.Work_Done;
                    result.Repair_Category = repair.Repair_Category;
                    result.Engineer = repair.Engineer;
                    result.Repair_Date = repair.Repair_Date;
                    result.Status = repair.Status;
                    result.Note = repair.Note;
                    SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SetDocumentStatus(int documentId, string status)
        {
            try
            {
                var result = db.ReceiptDocument.SingleOrDefault(d => d.DocumentId == documentId);
                if (result != null)
                {
                    result.Status = status;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Engineers GetEngineer(int userId)
        {
            Engineers engineer = new Engineers();
            try
            {
                engineer = (Engineers)db.Engineers.FirstOrDefault(e => e.UserId == userId);
            }
            catch (Exception)
            {
                throw;
            }
            return engineer;
        }
    }
}
