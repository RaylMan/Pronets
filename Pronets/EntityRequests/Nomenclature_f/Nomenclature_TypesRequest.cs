using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pronets.EntityRequests.Nomenclature_f
{
    class Nomenclature_TypesRequest
    {
        private static ObservableCollection<Nomenclature_Types> nomenclature_Types = new ObservableCollection<Nomenclature_Types>();
        public static ObservableCollection<Nomenclature_Types> FillList()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (nomenclature_Types != null)
                    nomenclature_Types.Clear();
                foreach (var item in db.Nomenclature_Types)
                {
                    nomenclature_Types.Add(new Nomenclature_Types
                    {
                        Type = item.Type
                    });
                }
            }
            return nomenclature_Types;
        }

        public static void AddToBase(Nomenclature_Types type, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
            {
                try
                {
                    db.Nomenclature_Types.Add(new Nomenclature_Types
                    {
                        Type = type.Type
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
        public static void RemoveFromBase(Nomenclature_Types type)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (type != null)
                {
                    db.Nomenclature_Types.Attach(type);
                    db.Nomenclature_Types.Remove(type);
                    db.SaveChanges();
                }
            }
        }
    }
}
