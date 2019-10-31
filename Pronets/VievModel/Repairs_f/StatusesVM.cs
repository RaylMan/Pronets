using Pronets.Data;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using Pronets.EntityRequests.Repairs_f;
using Pronets.EntityRequests;

namespace Pronets.VievModel.Repairs_f
{
    public class StatusesVM : VievModelBase
    {
        
        #region Properties
        private ObservableCollection<Statuses> statuses;
        public ObservableCollection<Statuses> Statuses
        {
            get { return this.statuses; }

            set
            {
                statuses = value;
                RaisedPropertyChanged("Statuses");
            }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisedPropertyChanged("Status");
            }
        }
        private Statuses selectedItem;
        public Statuses SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        #endregion
        public StatusesVM()
        {
            statuses = StatusesRequests.FillList();
        }

        #region Add To Base
        private ICommand addItem;
        public ICommand AddCommand
        {
            get
            {
                if (addItem == null)
                {
                    addItem = new RelayCommand(new Action<object>(AddType));
                }
                return addItem;
            }
            set
            {
                addItem = value;
                RaisedPropertyChanged("AddCommand");

            }
        }
        private void AddType(object Parameter)
        {
            if (status != null && status.Length > 0)
            {
                var result = MessageBox.Show("Вы действительно хотете добавить в базу?", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    StatusesRequests.AddToBase(status, out bool ex);
                    if (ex) //если ex == true, нет копии в базе, происходит запись в таблицу viev
                    {
                        Statuses.Add(new Statuses { Status = status });
                    }
                    Status = string.Empty;
                }
            }
            else
                MessageBox.Show("Необходимо ввести название!", "Ошибка");
        }
        #endregion

        #region Remove From Base
        private ICommand removeItem;
        public ICommand RemoveCommand
        {
            get
            {
                if (removeItem == null)
                {
                    removeItem = new RelayCommand(new Action<object>(RemoveItem));
                }
                return removeItem;
            }
            set
            {
                removeItem = value;
                RaisedPropertyChanged("RemoveCommand");
            }
        }
        private void RemoveItem(object Parameter)
        {
            if (selectedItem != null)
            {
                var result = MessageBox.Show("Вы действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    StatusesRequests.RemoveFromBase(SelectedItem, out bool ex);
                    if (ex)
                        Statuses.RemoveAt(selectedIndex);
                }

            }
            else
                MessageBox.Show("Необходимо выбрать элемент в списке!", "Ошибка");
        }
        #endregion
    }
}

