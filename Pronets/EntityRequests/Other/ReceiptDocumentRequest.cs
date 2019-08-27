using Pronets.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Pronets.EntityRequests.Other
{
    class ReceiptDocumentRequest
    {
        private static ObservableCollection<ReceiptDocument> receiptDocuments = new ObservableCollection<ReceiptDocument>();

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
        public static void RemoveFromBase(ReceiptDocument document)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (document != null)
                {
                    db.ReceiptDocument.Attach(document);
                    db.ReceiptDocument.Remove(document);
                    db.SaveChanges();
                }
            }
        }
    }
}
