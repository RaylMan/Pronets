using Pronets.Data;
using Pronets.EntityRequests.Other;
using Pronets.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Pronets.VievModel.Other
{
    public class UserOvertimeWindowVM : VievModelBase
    {
        #region Properrties
        private Users user;
        private string head;
        public string Head
        {
            get { return head; }
            set
            {
                head = value;
                RaisedPropertyChanged("Head");
            }
        }
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
        private string hours;
        public string Hours
        {
            get { return hours; }
            set
            {
                hours = value;
                RaisedPropertyChanged("Hours");
            }
        }
        private bool day = true;
        public bool Day
        {
            get { return day; }
            set
            {
                day = value;
                RaisedPropertyChanged("Day");
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
        private bool onlyAmounted;
        public bool OnlyAmounted
        {
            get { return onlyAmounted; }
            set
            {
                onlyAmounted = value;

                GetOvertime(onlyAmounted);
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
        public UserOvertimeWindowVM(Users user)
        {
            if (user != null)
                this.user = user;
            head = $"Переработка {user.LastName}";
            date = DateTime.Now;
            pricePerDay = user.SalaryPerDay.ToString();// значение по умолчанию из настроек
            pricePerHour = user.SalaryPerHour.ToString(); // значение по умолчанию из настроек
            onlyAmounted = true;
            GetTimeHours();
            GetInfo();
            GetOvertime(onlyAmounted);
            GetAmount();//вывод информации о зарплате
        }

        private void GetOvertime(bool amount)
        {
            if (amount)
            {
                overtimeList.Clear();
                var query = OvertimeRequest.FillList(user.LastName, "Не оплачено");
                if (query != null)
                {
                    foreach (var item in query)
                    {
                        overtimeList.Add(item);
                    }
                }
            }
            else
            {
                overtimeList.Clear();
                var query = OvertimeRequest.FillList(user.LastName);
                if (query != null)
                {
                    foreach (var item in query)
                    {
                        overtimeList.Add(item);
                    }
                }
            }
        }

        #region Add to base
        private ICommand addToBaseCommand;
        public ICommand AddToBaseCommand
        {
            get
            {
                if (addToBaseCommand == null)
                {
                    addToBaseCommand = new RelayCommand(new Action<object>(AddToBase));
                }
                return addToBaseCommand;
            }
            set
            {
                addToBaseCommand = value;
                RaisedPropertyChanged("AddToBaseCommand");
            }
        }
        public void AddToBase(object Parameter)
        {
            OverTime overtime = new OverTime
            {
                LastName = user.LastName,
                Date = date,
                Hours = SelectedOvertimeHour != null ? SelectedOvertimeHour.Time : 0,
                Day = day == true ? 1 : 0,
                Status = "Не оплачено"

            };
            OvertimeRequest.AddToBase(overtime);
            GetOvertime(onlyAmounted);
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
                var result = MessageBox.Show("Вы действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    OvertimeRequest.RemoveFromBase(SelectedOvertime);
                    GetOvertime(onlyAmounted);
                }
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
                double hoursCount = 0, daysCount = 0;
                foreach (var overtime in overtimeList)
                {
                    if (overtime.Status == "Не оплачено")
                    {
                        hoursCount += (double)overtime.Hours;
                        daysCount += (double)overtime.Day;
                    }
                }
                Amount = $"Количество часов:   {hoursCount}\n" +
                         $"Количество выходных:   {daysCount}\n" +
                         $"Сумма за часы:   {hoursCount * user.SalaryPerHour} рублей\n" +
                         $"Сумма за выходные:   {daysCount * user.SalaryPerDay} рублей\n" +
                         $"Общая сумма:   {(hoursCount * user.SalaryPerHour) + (daysCount * user.SalaryPerDay)} рублей";
            }
        }

        public void GetAmount()
        {
            if (OvertimeList.Count > 0)
            {
                double hoursCount = 0, daysCount = 0;
                foreach (var overtime in overtimeList)
                {
                    if (overtime.Status == "Не оплачено")
                    {
                        hoursCount += (double)overtime.Hours;
                        daysCount += (double)overtime.Day;
                    }
                }
                Amount = $"Количество часов:   {hoursCount}\n" +
                         $"Количество выходных:   {daysCount}\n" +
                         $"Сумма за часы:   {hoursCount * user.SalaryPerHour} рублей\n" +
                         $"Сумма за выходные:   {daysCount * user.SalaryPerDay} рублей\n" +
                         $"Общая сумма:   {(hoursCount * user.SalaryPerHour) + (daysCount * user.SalaryPerDay)} рублей";
            }
        }
        #endregion
        private void GetInfo()
        {
            info = "Нельзя одновременно выбрать переработу за день и часы. Одно заменяет другое автоматически.\n" +
                "Дата по умолчанию, сегодняшняя.\n" +
                "Значение зарплаты по умолчанию можно изменить в настройках.";
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
    }
}
