﻿using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Pronets.Data;
using Pronets.VievModel;
using Pronets.VievModel.MainWindows.Pages;
using Pronets.Navigation.WindowsNavigation;
using System.Reflection;

namespace Pronets.VievModel.MainWindows
{
    public class WorkWindowAdminVM : VievModelBase
    {
        private string assemblyVersion;
        public OpenWindowCommand OpenWindowCommand { get; private set; }
        public WorkWindowAdminVM()
        {
            
        }
        public WorkWindowAdminVM(IViewModelsResolver resolver, Users user)
        {
            if (user != null)
                currentUser = user;
            OpenWindowCommand = new OpenWindowCommand();
            _resolver = resolver;
            defectsPageVM = _resolver.GetViewModelInstance(DefectsPageVMAlias);
            receiptDocumentPageVM = _resolver.GetViewModelInstance(ReceiptDocumentPageVMAlias);
            receiptDocumentPagePronetsVM = _resolver.GetViewModelInstance(ReceiptDocumentPagePronetsVMAlias);
            repairsPageVM = _resolver.GetViewModelInstance(RepairsPageVMAlias);
            equipmentWindowVM = _resolver.GetViewModelInstance(EquipmentWindowVMAlias);
            InitializeCommands();
            assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //GetDefaultUser();
        }

        private Users currentUser;
        public Users CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                RaisedPropertyChanged("CurrenUser");
            }
        }
        protected string userName;
        public string UserName
        {
            get
            {
                if (currentUser != null)
                    return currentUser.LastName + " " + currentUser.FirstName + "\tВерсия: " + assemblyVersion;
                return userName = "Error";
            }
            set
            {
                userName = value;
                RaisedPropertyChanged("UserName");
            }
        }


        #region Open Page
        #region Constants

        public static readonly string DefectsPageVMAlias = "DefectsPageVM";
        public static readonly string ReceiptDocumentPageVMAlias = "ReceiptDocumentPageVM";
        public static readonly string ReceiptDocumentPagePronetsVMAlias = "ReceiptDocumentPagePronetsVM";
        public static readonly string RepairsPageVMAlias = "RepairsPageVM";
        public static readonly string EquipmentWindowVMAlias = "EquipmentWindow";
        public static readonly string NotFoundPageViewModelAlias = "404VM";

        #endregion

        #region Fields

        private readonly IViewModelsResolver _resolver;

        private ICommand goToReceiptDocumentCommand;

        private ICommand goToReceiptDocumentPronetsCommand;

        private ICommand goToRepairsPageCommand;

        private ICommand goToDefectsCommand;

        private ICommand goToEquipmentWindowCommand;

        private readonly INotifyPropertyChanged defectsPageVM;
        private readonly INotifyPropertyChanged receiptDocumentPageVM;
        private readonly INotifyPropertyChanged receiptDocumentPagePronetsVM;
        private readonly INotifyPropertyChanged repairsPageVM;
        private readonly INotifyPropertyChanged equipmentWindowVM;
        
        #endregion


        #region Properties

        public ICommand GoToReceiptDocumentCommand
        {
            get { return goToReceiptDocumentCommand; }
            set
            {
                goToReceiptDocumentCommand = value;
                RaisedPropertyChanged("GoToReceiptDocumentCommand");
            }
        }
        public ICommand GoToReceiptDocumentPronetsCommand
        {
            get { return goToReceiptDocumentPronetsCommand; }
            set
            {
                goToReceiptDocumentPronetsCommand = value;
                RaisedPropertyChanged("GoToReceiptDocumentPronetsCommand");
            }
        }

        public ICommand GoToRepairsPageCommand
        {
            get
            {
                return goToRepairsPageCommand;
            }
            set
            {
                goToRepairsPageCommand = value;
                RaisedPropertyChanged("GoToRepairsPageCommand");
            }
        }

        public ICommand GoToDefectsCommand
        {
            get { return goToDefectsCommand; }
            set
            {
                goToDefectsCommand = value;
                RaisedPropertyChanged("GoToDefectsCommand");
            }
        }
        public ICommand GoToEquipmentWindowCommand
        {
            get
            {
                return goToEquipmentWindowCommand;
            }
            set
            {
                goToEquipmentWindowCommand = value;
                RaisedPropertyChanged("GoToEquipmentWindowCommand");
            }
        }

        public INotifyPropertyChanged DefectsPageVM
        {
            get { return defectsPageVM; }
        }

        public INotifyPropertyChanged ReceiptDocumentPageVM
        {
            get { return receiptDocumentPageVM; }
        }
        public INotifyPropertyChanged ReceiptDocumentPagePronetsVM
        {
            get { return receiptDocumentPagePronetsVM; }
        }

        public INotifyPropertyChanged RepairsPageVM
        {
            get { return repairsPageVM; }
        }
        public INotifyPropertyChanged EquipmentWindowVM
        {
            get { return equipmentWindowVM; }
        }

        #endregion
        private void InitializeCommands()
        {

            GoToDefectsCommand = new RelayCommand<INotifyPropertyChanged>(GoToDefectsPageCommandExecute);

            GoToReceiptDocumentCommand = new RelayCommand<INotifyPropertyChanged>(GoToReceiptDocumentPageCommandExecute);

            GoToReceiptDocumentPronetsCommand = new RelayCommand<INotifyPropertyChanged>(GoToReceiptDocumentPagePronetsCommandExecute);

            GoToRepairsPageCommand = new RelayCommand<INotifyPropertyChanged>(GoToRepairsPageCommandExecute);

            GoToEquipmentWindowCommand = new RelayCommand<INotifyPropertyChanged>(GoTOEquipmentWindowCommandExecute);
        }

        private void GoToPathCommandExecute(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                return;
            }

            Navigation.Navigation.Navigate(alias);
        }

        private void GoToReceiptDocumentPageCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigation.Navigate(Navigation.Navigation.ReceiptDocumentPageAlias, ReceiptDocumentPageVM);
        }
        private void GoToReceiptDocumentPagePronetsCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigation.Navigate(Navigation.Navigation.ReceiptDocumentPagePronetsAlias, ReceiptDocumentPagePronetsVM);
        }

        private void GoToRepairsPageCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigation.Navigate(Navigation.Navigation.RepairsPageAlias, RepairsPageVM);
        }

        private void GoToDefectsPageCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigation.Navigate(Navigation.Navigation.DefectsPageAlias, DefectsPageVM);
        }
        public void GoTOEquipmentWindowCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigation.Navigate(Navigation.Navigation.EquipmentWindowAlias, EquipmentWindowVM);
        }
        #endregion
        /// <summary>
        /// Установка значений по умолчанию DefaultLastName и DefaultUserId в
        /// соответствии с пользователем который произвел логин, для установки 
        /// по умолчанию значений в других окнах
        /// </summary>
        //private void GetDefaultUser()
        //{
        //    if(currentUser != null)
        //    {
        //        Properties.Settings.Default.DefaultLastName = currentUser.LastName;
        //        Properties.Settings.Default.DefaultUserId = currentUser.UserId;
        //        Properties.Settings.Default.Save();
        //    }
        //}
    }
}
