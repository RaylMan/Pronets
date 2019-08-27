using Pronets.Data;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronets.VievModel
{
    class StartWindowVM : VievModelBase
    {
        #region Properties
        public OpenWindowCommand OpenWindowCommand { get; private set; }
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
        #endregion
    }
}
