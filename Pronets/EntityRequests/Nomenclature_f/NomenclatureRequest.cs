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
        /// <summary>
        /// <para>Возращает коллекцию Nomenclature</para>
        /// </summary>
        public static ObservableCollection<Nomenclature> FillList()
        {
            ObservableCollection<Nomenclature> nomenclatures = new ObservableCollection<Nomenclature>(); ;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    nomenclatures = new ObservableCollection<Nomenclature>(db.Nomenclature.ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
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
                catch (DbUpdateException)
                {
                    MessageBox.Show("Элемент уже существует в базе!", "Ошибка");
                    isExeption = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
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
                        MessageBox.Show(e.InnerException.Message, "Ошибка");
                        isExeption = false;
                    }
                }
            }
        }

        /// <summary>
        /// <para>Записывает в базу экземпляр Nomenclature</para>
        /// </summary>
        public static Nomenclature GetDefaultNomenclature()
        {
            Nomenclature nomenclature = null;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    nomenclature = db.Nomenclature.Where(n => n.Name == "Отсутствует").FirstOrDefault();
                }
                
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
            return nomenclature;
        }
        /// <summary>
        /// Возращает коллекцию номенклаьуры по типу
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ObservableCollection<Nomenclature> GetNomenclaturesByType(Nomenclature_Types type)
        {
            ObservableCollection<Nomenclature> nomenclatures = new ObservableCollection<Nomenclature>(); ;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    nomenclatures = new ObservableCollection<Nomenclature>(db.Nomenclature.Where(n => n.Type == type.Type).ToList());
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return nomenclatures;
        }
    }
}
