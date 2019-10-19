using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pronets.EntityRequests.Other
{
    public static class PartsOrderRequest
    {
        private static ObservableCollection<PartsOrder> partsOrder = new ObservableCollection<PartsOrder>();

        /// <summary>
        /// <para>Возращает коллекцию PartsOrder</para>
        /// </summary>
        public static ObservableCollection<PartsOrder> FillList()
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (partsOrder != null)
                        partsOrder.Clear();
                    foreach (var item in db.PartsOrder)
                    {
                        partsOrder.Add(new PartsOrder
                        {
                            OrderId = item.OrderId,
                            DocumentId = item.DocumentId,
                            PartName = item.PartName,
                            Count = item.Count
                        });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }

            }
            return partsOrder;
        }

        /// <summary>
        /// <para>Возращает коллекцию PartsOrder по номеру документа</para>
        /// </summary>
        public static ObservableCollection<PartsOrder> FillList(int documentId)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (partsOrder != null)
                        partsOrder.Clear();
                    var result = db.PartsOrder.Where(o => o.DocumentId == documentId).ToList();
                    partsOrder = new ObservableCollection<PartsOrder>(result);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return partsOrder;
        }

        /// <summary>
        /// <para>добавляет в базу экземпляр PartsOrder</para>
        /// </summary>
        public static void AddToBase(PartsOrder partOrder)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    db.PartsOrder.Add(new PartsOrder
                    {
                        DocumentId = partOrder.DocumentId,
                        PartName = partOrder.PartName,
                        Count = partOrder.Count
                    });
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
        }

        /// <summary>
        /// <para>Удаляет из базы экземпляр PartsOrder</para>
        /// </summary>
        public static void RemoveFromBase(PartsOrder partOrder, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                if (partOrder != null)
                {
                    try
                    {
                        db.PartsOrder.Attach(partOrder);
                        db.PartsOrder.Remove(partOrder);
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                        isExeption = false;
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message, "Ошибка");
                        isExeption = false;
                    }
                }
            }
        }

        /// <summary>
        /// <para>Удаляет из базы элементы PartsOrder по номеру документа</para>
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
                        db.PartsOrder.RemoveRange(db.PartsOrder.Where(r => r.DocumentId == documentId));
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
    }
}
