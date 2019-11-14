using Pronets.Data;
using Pronets.EntityRequests.Other;
using Pronets.EntityRequests.Users_f;
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
    public class OvertimeWindowVM : VievModelBase
    {
        #region Properties
        private ObservableCollection<OverTime> overtimeList = new ObservableCollection<OverTime>();
        public ObservableCollection<OverTime> OvertimeList
        {
            get { return overtimeList; }
            set
            {
                overtimeList = value;
                RaisedPropertyChanged("OvertimeList");
            }
        }
        private OverTime selectedOvertime;
        public OverTime SelectedOvertime
        {
            get { return selectedOvertime; }
            set
            {
                selectedOvertime = value;
                RaisedPropertyChanged("SelectedOvertime");
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
        private ObservableCollection<OvertimeStatuses> statuses = new ObservableCollection<OvertimeStatuses>();
        public ObservableCollection<OvertimeStatuses> Statuses
        {
            get { return statuses; }
            set
            {
                statuses = value;
                RaisedPropertyChanged("Statuses");
            }
        }
        private OvertimeStatuses selectedStatus;
        public OvertimeStatuses SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                RaisedPropertyChanged("SelectedStatus");
            }
        }
        private Users selectedUser;
        public Users SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                GetOvertimeList();
                GetAmount();
                RaisedPropertyChanged("SelectedUser");
            }
        }
       

        private string amount;
        public string Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                RaisedPropertyChanged("Amount");
            }
        }
        private string pricePerHour ;
        public string PricePerHour
        {
            get { return pricePerHour; }
            set
            {
                pricePerHour = value;
                RaisedPropertyChanged("PricePerHour");
            }
        }
        private string pricePerDay ;
        public string PricePerDay
        {
            get { return pricePerDay; }
            set
            {
                pricePerDay = value;
                RaisedPropertyChanged("PricePerDay");
            }
        }
        private bool onlyAmounted;
        public bool OnlyAmounted
        {
            get { return onlyAmounted; }
            set
            {
                onlyAmounted = value;
                GetOvertimeList();
                RaisedPropertyChanged("OnlyAmounted");
            }
        }
        private string info;
        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                RaisedPropertyChanged("Info");
            }
        }
        #endregion
        public OvertimeWindowVM()
        {
            Users = UsersRequest.FillList();
            Statuses.Add(new OvertimeStatuses { Status = "Оплачено" });
            Statuses.Add(new OvertimeStatuses { Status = "Не оплачено" });
            onlyAmounted = true;
        }
        private void GetOvertimeList()
        {
            if (SelectedUser != null)
            {
                if (onlyAmounted)
                {
                    OvertimeList.Clear();

                    foreach (var item in OvertimeRequest.FillList(SelectedUser.LastName, "Не оплачено"))
                    {
                        OvertimeList.Add(item);
                    }
                }
                else
                {
                    OvertimeList.Clear();
                    foreach (var item in OvertimeRequest.FillList(SelectedUser.LastName))
                    {
                        OvertimeList.Add(item);
                    }
                }
            }
        }

        #region Get status
        private ICommand getStatusCommand;
        public ICommand GetStatusCommand
        {
            get
            {
                if (getStatusCommand == null)
                {
                    getStatusCommand = new RelayCommand(new Action<object>(GetStatus));
                }
                return getStatusCommand;
            }
            set
            {
                getStatusCommand = value;
                RaisedPropertyChanged("GetStatusCommand");
            }
        }
        public void GetStatus(object Parameter)
        {
            if (SelectedStatus != null)
            {
                foreach (var item in OvertimeList)
                {
                    if (item.IsSelected == true)
                        OvertimeRequest.SetStatus(item.Id, SelectedStatus.Status);
                }
            }
            else
                MessageBox.Show("Необходимо вырать статус!", "Ошибка");
            GetOvertimeList();
            GetAmount();
        }
        #endregion

        #region Remove from base
        private ICommand removeFromBaseCommand;
        public ICommand RemoveFromBaseCommand
        {
            get
            {
                if (removeFromBaseCommand == null)
                {
                    removeFromBaseCommand = new RelayCommand(new Action<object>(RemoveFromBase));
                }
                return removeFromBaseCommand;
            }
            set
            {
                removeFromBaseCommand = value;
                RaisedPropertyChanged("RemoveFromBaseCommand");
            }
        }
        public void RemoveFromBase(object Parameter)
        {
            if (SelectedOvertime != null)
            {
                OvertimeRequest.RemoveFromBase(SelectedOvertime);
                OvertimeList.Remove(SelectedOvertime);
            }

        }
        #endregion

        #region Amount
        private ICommand amountCommand;
        public ICommand AmountCommand
        {
            get
            {
                if (amountCommand == null)
                {
                    amountCommand = new RelayCommand(new Action<object>(GetAmount));
                }
                return amountCommand;
            }
            set
            {
                amountCommand = value;
                RaisedPropertyChanged("AmountCommand");
            }
        }
        public void GetAmount(object Parameter)
        {
            if (OvertimeList.Count > 0)
            {
                int hoursCount = 0, daysCount = 0;
                foreach (var overtime in OvertimeList)
                {
                    if (overtime.Status == "Не оплачено")
                    {
                        hoursCount += (int)overtime.Hours;
                        daysCount += (int)overtime.Day;
                    }
                }
                if (string.IsNullOrWhiteSpace(pricePerDay))
                    PricePerDay = "0";
                if (string.IsNullOrWhiteSpace(pricePerHour))
                    PricePerHour = "0";
                if (int.TryParse(pricePerDay, out int numericPricePerDay) && int.TryParse(pricePerHour, out int numericPricePerHour))
                {
                    Amount = $"Количество часов:   {hoursCount}\n" +
                             $"Количество выходных:   {daysCount}\n" +
                             $"Сумма за часы:   {hoursCount * numericPricePerHour} рублей\n" +
                             $"Сумма за выходные:   {daysCount * numericPricePerDay} рублей\n" +
                             $"Общая сумма:   {(hoursCount * numericPricePerHour) + (daysCount * numericPricePerDay)} рублей";
                }
                else
                    MessageBox.Show("В полях \"Зарплата за час\" и \"Зарплата за день\" должны быть цифры!\n Если не хотите считать, установите \"0\"", "Ошибка");
            }

        }
        public void GetAmount()
        {
            if (OvertimeList.Count > 0)
            {
                int hoursCount = 0, daysCount = 0;
                foreach (var overtime in OvertimeList)
                {
                    if (overtime.Status == "Не оплачено")
                    {
                        hoursCount += (int)overtime.Hours;
                        daysCount += (int)overtime.Day;
                    }
                }
                if (string.IsNullOrWhiteSpace(pricePerDay))
                    PricePerDay = "0";
                if (string.IsNullOrWhiteSpace(pricePerHour))
                    PricePerHour = "0";
                if (int.TryParse(pricePerDay, out int numericPricePerDay) && int.TryParse(pricePerHour, out int numericPricePerHour))
                {
                    Amount = $"Количество часов:   {hoursCount}\n" +
                             $"Количество выходных:   {daysCount}\n" +
                             $"Сумма за часы:   {hoursCount * numericPricePerHour} рублей\n" +
                             $"Сумма за выходные:   {daysCount * numericPricePerDay} рублей\n" +
                             $"Общая сумма:   {(hoursCount * numericPricePerHour) + (daysCount * numericPricePerDay)} рублей";
                }
                else
                    MessageBox.Show("В полях \"Зарплата за час\" и \"Зарплата за день\" должны быть цифры!\n Если не хотите считать, установите \"0\"", "Ошибка");
            }

        }
        #endregion
    }
}
