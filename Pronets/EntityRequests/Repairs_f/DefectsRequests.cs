using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pronets.EntityRequests.Repairs_f
{
    public static class DefectsRequests
    {
        //private static ObservableCollection<Defects> defects = new ObservableCollection<Defects>();

        /// <summary>
        /// <para>Возращает коллекцию Defects</para>
        /// </summary>
        public static ObservableCollection<Defects> FillList()
        {
            ObservableCollection<Defects> defects = null;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    defects = new ObservableCollection<Defects>(db.Defects.ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return defects;
        }

        /// <summary>
        /// <para>Добавляет в базу экземпляр Defects</para>
        /// </summary>
        public static void AddToBase(Defects defect)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    db.Defects.Add(new Defects
                    {
                        Id = defect.Id,
                        Defect = defect.Defect,
                        Work = defect.Work
                    });
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
        }

        /// <summary>
        /// <para>Удаляет из базы экземпляр Defects</para>
        /// </summary>
        public static void RemoveFromBase(Defects defect, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                if (defect != null)
                {

                    db.Defects.Attach(defect);
                    db.Defects.Remove(defect);
                    db.SaveChanges();


                    //catch (Exception e)
                    //{
                    //    MessageBox.Show(e.Message, "Ошибка");
                    //    isExeption = false;
                    //}
                }
            }
        }
        /// <summary>
        /// <para>Добавляет в базу экземпляр Defects</para>
        /// </summary>
        public static void EditItem(Defects defect)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (defect != null)
                    {
                        var defectFrombase = db.Defects.Where(d => d.Id == defect.Id).FirstOrDefault();
                        defectFrombase.Defect = defect.Defect;
                        defectFrombase.Work = defect.Work;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
        }
    }
}
