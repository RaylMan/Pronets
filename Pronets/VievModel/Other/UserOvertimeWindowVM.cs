using Pronets.Data;
using Pronets.EntityRequests.Other;
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

                if(onlyAmounted)
                {
                    overtimeList.Clear();
                    foreach (var item in OvertimeRequest.FillList(user.LastName, "Не оплачено"))
                    {
                        overtimeList.Add(item);
                    }
                }
                else
                {
                    overtimeList.Clear();
                    foreach (var item in OvertimeRequest.FillList(user.LastName))
                    {
                        overtimeList.Add(item);
                    }
                }
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
            pricePerDay = Properties.Settings.Default.PricePerDay.ToString();// значение по умолчанию из настроек
            pricePerHour = Properties.Settings.Default.PricePerHour.ToString(); // значение по умолчанию из настроек
            onlyAmounted = true; 
            GetAmount();//вывод информации о зарплате
            GetInfo();
            GetOvertime();
        }
        private void GetOvertime()
        {
            OvertimeList.Clear();
            OvertimeList = OvertimeRequest.FillList(user.LastName, "Не оплачено");
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

            if (int.TryParse(hours, out int numericHours))
            {
                OverTime overtime = new OverTime
                {
                    LastName = user.LastName,
                    Date = date,
                    Hours = numericHours,
                    Day = day == true ? 1 : 0,
                    Status = "Не оплачено"

                };
                OvertimeRequest.AddToBase(overtime);
                GetOvertime();
                GetAmount();
                
            }
            else
                MessageBox.Show("Введите в поле \"Количество часов\" цифру", "Ошибка");
            
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
                GetOvertime();
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
            if(OvertimeList.Count > 0)
            {
                int hoursCount = 0, daysCount = 0;
                foreach (var overtime in overtimeList)
                {
                    if (overtime.Status == "Не оплачено")
                    {
                        hoursCount += (int)overtime.Hours;
                        daysCount += (int)overtime.Day;
                    }
                }
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
                foreach (var overtime in overtimeList)
                {
                    if (overtime.Status == "Не оплачено")
                    {
                        hoursCount += (int)overtime.Hours;
                        daysCount += (int)overtime.Day;
                    }
                }
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
        private void GetInfo()
        {
            info = "Нельзя одновременно выбрать переработу за день и часы. Одно заменяет другое автоматически.\n" +
                "Дата по умолчанию, сегодняшняя.\n" +
                "Значение зарплаты по умолчанию можно изменить в настройках.";
        }
    }
}
