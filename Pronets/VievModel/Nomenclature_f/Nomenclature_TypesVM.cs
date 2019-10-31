using Pronets.Data;
using Pronets.EntityRequests.Nomenclature_f;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Nomenclature_f
{
    public class Nomenclature_TypesVM : VievModelBase
    {
        #region Properties
        private ObservableCollection<Nomenclature_Types> nomenclatures_Types;
        public ObservableCollection<Nomenclature_Types> Nomenclatures_Types
        {
            get { return this.nomenclatures_Types; }

            set
            {
                nomenclatures_Types = value;
                RaisedPropertyChanged("Nomenclatures_Types");
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
        private Nomenclature_Types selectedItem;
        public Nomenclature_Types SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        #endregion

        public Nomenclature_TypesVM()
        {
            nomenclatures_Types = Nomenclature_TypesRequest.FillList();
        }

        #region AddToBase
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
        public void AddType(object Parameter)
        {
            if (nomType != null && nomType.Length > 0)
            {
                Nomenclature_Types nt = new Nomenclature_Types
                {
                    Type = nomType
                };
                var result = MessageBox.Show("Вы действительно хотете добавить?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Nomenclature_TypesRequest.AddToBase(nt, out bool ex);
                    if (ex) //если ex == true, нет копии в базе, происходит запись в таблицу viev
                    {
                        Nomenclatures_Types.Add(nt);
                    }
                    NomType = string.Empty;
                }
            }
            else
                MessageBox.Show("Необходимо ввести название!", "Ошибка");
        }
        #endregion

        #region Remove Item
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

        public void RemoveItem(object Parameter)
        {
            if (selectedItem != null)
            {
                var result = MessageBox.Show("Вы действительно хотете удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Nomenclature_TypesRequest.RemoveFromBase(SelectedItem, out bool ex);
                    if (ex)
                        Nomenclatures_Types.RemoveAt(SelectedIndex);
                }
            }
            else
                MessageBox.Show("Необходимо выбрать элемент в списке!", "Ошибка");
        }
        #endregion
    }
}
