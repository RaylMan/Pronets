using Pronets.Data;
using Pronets.EntityRequests.Repairs_f;
using Pronets.EntityRequests.Users_f;
using Pronets.Model;
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
    class UserInfoWindowVM : UsersVM
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


        #endregion
        public UserInfoWindowVM(Users user)
        {
            this.user = user;
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
            GetPosition();
            FirstDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            SecondDate = DateTime.Now.Date.AddHours(23);
            repair_Categories = RepairCategoriesRequests.FillList();
            engineer = UsersRequest.GetEngineer(user.LastName);
            StartInfo();
        }
        #region other
        //Устанавливает значение по умолчанию Combobox "Должность" в соответствии с БД
        private void GetPosition()
        {
            positions = UsersRequest.FillPosoitions();
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
            foreach (var item in RepairsRequest.SortUserList(engineer.Id, firstDate, secondDate))
            {
                v_Repairs.Add(item);
            }
            if (v_Repairs != null)
            {
                var result = from equip in v_Repairs
                             group equip by new
                             {
                                 equip.Nomenclature
                             } into n
                             select new { n.Key.Nomenclature, Count = n.Count() };
                foreach (var item in result)
                {
                    sortingRepair.Add(new SortingRepair { NomenclatureName = item.Nomenclature, RepairsCount = item.Count });
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
                    foreach (var item in RepairsRequest.SortUserList(engineer.Id, firstDate, secondDate, selectedCategory.Category))
                    {
                        v_Repairs.Add(item);
                    }
                    if (v_Repairs != null)
                    {
                        var result = from equip in v_Repairs
                                     group equip by new
                                     {
                                         equip.Nomenclature
                                     } into n
                                     select new { n.Key.Nomenclature, Count = n.Count() };
                        foreach (var item in result)
                        {
                            sortingRepair.Add(new SortingRepair { NomenclatureName = item.Nomenclature, RepairsCount = item.Count });
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

        protected ICommand editItem;
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
            if (selectedPosition != null)
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
                    Adress = Adress
                };
                modifiedEngineer = new Engineers
                {
                    Id = engineer.Id,
                    LastName = LastName,
                    Position = selectedPosition.Position
                };

                var result = MessageBox.Show("Вы Действительно хотите редактировать?", "Редактирование", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (modifiedUser != null && modifiedEngineer!= null)
                    {
                        UsersRequest.EditItem(modifiedUser);
                        UsersRequest.EditEngineer(modifiedEngineer);
                    }
                }
            }
        }
        #endregion

    }
}
