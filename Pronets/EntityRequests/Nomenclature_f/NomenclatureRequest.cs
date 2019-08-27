using Pronets.Data;
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
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    MessageBox.Show("Элемент уже существует в базе!", "Ошибка");
                    isExeption = false;
                }
            }
        }
        public static void RemoveFromBase(Nomenclature nomenclature)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (nomenclature != null)
                {
                    db.Nomenclature.Attach(nomenclature);
                    db.Nomenclature.Remove(nomenclature);
                    db.SaveChanges();
                }
            }
        }
    }
}
