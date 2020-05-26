using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.EntityRequests.DefectiveStatements_f
{
    public static class DefectiveStatementsRequests
    {
        /// <summary>
        /// <para>Возращает коллекцию Nomenclature_Types</para>
        /// </summary>
        public static ObservableCollection<v_DefectiveStatements> FillList()
        {
            ObservableCollection<v_DefectiveStatements> defectiveStatements = new ObservableCollection<v_DefectiveStatements>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    foreach (var item in db.v_DefectiveStatements)
                    {
                        defectiveStatements.Add(new v_DefectiveStatements
                        {
                            Id = item.Id,
                            ClientName = item.ClientName,
                            LastName = item.LastName,
                            DocumentId = item.DocumentId,
                            ClientId = item.ClientId,
                            Date = item.Date,
                            Count = db.DefectiveStatementRepairs.Count(s => s.DefectiveStatementId == item.Id)
                        });
                    }
                    defectiveStatements = new ObservableCollection<v_DefectiveStatements>(defectiveStatements.OrderByDescending(i => i.Id));
                }
                catch
                {
                    throw;
                }
            }
            return defectiveStatements;
        }
        public static ObservableCollection<v_DefectiveStatements> FillListFromDocument(int id)
        {
            ObservableCollection<v_DefectiveStatements> defectiveStatements = new ObservableCollection<v_DefectiveStatements>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    foreach (var item in db.v_DefectiveStatements.Where(d => d.DocumentId == id))
                    {
                        defectiveStatements.Add(new v_DefectiveStatements
                        {
                            Id = item.Id,
                            ClientName = item.ClientName,
                            LastName = item.LastName,
                            DocumentId = item.DocumentId,
                            ClientId = item.ClientId,
                            Date = item.Date,
                            Count = db.DefectiveStatementRepairs.Count(s => s.DefectiveStatementId == item.Id)
                        });
                    }
                    defectiveStatements = new ObservableCollection<v_DefectiveStatements>(defectiveStatements.OrderByDescending(i => i.Id));
                }
                catch
                {
                    throw;
                }
            }
            return defectiveStatements;
        }
        public static ObservableCollection<DefectiveStatementRepairs> GetSatermentsRepairs(int id)
        {
            ObservableCollection<DefectiveStatementRepairs> defectiveStatements = new ObservableCollection<DefectiveStatementRepairs>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    defectiveStatements = new ObservableCollection<DefectiveStatementRepairs>(db.DefectiveStatementRepairs.Where(s => s.DefectiveStatementId == id).Include(r => r.Repairs).ToList());
                }
                catch
                {
                    throw;
                }
            }
            return defectiveStatements;
        }
        public static void AddStatements(DefectiveStatements statement)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    db.DefectiveStatements.Add(statement);
                    db.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }
        public static void AddStatementRepairs(DefectiveStatementRepairs statementRepair)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    db.DefectiveStatementRepairs.Add(statementRepair);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public static List<int> GetRepairsId(int statementId)
        {
            List<int> ids = new List<int>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.DefectiveStatementRepairs.Where(r => r.DefectiveStatementId == statementId);
                    if (result != null)
                        foreach (var repair in result)
                        {
                            ids.Add(repair.RepairId);
                        }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return ids;
        }
        public static void RemoveStatements(v_DefectiveStatements statement)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (statement != null)
                {
                    try
                    {
                        var deletingStatement = db.DefectiveStatements.Find(statement.Id);
                        if (deletingStatement == null) return;

                        var result = db.DefectiveStatementRepairs.Where(r => r.DefectiveStatementId == statement.Id).ToList();
                        if (result != null)
                        {
                            db.DefectiveStatementRepairs.RemoveRange(result);
                        }
                        db.DefectiveStatements.Attach(deletingStatement);
                        db.DefectiveStatements.Remove(deletingStatement);
                        db.SaveChanges();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }
        public static void RemoveStatementRepairs(int id)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.DefectiveStatementRepairs.Where(r => r.DefectiveStatementId == id).ToList();
                    db.DefectiveStatementRepairs.RemoveRange(result);
                    db.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
