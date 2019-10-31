using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Pronets.Data;

namespace Pronets.EntityRequests
{
    public static class StatusesRequests
    {
       // private static ObservableCollection<Statuses> statuses = new ObservableCollection<Statuses>();

        /// <summary>
        /// <para>Возращает коллекцию Statuses</para>
        /// </summary>
        public static ObservableCollection<Statuses> FillList()
        {
            ObservableCollection<Statuses> statuses = null;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    statuses = new ObservableCollection<Statuses>(db.Statuses.Where(s => s.Status != "Готов").ToList());
                    
                    statuses.Add((Statuses)db.Statuses.Where(s => s.Status == "Готов").FirstOrDefault());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return statuses;
        }

        /// <summary>
        /// <para>Добавляет в базу экземпляр Statuses</para>
        /// </summary>
        public static void AddToBase(string status, out bool isExeption)
        {
            isExeption = true; //проверка на копию в базе
            if (status != null && status.Length > 0)
            {
                using (var db = ConnectionTools.GetConnection())
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
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Ошибка");
                    }
                }
            }
        }
        /// <summary>
        /// <para>Удаляет из базы экземпляр Statuses</para>
        /// </summary>
        public static void RemoveFromBase(Statuses status, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                if (status != null)
                {
                    try
                    {
                        db.Statuses.Attach(status);
                        db.Statuses.Remove(status);
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                        isExeption = false;
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
