﻿using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pronets.EntityRequests.Other
{
    public static class ReceiptOfPartsRequest
    {
        private static ObservableCollection<ReceiptOfParts> receiptOfParts = new ObservableCollection<ReceiptOfParts>();

        public static ObservableCollection<ReceiptOfParts> FillList()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (receiptOfParts != null)
                    receiptOfParts.Clear();
                foreach (var item in db.ReceiptOfParts)
                {
                    receiptOfParts.Add(new ReceiptOfParts
                    {
                        Id = item.Id,
                        Order_Date = item.Order_Date,
                        Date_Arrival = item.Date_Arrival,
                        Status = item.Status
                    });
                }
            }
            receiptOfParts = new ObservableCollection<ReceiptOfParts>(receiptOfParts.OrderByDescending(i => i.Id));
            return receiptOfParts;
        }
        public static void AddToBase(ReceiptOfParts receiptOfParts)
        {
           
            using (var db = new PronetsDataBaseEntities())
            {
                try
                {
                    db.ReceiptOfParts.Add(new ReceiptOfParts
                    {
                        Order_Date = receiptOfParts.Order_Date,
                        Status = receiptOfParts.Status
                    });
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
        }
        public static void RemoveFromBase(ReceiptOfParts receiptOfParts, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
            {
                if (receiptOfParts != null)
                {
                    try
                    {
                        db.ReceiptOfParts.Attach(receiptOfParts);
                        db.ReceiptOfParts.Remove(receiptOfParts);
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                        isExeption = false;
                    }
                }
            }
        }
        public static void EditItem(ReceiptOfParts document)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                var result = db.ReceiptOfParts.SingleOrDefault(d => d.Id == document.Id);
                if (result != null)
                {
                    result.Order_Date = document.Order_Date;
                    result.Date_Arrival = document.Date_Arrival;
                    result.Status = document.Status;
                    db.SaveChanges();
                }
            }
        }
    }
}
