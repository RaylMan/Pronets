using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pronets.EntityRequests.Repairs_f
{
    class RepairsRequest
    {
        private static Repairs repair;
        private static v_Repairs v_Repair;
        /// <summary>
        /// <para>Возращает коллекцию Repairs</para>
        /// </summary>
        public static ObservableCollection<Repairs> FillList()
        {
            ObservableCollection<Repairs> repairs = new ObservableCollection<Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    repairs = new ObservableCollection<Repairs>(db.Repairs.ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return repairs;
        }
        public static ObservableCollection<Repairs> FillListDocument(int documentId)
        {
            ObservableCollection<Repairs> repairs = new ObservableCollection<Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    repairs = new ObservableCollection<Repairs>(db.Repairs.Where(d => d.DocumentId == documentId).ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return repairs;
        }

        /// <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL)</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> v_FillList()
        {
            ObservableCollection<v_Repairs> v_RepairsAll = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    v_RepairsAll = new ObservableCollection<v_Repairs>(db.v_Repairs.ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_RepairsAll;
        }
        /// <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL)</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> v_FillListFromDate(DateTime firstDate, DateTime secondDate)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Repair_Date >= firstDate && repair.Repair_Date <= secondDate
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }
        /// <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по серийному номеру</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> v_FillList(string SerialNumber)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Serial_Number == SerialNumber
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        /// <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по номеру документа</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> v_FillList(int documentId)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from repair in db.v_Repairs
                                 where repair.DocumentId == documentId
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }
        /// <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по серийному Статусу</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> FillListPronets(string status)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Name == "Пронетс" &&
                                       repair.Status == status
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        /// <summary>
        /// <para>Возращает коллекцию Repairs клиента "Пронетс"</para>
        /// </summary>
        public static ObservableCollection<Repairs> GetPronetsRepairs()
        {
            ObservableCollection<Repairs> repairs = new ObservableCollection<Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    repairs = new ObservableCollection<Repairs>(db.Repairs.Where(r => r.Client == 3).Include(r => r.Nomenclature1).ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return repairs;
        }
        /// <summary>
        /// <para>Возращает коллекцию Repairs клиента "Пронетс" по статусу</para>
        /// </summary>
        public static ObservableCollection<Repairs> GetPronetsRepairs(string status, int clientId)
        {
            ObservableCollection<Repairs> repairs = new ObservableCollection<Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.Repairs.Where(r => r.Client == clientId && r.Status == status).Include(r => r.Nomenclature1).ToList();
                    repairs = new ObservableCollection<Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return repairs;
        }

        /// <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по номеру документа</para>
        /// </summary>
        /// 
        public static ObservableCollection<v_Repairs> FillList(int DocumentId)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from repair in db.v_Repairs
                                 where repair.DocumentId == DocumentId
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        /// <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по номеру документа</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> FillReportList(int DocumentId)
        {
            ObservableCollection<v_Repairs> repairsTable = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from repair in db.v_Repairs
                                 where repair.DocumentId == DocumentId
                                 select repair;
                    repairsTable = new ObservableCollection<v_Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return repairsTable;
        }

        /// <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по Id клиента</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> FillListClient(int clientId)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Client_Id == clientId
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        #region Sorting by client

        // <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по Id клиента и статусу</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SortList(int clientId, string status = null)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        // <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по Id клиента, номеру документа и статусу</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SortList(int clientId, int documentId, string status = null)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        // <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по Id клиента, гарантии, номеру документа и статусу</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SortList(int clientId, string warranty, int documentId, string status = null)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        // <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по гарантии, Id клиента и статусу</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SortList(string warranty, int clientId, string status = null)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        // <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по Id клиента, номенклатуре и статусу</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SortList(int clientId, string nomenclature, string status = null)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        // <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по Id клиента, номеру документа, номенклатуре и статусу</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SortListDocNom(int clientId, int documentId, string nomenclature, string status = null)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        // <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по Id клиента, гарантии, номенклатуре и статусу</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SortListWarNom(int clientId, string warranty, string nomenclature, string status = null)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }
        // <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по Id клиента, номеру документа, номенклатуре, гарантии и статусу</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SortList(int clientId, int documentId, string nomenclature, string warranty, string status = null)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        #endregion

        #region Sorting by User

        // <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по Id пользователя</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SortUserList(int userId)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from repair in db.v_Repairs
                                 where repair.EngineerId == userId
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        // <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по Id пользователя и промежутку дат</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SortUserList(int userId, DateTime firstDate, DateTime secondDate)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from repair in db.v_Repairs
                                 where repair.EngineerId == userId &&
                                 repair.Repair_Date >= firstDate && repair.Repair_Date <= secondDate
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        // <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) по Id пользователя, промежутку дат и категории ремонта</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SortUserList(int userId, DateTime firstDate, DateTime secondDate, string category)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from repair in db.v_Repairs
                                 where repair.EngineerId == userId &&
                                 repair.Repair_Category == category &&
                                 repair.Repair_Date >= firstDate && repair.Repair_Date <= secondDate
                                 select repair;
                    v_Repairs = new ObservableCollection<v_Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }
        /// <summary>
        /// Возвращает коллекцию Repairs сортированную по клиенту, статусу и датам
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="status"></param>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns></returns>
        public static ObservableCollection<Repairs> CalculateRepairs(int clientId, string status, DateTime firstDate, DateTime secondDate)
        {
            ObservableCollection<Repairs> repairs = new ObservableCollection<Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from repair in db.Repairs
                                 where repair.Client == clientId &&
                                 repair.Status == status &&
                                 repair.Repair_Date >= firstDate && repair.Repair_Date <= secondDate
                                 select repair;
                    repairs = new ObservableCollection<Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return repairs;
        }
        #endregion

        // <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL) с одинаковыми серийными номерами номер ремонта указаный в сигнатуре - не включен в коллекцию</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> GetCopy(int repairId, string serialNumber)
        {
            ObservableCollection<v_Repairs> v_RepairsCopy = new ObservableCollection<v_Repairs>();

            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from repair in db.v_Repairs
                                 where repair.Serial_Number == serialNumber &&
                                 repair.RepairId != repairId
                                 select repair;
                    v_RepairsCopy = new ObservableCollection<v_Repairs>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_RepairsCopy;
        }

        /// <summary>
        /// <para>Возращает экземпляр Repairs по Id</para>
        /// </summary>
        public static Repairs GetRepair(int repairId)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    repair = db.Repairs.Where(r => r.RepairId == repairId).FirstOrDefault();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return repair;
        }

        /// <summary>
        /// <para>Возращает экземпляр v_Repairs(Представление SQL) по Id</para>
        /// </summary>
        public static v_Repairs v_GetRepair(int repairId)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    v_Repair = db.v_Repairs.Where(r => r.RepairId == repairId).FirstOrDefault();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repair;
        }

        /// <summary>
        /// <para>Добавляет в базу экземпляр Repairs</para>
        /// </summary>
        public static void AddToBase(Repairs repair)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (repair != null)
                    {
                        db.Repairs.Add(repair);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        /// <summary>
        /// <para>Добавляет в базу коллекцию Repairs</para>
        /// </summary>
        public static void AddToBase(ObservableCollection<Repairs> repairs)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (repairs != null)
                {
                    try
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
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Console.WriteLine("Property: {0} Error: {1}",
                                                        validationError.PropertyName,
                                                        validationError.ErrorMessage);
                            }
                        }
                        MessageBox.Show(dbEx.Message, "Ошибка");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Ошибка");
                    }

                }
            }
        }

        /// <summary>
        /// <para>Изменяет в базе экземляр Repairs</para>
        /// </summary>
        public static void EditItem(Repairs repair)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.Repairs.SingleOrDefault(r => r.RepairId == repair.RepairId);
                    if (result != null)
                    {
                        result.Nomenclature = repair.Nomenclature;
                        result.Serial_Number = repair.Serial_Number;
                        result.Client = repair.Client;
                        result.Claimed_Malfunction = repair.Claimed_Malfunction;
                        result.Date_Of_Receipt = repair.Date_Of_Receipt;
                        result.Recipient = repair.Recipient;
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        /// <summary>
        /// <para>Изменяет в базе у экземляра Repairs статус и дату отправки, по номеру документа</para>
        /// </summary>
        public static void EditItemStatusToSendToClient(int documentId, DateTime date, string recipient)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.Repairs.Where(r => r.DocumentId == documentId).ToList();
                    result.ForEach(s => { s.Status = "Отправлено заказчику"; s.Departure_Date = date; s.Recipient = recipient; });
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        /// <summary>
        /// <para>Изменяет в базе у экземляра Repairs статус и дату отправки, по номеру ремонта</para>
        /// </summary>
        public static void EditItemStatus(int repairId, DateTime date, string recipient)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.Repairs.Where(r => r.RepairId == repairId).ToList();
                    result.ForEach(s => { s.Status = "Отправлено заказчику"; s.Departure_Date = date; s.Recipient = recipient; });
                    db.SaveChanges();

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        /// <summary>
        /// <para>Изменяет в базе у экземляра Repairs получателя и дату отправки, по номеру ремонта</para>
        /// </summary>
        public static void SetRepairRecipient(int repairId, string clientName)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.Repairs.Where(r => r.RepairId == repairId).FirstOrDefault();
                    if (result != null && clientName != null)
                    {
                        result.Recipient = clientName;
                        result.Departure_Date = DateTime.Now;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        /// <summary>
        /// <para>Изменяет в базе у экземляра Repairs клиента, по номеру документа</para>
        /// </summary>
        public static void EditItemClient(int documentID, int clientId)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.Repairs.Where(r => r.DocumentId == documentID).ToList();
                    result.ForEach(c => c.Client = clientId);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        /// <summary>
        /// <para>Удаляет из базы экземляр Repairs</para>
        /// </summary>
        public static void RemoveFromBase(Repairs repair, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                if (repair != null)
                {
                    try
                    {
                        db.Repairs.Attach(repair);
                        db.Repairs.Remove(repair);
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                        isExeption = false;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Ошибка");
                        isExeption = false;
                    }
                }
            }
        }

        /// <summary>
        /// <para>Удаляет из базы экземляр Repairs по номеру ремонта</para>
        /// </summary>
        public static void RemoveFromBaseById(int repairId, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.Repairs.Where(r => r.RepairId == repairId).FirstOrDefault();
                    repair = (Repairs)result;
                    db.Repairs.Attach(repair);
                    db.Repairs.Remove(repair);
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                    isExeption = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                    isExeption = false;
                }
            }
        }

        /// <summary>
        /// <para>Удаляет из базы экземляр Repairs по номеру документа</para>
        /// </summary>
        public static void RemoveFromBase(int documentId, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
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

        /// <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL)</para>
        /// <para>Поиск по всем полям, по Id клиента</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SearchItem(string word, int clientId)
        {

            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    Int32.TryParse(word, out int numericWord);
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }

        /// <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL), поиск по всем полям</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SearchItemAllColumns(string word)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    Int32.TryParse(word, out int numericWord);
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
                    v_Repairs = new ObservableCollection<v_Repairs>(searchItems.OrderByDescending(r => r.Date_Of_Receipt));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }
        /// <summary>
        /// <para>Возращает коллекцию v_Repairs(Представление SQL), поиск по серийному нрмепу, номеру документа или номеру ремонта</para>
        /// </summary>
        public static ObservableCollection<v_Repairs> SearchItem(string word)
        {
            ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    Int32.TryParse(word, out int numericWord);
                    var searchItems = from u in db.v_Repairs
                                      where
                                      u.Serial_Number.Contains(word) ||
                                      u.DocumentId == numericWord ||
                                      u.RepairId == numericWord
                                      select u;
                    v_Repairs = new ObservableCollection<v_Repairs>(searchItems.OrderByDescending(r => r.Date_Of_Receipt));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return v_Repairs;
        }
    }
}
