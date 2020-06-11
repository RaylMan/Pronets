using Pronets.Data;
using Pronets.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Pronets.EntityRequests.Other
{
    class ReceiptDocumentRequest
    {
        #region FillLists
        /// <summary>
        /// <para>Возращает коллекцию ReceiptDocument</para>
        /// </summary>
        public static ObservableCollection<ReceiptDocument> FillList()
        {
            ObservableCollection<ReceiptDocument> receiptDocuments = new ObservableCollection<ReceiptDocument>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    receiptDocuments = new ObservableCollection<ReceiptDocument>(db.ReceiptDocument.ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                }
            }
            return receiptDocuments;
        }

        /// <summary>
        /// <para>Возращает коллекцию ReceiptDocument по id Клиента</para>
        /// </summary>
        public static ObservableCollection<ReceiptDocument> FillListClient(int clientId)
        {
            ObservableCollection<ReceiptDocument> receiptDocuments = new ObservableCollection<ReceiptDocument>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from document in db.ReceiptDocument
                                 where document.ClientId == clientId
                                 select document;
                    receiptDocuments = new ObservableCollection<ReceiptDocument>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                }
            }
            return receiptDocuments;
        }

        /// <summary>
        /// <para>Возращает коллекцию ReceiptDocument по статусу</para>
        /// </summary>
        public static ObservableCollection<ReceiptDocument> FillListWithStatus(string status)
        {
            ObservableCollection<ReceiptDocument> receiptDocuments = new ObservableCollection<ReceiptDocument>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = from document in db.ReceiptDocument
                                 where document.Status == status
                                 select document;
                    receiptDocuments = new ObservableCollection<ReceiptDocument>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                }
            }
            return receiptDocuments;
        }

        /// <summary>
        /// <para>Возращает коллекцию v_ReceiptDocument(Представление SQL)</para>
        /// </summary>
        public static ObservableCollection<v_Receipt_Document> v_FillList() // Представление(вместо Id - имена)
        {
            ObservableCollection<v_Receipt_Document> v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    foreach (var item in db.v_Receipt_Document)
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
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                }
            }
            return v_ReceiptDocuments;
        }
        /// <summary>
        /// <para>Возращает коллекцию v_ReceiptDocument(Представление SQL) по статусу клиента пронетс</para>
        /// </summary>
        public static ObservableCollection<v_Receipt_Document> v_FillListPronets(string status = null) // Представление(вместо Id - имена)
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
                            if(item.Client == "Пронетс")
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
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                }
            }
            return v_ReceiptDocuments;
        }

        /// <summary>
        /// <para>Возращает коллекцию v_ReceiptDocument(Представление SQL) имени клиента и статусу(кроме пронетс)</para>
        /// </summary>
        public static ObservableCollection<v_Receipt_Document> v_FillList(string status = null, string clientName = null) // Представление(вместо Id - имена)
        {
            ObservableCollection<v_Receipt_Document> v_ReceiptDocuments = new ObservableCollection<v_Receipt_Document>();
            using (var db = ConnectionTools.GetConnection())
            {
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
                        //var result = from document in db.v_Receipt_Document
                        //             where document.Client == clientName
                        //             select document;
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
                            if(item.Client != "Пронетс")
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
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                }
            }
            return v_ReceiptDocuments;
        }

        #endregion

        /// <summary>
        /// <para>Возращает экземпляр v_ReceiptDocument(Представление SQL)</para>
        /// </summary>
        public static v_Receipt_Document GetDocument(int documentId)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                v_Receipt_Document document = db.v_Receipt_Document.Where(d => d.Document_Id == documentId).FirstOrDefault();
                return document;
            }
        }

        /// <summary>
        /// <para>Возращает номер последней записи в базе ReceiptDocument</para>
        /// </summary>
        public static int GetDocumentID()
        {
            int lastId = 0;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    lastId = db.ReceiptDocument.Max(d => (int)d.DocumentId);
                }
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                }
            }
            return lastId;
        }

        /// <summary>
        /// <para>Добавляет в базу экземпляр ReceiptDocument</para>
        /// </summary>
        public static void AddToBase(ReceiptDocument document)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (document != null)
                {
                    try
                    {
                        db.ReceiptDocument.Add(new ReceiptDocument
                        {
                            ClientId = document.ClientId,
                            InspectorId = document.InspectorId,
                            Date = document.Date,
                            Status = document.Status,
                            DepartureDate = document.DepartureDate,
                            Note = document.Note,
                        });
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(ExceptionMessanger.Message(e));
                    }
                }
            }
        }

        /// <summary>
        /// <para>Удаляет из базы экземпляр ReceiptDocument</para>
        /// </summary>
        public static void RemoveFromBase(ReceiptDocument document, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                if (document != null)
                {
                    try
                    {
                        db.ReceiptDocument.Attach(document);
                        db.ReceiptDocument.Remove(document);
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                        isExeption = false;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(ExceptionMessanger.Message(e));
                        isExeption = false;
                    }
                }
            }
        }

        /// <summary>
        /// <para>Удаляет из базы элемент ReceiptDocument по documentId</para>
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
                        var result = db.ReceiptDocument.Where(d => d.DocumentId == documentId).FirstOrDefault();
                        ReceiptDocument removableDocument = (ReceiptDocument)result;
                        db.ReceiptDocument.Attach(removableDocument);
                        db.ReceiptDocument.Remove(removableDocument);
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                        isExeption = false;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(ExceptionMessanger.Message(e));
                        isExeption = false;
                    }
                }
            }
        }

        /// <summary>
        /// <para>Изменяет в базе экземпляр ReceiptDocument</para>
        /// </summary>
        public static void EditItem(ReceiptDocument document)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.ReceiptDocument.SingleOrDefault(d => d.DocumentId == document.DocumentId);
                    if (result != null)
                    {
                        result.ClientId = document.ClientId;
                        result.Status = document.Status;
                        result.DepartureDate = document.DepartureDate;
                        result.Note = document.Note;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                }
            }
        }

        /// <summary>
        /// <para>Устанавливает статус ReceiptDocument по documentId</para>
        /// </summary>
        public static void SetStatus(int documentId, string status)
        {
            using (var db = ConnectionTools.GetConnection())
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
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                }
            }
        }
    }
}
