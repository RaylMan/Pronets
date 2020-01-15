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
    public static class OvertimeRequest
    {
        /// <summary>
        /// <para>Возращает коллекцию OverTime</para>
        /// </summary>
        public static ObservableCollection<OverTime> FillList()
        {
            ObservableCollection<OverTime> overtimeList = new ObservableCollection<OverTime>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    overtimeList = new ObservableCollection<OverTime>(db.OverTime.OrderBy(o => o.Date).ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return overtimeList;
        }
        /// <summary>
        /// <para>Возращает коллекцию OverTime по фамилии</para>
        /// </summary>
        public static ObservableCollection<OverTime> FillList(string lastName)
        {

            ObservableCollection<OverTime> overtimeList = new ObservableCollection<OverTime>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (lastName != null)
                        overtimeList = new ObservableCollection<OverTime>(db.OverTime.Where(o => o.LastName == lastName).OrderBy(o => o.Date).ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return overtimeList;
        }
        /// <summary>
        /// <para>Возращает коллекцию OverTime по фамилии и статусу</para>
        /// </summary>
        public static ObservableCollection<OverTime> FillList(string lastName, string status)
        {
            ObservableCollection<OverTime> overtimeList = new ObservableCollection<OverTime>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (lastName != null)
                        overtimeList = new ObservableCollection<OverTime>(db.OverTime.Where(o => o.LastName == lastName && o.Status == "Не оплачено").OrderBy(o => o.Date).ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return overtimeList;
        }
        /// <summary>
        /// <para>Установить статус по id</para>
        /// </summary>
        public static void SetStatus(int id, string status)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (id != 0)
                    {
                        var result = db.OverTime.Where(o => o.Id == id).FirstOrDefault();
                        if (result != null)
                            result.Status = status;
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
        /// <para>Добавляет в базу экземпляр OverTime</para>
        /// </summary>
        public static void AddToBase(OverTime overtime)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if(overtime != null)
                    {
                        db.OverTime.Add(overtime);
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
        /// <para>Удаляет из базы экземпляр OverTime</para>
        /// </summary>
        public static void RemoveFromBase(OverTime overtime)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (overtime != null)
                {
                    try
                    {
                        db.OverTime.Attach(overtime);
                        db.OverTime.Remove(overtime);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }
    }
}
