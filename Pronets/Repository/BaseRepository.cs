using AngleSharp.Dom;
using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.Repository
{
    public class BaseRepository : IDisposable
    {
        protected PronetsDBEntities1 db { get; set; }
        public BaseRepository()
        {
            db = ConnectionTools.GetConnection();
        }
        public void Dispose()
        {
            db?.Dispose();
        }

        internal int SaveChanges()
        {
            try
            {
               return db.SaveChanges();
            }
            catch 
            {
                throw;
            }
        }
        public ObservableCollection<Statuses> GetStatuses()
        {
            ObservableCollection<Statuses> statuses = new ObservableCollection<Statuses>();
            try
            {
                statuses = new ObservableCollection<Statuses>(db.Statuses.Where(s => s.Status != "Готово").ToList());

                statuses.Add((Statuses)db.Statuses.Where(s => s.Status == "Готово").FirstOrDefault());
            }
            catch (Exception)
            {
                throw;
            }
            return statuses;
        }
        public ObservableCollection<Repair_Categories> GetRepairsCategories()
        {
            ObservableCollection<Repair_Categories> repair_Categories = new ObservableCollection<Repair_Categories>();
            try
            {
                repair_Categories = new ObservableCollection<Repair_Categories>(db.Repair_Categories.ToList());
                //foreach (var category in db.Repair_Categories)
                //{
                //    repair_Categories.Add(new Repair_Categories
                //    {
                //        Category = category.Category
                //    });
                //}
            }
            catch
            {
                throw;
            }
            return repair_Categories;
        }
        public ObservableCollection<Clients> GetClients()
        {
            ObservableCollection<Clients> clients = new ObservableCollection<Clients>();
            try
            {
                clients = new ObservableCollection<Clients>(db.Clients.ToList());
            }
            catch (Exception)
            {
                throw;
            }
            return clients;
        }
        public ObservableCollection<Users> GetUsers()
        {
            ObservableCollection<Users> users = new ObservableCollection<Users>();
            try
            {
                users = new ObservableCollection<Users>(db.Users.ToList());
            }
            catch (Exception)
            {
                throw;
            }
            return users;
        }
    }
}
