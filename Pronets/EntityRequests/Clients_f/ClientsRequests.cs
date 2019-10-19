using Pronets.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace Pronets.EntityRequests.Clients_f
{
    public static class ClientsRequests
    {
        private static ObservableCollection<Clients> clients = new ObservableCollection<Clients>();
        private static ObservableCollection<Clients> searchClients = new ObservableCollection<Clients>();
        private static Clients client;

        /// <summary>
        /// <para>Возвращает коллекцию Clients</para>
        /// </summary>
        public static ObservableCollection<Clients> FillList()
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (clients != null)
                        clients.Clear();
                    db.ReceiptDocument.Load();
                    db.Repairs.Load();

                    foreach (var item in db.Clients)
                    {
                        clients.Add(new Clients
                        {
                            ClientId = item.ClientId,
                            ClientName = item.ClientName,
                            Inn = item.Inn,
                            Contact_Person = item.Contact_Person,
                            Telephone_1 = item.Telephone_1,
                            Telephone_2 = item.Telephone_2,
                            Telephone_3 = item.Telephone_3,
                            Email = item.Email,
                            Adress = item.Adress,
                            ReceiptDocument = item.ReceiptDocument,
                            Repairs = item.Repairs
                        });
                    }
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return clients;
        }
        /// <summary>
        /// <para>Возвращает экземпляр Clients, по его Id</para>
        /// </summary>
        public static Clients GetClient(int? id)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    client = db.Clients.Where(c => c.ClientId == id).FirstOrDefault();
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message, "Ошибка");
                }
                return client;
            }
        }
        /// <summary>
        /// <para>Записывает в базу экземпляр Clients</para>
        /// </summary>
        public static void AddToBase(Clients client)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (client != null)
                    {
                        db.Clients.Add(client);
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
        /// <para>Удаляет экземпляр Clients</para>
        /// </summary>
        public static void RemoveFromBase(Clients client, out bool isExeption)
        {
            isExeption = true;
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    if (client != null)
                    {
                        db.Clients.Attach(client);
                        db.Clients.Remove(client);
                        db.SaveChanges();
                    }
                }
                catch (InvalidOperationException e)
                {
                    MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                    isExeption = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
        }

        /// <summary>
        /// <para>Изменяет экземпляр Clients</para>
        /// </summary>
        public static void EditItem(Clients client)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                var result = db.Clients.SingleOrDefault(c => c.ClientId == client.ClientId);
                if (result != null)
                {
                    result.ClientName = client.ClientName;
                    result.Inn = client.Inn;
                    result.Contact_Person = client.Contact_Person;
                    result.Telephone_1 = client.Telephone_1;
                    result.Telephone_2 = client.Telephone_2;
                    result.Telephone_3 = client.Telephone_3;
                    result.Email = client.Email;
                    result.Adress = client.Adress;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        List<string> errors = new List<string>();
                        string error = null;

                        foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                        {
                            error += "Object: " + validationError.Entry.Entity.ToString() + "\n";

                            foreach (DbValidationError err in validationError.ValidationErrors)
                            {
                                error += err.ErrorMessage + "\n";
                            }
                        }
                        MessageBox.Show(error, "Ошибка");
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message, "Ошибка");
                    }

                }
            }
        }
        /// <summary>
        /// <para>Возвращает коллекию Clients по ключевому слову</para>
        /// </summary>
        public static ObservableCollection<Clients> SearchItem(string word)
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    var searchItems = db.Clients.Where(c => c.ClientName.Contains(word) ||
                                               c.Contact_Person.Contains(word) ||
                                               c.Inn.Contains(word) ||
                                               c.Telephone_1.Contains(word) ||
                                               c.Telephone_2.Contains(word) ||
                                               c.Telephone_3.Contains(word) ||
                                               c.Email.Contains(word) ||
                                               c.Adress.Contains(word)).ToList();
                    searchClients = new ObservableCollection<Clients>(searchItems);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
            }
            return searchClients;
        }
        /// <summary>
        /// <para>Возращает экземпляр Clients "Пронетс" по Id</para>
        /// </summary>
        public static Clients GetPronetsClient()
        {
            using (var db = ConnectionTools.GetConnection())
            {
                try
                {
                    client = db.Clients.FirstOrDefault(c => c.ClientName == "Пронетс");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка");
                }
                
            }
            return client;
        }
    }
}