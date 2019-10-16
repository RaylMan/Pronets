using Pronets.Data;
using Pronets.Navigation.WindowsNavigation;


namespace Pronets.VievModel.MainWindows
{
    public class WorkWindowEngineerVM : VievModelBase
    {
        public OpenWindowCommand OpenWindowCommand { get; private set; }
        private Users currentUser;
        public Users CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                RaisedPropertyChanged("CurrenUser");
            }
        }
        protected string userName;
        public string UserName
        {
            get
            {
                if (currentUser != null)
                    return currentUser.Login + " " + currentUser.LastName + " " + currentUser.FirstName;
                return userName = "Error";
            }
            set
            {
                userName = value;
                RaisedPropertyChanged("UserName");
            }
        }
        public WorkWindowEngineerVM(Users user)
        {
            if (user != null)
                currentUser = user;
            GetDefaultUser();
            OpenWindowCommand = new OpenWindowCommand();
        }

        /// <summary>
        /// Установка значений по умолчанию DefaultLastName и DefaultUserId в
        /// соответствии с пользователем который произвел логин, для установки 
        /// по умолчанию значений в других окнах
        /// </summary>
        private void GetDefaultUser()
        {
            if (currentUser != null)
            {
                Properties.Settings.Default.DefaultLastName = currentUser.LastName;
                Properties.Settings.Default.DefaultUserId = currentUser.UserId;
                Properties.Settings.Default.Save();
            }
        }
    }
}
