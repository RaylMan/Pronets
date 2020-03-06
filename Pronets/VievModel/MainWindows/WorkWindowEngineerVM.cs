using Pronets.Data;
using Pronets.Navigation.WindowsNavigation;
using System.Reflection;

namespace Pronets.VievModel.MainWindows
{
    public class WorkWindowEngineerVM : VievModelBase
    {
        private string assemblyVersion;
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
                    return currentUser.LastName + " " + currentUser.FirstName + "\tВерсия: " + assemblyVersion;
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
            assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
