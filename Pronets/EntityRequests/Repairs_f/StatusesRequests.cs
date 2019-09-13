using System;
using System.Collections.ObjectModel;
using System.Windows;
using Pronets.Data;

namespace Pronets.EntityRequests
{
    public static class StatusesRequets
    {
        private static ObservableCollection<Statuses> statuses = new ObservableCollection<Statuses>();
        public static ObservableCollection<Statuses> FillList()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (statuses != null)
                    statuses.Clear();
                foreach (var item in db.Statuses)
                {
                    statuses.Add(new Statuses
                    {
                        Status = item.Status
                    });
                }
            }
            return statuses;
        }
        public static void AddToBase(string status, out bool isExeption)
        {
            isExeption = true; //проверка на копию в базе
            if (status != null && status.Length > 0)
            {
                using (var db = new PronetsDataBaseEntities())
                {
                    try
                    {
                        db.Statuses.Add(new Statuses { Status = status });
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Элемент уже существует в базе!", "Ошибка");
                        isExeption = false;
                    }
                }
            }
        }
        public static void RemoveFromBase(Statuses status, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
            {
                if (status != null)
                {
                    try
                    {
                        db.Statuses.Attach(status);
                        db.Statuses.Remove(status);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!","Ошибка");
                        isExeption = false;
                    }
                }
            }
        }
    }
}
