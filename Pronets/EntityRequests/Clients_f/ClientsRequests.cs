﻿using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
        /// <para> List all customers</para>
        /// </summary>
        public static ObservableCollection<Clients> FillList()
        {
            using (var db = new PronetsDataBaseEntities())
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
            return clients;
        }
        /// <summary>
        /// <para>Returns a client instance by Id</para>
        /// </summary>
        public static Clients GetClient(int? id)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                return client = db.Clients.Where(c => c.ClientId == id).FirstOrDefault();
            }
        }
        /// <summary>
        /// <para>Write a new client to the DataBase</para>
        /// </summary>
        public static void AddToBase(Clients client)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                if (client != null)
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// <para> Removes a client instance from the database</para>
        /// </summary>
        public static void RemoveFromBase(Clients client, out bool isExeption)
        {
            isExeption = true;
            using (var db = new PronetsDataBaseEntities())
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
                catch (Exception e)
                {
                    MessageBox.Show("Невозможно удалить , так как есть связи с данными!", "Ошибка");
                    isExeption = false;
                }
            }
        }

        /// <summary>
        /// <para>Modifies a received customer</para>
        /// </summary>
        public static void EditItem(Clients client)
        {
            using (var db = new PronetsDataBaseEntities())
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
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// <para>Returns a list of customers</para>
        /// </summary>
        public static ObservableCollection<Clients> SearchItem(string word)
        {
            using (var db = new PronetsDataBaseEntities())
            {
                var searchItems = db.Clients.Where(c => c.ClientName.Contains(word) ||
                                                c.Contact_Person.Contains(word) ||
                                                c.Inn.Contains(word) ||
                                                c.Telephone_1.Contains(word) ||
                                                c.Telephone_2.Contains(word) ||
                                                c.Telephone_3.Contains(word) ||
                                                c.Email.Contains(word) ||
                                                c.Adress.Contains(word)).ToList();
                //c.Adress.Contains(word)).Include(rd => rd.ReceiptDocument).Include(r => r.Repairs).ToList();
                searchClients = new ObservableCollection<Clients>(searchItems);
            }
            return searchClients;
        }
    }
}
