using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.EntityRequests.Users_f
{
    public static class UsersRequest
    {
        private static ObservableCollection<Users> users = new ObservableCollection<Users>();
        private static ObservableCollection<Positions> positions = new ObservableCollection<Positions>();
        public static ObservableCollection<Users> FillList()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                foreach (var item in db.Users)
                {
                    users.Add(new Users
                    {
                        UserId = item.UserId,
                        Login = item.Login,
                        Password = item.Password,
                        Position = item.Position,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Patronymic = item.Patronymic,
                        Birthday = item.Birthday,
                        Telephone = item.Telephone,
                        Adress = item.Adress
                    });
                }
            }
            return users;
        }
        public static ObservableCollection<Positions> FillPosoitions()
        {
            using (var db = new PronetsDataBaseEntities())
            {
                foreach (var item in db.Positions)
                {
                    positions.Add(new Positions { Position = item.Position });
                }
            }
            return positions;
        }
        public static void AddToBase(Users user)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (user != null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }

            }
        }
        public static void RemoveFromBase(Users user)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (user != null)
                {
                    db.Users.Attach(user);
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }
        }
        public static void EditItem(Users user)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                var result = db.Users.SingleOrDefault(u => u.UserId == user.UserId);
                if (result != null)
                {
                    result.UserId = user.UserId;
                    result.Login = user.Login;
                    result.Password = user.Password;
                    result.Position = user.Position;
                    result.FirstName = user.FirstName;
                    result.LastName = user.LastName;
                    result.Patronymic = user.Patronymic;
                    result.Birthday = user.Birthday;
                    result.Telephone = user.Telephone;
                    result.Adress = user.Adress;
                    db.SaveChanges();
                }
            }
        }
    }
}
