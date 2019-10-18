﻿using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Pronets.EntityRequests.Repairs_f
{
    public static class RepairCategoriesRequests
    {
        private static ObservableCollection<Repair_Categories> repair_Categories = new ObservableCollection<Repair_Categories>();
        public static ObservableCollection<Repair_Categories> FillList()
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (repair_Categories != null)
                    repair_Categories.Clear();
                foreach (var item in db.Repair_Categories)
                {
                    repair_Categories.Add(new Repair_Categories
                    {
                        Category = item.Category,
                        Price = item.Price
                    });
                }
            }
            return repair_Categories;
        }
        
        public static void AddToBase(Repair_Categories category, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    db.Repair_Categories.Add(new Repair_Categories
                    {
                        Category = category.Category,
                        Price = category.Price
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
        public static void RemoveFromBase(Repair_Categories category, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                if (category != null)
                {
                    try
                    {
                        db.Repair_Categories.Attach(category);
                        db.Repair_Categories.Remove(category);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                        isExeption = false;
                    }
                }
            }
        }
    }
}
