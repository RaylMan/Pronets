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

                    }) ;
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
    }
}
