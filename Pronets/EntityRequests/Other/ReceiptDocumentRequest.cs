using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Pronets.EntityRequests.Other
{
    class ReceiptDocumentRequest
    {
        private static ObservableCollection<ReceiptDocument> receiptDocuments = new ObservableCollection<ReceiptDocument>();
        private static ObservableCollection<v_Receipt_Document> v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>();

        #region FillLists
        public static ObservableCollection<ReceiptDocument> FillList()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (receiptDocuments != null)
                    receiptDocuments.Clear();
                foreach (var item in db.ReceiptDocument)
                {
                    receiptDocuments.Add(new ReceiptDocument
                    {
                        DocumentId = item.DocumentId,
                        ClientId = item.ClientId,
                        InspectorId = item.InspectorId,
                        Date = item.Date,
                        Status = item.Status,
                        Note = item.Note,
                        Clients = item.Clients,
                        Users = item.Users
                    });
                }
            }
            return receiptDocuments;
        }

        public static ObservableCollection<ReceiptDocument> FillListClient(int clientId) // сортировка по клиенту
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (receiptDocuments != null)
                    receiptDocuments.Clear();
                var result = from document in db.ReceiptDocument
                             where document.ClientId == clientId
                             select document;
                receiptDocuments = new ObservableCollection<ReceiptDocument>(result);
            }
            return receiptDocuments;
        }

        public static ObservableCollection<ReceiptDocument> FillListWithStatus(string status) // сортировка по статусу ремонта
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (receiptDocuments != null)
                    receiptDocuments.Clear();
                var result = from document in db.ReceiptDocument
                             where document.Status == status
                             select document;
                receiptDocuments = new ObservableCollection<ReceiptDocument>(result);
            }
            return receiptDocuments;
        }

        public static ObservableCollection<v_Receipt_Document> v_FillList() // Представление(вместо Id - имена)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (v_ReceiptDocuments != null)
                    v_ReceiptDocuments.Clear();

                foreach (var item in db.v_Receipt_Document)
                {
                    v_ReceiptDocuments.Add(new v_Receipt_Document
                    {
                        Document_Id = item.Document_Id,
                        Client = item.Client,
                        Inspector = item.Inspector,
                        Date = item.Date,
                        Status = item.Status,
                        Note = item.Note,
                        Count = db.Repairs.Count(r => r.DocumentId == item.Document_Id)
                    });
                }
            }
            v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>(v_ReceiptDocuments.OrderByDescending(i => i.Document_Id));
            return v_ReceiptDocuments;
        }

        #endregion

        public static v_Receipt_Document GetDocument(int documentId)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                v_Receipt_Document document = db.v_Receipt_Document.Where(d => d.Document_Id == documentId).FirstOrDefault();
                return document;
            }
        }
        public static int GetDocumentID()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                return db.ReceiptDocument.Max(d => (int)d.DocumentId);
            }
        }

        public static void AddToBase(ReceiptDocument document)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (document != null)
                {
                    db.ReceiptDocument.Add(new ReceiptDocument
                    {
                        ClientId = document.ClientId,
                        InspectorId = document.InspectorId,
                        Date = document.Date,
                        Status = document.Status,
                        Note = document.Note,
                    });
                    db.SaveChanges();
                }
            }
        }
        public static void RemoveFromBase(ReceiptDocument document, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
            {
                if (document != null)
                {
                    try
                    {
                        db.ReceiptDocument.Attach(document);
                        db.ReceiptDocument.Remove(document);
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
        public static void RemoveFromBase(int documentId, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
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
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                        isExeption = false;
                    }
                }
            }
        }
        public static void EditItem(ReceiptDocument document)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                var result = db.ReceiptDocument.SingleOrDefault(d => d.DocumentId == document.DocumentId);
                if (result != null)
                {
                    result.Status = document.Status;
                    result.Note = document.Note;
                    db.SaveChanges();
                }
            }
        }
    }
}
