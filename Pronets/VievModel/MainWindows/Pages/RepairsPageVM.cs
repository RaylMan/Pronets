using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pronets.VievModel.MainWindows.Pages
{
    class RepairsPageVM : RepairsModel
    {
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
        public RepairsPageVM()
        {

        }
        #region Search Command
        protected ICommand searchItem;
        public ICommand SearchCommand
        {
            get
            {
                if (searchItem == null)
                {
                    searchItem = new RelayCommand(new Action<object>(SearchItem));
                }
                return searchItem;
            }
            set
            {
                searchItem = value;
                RaisedPropertyChanged("SearchCommand");
            }
        }

        public void SearchItem(object Parameter)
        {
            if (SearchText != null && SearchText != "")
            {
                repairs.Clear();
                foreach (var repair in RepairsRequest.SearchItem(searchText))
                {
                    repairs.Add(repair);
                }
                SearchText = string.Empty;
            }
        }
        #endregion
    }
}
