﻿using Pronets.Data;
using Pronets.EntityRequests.Users_f;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Users_f
{
    public class AddUserVM : UsersVM
    {

        #region Properties
        private ObservableCollection<Positions> positions;
        public ObservableCollection<Positions> Positions
        {
            get { return positions; }

            set
            {
                positions = value;
                RaisedPropertyChanged("Positions");
            }
        }
        private Positions selItem;
        public Positions SelItem
        {
            get { return selItem; }
            set
            {
                selItem = value;
                RaisedPropertyChanged("SelItem");
            }
        }
        #endregion
        public AddUserVM()
        {
            Birthday = new DateTime(1980, 1, 1);
            positions = UsersRequest.FillPosoitions();
        }

        #region AddCommand
        private ICommand addItem;
        public ICommand AddCommand
        {
            get
            {
                if (addItem == null)
                {
                    addItem = new RelayCommand(new Action<object>(AddItem));
                }
                return addItem;
            }
            set
            {
                addItem = value;
                RaisedPropertyChanged("AddCommand");
            }
        }

        public void AddItem(object Parameter)
        {
            Users user = null;
            if (selItem != null && login != null && login != "" && password != null && password != "" && lastName != "")
            {
                if (!UsersRequest.IsSameLogin(login))
                {
                    
                    user = new Users
                    {
                        Login = base.Login,
                        Password = base.Password,
                        Position = selItem.Position,
                        FirstName = base.FirstName,
                        LastName = base.LastName,
                        Patronymic = base.Patronymic,
                        Birthday = base.Birthday,
                        Telephone = base.Telephone,
                        Adress = base.Adress,
                        SalaryPerHour = 0,
                        SalaryPerDay = 0
                    };
                    var result = MessageBox.Show("Вы действительно хотете добавить работника?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        UsersRequest.AddToBase(user);
                        var engineer = new Engineers
                        {
                            UserId = user.UserId,
                            LastName = base.LastName,
                            Position = selItem.Position
                        };
                        UsersRequest.AddEngineer(engineer);
                        
                       
                        Login = string.Empty;
                        Password = string.Empty;
                        FirstName = string.Empty;
                        LastName = string.Empty;
                        Patronymic = string.Empty;
                        Birthday = DateTime.MinValue;
                        Telephone = string.Empty;
                        Adress = string.Empty;
                    }
                }
                else
                    MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка");
            }
            else
                MessageBox.Show("Необходимо заполнить поля: \n1)Логин\n2)Пароль\n3)Фамилия\n4)Выбрать уровень доступа!", "Ошибка");
        }
        #endregion
    }
}
