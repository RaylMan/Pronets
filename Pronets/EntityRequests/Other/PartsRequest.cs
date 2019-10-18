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
    public static class PartsRequest
    {
        
        private static ObservableCollection<Parts> parts = new ObservableCollection<Parts>();
        
       
        public static ObservableCollection<Parts> FillList()
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (parts != null)
                    parts.Clear();
                foreach (var item in db.Parts)
                {
                    parts.Add(new Parts
                    {
                        Part_Name = item.Part_Name,
                        Part_Price = item.Part_Price,
                        PartsOrder = item.PartsOrder
                    });
                }
            }
            return parts;
        }
        public static void AddToBase(Parts part, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    db.Parts.Add(new Parts
                    {
                        Part_Name = part.Part_Name,
                        Part_Price = part.Part_Price,
                        PartsOrder = part.PartsOrder
                    });
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Запчасть с таким именем уже существует в базе!", "Ошибка");
                    isExeption = false;
                }
            }
        }
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
                }
            }
        }

    }
}
