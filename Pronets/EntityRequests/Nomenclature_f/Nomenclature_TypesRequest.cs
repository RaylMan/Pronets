using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;

namespace Pronets.EntityRequests.Nomenclature_f
{
    public static class Nomenclature_TypesRequest
    {
        /// <summary>
        /// <para>Возращает коллекцию Nomenclature_Types</para>
        /// </summary>
        public static ObservableCollection<Nomenclature_Types> FillList()
        {
            ObservableCollection<Nomenclature_Types> nomenclature_Types = new ObservableCollection<Nomenclature_Types>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    nomenclature_Types = new ObservableCollection<Nomenclature_Types>(db.Nomenclature_Types.ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return nomenclature_Types;
        }

        /// <summary>
        /// <para>Записывает в базу экземпляр Nomenclature_Types</para>
        /// </summary>
        public static void AddToBase(Nomenclature_Types type, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    db.Nomenclature_Types.Add(new Nomenclature_Types
                    {
                        Type = type.Type
                    });
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Элемент уже существует в базе!", "Ошибка");
                    isExeption = false;
                }
                catch (System.Data.Entity.Core.EntityException)
                {
                    MessageBox.Show("Отсутствует соединение с сервером!", "Ошибка");
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
        /// <para>Удаляет из базы экзепляр класса Nomenclature_Types</para>
        /// </summary>
        public static void RemoveFromBase(Nomenclature_Types type, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                if (type != null)
                {
                    try
                    {
                        db.Nomenclature_Types.Attach(type);
                        db.Nomenclature_Types.Remove(type);
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                        isExeption = false;
                    }
                    catch (System.Data.Entity.Core.EntityException)
                    {
                        MessageBox.Show("Отсутствует соединение с сервером!", "Ошибка");
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

        /// <summary>
        /// <para>Изменяет экземпляр класса Nomenclature_Types</para>
        /// </summary>
        public static void EditType(string type, string newType, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.Nomenclature_Types.Where(t => t.Type == type).FirstOrDefault();
                    if (result != null)
                        result.Type = newType;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
        }
    }
}
