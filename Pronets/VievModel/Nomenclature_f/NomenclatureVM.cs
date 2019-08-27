using Pronets.Data;
using Pronets.EntityRequests.Nomenclature_f;
using Pronets.Navigation.WindowsNavigation;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Nomenclature_f
{
    class NomenclatureVM : VievModelBase
    {
        #region Property
        public OpenWindowCommand OpenWindowCommand { get; private set; }
        private ObservableCollection<Nomenclature> nomenclature;
        public ObservableCollection<Nomenclature> Nomenclature
        {
            get { return this.nomenclature; }

            set
            {
                nomenclature = value;
                RaisedPropertyChanged("Nomenclature");
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisedPropertyChanged("Name");
            }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                RaisedPropertyChanged("Type");
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
        protected Nomenclature selectedItem;
        public Nomenclature SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }

        private ObservableCollection<Nomenclature_Types> nomenclature_Types;
        public ObservableCollection<Nomenclature_Types> Nomenclature_Types
        {
            get { return nomenclature_Types; }

            set
            {
                nomenclature_Types = value;
                RaisedPropertyChanged("Nomenclature_Types");
            }
        }
        private Nomenclature_Types selItem;
        public Nomenclature_Types SelItem
        {
            get { return selItem; }
            set
            {
                selItem = value;
                RaisedPropertyChanged("SelItem");
            }
        }

        #endregion
        public NomenclatureVM()
        {
            nomenclature = NomenclatureRequest.FillList();
            nomenclature_Types = Nomenclature_TypesRequest.FillList();
            OpenWindowCommand = new OpenWindowCommand();

        }

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
            if (name != null && name != " " && name != "" && selItem != null)
            {
                Nomenclature nom = new Nomenclature
                {
                    Name = this.Name,
                    Type = selItem.Type,
                    Price = this.Price
                };

                var result = MessageBox.Show("Вы Действительно хотете добавить?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    NomenclatureRequest.AddToBase(nom, out bool ex);
                    if (ex) //если ex == true, нет копии в базе, происходит запись в таблицу viev
                    {
                        nomenclature.Add(nom);
                    }
                    Name = string.Empty;
                    Price = 0;
                }
            }
            else
                MessageBox.Show("1) Введите название номенклатуры;\n2) Выберете тип оборудования.", "Ошибка");
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
            nomenclature.Clear();
            nomenclature = NomenclatureRequest.FillList();
        }
        #endregion

        #region FillComboBox
        protected ICommand fillComboItems;
        public ICommand FillComboBoxCommand
        {
            get
            {
                if (fillComboItems == null)
                {
                    fillComboItems = new RelayCommand(new Action<object>(FillComboBox));
                }
                return fillComboItems;
            }
            set
            {
                fillItems = value;
                RaisedPropertyChanged("FillComboBoxCommand");
            }
        }
        public void FillComboBox(object Parametr)
        {
            nomenclature_Types.Clear();
            nomenclature_Types = Nomenclature_TypesRequest.FillList();
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
                    NomenclatureRequest.RemoveFromBase(SelectedItem);
                    nomenclature.RemoveAt(SelectedIndex);
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент в таблице!", "Ошибка");
        }
        #endregion
    }
}
