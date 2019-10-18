using Pronets.Data;
using Pronets.EntityRequests.Users_f;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Other
{
    public class SettingsWindowVM : VievModelBase
    {

        #region Properties
        Users user;
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

        private string oldPassword;
        public string OldPassword
        {
            get { return oldPassword; }
            set
            {
                oldPassword = value;
                RaisedPropertyChanged("OldPassword");
            }
        }
        private string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                newPassword = value;
                RaisedPropertyChanged("NewPassword");
            }
        }
        private string verifiedPassword;
        public string VerifiedPassword
        {
            get { return verifiedPassword; }
            set
            {
                verifiedPassword = value;
                RaisedPropertyChanged("VerifiedPassword");
            }
        }
        private string chiefEngineer;
        public string ChiefEngineer
        {
            get { return chiefEngineer; }
            set
            {
                chiefEngineer = value;
                RaisedPropertyChanged("ChiefEngineer");
            }
        }
        private string responsiblePerson;
        public string ResponsiblePerson
        {
            get { return responsiblePerson; }
            set
            {
                responsiblePerson = value;
                RaisedPropertyChanged("ResponsiblePerson");
            }
        }
        private string pricePerHour;
        public string PricePerHour
        {
            get { return pricePerHour; }
            set
            {
                pricePerHour = value;
                RaisedPropertyChanged("PricePerHour");
            }
        }
        private string pricePerDay;
        public string PricePerDay
        {
            get { return pricePerDay; }
            set
            {
                pricePerDay = value;
                RaisedPropertyChanged("PricePerDay");
            }
        }

        #endregion
        public SettingsWindowVM(Users user)
        {
            this.user = user;
            login = user.Login;
            chiefEngineer = Properties.Settings.Default.ChiefEngineer.ToString();
            responsiblePerson = Properties.Settings.Default.ResponsiblePerson.ToString();
            pricePerDay = Properties.Settings.Default.PricePerDay.ToString();
            pricePerHour = Properties.Settings.Default.PricePerHour.ToString();
        }

        #region SaveCommand
        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(new Action<object>(SaveSettings));
                }
                return saveCommand;
            }
            set
            {
                saveCommand = value;
                RaisedPropertyChanged("SaveCommand");
            }
        }
        public void SaveSettings(object Parameter)
        {
            string message = "Успешная запись:";
            string errors = "\nОшибки: ";
            //if (!string.IsNullOrWhiteSpace(login))
            //{
            //    UsersRequest.ChangeLogin(user.UserId, login);
            //    message += "\n-Логин";
            //}
            //else
            //    errors += "\n-Логин: Невозможно сохранить пустое поле";

            if (!string.IsNullOrWhiteSpace(chiefEngineer))
            {
                Properties.Settings.Default.ChiefEngineer = chiefEngineer;
                message += "\n-Инженер (Для печати)";
            }
            else
                errors += "\n-Инженер (Для печати): Невозможно сохранить пустое поле";

            if (!string.IsNullOrWhiteSpace(responsiblePerson))
            {
                Properties.Settings.Default.ResponsiblePerson = responsiblePerson;
                message += "\n-Ответственное лицо (Для печати)";
            }
            else
                errors += "\n-Ответственное лицо (Для печати): Невозможно сохранить пустое поле";
            if (int.TryParse(pricePerDay, out int numericPricePerDay))
            {
                Properties.Settings.Default.PricePerDay = numericPricePerDay;
                message += "\n-Зарплата за час (По умолчанию)";
            }
            else
                errors += "\n-Зарплата за час (По умолчанию): Необходимо ввести число";

            if (int.TryParse(pricePerHour, out int numericPricePerHour))
            {
                Properties.Settings.Default.PricePerHour = numericPricePerHour;
                message += "\n-Зарплата за день (По умолчанию)";
            }
            else
                errors += "\n-Зарплата за день (По умолчанию): Необходимо ввести число";

            Properties.Settings.Default.Save();

            if (errors.Length > 10 && message.Length > 20)
                MessageBox.Show(message + errors, "Запись");
            else if (message.Length > 20)
                MessageBox.Show(message, "Запись");
            else if (message.Length < 20 && errors.Length > 10)
                MessageBox.Show(errors, "Запись");


        }
        #endregion

        #region SaveCommand
        private ICommand changePasswordCommand;
        public ICommand ChangePasswordCommand
        {
            get
            {
                if (changePasswordCommand == null)
                {
                    changePasswordCommand = new RelayCommand(new Action<object>(ChangePassword));
                }
                return changePasswordCommand;
            }
            set
            {
                saveCommand = value;
                RaisedPropertyChanged("ChangePasswordCommand");
            }
        }
        public void ChangePassword(object Parameter)
        {
            if (oldPassword == user.Password)
            {
                if (newPassword == verifiedPassword)
                {
                    UsersRequest.ChangePassword(user.UserId, verifiedPassword);
                    MessageBox.Show("Пароль изменен", "Запись");
                }
                else
                    MessageBox.Show("Пароли не совпадают!", "Ошибка");
            }
            else
                MessageBox.Show("Не верно введен старый пароль!", "Ошибка");
        }
        #endregion
    }
}
