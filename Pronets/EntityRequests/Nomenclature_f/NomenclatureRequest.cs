using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Pronets.EntityRequests.Nomenclature_f
{
    class NomenclatureRequest
    {
        private static ObservableCollection<Nomenclature> nomenclatures = new ObservableCollection<Nomenclature>();
        public static ObservableCollection<Nomenclature> FillList()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (nomenclatures != null)
                    nomenclatures.Clear();
                foreach (var item in db.Nomenclature)
                {
                    nomenclatures.Add(new Nomenclature
                    {
                        Name = item.Name,
                        Type = item.Type,
                        Price = item.Price
                    });
                }
            }
            return nomenclatures;
        }

        public static void AddToBase(Nomenclature nomenenclature, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
            {
                try
                {
                    db.Nomenclature.Add(new Nomenclature
                    {
                        Name = nomenenclature.Name,
                        Type = nomenenclature.Type,
                        Price = nomenenclature.Price
                    });
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Элемент уже существует в базе!", "Ошибка");
                    isExeption = false;
                }
            }
        }
        public static void RemoveFromBase(Nomenclature nomenclature, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
            {
                if (nomenclature != null)
                {
                    try
                    {
                        db.Nomenclature.Attach(nomenclature);
                        db.Nomenclature.Remove(nomenclature);
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
    }
}
