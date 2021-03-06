﻿using Pronets.Data;
using Pronets.EntityRequests.Repairs_f;
using Pronets.EntityRequests.Users_f;
using Pronets.Model;
using Pronets.Viev.Repairs_f;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Users_f
{
    public class UserInfoWindowVM : VievModelBase
    {
        #region Properties
        Users user;
        private Engineers engineer;
        private ObservableCollection<Positions> positions = new ObservableCollection<Positions>();
        public ObservableCollection<Positions> Positions
        {
            get { return positions; }
            set
            {
                positions = value;
                RaisedPropertyChanged("Positions");
            }
        }

        private ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();
        public ObservableCollection<v_Repairs> V_Repairs
        {
            get { return v_Repairs; }
            set
            {
                v_Repairs = value;
                RaisedPropertyChanged("V_Repairs");
            }
        }
        private ObservableCollection<SortingRepair> sortingRepair = new ObservableCollection<SortingRepair>();
        public ObservableCollection<SortingRepair> SortingRepair
        {
            get { return sortingRepair; }
            set
            {
                sortingRepair = value;
                RaisedPropertyChanged("SortingRepair");
            }
        }
        private Positions selectedPosition;
        public Positions SelectedPosition
        {
            get { return selectedPosition; }
            set
            {
                selectedPosition = value;
                RaisedPropertyChanged("SelectedPosition");
            }
        }
        private SortingRepair selectedRepairItem;
        public SortingRepair SelectedRepairItem
        {
            get { return selectedRepairItem; }
            set
            {
                selectedRepairItem = value;
                RaisedPropertyChanged("SelectedRepairItem");
            }
        }
        private ObservableCollection<Repair_Categories> repair_Categories;
        public ObservableCollection<Repair_Categories> Repair_Categories
        {
            get { return repair_Categories; }
            set
            {
                repair_Categories = value;
                RaisedPropertyChanged("Repair_Categories");
            }
        }
        private Repair_Categories selectedCategory;
        public Repair_Categories SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                RaisedPropertyChanged("SelectedCategory");
            }
        }


        private string titleName;
        public string TitleName
        {
            get { return titleName; }
            set
            {
                titleName = value;
                RaisedPropertyChanged("TitleName");
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisedPropertyChanged("Name");
            }
        }
        private DateTime firstDate;
        public DateTime FirstDate
        {
            get { return firstDate; }
            set
            {
                firstDate = value;
                RaisedPropertyChanged("FirstDate");
            }
        }
        private DateTime secondDate;
        public DateTime SecondDate
        {
            get { return secondDate; }
            set
            {
                secondDate = value;
                RaisedPropertyChanged("SecondDate");
            }
        }
        private bool allCategory = true;
        public bool AllCategory
        {
            get { return allCategory; }
            set
            {
                allCategory = value;
                RaisedPropertyChanged("AllCategory");
            }
        }
        private string information;
        public string Information
        {
            get { return information; }
            set
            {
                information = value;
                RaisedPropertyChanged("Information");
            }
        }
        private ObservableCollection<Users> users = new ObservableCollection<Users>();
        public ObservableCollection<Users> Users
        {
            get { return users; }
            set
            {
                users = value;
                RaisedPropertyChanged("Users");
            }
        }

        private string userId;
        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                RaisedPropertyChanged("UserId");
            }
        }

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                RaisedPropertyChanged("Login");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisedPropertyChanged("Password");
            }
        }

        private string position;
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                RaisedPropertyChanged("Position");
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                RaisedPropertyChanged("LastName");
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                RaisedPropertyChanged("FirstName");
            }
        }

        private string patronymic;
        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                patronymic = value;
                RaisedPropertyChanged("Patronymic");
            }
        }

        private DateTime? birthday;
        public DateTime? Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                RaisedPropertyChanged("Birthday");
            }
        }

        private string telephone;
        public string Telephone
        {
            get { return telephone; }
            set
            {
                telephone = value;
                RaisedPropertyChanged("Telephone");
            }
        }

        private string adress;
        public string Adress
        {
            get { return adress; }
            set
            {
                adress = value;
                RaisedPropertyChanged("Adress");
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                RaisedPropertyChanged("SearchText");
            }
        }

        private Users selectedItem;
        public Users SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        private string salaryPerHour;
        public string SalaryPerHour
        {
            get { return salaryPerHour; }
            set
            {
                salaryPerHour = value;
                RaisedPropertyChanged("SalaryPerHour");
            }
        }
        private string salaryPerDay;
        public string SalaryPerDay
        {
            get { return salaryPerDay; }
            set
            {
                salaryPerDay = value;
                RaisedPropertyChanged("SalaryPerDay");
            }
        }

        #endregion
        public UserInfoWindowVM(Users user)
        {
            this.user = user;
            GetContent();
        }
        private void GetContent()
        {
            titleName = $"Информация о работнике {user.LastName} {user.FirstName}";
            name = $"{user.LastName} {user.FirstName} [{user.UserId}]";
            Login = user.Login;
            Password = user.Password;
            Position = user.Position;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Patronymic = user.Patronymic;
            Birthday = user.Birthday;
            Adress = user.Adress;
            SalaryPerDay = user.SalaryPerDay.ToString();
            SalaryPerHour = user.SalaryPerHour.ToString();
            GetPosition();
            FirstDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            SecondDate = DateTime.Now.Date.AddHours(23);
            Repair_Categories = RepairCategoriesRequests.FillList();
            engineer = UsersRequest.GetEngineer(user.LastName);
            StartInfo();
        }
        #region other
        //Устанавливает значение по умолчанию Combobox "Должность" в соответствии с БД
        private void GetPosition()
        {
            Positions.Clear();
            Positions = UsersRequest.FillPosoitions();
            foreach (var item in positions)
            {
                if (item.Position == user.Position)
                    SelectedPosition = item;
            }
        }
        //выводит в textbox информацию о количестве ремонтов по категориям
        private void GetRepairInfo(DateTime firstDate, DateTime secondDate)
        {
            Information = null;
            Information = $"С {FirstDate.ToShortDateString()} по {SecondDate.ToShortDateString()}\nОбщее количество: {V_Repairs.Count}\n\nПо категориям:\n";
            var result = from item in v_Repairs
                         group item by new
                         {
                             item.Repair_Category
                         }
                         into info
                         select new { info.Key.Repair_Category, Count = info.Count() };
            foreach (var info in result)
            {
                Information += $"{info.Repair_Category}: {info.Count} шт.\n";
            }
        }

        private void StartInfo()
        {
            var repairs = RepairsRequest.SortUserList(engineer.Id, firstDate, secondDate);
            if (repairs != null)
            {
                foreach (var item in repairs)
                {
                    V_Repairs.Add(item);
                }
            }

            if (V_Repairs != null)
            {
                var result = from equip in V_Repairs
                             group equip by new
                             {
                                 equip.Nomenclature
                             } into n
                             select new { n.Key.Nomenclature, Count = n.Count() };
                foreach (var item in result)
                {
                    SortingRepair.Add(new SortingRepair { NomenclatureName = item.Nomenclature, RepairsCount = item.Count });
                }
            }
            GetRepairInfo(firstDate, secondDate);
        }
        #endregion

        #region Sorting
        private ICommand sortCommand;
        public ICommand SortCommand
        {
            get
            {
                if (sortCommand == null)
                {
                    sortCommand = new RelayCommand(new Action<object>(Sort));
                }
                return sortCommand;
            }
            set
            {
                sortCommand = value;
                RaisedPropertyChanged("Sort");
            }
        }

        public void Sort(object Parameter)
        {
            if (v_Repairs != null && sortingRepair != null)
            {
                v_Repairs.Clear();
                sortingRepair.Clear();
            }

            if (firstDate != null && secondDate != null)
            {

                if (!AllCategory && selectedCategory != null)
                {
                    var repairs = RepairsRequest.SortUserList(engineer.Id, firstDate, secondDate, selectedCategory.Category);
                    if (repairs != null)
                    {
                        foreach (var item in repairs)
                        {
                            V_Repairs.Add(item);
                        }
                    }

                    if (V_Repairs != null)
                    {
                        var result = from equip in V_Repairs
                                     group equip by new
                                     {
                                         equip.Nomenclature
                                     } into n
                                     select new { n.Key.Nomenclature, Count = n.Count() };
                        foreach (var item in result)
                        {
                            SortingRepair.Add(new SortingRepair { NomenclatureName = item.Nomenclature, RepairsCount = item.Count });
                        }
                    }
                }
                else
                {
                    StartInfo();
                }
                GetRepairInfo(firstDate, secondDate);
            }
        }
        #endregion

        #region Edit Command

        private ICommand editItem;
        public ICommand EditCommand
        {
            get
            {
                if (editItem == null)
                {
                    editItem = new RelayCommand(new Action<object>(EditItem));
                }
                return editItem;
            }
            set
            {
                editItem = value;
                RaisedPropertyChanged("EditCommand");
            }
        }
        public void EditItem(object Parameter)
        {
            Users modifiedUser = null;
            Engineers modifiedEngineer = null;
            if (double.TryParse(SalaryPerHour, out double salaryPerHourNumeric) && double.TryParse(SalaryPerDay, out double salaryPerDayNumeric))
            {
                if (selectedPosition != null && engineer != null)
                {

                    modifiedUser = new Users
                    {
                        UserId = user.UserId,
                        Login = Login,
                        Password = Password,
                        Position = selectedPosition.Position,
                        FirstName = FirstName,
                        LastName = LastName,
                        Patronymic = Patronymic,
                        Birthday = Birthday,
                        Telephone = Telephone,
                        Adress = Adress,
                        SalaryPerHour = salaryPerHourNumeric,
                        SalaryPerDay = salaryPerDayNumeric

                    };
                    modifiedEngineer = new Engineers
                    {
                        Id = engineer.Id,
                        LastName = LastName,
                        Position = selectedPosition.Position
                    };

                    var result = MessageBox.Show("Вы действительно хотите редактировать?", "Редактирование", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (modifiedUser != null && modifiedEngineer != null)
                        {
                            UsersRequest.EditItem(modifiedUser);
                            UsersRequest.EditEngineer(modifiedEngineer);
                        }
                    }
                }
            }
            else
                MessageBox.Show("В полях \"Зарплата за день\" и \"Зарплата за час\"\nдолжны быть числа!", "Ошибка");
        }
        #endregion

        #region SortCommand
        private ICommand refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new RelayCommand(new Action<object>(Refresh));
                }
                return refreshCommand;
            }
            set
            {
                refreshCommand = value;
                RaisedPropertyChanged("RefreshCommand");
            }
        }
        private void Refresh(object parametr)
        {
            GetContent();
        }

        #endregion

        
        public void OpenRepairs(string nomenclature)
        {
            if (nomenclature != null)
            {
                var repairs = V_Repairs.Where(r => r.Nomenclature == nomenclature);
                if (repairs.Count() > 0)
                {
                    AllRepairsWindow win = new AllRepairsWindow(new ObservableCollection<v_Repairs>(repairs));
                    win.Show();
                }
            }
        }
        
    }
}
