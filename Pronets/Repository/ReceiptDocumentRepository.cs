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
    public class ReceiptDocumentRepository : BaseRepository
    {
        public ObservableCollection<v_Receipt_Document> GetDocuments(string status = null, string clientName = null)
        {
            ObservableCollection<v_Receipt_Document> v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>();
            try
            {
                if (status != null && clientName != null)
                {
                    foreach (var item in db.v_Receipt_Document)
                    {
                        if (item.Client == clientName && item.Client != "Пронетс" && item.Status == status)
                            v_ReceiptDocuments.Add(new v_Receipt_Document
                            {
                                Document_Id = item.Document_Id,
                                Client = item.Client,
                                Inspector = item.Inspector,
                                Date = item.Date,
                                DepartureDate = item.DepartureDate,
                                Status = item.Status,
                                Note = item.Note,
                                Count = db.Repairs.Count(r => r.DocumentId == item.Document_Id)
                            });
                    }
                    v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>(v_ReceiptDocuments.OrderByDescending(i => i.Document_Id));
                }
                else if (status == null && clientName != null)
                {
                    foreach (var item in db.v_Receipt_Document)
                    {
                        if (item.Client == clientName && item.Client != "Пронетс")
                            v_ReceiptDocuments.Add(new v_Receipt_Document
                            {
                                Document_Id = item.Document_Id,
                                Client = item.Client,
                                Inspector = item.Inspector,
                                Date = item.Date,
                                DepartureDate = item.DepartureDate,
                                Status = item.Status,
                                Note = item.Note,
                                Count = db.Repairs.Count(r => r.DocumentId == item.Document_Id)
                            });
                    }
                    v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>(v_ReceiptDocuments.OrderByDescending(i => i.Document_Id));
                }
                else if (status != null && clientName == null)
                {
                    //var result = from document in db.v_Receipt_Document
                    //             where document.Status == status
                    //             select document;
                    foreach (var item in db.v_Receipt_Document)
                    {
                        if (item.Status == status && item.Client != "Пронетс")
                            v_ReceiptDocuments.Add(new v_Receipt_Document
                            {
                                Document_Id = item.Document_Id,
                                Client = item.Client,
                                Inspector = item.Inspector,
                                Date = item.Date,
                                DepartureDate = item.DepartureDate,
                                Status = item.Status,
                                Note = item.Note,
                                Count = db.Repairs.Count(r => r.DocumentId == item.Document_Id)
                            });
                    }
                    v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>(v_ReceiptDocuments.OrderByDescending(i => i.Document_Id));
                }
                else
                {
                    //var result = db.v_Receipt_Document.ToList();
                    foreach (var item in db.v_Receipt_Document)
                    {
                        if (item.Client != "Пронетс")
                        {
                            v_ReceiptDocuments.Add(new v_Receipt_Document
                            {
                                Document_Id = item.Document_Id,
                                Client = item.Client,
                                Inspector = item.Inspector,
                                Date = item.Date,
                                DepartureDate = item.DepartureDate,
                                Status = item.Status,
                                Note = item.Note,
                                Count = db.Repairs.Count(r => r.DocumentId == item.Document_Id)
                            });
                        }
                    }
                    v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>(v_ReceiptDocuments.OrderByDescending(i => i.Document_Id));
                }
            }
            catch
            {
                throw;
            }
            return v_ReceiptDocuments;
        }

        public ObservableCollection<v_Receipt_Document> GetDocumentsPronets(string status = null) // Представление(вместо Id - имена)
        {
            ObservableCollection<v_Receipt_Document> v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (status != null)
                    {
                        foreach (var item in db.v_Receipt_Document)
                        {
                            if (item.Client == "Пронетс" && item.Status == status)
                                v_ReceiptDocuments.Add(new v_Receipt_Document
                                {
                                    Document_Id = item.Document_Id,
                                    Client = item.Client,
                                    Inspector = item.Inspector,
                                    Date = item.Date,
                                    DepartureDate = item.DepartureDate,
                                    Status = item.Status,
                                    Note = item.Note,
                                    Count = db.Repairs.Count(r => r.DocumentId == item.Document_Id)
                                });
                        }
                        v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>(v_ReceiptDocuments.OrderByDescending(i => i.Document_Id));
                    }
                    else
                    {
                        foreach (var item in db.v_Receipt_Document)
                        {
                            if (item.Client == "Пронетс")
                            {
                                v_ReceiptDocuments.Add(new v_Receipt_Document
                                {
                                    Document_Id = item.Document_Id,
                                    Client = item.Client,
                                    Inspector = item.Inspector,
                                    Date = item.Date,
                                    DepartureDate = item.DepartureDate,
                                    Status = item.Status,
                                    Note = item.Note,
                                    Count = db.Repairs.Count(r => r.DocumentId == item.Document_Id)
                                });
                            }
                        }
                        v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>(v_ReceiptDocuments.OrderByDescending(i => i.Document_Id));
                    }
                }
                catch
                {
                    throw;
                }
            }
            return v_ReceiptDocuments;
        }

        public void RemoveDocument(int documentId)
        {
            if (documentId != 0)
            {
                try
                {
                    var result = db.ReceiptDocument.Where(d => d.DocumentId == documentId).FirstOrDefault();
                    ReceiptDocument removableDocument = (ReceiptDocument)result;
                    db.ReceiptDocument.Attach(removableDocument);
                    db.ReceiptDocument.Remove(removableDocument);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
        public void RemoveRepairs(int documentId)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (documentId != 0)
                {
                    try
                    {
                        db.Repairs.RemoveRange(db.Repairs.Where(r => r.DocumentId == documentId));
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
}
