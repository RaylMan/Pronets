using Pronets.Data;
using Pronets.EntityRequests;
using Pronets.EntityRequests.Nomenclature_f;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pronets.VievModel.Repairs_f
{
    class AllRepairsWindowVM : RepairsModel
    {
        #region Properties

        public OpenWindowCommand OpenWindowCommand { get; set; }
        private v_Repairs selectedItem;
        private List<v_Repairs> searchRepairs = new List<v_Repairs>(); // список для поиска
        public v_Repairs SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                RaisedPropertyChanged("SearchText");
            }
        }
        #endregion

        public AllRepairsWindowVM()
        {
            v_Repairs = RepairsRequest.v_FillList();
            OpenWindowCommand = new OpenWindowCommand();
        }
        #region Search
        int searchCount = 0; // общеек количество совпадения поиска
        int searchPosition = 0; // номер элемента поиска для выделения строки(FindNext)
        protected ICommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(new Action<object>(Search));
                }
                return searchCommand;
            }
            set
            {
                searchCommand = value;
                RaisedPropertyChanged("SearchCommand");
            }
        }
        public void Search(object Parameter)
        {
            if (!String.IsNullOrWhiteSpace(SearchText))
            {
                searchPosition = 0;
                searchRepairs = V_Repairs.Where(r => r.Serial_Number.Contains(SearchText)).ToList();
                searchCount = searchRepairs.Count;
                if (searchRepairs.Count > 0)
                {
                    SelectedItem = (v_Repairs)searchRepairs[0];
                    searchPosition++;
                }
            }
        }

        #endregion
        #region Search next
        protected ICommand searchNextCommand;
        public ICommand SearchNextCommand
        {
            get
            {
                if (searchNextCommand == null)
                {
                    searchNextCommand = new RelayCommand(new Action<object>(SearchNext));
                }
                return searchNextCommand;
            }
            set
            {
                searchNextCommand = value;
                RaisedPropertyChanged("SearchNextCommand");
            }
        }
        public void SearchNext(object Parameter)
        {
            if (searchRepairs.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(SearchText) && searchRepairs[0].Serial_Number.Contains(SearchText))
                {
                    if (searchPosition < searchRepairs.Count)
                    {
                        SelectedItem = (v_Repairs)searchRepairs[searchPosition];
                        searchPosition++;
                    }
                    else if (searchPosition == searchRepairs.Count)
                    {
                        SelectedItem = (v_Repairs)searchRepairs[0];
                        searchPosition = 1;
                    }
                }
                else
                {
                    searchPosition = 0;
                    searchCount = 0;
                    searchRepairs.Clear();
                }
            }
        }
        #endregion
    }
}
