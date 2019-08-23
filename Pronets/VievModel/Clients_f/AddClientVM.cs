using Pronets.Data;
using Pronets.EntityRequests.Clients_f;
using System;
using System.Windows;
using System.Windows.Input;

namespace Pronets.VievModel.Clients_f
{
    class AddClientVM : ClientsVM
    {
        public AddClientVM()
        {
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
            Clients client = null;
            if (clientName != null && clientName != "" && clientName != " ")
            {
                client = new Clients
                {
                    ClientName = base.ClientName,
                    Inn = base.Inn,
                    Contact_Person = base.Contact_Person,
                    Telephone_1 = base.Telephone_1,
                    Telephone_2 = base.Telephone_2,
                    Telephone_3 = base.Telephone_3,
                    Email = base.Email,
                    Adress = base.Adress
                };

                var result = MessageBox.Show("Вы Действительно хотете добавить клиента?\nПроверьте правильность данных!", "Создание экземпляра", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (client != null)
                    {
                        ClientsRequests.AddToBase(client);
                        ClientName = string.Empty;
                        Inn = string.Empty;
                        Contact_Person = string.Empty;
                        Telephone_1 = string.Empty;
                        Telephone_2 = string.Empty;
                        Telephone_3 = string.Empty;
                        Email = string.Empty;
                        Adress = string.Empty;
                    }

                }
            }
            else
                MessageBox.Show("Необходимо ввести Название организации", "Сошибка");
            
        }
        #endregion
    }
}
