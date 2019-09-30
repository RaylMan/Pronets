using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pronets.EntityRequests.Repairs_f
{
    class RepairsRequest
    {
        private static Repairs repair;
        private static ObservableCollection<Repairs> repairs = new ObservableCollection<Repairs>();
        private static ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();

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
       
        public static ObservableCollection<v_Repairs> v_FillList()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                var result = from repair in db.v_Repairs
                             select repair;
                v_Repairs = new ObservableCollection<v_Repairs>(result);
            }
            return v_Repairs;
        }
        public static ObservableCollection<v_Repairs> FillListPronets()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                
                var result = from repair in db.v_Repairs
                             where repair.Client_Name == "Пронетс"
                             select repair;
                v_Repairs = new ObservableCollection<v_Repairs>(result);
            }
            return v_Repairs;
        }
        public static ObservableCollection<Repairs> GetPronetsRepairs()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (repairs != null)
                    repairs.Clear();

                var result = db.Repairs.Where(r => r.Client == 3).Include(r => r.Nomenclature1).ToList();
                repairs = new ObservableCollection<Repairs>(result);
            }
            return repairs;
        }
        public static ObservableCollection<v_Repairs> FillList(int DocumentId)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                var result = from repair in db.v_Repairs
                             where repair.DocumentId == DocumentId
                             select repair;
                v_Repairs = new ObservableCollection<v_Repairs>(result);
            }
            return v_Repairs;
        }
        public static ObservableCollection<v_Repairs> FillListClient(int clientId)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                var result = from repair in db.v_Repairs
                             where repair.Client_Id == clientId
                             select repair;
                v_Repairs = new ObservableCollection<v_Repairs>(result);
            }
            return v_Repairs;
        }

        #region Sorting by client
        public static ObservableCollection<v_Repairs> SortList(int clientId, string status = null)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                if (status != null)
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.Status == status
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                else
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }

            }
            return v_Repairs;
        }
        public static ObservableCollection<v_Repairs> SortList(int clientId, int documentId, string status = null)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                if (status != null)
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.Status == status && repair.DocumentId == documentId
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                else
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.DocumentId == documentId
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }

            }
            return v_Repairs;
        }
        public static ObservableCollection<v_Repairs> SortList(int clientId, string warranty, int documentId, string status = null)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                if (status != null)
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.Status == status && repair.DocumentId == documentId &&
                                 repair.Warranty == warranty
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                else
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.DocumentId == documentId &&
                                 repair.Warranty == warranty
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }

            }
            return v_Repairs;
        }
        public static ObservableCollection<v_Repairs> SortList(string warranty, int clientId, string status = null)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                if (status != null)
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.Status == status &&
                                 repair.Warranty == warranty
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                else
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.Warranty == warranty
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }

            }
            return v_Repairs;
        }
        public static ObservableCollection<v_Repairs> SortList(int clientId, string nomenclature, string status = null)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                if (status != null)
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.Status == status &&
                                 repair.Nomenclature == nomenclature
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                else
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.Nomenclature == nomenclature
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }

            }
            return v_Repairs;
        }
        public static ObservableCollection<v_Repairs> SortListDocNom(int clientId, int documentId, string nomenclature, string status = null)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                if (status != null)
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.Status == status &&
                                 repair.DocumentId == documentId &&
                                 repair.Nomenclature == nomenclature
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                else
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.DocumentId == documentId &&
                                 repair.Nomenclature == nomenclature
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }

            }
            return v_Repairs;
        }
        public static ObservableCollection<v_Repairs> SortListWarNom(int clientId, string warranty, string nomenclature, string status = null)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                if (status != null)
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.Status == status &&
                                 repair.Warranty == warranty &&
                                 repair.Nomenclature == nomenclature
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                else
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.Warranty == warranty &&
                                 repair.Nomenclature == nomenclature
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }

            }
            return v_Repairs;
        }
        public static ObservableCollection<v_Repairs> SortList(int clientId, int documentId, string nomenclature, string warranty, string status = null)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                if (status != null)
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.Status == status &&
                                 repair.DocumentId == documentId
                                 && repair.Nomenclature == nomenclature &&
                                 repair.Warranty == warranty
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                else
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId &&
                                 repair.DocumentId == documentId &&
                                 repair.Nomenclature == nomenclature &&
                                 repair.Warranty == warranty
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);

                }

            }
            return v_Repairs;
        }

        #endregion

        #region Sorting by User
        public static ObservableCollection<v_Repairs> SortUserList(int userId)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                var result = from repair in db.v_Repairs
                             where repair.EngineerId == userId
                             select repair;
                v_Repairs = new ObservableCollection<v_Repairs>(result);
            }
            return v_Repairs;
        }
        public static ObservableCollection<v_Repairs> SortUserList(int userId, DateTime firstDate, DateTime secondDate)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                var result = from repair in db.v_Repairs
                             where repair.EngineerId == userId &&
                             repair.Repair_Date >= firstDate && repair.Repair_Date <= secondDate
                             select repair;
                v_Repairs = new ObservableCollection<v_Repairs>(result);
            }
            return v_Repairs;
        }

        public static ObservableCollection<v_Repairs> SortUserList(int userId, DateTime firstDate, DateTime secondDate, string category)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_Repairs != null)
                    v_Repairs.Clear();
                var result = from repair in db.v_Repairs
                             where repair.EngineerId == userId &&
                             repair.Repair_Category == category &&
                             repair.Repair_Date >= firstDate && repair.Repair_Date <= secondDate
                             select repair;
                v_Repairs = new ObservableCollection<v_Repairs>(result);
            }
            return v_Repairs;
        }
        #endregion

        public static ObservableCollection<v_Repairs> GetCopy(int repairId, string serialNumber)
        {
            ObservableCollection<v_Repairs> v_RepairsCopy = new ObservableCollection<v_Repairs>();

            using (var db = new PronetsDataBaseEntities())
            {
                var result = from repair in db.v_Repairs
                             where repair.Serial_Number == serialNumber &&
                             repair.RepairId != repairId
                             select repair;
                v_RepairsCopy = new ObservableCollection<v_Repairs>(result);
            }
            return v_RepairsCopy;
        }

        public static Repairs GetRepair(int repairId)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                return db.Repairs.Where(r => r.RepairId == repairId).FirstOrDefault();
            }

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
        public static void EditItemStatusToSendToClient(int documentId, DateTime date)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                var result = db.Repairs.Where(r => r.DocumentId == documentId).ToList();
                result.ForEach(s => { s.Status = "Отправлен заказчику"; s.Departure_Date = date; });
                db.SaveChanges();
            }
        }
        public static void EditItemStatus(int repairId, DateTime date)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                var result = db.Repairs.Where(r => r.RepairId == repairId).ToList();
                result.ForEach(s => { s.Status = "Отправлен заказчику"; s.Departure_Date = date; });
                db.SaveChanges();

            }
        }
        public static void EditItemClient(int documentID, int clientId)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                var result = db.Repairs.Where(r => r.DocumentId == documentID).ToList();
                result.ForEach(c => c.Client = clientId);
                db.SaveChanges();

            }
        }
        public static void RemoveFromBase(Repairs repair, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
            {
                if (repair != null)
                {
                    try
                    {
                        db.Repairs.Attach(repair);
                        db.Repairs.Remove(repair);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                        isExeption = false;
                    }
                }
            }
        }
        public static void RemoveFromBaseById(int repairId, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
            {
                try
                {
                    var result = db.Repairs.Where(r => r.RepairId == repairId).FirstOrDefault();
                    repair = (Repairs)result;
                    db.Repairs.Attach(repair);
                    db.Repairs.Remove(repair);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                    isExeption = false;
                }
            }
        }
        public static void RemoveFromBase(int documentId, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
            {
                if (documentId != 0)
                {
                    try
                    {
                        //db.Repairs.AttachRange(db.Repairs.Where(r=>r.DocumentId == documentId));
                        db.Repairs.RemoveRange(db.Repairs.Where(r => r.DocumentId == documentId));
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Ошибка");
                        isExeption = false;
                    }
                }
            }
        }
        public static ObservableCollection<v_Repairs> SearchItem(string word, int clientId)
        {

            using (var db = new PronetsDataBaseEntities())
            {
                Int32.TryParse(word, out int numericWord);
                v_Repairs.Clear();
                var clientsItems = from u in db.v_Repairs
                                   where u.Client_Id == clientId
                                   select u;
                var searchItems = from u in clientsItems
                                  where
                                  u.Serial_Number.Contains(word) ||
                                  u.DocumentId == numericWord ||
                                  u.Engineer.Contains(word) ||
                                  u.Inspector.Contains(word) ||
                                  u.RepairId == numericWord ||
                                  u.Identifie_Fault.Contains(word) ||
                                  u.Claimed_Malfunction.Contains(word) ||
                                  u.Nomenclature.Contains(word) ||
                                  u.Status.Contains(word) ||
                                  u.Work_Done.Contains(word) ||
                                  u.Note.Contains(word) ||
                                  u.Repair_Category.Contains(word)
                                  select u;

                v_Repairs = new ObservableCollection<v_Repairs>(searchItems);
            }
            return v_Repairs;
        }
        public static ObservableCollection<v_Repairs> SearchItem(string word)
        {

            using (var db = new PronetsDataBaseEntities())
            {
                Int32.TryParse(word, out int numericWord);
                v_Repairs.Clear();
                var searchItems = from u in db.v_Repairs
                                  where
                                  u.Serial_Number.Contains(word) ||
                                  u.DocumentId == numericWord ||
                                  u.Engineer.Contains(word) ||
                                  u.Inspector.Contains(word) ||
                                  u.RepairId == numericWord ||
                                  u.Identifie_Fault.Contains(word) ||
                                  u.Claimed_Malfunction.Contains(word) ||
                                  u.Nomenclature.Contains(word) ||
                                  u.Status.Contains(word) ||
                                  u.Work_Done.Contains(word) ||
                                  u.Note.Contains(word) ||
                                  u.Repair_Category.Contains(word)
                                  select u;
                v_Repairs = new ObservableCollection<v_Repairs>(searchItems);
            }
            return v_Repairs;
        }
    }
}
