using Pronets.Data;
using Pronets.EntityRequests.DefectiveStatements_f;
using Pronets.Viev.Other;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Pronets.VievModel.Other.DefectiveStatement_f
{
    public class DefectiveStatementWindowVM : VievModelBase
    {
        private Dispatcher dispatcher;
        private ObservableCollection<v_DefectiveStatements> defectiveStatements = new ObservableCollection<v_DefectiveStatements>();
        public ObservableCollection<v_DefectiveStatements> DefectiveStatements
        {
            get { return defectiveStatements; }
            set
            {
                defectiveStatements = value;
                RaisedPropertyChanged("DefectiveStatements");
            }
        }

        private v_DefectiveStatements selectedItem;
        public v_DefectiveStatements SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisedPropertyChanged("SelectedItem");
            }
        }
        public DefectiveStatementWindowVM()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            DefectiveStatements = DefectiveStatementsRequests.FillList();
        }
        public DefectiveStatementWindowVM(int documentId)
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            DefectiveStatements = DefectiveStatementsRequests.FillListFromDocument(documentId);
        }
        #region RemoveCommand
        private ICommand removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                {
                    removeCommand = new RelayCommand(new Action<object>(RemoveAsync));
                }
                return removeCommand;
            }
            set
            {
                removeCommand = value;
                RaisedPropertyChanged("RemoveCommand");
            }
        }
        public async void RemoveAsync(object Parameter)
        {
            await Task.Run(Remove);
        }
        private void Remove()
        {
            if (selectedItem == null) return;
            try
            {
                var result = MessageBox.Show("Вы действительно хотите удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DefectiveStatementsRequests.RemoveStatements(selectedItem);
                    dispatcher.Invoke(() => DefectiveStatements.Remove(selectedItem));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }
        #endregion

        #region OpenPrintingDocument
        private ICommand openPrintingDocument;
        public ICommand OpenPrintingDocument
        {
            get
            {
                if (openPrintingDocument == null)
                {
                    openPrintingDocument = new RelayCommand(new Action<object>(Open));
                }
                return openPrintingDocument;
            }
            set
            {
                openPrintingDocument = value;
                RaisedPropertyChanged("OpenPrintingDocument");
            }
        }
        public  void Open(object Parameter)
        {
            if (SelectedItem == null) return;
            try
            {
                var idsList = DefectiveStatementsRequests.GetRepairsId(SelectedItem.Id);
                var win = new PrintingWindow(idsList, SelectedItem.ClientId);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }
        #endregion
    }
}
