using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Pronets.EntityRequests.Repairs_f
{
    public static class RepairCategoriesRequests
    {
        /// <summary>
        /// <para>Возращает коллекцию Repair_Categories</para>
        /// </summary>
        public static ObservableCollection<Repair_Categories> FillList()
        {
            ObservableCollection<Repair_Categories> repair_Categories = new ObservableCollection<Repair_Categories>() ;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    //repair_Categories = new ObservableCollection<Repair_Categories>(db.Repair_Categories.ToList());
                    foreach (var category in db.Repair_Categories)
                    {
                        repair_Categories.Add(new Repair_Categories
                        {
                            Category = category.Category
                        });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
            return repair_Categories;
        }

        /// <summary>
        /// <para>Добавляет в базу экземпляр Repair_Categories</para>
        /// </summary>
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
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
        }

        /// <summary>
        /// <para>Удаляет из базы экземпляр Repair_Categories</para>
        /// </summary>
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
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                        isExeption = false;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.InnerException.Message, "Ошибка");
                        isExeption = false;
                    }
                }
            }
        }
    }
}
