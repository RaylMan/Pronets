using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Data.Entity;

namespace Pronets.EntityRequests.Users_f
{
    public static class UsersRequest
    {
        /// <summary>
        /// <para>Возращает коллекцию Users</para>
        /// </summary>
        public static ObservableCollection<Users> FillList()
        {
            ObservableCollection<Users> allUsers = new ObservableCollection<Users>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    allUsers = new ObservableCollection<Users>(db.Users.ToList());
                }

                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
            return allUsers;
        }

        /// <summary>
        /// <para>Возращает коллекцию Engineers</para>
        /// </summary>
        public static ObservableCollection<Engineers> FillListEngineers()
        {
            ObservableCollection<Engineers> engineers = new ObservableCollection<Engineers>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.Engineers.ToList();
                    engineers = new ObservableCollection<Engineers>(result);

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
            return engineers;
        }

        /// <summary>
        /// <para>Возращает коллекцию Engineers</para>
        /// </summary>
        public static ObservableCollection<Engineers> FillListEngineersWithRepairs()
        {
            ObservableCollection<Engineers> engineers = new ObservableCollection<Engineers>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var result = db.Engineers.Include(e => e.Repairs).ToList();
                    engineers = new ObservableCollection<Engineers>(result);

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
            return engineers;
        }

        /// <summary>
        /// <para>Возращает экземпляр Users по Id</para>
        /// </summary>
        public static Users GetUser(int? id)
        {
            Users user = new Users();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    user = db.Users.Where(u => u.UserId == id).FirstOrDefault();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
            return user;
        }

        /// <summary>
        /// <para>Возращает экземпляр Engineers по Id</para>
        /// </summary>
        public static Engineers GetEngineer(int? id)
        {
            Engineers engineer = new Engineers();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    engineer = db.Engineers.Where(e => e.Id == id).Include(e => e.Repairs).FirstOrDefault();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
            return engineer;
        }

        /// <summary>
        /// <para>Возращает экземпляр Engineers по фамилии</para>
        /// </summary>
        public static Engineers GetEngineer(string lastName)
        {
            Engineers engineer = new Engineers();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    engineer = db.Engineers.Where(e => e.LastName == lastName).Include(e => e.Repairs).FirstOrDefault();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
            return engineer;
        }
        /// <summary>
        /// <para>Возращает коллецию Positions</para>
        /// </summary>
        public static ObservableCollection<Positions> FillPosoitions()
        {
            ObservableCollection<Positions> positions = new ObservableCollection<Positions>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    positions = new ObservableCollection<Positions>(db.Positions.ToList());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
            return positions;
        }

        /// <summary>
        /// <para>Добавляет в базу экземпляр Users</para>
        /// </summary>
        public static void AddToBase(Users user)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                if (user != null)
                {
                    try
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.InnerException.Message, "Ошибка");
                    }
                }
            }
        }
        /// <summary>
        /// <para>Добавляет в базу экземпляр Engineers</para>
        /// </summary>
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

        /// <summary>
        /// <para>Удаляет из базы экземпляр Users</para>
        /// </summary>
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
        /// <summary>
        /// <para>Удаляет из базы экземпляр Engineers по фамилии</para>
        /// </summary>
        public static void RemoveFromBaseEngineer(string name, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                if (name != null)
                {
                    try
                    {
                        db.Engineers.Attach(db.Engineers.Where(e => e.LastName == name).FirstOrDefault());
                        db.Engineers.Remove(db.Engineers.Where(e => e.LastName == name).FirstOrDefault());
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

        /// <summary>
        /// <para>Изменяет в базе экземпляр Users</para>
        /// </summary>
        public static void EditItem(Users user)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
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
                        result.SalaryPerHour = user.SalaryPerHour;
                        result.SalaryPerDay = user.SalaryPerDay;
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
        }

        /// <summary>
        /// <para>Изменяет в базе экземпляр Engineers</para>
        /// </summary>
        public static void EditEngineer(Engineers eng)
        {
            Engineers engineer = new Engineers();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    engineer = db.Engineers.Where(e => e.Id == eng.Id).FirstOrDefault();
                    engineer.LastName = eng.LastName;
                    engineer.Position = eng.Position;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
        }

        /// <summary>
        /// <para>Возращает коллекцию Users по ключевому слову</para>
        /// </summary>
        public static ObservableCollection<Users> SearchItem(string word)
        {
            ObservableCollection<Users> searchUsers = new ObservableCollection<Users>();
            using (var db = ConnectionTools.GetConnection())
            {
                try
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
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message, "Ошибка");
                }
            }
            return searchUsers;
        }
        /// <summary>
        /// <para>Возращает экземпляр Users по логину и паролю</para>
        /// </summary>
        public static Users Login(string login, string password, out bool ex)
        {
            Users loginUser = new Users();
            ex = true;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (loginUser != null)
                        loginUser = null;
                    loginUser = db.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
                }
                catch (System.Data.Entity.Core.EntityException)
                {
                    MessageBox.Show("Отсутствует соединение с сервером!", "Ошибка");
                    ex = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.InnerException.Message);
                    ex = false;
                }
                return loginUser;
            }
        }
        /// <summary>
        /// <para>Возращает экземпляр Users по логину и паролю</para>
        /// </summary>
        public static Users LoginWork(string login, string password)
        {
            Users loginUser = new Users();
            using (var db = ConnectionTools.GetConnection())
            {
                return loginUser = db.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
            }
        }

        /// <summary>
        /// <para>Изменяет в экземпляре Users пароль, по id пользователя</para>
        /// </summary>
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
                    MessageBox.Show(e.InnerException.Message);
                }
            }
        }
        /// <summary>
        /// <para>Изменяет в экземпляре Users логин, по id пользователя</para>
        /// </summary>
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
                    MessageBox.Show(e.InnerException.Message);
                }

            }
        }

        /// <summary>
        /// <para>Проверяет на совпадение логин в базе, если есть совпадение возвращает true</para>
        /// </summary>
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
                    MessageBox.Show(e.InnerException.Message);
                }
            }
            return isSame;
        }
    }
}
