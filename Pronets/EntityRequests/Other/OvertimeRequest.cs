﻿using Pronets.Data;
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
        private static ObservableCollection<OverTime> overtimeList = new ObservableCollection<OverTime>();

        public static ObservableCollection<OverTime> FillList()
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    overtimeList = new ObservableCollection<OverTime>(db.OverTime.ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return overtimeList;
        }
        public static ObservableCollection<OverTime> FillList(string lastName)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (lastName != null)
                        overtimeList = new ObservableCollection<OverTime>(db.OverTime.Where(o => o.LastName == lastName).ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return overtimeList;
        }
        public static ObservableCollection<OverTime> FillList(string lastName, string status)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (lastName != null)
                        overtimeList = new ObservableCollection<OverTime>(db.OverTime.Where(o => o.LastName == lastName && o.Status == "Не оплачено").ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return overtimeList;
        }
        public static void GetStatus(int id, string status)
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
