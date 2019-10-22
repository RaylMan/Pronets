using Pronets.Data;
using Pronets.EntityRequests.Repairs_f;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Repairs_f
{
    class RepairCategoriesVM : VievModelBase
    {
        public RepairCategoriesVM()
        {
            Repair_Categories = RepairCategoriesRequests.FillList();
        }

        #region Property
        private ObservableCollection<Repair_Categories> repair_Categories;
        public ObservableCollection<Repair_Categories> Repair_Categories
        {
            get { return this.repair_Categories; }

            set
            {
                repair_Categories = value;
                RaisedPropertyChanged("Repair_Categories");
            }
        }
        private string category;
        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                RaisedPropertyChanged("Category");
            }
        }
        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                RaisedPropertyChanged("Price");
            }
        }
        protected Repair_Categories selectedItem;
        public Repair_Categories SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        #endregion

        #region AddCommand
        private ICommand addItem;
        public ICommand AddCommand
        {
            get
            {
                if (addItem == null)
                {
                    addItem = new RelayCommand(new Action<object>(AddItem));
                }
                return addItem;
            }
            set
            {
                addItem = value;
                RaisedPropertyChanged("AddCommand");
            }
        }
        public void AddItem(object Parameter)
        {
            if (category != null && category != "" && category != " ")
            {
                Repair_Categories rc = new Repair_Categories
                {
                    Category = category,
                    Price = price
                };
                var result = MessageBox.Show("Вы Действительно хотете добавить?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    RepairCategoriesRequests.AddToBase(rc, out bool ex);
                    if (ex) //если ex == true, нет копии в базе, происходит запись в таблицу viev
                    {
                        Repair_Categories.Add(rc);
                    }
                    Category = string.Empty;
                    Price = 0;
                }
            }
            else
                MessageBox.Show("Необходимо заполнить все поля!", "Ошибка");
        }
        #endregion

        #region FillList button
        protected ICommand fillItems;
        public ICommand FillListCommand
        {
            get
            {
                if (fillItems == null)
                {
                    fillItems = new RelayCommand(new Action<object>(FillList));
                }
                return fillItems;
            }
            set
            {
                fillItems = value;
                RaisedPropertyChanged("FillListCommand");
            }
        }
        void FillList(object Parameter)
        {
            Repair_Categories.Clear();
            Repair_Categories = RepairCategoriesRequests.FillList();
        }
        #endregion

        #region RemoveCommand
        protected ICommand removeItem;
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
        public void RemoveItem(object Parameter)
        {
            if (selectedItem != null)
            {
                var result = MessageBox.Show("Вы Действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    RepairCategoriesRequests.RemoveFromBase(SelectedItem, out bool ex);
                    if (ex)
                        Repair_Categories.RemoveAt(SelectedIndex);
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент в таблице!", "Ошибка");
        }
        #endregion
    }
}
