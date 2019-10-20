using Pronets.Data;
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

        /// <summary>
        /// <para>Возращает коллекцию Users</para>
        /// </summary>
        public static ObservableCollection<Users> FillList()
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
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

                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return users;
        }

        /// <summary>
        /// <para>Возращает коллекцию Engineers</para>
        /// </summary>
        public static ObservableCollection<Engineers> FillListEngineers()
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return engineers;
        }

        /// <summary>
        /// <para>Возращает экземпляр Users по Id</para>
        /// </summary>
        public static Users GetUser(int? id)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    user = db.Users.Where(u => u.UserId == id).FirstOrDefault();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return user;
        }

        /// <summary>
        /// <para>Возращает экземпляр Engineers по Id</para>
        /// </summary>
        public static Engineers GetEngineer(int? id)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    engineer = db.Engineers.Where(e => e.Id == id).FirstOrDefault();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return engineer;
        }

        /// <summary>
        /// <para>Возращает экземпляр Engineers по фамилии</para>
        /// </summary>
        public static Engineers GetEngineer(string lastName)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    engineer = db.Engineers.Where(e => e.LastName == lastName).FirstOrDefault();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return engineer;
        }
        /// <summary>
        /// <para>Возращает коллецию Positions</para>
        /// </summary>
        public static ObservableCollection<Positions> FillPosoitions()
        {
            if (positions != null)
                positions.Clear();
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    foreach (var item in db.Positions)
                    {
                        positions.Add(new Positions { Position = item.Position });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
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
                        MessageBox.Show(e.Message, "Ошибка");
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
                        MessageBox.Show(e.Message, "Ошибка");
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
                        MessageBox.Show(e.Message, "Ошибка");
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
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
        }

        /// <summary>
        /// <para>Изменяет в базе экземпляр Engineers</para>
        /// </summary>
        public static void EditEngineer(Engineers eng)
        {
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
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
        }

        /// <summary>
        /// <para>Возращает коллекцию Users по ключевому слову</para>
        /// </summary>
        public static ObservableCollection<Users> SearchItem(string word)
        {
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
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return searchUsers;
        }
        /// <summary>
        /// <para>Возращает экземпляр Users по логину и паролю</para>
        /// </summary>
        public static Users Login(string login, string password)
        {
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
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                return loginUser;
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
                    MessageBox.Show(e.Message);
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
                    MessageBox.Show(e.Message);
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
                    MessageBox.Show(e.Message);
                }
            }
            return isSame;
        }
    }
}
