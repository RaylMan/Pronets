using Pronets.Data;
using Pronets.EntityRequests.Users_f;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Users_f
{
    public class UsersVM : VievModelBase
    {
        #region UsersVM Properties
        public OpenWindowCommand OpenWindowCommand { get; private set; }
        protected ObservableCollection<Users> users = new ObservableCollection<Users>();
        public ObservableCollection<Users> Users
        {
            get { return users; }
            set
            {
                users = value;
                RaisedPropertyChanged("Users");
            }
        }

        protected string userId;
        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                RaisedPropertyChanged("UserId");
            }
        }

        protected string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                RaisedPropertyChanged("Login");
            }
        }

        protected string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisedPropertyChanged("Password");
            }
        }

        protected string position;
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                RaisedPropertyChanged("Position");
            }
        }

        protected string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                RaisedPropertyChanged("LastName");
            }
        }

        protected string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                RaisedPropertyChanged("FirstName");
            }
        }

        protected string patronymic;
        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                patronymic = value;
                RaisedPropertyChanged("Patronymic");
            }
        }

        protected DateTime birthday;
        public DateTime Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                RaisedPropertyChanged("Birthday");
            }
        }

        protected string telephone;
        public string Telephone
        {
            get { return telephone; }
            set
            {
                telephone = value;
                RaisedPropertyChanged("Telephone");
            }
        }

        protected string adress;
        public string Adress
        {
            get { return adress; }
            set
            {
                adress = value;
                RaisedPropertyChanged("Adress");
            }
        }

        protected string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                RaisedPropertyChanged("SearchText");
            }
        }

        protected Users selectedItem;
        public Users SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        #endregion

        public UsersVM()
        {
            users = UsersRequest.FillList();
            OpenWindowCommand = new OpenWindowCommand();
        }
        
        #region Delete Command

        protected ICommand removeItem;
        public ICommand RemoveCommand
        {
            get
            {
                if (removeItem == null)
                {
                    removeItem = new RelayCommand(new Action<object>(RemoveItem));
                }
                return removeItem;
            }
            set
            {
                removeItem = value;
                RaisedPropertyChanged("RemoveCommand");
            }
        }
        public void RemoveItem(object Parameter)
        {
            if (selectedItem != null)
            {
                var result = MessageBox.Show("Вы Действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    UsersRequest.RemoveFromBase(selectedItem);
                    users.RemoveAt(SelectedIndex);
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент", "Ошибка");
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
            if (selectedItem != null)
            {
                if (SelectedItem.UserId != 0)
                {
                    modifiedUser = new Users
                    {
                        UserId = SelectedItem.UserId,
                        Login = selectedItem.Login,
                        Password = selectedItem.Password,
                        Position = selectedItem.Position,
                        FirstName = selectedItem.FirstName,
                        LastName = selectedItem.LastName,
                        Patronymic = selectedItem.Patronymic,
                        Birthday = selectedItem.Birthday,
                        Telephone = selectedItem.Telephone,
                        Adress = selectedItem.Adress
                    };

                    var result = MessageBox.Show("Вы Действительно хотете редактировать?", "Редактирование", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (modifiedUser != null)
                        {
                            UsersRequest.EditItem(modifiedUser);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент", "Ошибка");
        }
        #endregion

        #region FillList button command

        protected ICommand fillItems;
        public ICommand FillListCommand
        {
            get
            {
                if (fillItems == null)
                {
                    fillItems = new RelayCommand(new Action<object>(FillList));
                }
                return fillItems;
            }
            set
            {
                fillItems = value;
                RaisedPropertyChanged("FillListCommand");
            }
        }
        void FillList(object Parameter)
        {
            users.Clear();
            users = UsersRequest.FillList();
        }

        #endregion

        #region Search Command
        protected ICommand searchItem;
        public ICommand SearchCommand
        {
            get
            {
                if (searchItem == null)
                {
                    searchItem = new RelayCommand(new Action<object>(SearchItem));
                }
                return searchItem;
            }
            set
            {
                editItem = value;
                RaisedPropertyChanged("SearchCommand");
            }
        }

        public void SearchItem(object Parameter)
        {
            if(SearchText != null && SearchText != "")
            {
                users.Clear();
                foreach (var user in UsersRequest.SearchItem(searchText))
                {
                    users.Add(user);
                }
                SearchText = string.Empty;
            }
        }
        #endregion
    }
}
