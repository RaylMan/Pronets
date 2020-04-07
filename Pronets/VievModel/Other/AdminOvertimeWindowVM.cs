using Pronets.Data;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Users_f;
using Pronets.Model;
using Pronets.Viev.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Other
{
    public class AdminOvertimeWindowVM : VievModelBase
    {
        #region Fields and properties
        OvertimeWindowVM baseWindowVM;
        private ObservableCollection<Data.Users> users = new ObservableCollection<Users>();
        public ObservableCollection<Users> Users
        {
            get { return users; }
            set
            {
                users = value;
                RaisedPropertyChanged("Users");
            }
        }
        private Users selectedUser;
        public Users SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                RaisedPropertyChanged("SelectedUser");
            }
        }
        private ObservableCollection<OvertimeHours> overtimeHoursList = new ObservableCollection<OvertimeHours>();
        public ObservableCollection<OvertimeHours> OvertimeHoursList
        {
            get { return overtimeHoursList; }
            set
            {
                overtimeHoursList = value;
                RaisedPropertyChanged("OvertimeHoursList");
            }
        }
        private OvertimeHours selectedOvertimeHour;
        public OvertimeHours SelectedOvertimeHour
        {
            get { return selectedOvertimeHour; }
            set
            {
                selectedOvertimeHour = value;
                RaisedPropertyChanged("SelectedOvertimeHour");
            }
        }
        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                RaisedPropertyChanged("Date");
            }
        }
        #endregion

        public AdminOvertimeWindowVM(OvertimeWindowVM vm)
        {
            baseWindowVM = vm;
            GetContent();
            GetTimeHours();
        }

        private void GetContent()
        {
            Users.Clear();
            Users = UsersRequest.FillList();
        }
        private void GetTimeHours()
        {
            double time = 0.5;
            while (time != 10.5)
            {
                overtimeHoursList.Add(new OvertimeHours { Time = time });
                time += 0.5;
            }
        }
        private ICommand writeCommand;
        public ICommand WriteCommand
        {
            get
            {
                if (writeCommand == null)
                {
                    writeCommand = new RelayCommand(new Action<object>(WriteToBase));
                }
                return writeCommand;
            }
            set
            {
                writeCommand = value;
                RaisedPropertyChanged("WriteCommand");
            }
        }
        private void WriteToBase(object parametr)
        {
            if (SelectedUser != null && SelectedOvertimeHour != null)
            {
                OverTime overtime = new OverTime
                {
                    LastName = SelectedUser.LastName,
                    Date = date,
                    Day = 0,
                    Hours = SelectedOvertimeHour.Time,
                    Status = "Не оплачено"
                };
                OvertimeRequest.AddToBase(overtime);
                baseWindowVM.SelectedUser = SelectedUser;
                baseWindowVM.GetOvertimeList();
                baseWindowVM.GetAmount();
            }
            else
                MessageBox.Show("Необходимо выбрать работника и время!", "Ошибка");
            
        }
    }
}
