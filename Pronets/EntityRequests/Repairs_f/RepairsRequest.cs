using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.EntityRequests.Repairs_f
{
    class RepairsRequest
    {
        private static ObservableCollection<Repairs> repairs = new ObservableCollection<Repairs>();

        public static ObservableCollection<Repairs> FillList()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (repairs != null)
                    repairs.Clear();
                foreach (var item in db.Repairs)
                {
                    repairs.Add(new Repairs
                    {
                        RepairId = item.RepairId,
                        DocumentId = item.DocumentId,
                        Nomenclature = item.Nomenclature,
                        Serial_Number = item.Serial_Number,
                        Client = item.Client,
                        Date_Of_Receipt = item.Date_Of_Receipt,
                        Departure_Date = item.Departure_Date,
                        Inspector = item.Inspector,
                        Warranty = item.Warranty,
                        Identifie_Fault = item.Identifie_Fault,
                        Work_Done = item.Work_Done,
                        Repair_Category = item.Repair_Category,
                        Engineer = item.Engineer,
                        Repair_Date = item.Repair_Date,
                        Status = item.Status,
                        Note = item.Note,
                        Clients = item.Clients,
                        Nomenclature1 = item.Nomenclature1,
                        ReceiptDocument = item.ReceiptDocument,
                        Statuses = item.Statuses,
                        Users = item.Users,
                        Users1 = item.Users1

                    });
                }
            }
            return repairs;
        }
        public static ObservableCollection<Repairs> FillList(int DocumentId)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (repairs != null)
                    repairs.Clear();
                var result = from repair in db.Repairs
                             where repair.DocumentId == DocumentId
                             select repair;
                repairs = new ObservableCollection<Repairs>(result);
            }
            return repairs;
        }
        public static void AddToBase(ObservableCollection<Repairs> repairs)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (repairs != null)
                {
                    foreach (var repair in repairs)
                    {
                        db.Repairs.Add(new Repairs
                        {
                            DocumentId = repair.DocumentId,
                            Nomenclature = repair.Nomenclature,
                            Serial_Number = repair.Serial_Number,
                            Claimed_Malfunction = repair.Claimed_Malfunction,
                            Client = repair.Client,
                            Date_Of_Receipt = repair.Date_Of_Receipt,
                            Departure_Date = repair.Departure_Date,
                            Inspector = repair.Inspector,
                            Warranty = repair.Warranty,
                            Identifie_Fault = repair.Identifie_Fault,
                            Work_Done = repair.Work_Done,
                            Repair_Category = repair.Repair_Category,
                            Engineer = repair.Engineer,
                            Repair_Date = repair.Repair_Date,
                            Status = repair.Status,
                            Note = repair.Note,
                        });
                    }
                    db.SaveChanges();
                }
            }
        }
        public static void EditItem(Repairs repair)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                var result = db.Repairs.SingleOrDefault(r => r.RepairId == repair.RepairId);
                if (result != null)
                {
                    result.Nomenclature = repair.Nomenclature;
                    result.Serial_Number = repair.Serial_Number;
                    result.Client = repair.Client;
                    result.Date_Of_Receipt = repair.Date_Of_Receipt;
                    result.Departure_Date = repair.Departure_Date;
                    result.Inspector = repair.Inspector;
                    result.Warranty = repair.Warranty;
                    result.Identifie_Fault = repair.Identifie_Fault;
                    result.Work_Done = repair.Work_Done;
                    result.Repair_Category = repair.Repair_Category;
                    result.Engineer = repair.Engineer;
                    result.Repair_Date = repair.Repair_Date;
                    result.Status = repair.Status;
                    result.Note = repair.Note;
                    db.SaveChanges();
                }
            }
        }
        public static void RemoveFromBase(Repairs repair)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (repair != null)
                {
                    db.Repairs.Attach(repair);
                    db.Repairs.Remove(repair);
                    db.SaveChanges();
                }
            }
        }
        public static ObservableCollection<Repairs> SearchItem(string word)
        {

            using (var db = new PronetsDataBaseEntities())
            {
                Int32.TryParse(word, out int numericWord);
                repairs.Clear();
                var searchItems = from u in db.Repairs
                                  where u.Serial_Number.Contains(word) ||
                                  u.DocumentId == numericWord ||
                                  u.RepairId == numericWord
                                  select u;
                repairs = new ObservableCollection<Repairs>(searchItems);
            }
            return repairs;
        }
    }
}
