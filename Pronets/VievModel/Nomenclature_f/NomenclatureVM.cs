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
        private Nomenclature selectedNomenclatureItem;
        public Nomenclature SelectedNomenclatureItem
        {
            get { return selectedNomenclatureItem; }
            set
            {
                selectedNomenclatureItem = value;
                RaisedPropertyChanged("SelectedNomenclatureItem");
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
        private Nomenclature_Types selectedNomenclature_type;
        public Nomenclature_Types SelectedNomenclature_type
        {
            get { return selectedNomenclature_type; }
            set
            {
                selectedNomenclature_type = value;
                if (selectedNomenclature_type != null)
                    Type = selectedNomenclature_type.Type;

                RaisedPropertyChanged("SelectedNomenclature_type");
            }
        }
        private string nomType;
        public string NomType
        {
            get { return nomType; }
            set
            {
                nomType = value;
                RaisedPropertyChanged("NomType");
            }
        }
        private int selectedTypeIndex;
        public int SelectedTypeIndex
        {
            get { return selectedTypeIndex; }
            set
            {
                selectedTypeIndex = value;
                RaisedPropertyChanged("SelectedTypeIndex");
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
            if (name != null && name != " " && name != "" && selectedNomenclature_type != null)
            {
                Nomenclature nom = new Nomenclature
                {
                    Name = this.Name,
                    Type = selectedNomenclature_type.Type,
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
            if (selectedNomenclatureItem != null)
            {
                var result = MessageBox.Show("Вы Действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    NomenclatureRequest.RemoveFromBase(SelectedNomenclatureItem, out bool ex);
                    if (ex)
                        nomenclature.RemoveAt(SelectedIndex);
                    Type = string.Empty;
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент в таблице!", "Ошибка");
        }
        #endregion

        #region AddToBase type
        private ICommand addTypeItem;
        public ICommand AddTypeCommand
        {
            get
            {
                if (addTypeItem == null)
                {
                    addTypeItem = new RelayCommand(new Action<object>(AddType));
                }
                return addTypeItem;
            }
            set
            {
                addTypeItem = value;
                RaisedPropertyChanged("AddTypeCommand");

            }
        }
        public void AddType(object Parameter)
        {
            if (!string.IsNullOrWhiteSpace(nomType))
            {
                Nomenclature_Types nt = new Nomenclature_Types
                {
                    Type = nomType
                };
                var result = MessageBox.Show("Вы Действительно хотете добавить?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Nomenclature_TypesRequest.AddToBase(nt, out bool ex);
                    if (ex) //если ex == true, нет копии в базе, происходит запись в таблицу viev
                    {
                        nomenclature_Types.Add(nt);
                    }
                    NomType = string.Empty;
                }
            }
            else
                MessageBox.Show("Необходимо ввести название!", "Ошибка");
        }
        #endregion

        #region Remove Type Item
        private ICommand removeTypeItem;
        public ICommand RemoveTypeCommand
        {
            get
            {
                if (removeTypeItem == null)
                {
                    removeTypeItem = new RelayCommand(new Action<object>(RemoveType));
                }
                return removeTypeItem;
            }
            set
            {
                removeItem = value;
                RaisedPropertyChanged("RemoveTypeCommand");
            }
        }

        public void RemoveType(object Parameter)
        {
            if (selectedNomenclature_type != null)
            {
                var result = MessageBox.Show("Вы Действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Nomenclature_TypesRequest.RemoveFromBase(selectedNomenclature_type, out bool ex);
                    if (ex)
                        nomenclature_Types.RemoveAt(SelectedTypeIndex);
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент в списке!", "Ошибка");
        }
        #endregion
    }
}
