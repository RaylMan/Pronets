using System.Collections.ObjectModel;
using System.Windows;
using Pronets.Data;

namespace Pronets.EntityRequests
{
    public static class StatusesRequets
    {
        private static ObservableCollection<Statuses> statuses = new ObservableCollection<Statuses>();
        public static ObservableCollection<Statuses> FillLst()
        {
            using (var db = new PronetsDataBaseEntities())
            {
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
        public static void RemoveFromBase(Statuses status)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (status != null)
                {
                    db.Statuses.Attach(status);
                    db.Statuses.Remove(status);
                    db.SaveChanges();

                }
            }
        }
    }
}
