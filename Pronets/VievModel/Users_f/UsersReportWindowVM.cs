using DocumentFormat.OpenXml.Spreadsheet;
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
using System.Windows.Threading;

namespace Pronets.VievModel.Users_f
{
    class UsersReportWindowVM : VievModelBase
    {
        #region Properties
        Dispatcher dispatcher;
        private ObservableCollection<v_Repairs> v_Repairs = new ObservableCollection<v_Repairs>();

        private ObservableCollection<Engineers> engineers = new ObservableCollection<Engineers>();
        public ObservableCollection<Engineers> Engineers
        {
            get { return engineers; }
            set
            {
                engineers = value;
                RaisedPropertyChanged("Engineers");
            }
        }

        private string reportInfo;
        public string ReportInfo
        {
            get { return reportInfo; }
            set
            {
                reportInfo = value;
                RaisedPropertyChanged("ReportInfo");
            }
        }
        private DateTime firstDate;
        public DateTime FirstDate
        {
            get { return firstDate; }
            set
            {
                firstDate = value;
                if (firstDate > secondDate)
                    SecondDate = firstDate;
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
        #endregion
        public UsersReportWindowVM()
        {
            GetContent();
        }
        #region Methods
        private void GetContent()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            TextVisibility = Visibility.Visible;
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            FirstDate = month.AddMonths(-1); 
            SecondDate = month.AddDays(-1).AddHours(23);
            GetEngineersAsync();

        }
        private async void GetEngineersAsync()
        {
            Engineers.Clear();
            await Task.Run(() => GetEngineers());
            TextVisibility = Visibility.Hidden;
        }
        private void GetEngineers()
        {
            try
            {
                foreach (var item in UsersRequest.FillListEngineersWithRepairs())
                {
                    if (item.LastName != "admin" && item.LastName != "Не выбран")
                    {
                        dispatcher.Invoke(new Action(() =>
                        {
                            Engineers.Add(item);
                        }));
                    }
                }
            }
            catch (Exception) { }
        }
        #endregion

        #region Generate Report
        private ICommand generateReportCommand;
        public ICommand GenerateReportCommand
        {
            get
            {
                if (generateReportCommand == null)
                {
                    generateReportCommand = new RelayCommand(new Action<object>(GenerateReport));
                }
                return generateReportCommand;
            }
            set
            {
                generateReportCommand = value;
                RaisedPropertyChanged("GenerateReportCommand");
            }
        }

        public void GenerateReport(object parametr)
        {
            GenerateReport(FirstDate, SecondDate);
        }

        private void GenerateReport(DateTime fDate, DateTime sDate)
        {
            ReportInfo = null;
            v_Repairs.Clear();
            v_Repairs = RepairsRequest.v_FillListFromDate(fDate, sDate);

            ReportInfo = $"С {fDate.ToShortDateString()} по {sDate.ToShortDateString()}\nОбщее количество: {v_Repairs.Count} шт.\n";
            foreach (var item in Engineers)
            {
                if (item.IsChecked)
                {
                    ReportInfo += $"\n{item.LastName}, ";
                    //GenerateEngineerInfo(item.Id);
                    ReportInfo += item.GetRepairsCountInfo(fDate, sDate);
                }
            }
        }
        #endregion

        #region Generate Report This Month
        private ICommand generateReportThisMonthCommand;
        public ICommand GenerateReportThisMonthCommand
        {
            get
            {
                if (generateReportThisMonthCommand == null)
                {
                    generateReportThisMonthCommand = new RelayCommand(new Action<object>(GenerateReportThisMonth));
                }
                return generateReportThisMonthCommand;
            }
            set
            {
                generateReportThisMonthCommand = value;
                RaisedPropertyChanged("GenerateReportThisMonthCommand");
            }
        }

        public void GenerateReportThisMonth(object parametr)
        {
            DateTime fDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime SDate = DateTime.Now.Date.AddHours(23);
            GenerateReport(fDate, SDate);
        }
        #endregion
    }

}
    


