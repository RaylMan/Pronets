﻿using Pronets.Data;
using Pronets.EntityRequests.Repairs_f;
using Pronets.Model;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Pronets.VievModel.Repairs_f
{
    class AllRepairsWindowVM : RepairsModel
    {
        #region Properties
        Dispatcher _dispatcher;
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
        private string loadingStatus;
        public string LoadingStatus
        {
            get { return loadingStatus; }
            set
            {
                loadingStatus = value;
                RaisedPropertyChanged("LoadingStatus");
            }
        }

        #endregion

        public AllRepairsWindowVM()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            GetRepairsAsync();
            OpenWindowCommand = new OpenWindowCommand();
        }
        public AllRepairsWindowVM(ObservableCollection<v_Repairs> repairs)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            V_Repairs = repairs;
        }
        private async void GetRepairsAsync()
        {
            LoadingStatus = "Выполняется загрузка данных в таблицу!";
            if (V_Repairs != null)
                V_Repairs.Clear();
            await Task.Run(() => GetRepairs());
            LoadingStatus = "Загрузка завершена";
        }
        private void GetRepairs()
        {
            try
            {
                foreach (var repair in RepairsRequest.v_FillList())
                {
                    _dispatcher.Invoke(new Action(() =>
                    {
                        V_Repairs.Add(repair);
                    }));
                }
            }
            catch (Exception)
            {
                LoadingStatus = "Отсутствует соединение с сервером";
            }
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
                string searchWord = IsChecked != true ? EditChars.ToEnglish(SearchText) : SearchText;
                searchPosition = 0;
                searchRepairs = V_Repairs.Where(r => r.Serial_Number.ToLower().Contains(searchWord.ToLower())).ToList();
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
                string searchWord = IsChecked != true ? EditChars.ToEnglish(SearchText) : SearchText;
                if (!string.IsNullOrWhiteSpace(searchWord) && searchRepairs[0].Serial_Number.ToLower().Contains(searchWord.ToLower()))
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
