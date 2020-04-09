using Pronets.Data;
using Pronets.EntityRequests.Users_f;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pronets.VievModel.Users_f
{
    public class SelfUserReportWindowVM : VievModelBase
    {
        #region Properties
        private Engineers engineer;
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
        public SelfUserReportWindowVM(Users user)
        {
            engineer = UsersRequest.GetEngineer(user.LastName);
            FirstDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            SecondDate = DateTime.Now.Date.AddHours(23);
            GenerateReport(new object());
        }
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
            ReportInfo = null;
            ReportInfo = $"С {FirstDate.ToShortDateString()} по {SecondDate.ToShortDateString()}\n";
            ReportInfo += engineer.GetRepairsCountInfo(firstDate, secondDate);
        }
        #endregion

        #region Generate Report Last Month
        private ICommand generateLastMonthReportCommand;
        public ICommand GenerateLastMonthReportCommand
        {
            get
            {
                if (generateLastMonthReportCommand == null)
                {
                    generateLastMonthReportCommand = new RelayCommand(new Action<object>(GenerateLastMonthReport));
                }
                return generateLastMonthReportCommand;
            }
            set
            {
                generateLastMonthReportCommand = value;
                RaisedPropertyChanged("GenerateLastMonthReportCommand");
            }
        }

        public void GenerateLastMonthReport(object parametr)
        {
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var firstDateLastMonth = month.AddMonths(-1);
            var secondDateLastMonth = month.AddDays(-1).AddHours(23);
            ReportInfo = null;
            ReportInfo = $"С {firstDateLastMonth.ToShortDateString()} по {secondDateLastMonth.ToShortDateString()}\n";
            ReportInfo += engineer.GetRepairsCountInfo(firstDateLastMonth, secondDateLastMonth);
        }
        #endregion
    }

}

