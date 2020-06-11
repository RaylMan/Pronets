using Pronets.Data;
using Pronets.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pronets.EntityRequests.Other
{
    public static class PartsRequest
    {
        /// <summary>
        /// <para>Возращает коллекцию Parts</para>
        /// </summary>
        public static ObservableCollection<Parts> FillList()
        {
            ObservableCollection<Parts> parts = new ObservableCollection<Parts>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    parts = new ObservableCollection<Parts>(db.Parts.ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                }
            }
            return parts;
        }

        /// <summary>
        /// <para>Добавляет в базу экземпляр Parts</para>
        /// </summary>
        public static void AddToBase(Parts part, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    db.Parts.Add(part);
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Запчасть с таким именем уже существует в базе!", "Ошибка");
                    isExeption = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(ExceptionMessanger.Message(e));
                    isExeption = false;
                }
            }
        }

        /// <summary>
        /// <para>Удаляет из базы экземпляр Parts</para>
        /// </summary>
        public static void RemoveFromBase(Parts part, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                if (part != null)
                {
                    try
                    {
                        db.Parts.Attach(part);
                        db.Parts.Remove(part);
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                        isExeption = false;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(ExceptionMessanger.Message(e));
                        isExeption = false;
                    }
                }
            }
        }
        /// <summary>
        /// <para>Удаляет из базы экземпляр Parts</para>
        /// </summary>
        public static void EditPartInfo(string name, string info)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    try
                    {
                        var part = db.Parts.Where(n => n.Part_Name == name).FirstOrDefault();
                        if (part != null)
                        {
                            part.Part_Info = info;
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(ExceptionMessanger.Message(e));
                    }
                }
            }
        }
        /// <summary>
        /// <para>Удаляет из базы экземпляр Parts</para>
        /// </summary>
        public static void EditPart(Parts part)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (part == null) throw new ArgumentNullException();
                try
                {
                    var result = db.Parts.FirstOrDefault(n => n.Part_Name == part.Part_Name);
                    if (part != null)
                    {
                        result.Equipment = part.Equipment;
                        result.Part_Info = part.Part_Info;
                        db.SaveChanges();
                    }
                }
                catch 
                {
                    throw;
                }
            }
        }
    }
}
