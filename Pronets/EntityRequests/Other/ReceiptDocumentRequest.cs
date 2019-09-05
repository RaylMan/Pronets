﻿using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Pronets.EntityRequests.Other
{
    class ReceiptDocumentRequest
    {
        private static ObservableCollection<ReceiptDocument> receiptDocuments = new ObservableCollection<ReceiptDocument>();
        private static ObservableCollection<v_Receipt_Document> v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>();

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
        public static ObservableCollection<v_Receipt_Document> v_FillList()
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
