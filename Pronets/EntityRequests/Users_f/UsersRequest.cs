﻿using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Pronets.EntityRequests.Users_f
{
    public static class UsersRequest
    {
        private static Users loginUser = new Users();
        private static ObservableCollection<Users> users = new ObservableCollection<Users>();
        private static ObservableCollection<Engineers> engineers = new ObservableCollection<Engineers>();
        private static ObservableCollection<Users> searchUsers = new ObservableCollection<Users>();
        private static ObservableCollection<Positions> positions = new ObservableCollection<Positions>();
        private static Users user;
        private static Engineers engineer;
        public static ObservableCollection<Users> FillList()
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (users != null)
                    users.Clear();
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
        public static ObservableCollection<Engineers> FillListEngineers()
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (engineers != null)
                    engineers.Clear();
                foreach (var item in db.Engineers)
                {
                    engineers.Add(new Engineers
                    {
                        Id = item.Id,
                        LastName = item.LastName,
                        Position = item.Position,
                        Repairs = item.Repairs
                    });
                }
            }
            return engineers;
        }

        public static Users GetUser(int? id)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                return user = db.Users.Where(u => u.UserId == id).FirstOrDefault();
            }
        }
       
        public static Engineers GetEngineer(int? id)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                return engineer = db.Engineers.Where(e => e.Id == id).FirstOrDefault();
            }
        }
        public static Engineers GetEngineer(string lastName)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                return engineer = db.Engineers.Where(e => e.LastName == lastName).FirstOrDefault();
            }
        }
        
        public static ObservableCollection<Positions> FillPosoitions()
        {
            if (positions != null)
                positions.Clear();
            using (var db = ConnectionTools.GetConnection())
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
            using (var db = ConnectionTools.GetConnection())
            {
                if (user != null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }

            }
        }
        public static void AddEngineer(Engineers engineer)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (engineer != null)
                {
                    try
                    {
                        db.Engineers.Add(engineer);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка");
                    }
                    
                }

            }
        }
        public static void RemoveFromBase(Users user, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                if (user != null)
                {
                    try
                    {
                        db.Users.Attach(user);
                        db.Users.Remove(user);
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
        public static void RemoveFromBaseEngineer(string name, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                if (name != null)
                {
                    try
                    {
                        db.Engineers.Attach(db.Engineers.Where(e=> e.LastName == name).FirstOrDefault());
                        db.Engineers.Remove(db.Engineers.Where(e => e.LastName == name).FirstOrDefault());
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
        public static void EditItem(Users user)
        {
            using (var db = ConnectionTools.GetConnection())
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
        public static void EditEngineer(Engineers eng)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                engineer = db.Engineers.Where(e => e.Id == eng.Id).FirstOrDefault();
                engineer.LastName = eng.LastName;
                engineer.Position = eng.Position;
                db.SaveChanges();
                
            }
        }
        public static ObservableCollection<Users> SearchItem(string word)
        {

            using (var db = ConnectionTools.GetConnection())
            {
                var searchItems = from u in db.Users
                                  where u.Login.Contains(word) ||
                                                u.Password.Contains(word) ||
                                                u.Position.Contains(word) ||
                                                u.FirstName.Contains(word) ||
                                                u.LastName.Contains(word) ||
                                                u.Patronymic.Contains(word) ||
                                                u.Telephone.Contains(word) ||
                                               u.Adress.Contains(word)
                                  select u;
                searchUsers = new ObservableCollection<Users>(searchItems);
            }
            return searchUsers;
        }
        public static Users Login(string name, string password)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (loginUser != null)
                        loginUser = null;
                    loginUser = db.Users.Where(u => u.Login == name && u.Password == password).FirstOrDefault();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                return loginUser;
            }
        }
        public static void ChangePassword(int id, string password)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (id != 0 && !string.IsNullOrWhiteSpace(password))
                    {
                        var result = db.Users.Where(u => u.UserId == id).FirstOrDefault();
                        result.Password = password;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        public static void ChangeLogin(int id, string login)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (id != 0 && !string.IsNullOrWhiteSpace(login))
                    {
                        var result = db.Users.Where(u => u.UserId == id).FirstOrDefault();
                        result.Login = login;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
        }
        public static bool IsSameLogin(string login)
        {
            bool isSame = false;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.Users.Where(u => u.Login == login).FirstOrDefault();
                    if (result != null)
                        isSame = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
            return isSame;
        }
    }
}
