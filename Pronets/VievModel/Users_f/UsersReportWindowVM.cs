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
using System.Windows.Input;

namespace Pronets.VievModel.Users_f
{
    class UsersReportWindowVM : VievModelBase
    {
        #region Properties
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
            private set
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
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            FirstDate = month.AddMonths(-1); //new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            SecondDate = month.AddDays(-1).AddHours(23);//DateTime.Now.Date.AddHours(23);
            GetEngineers();
        }
        private void GetEngineers()
        {
            Engineers.Clear();
            Engineers = UsersRequest.FillListEngineers();
            var deletableItems = Engineers.Where(item => item.LastName == "admin" || item.LastName == "Не выбран").ToList();
            foreach (var item in deletableItems)
            {
                Engineers.Remove(item);
            }
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
            ReportInfo = null;
            v_Repairs.Clear();
            v_Repairs = RepairsRequest.v_FillListFromDate(firstDate, secondDate);
            
            ReportInfo = $"С {FirstDate.ToShortDateString()} по {SecondDate.ToShortDateString()}\nОбщее количество: {v_Repairs.Count} шт.\n";
            foreach (var item in Engineers)
            {
                if (item.IsChecked)
                {
                    ReportInfo += $"\n{item.LastName}, всего: ";
                     GenerateEngineerInfo(item.Id);
                }
            }
        }
        private void GenerateEngineerInfo(int engineerId)
        {
            List<SortingRepair> list = new List<SortingRepair>();
            if (v_Repairs.Count > 0)
            {
                var allCount = from item in v_Repairs
                               where item.EngineerId == engineerId
                               select item;
                ReportInfo += $"{allCount.Count()}\n";

                var result = from item in v_Repairs
                             where item.EngineerId == engineerId
                             group item by new
                             {
                                 item.Repair_Category
                             }
                             into info
                             select new { info.Key.Repair_Category, Count = info.Count() };
                foreach (var info in result)
                {
                    ReportInfo += $"\t  {info.Repair_Category}: {info.Count} шт.\n";
                }
            }
        }
    }
    #endregion
}

