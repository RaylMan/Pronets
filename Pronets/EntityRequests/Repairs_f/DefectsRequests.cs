using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pronets.EntityRequests.Repairs_f
{
    public static class DefectsRequests
    {
        private static ObservableCollection<Defects> defects = new ObservableCollection<Defects>();
        public static ObservableCollection<Defects> FillList()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (defects != null)
                    defects.Clear();
                foreach (var item in db.Defects)
                {
                    defects.Add(new Defects
                    {
                        Id = item.Id,
                        Defect = item.Defect,
                        Work = item.Work
                    });
                }
            }
            return defects;
        }

        public static void AddToBase(Defects defect, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
            {
                try
                {
                    db.Defects.Add(new Defects
                    {
                        Id = defect.Id,
                        Defect = defect.Defect,
                        Work = defect.Work
                    });
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Элемент уже существует в базе!", "Ошибка");
                    isExeption = false;
                }
            }
        }
        public static void RemoveFromBase(Defects defect, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
            {
                if (defect != null)
                {
                    try
                    {
                        db.Defects.Attach(defect);
                        db.Defects.Remove(defect);
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
    }
}
