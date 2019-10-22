using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;

namespace Pronets.EntityRequests.Nomenclature_f
{
    class NomenclatureRequest
    {
        //private static ObservableCollection<Nomenclature> nomenclatures = new ObservableCollection<Nomenclature>();

        /// <summary>
        /// <para>Возращает коллекцию Nomenclature</para>
        /// </summary>
        public static ObservableCollection<Nomenclature> FillList()
        {
            ObservableCollection<Nomenclature> nomenclatures = null;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    nomenclatures = new ObservableCollection<Nomenclature>(db.Nomenclature.ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return nomenclatures;
        }

        /// <summary>
        /// <para>Записывает в базу экземпляр Nomenclature</para>
        /// </summary>
        public static void AddToBase(Nomenclature nomenenclature, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
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
                catch (DbUpdateException e)
                {
                    MessageBox.Show("Элемент уже существует в базе!", "Ошибка");
                    isExeption = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                    isExeption = false;
                }
            }
        }

        /// <summary>
        /// <para>Удаляет экземпляр Nomenclature</para>
        /// </summary>
        public static void RemoveFromBase(Nomenclature nomenclature, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
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
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message, "Ошибка");
                        isExeption = false;
                    }
                }
            }
        }
    }
}
