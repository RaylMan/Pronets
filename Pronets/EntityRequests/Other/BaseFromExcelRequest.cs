using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pronets.EntityRequests.Other
{
    public static class BaseFromExcelRequest
    {
        private static ObservableCollection<BaseFromExcel> baseFromExcel = new ObservableCollection<BaseFromExcel>();

        /// <summary>
        /// <para>Возвращает колекцию BaseFromExcel</para>
        /// </summary>
        public static ObservableCollection<BaseFromExcel> FillList()
        {

            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    baseFromExcel = new ObservableCollection<BaseFromExcel>(db.BaseFromExcel.ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return baseFromExcel;
        }

        /// <summary>
        /// <para>Добавляет в базу эксемпляр BaseFromExcel</para>
        /// </summary>
        public static void AddToBase(BaseFromExcel item)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (item != null)
                    {
                        db.BaseFromExcel.Add(item);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        /// <summary>
        /// <para>Удаляет экземпляр BaseFromExcel</para>
        /// </summary>
        public static void RemoveFromBase(BaseFromExcel item)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (item != null)
                {
                    try
                    {
                        db.BaseFromExcel.Attach(item);
                        db.BaseFromExcel.Remove(item);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// <para>Очищает таблицу BaseFromExcel</para>
        /// </summary>
        public static void ClearBase()
        {
            using (var db = ConnectionTools.GetConnection())
            {

                try
                {
                    db.Database.ExecuteSqlCommand("TRUNCATE TABLE [BaseFromExcel]");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}

