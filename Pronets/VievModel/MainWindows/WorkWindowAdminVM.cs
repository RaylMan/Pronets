using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Pronets.Data;
using Pronets.VievModel;
using Pronets.VievModel.MainWindows.Pages;
using Pronets.Navigation.WindowsNavigation;

namespace Pronets.VievModel.MainWindows
{
    public class WorkWindowAdminVM : VievModelBase
    {
        public OpenWindowCommand OpenWindowCommand { get; private set; }
        public WorkWindowAdminVM()
        {
           
        }
        public WorkWindowAdminVM(IViewModelsResolver resolver)
        {
            OpenWindowCommand = new OpenWindowCommand();
            _resolver = resolver;
            defectsPageVM = _resolver.GetViewModelInstance(DefectsPageVMAlias);
            receiptDocumentPageVM = _resolver.GetViewModelInstance(ReceiptDocumentPageVMAlias);
            repairsPageVM = _resolver.GetViewModelInstance(RepairsPageVMAlias);
            addRecipeDocumentVM = _resolver.GetViewModelInstance(AddRecipeDocumentVMAlias);
            InitializeCommands();

        }
        #region Open Window
        
        #endregion

        #region Open Page
        #region Constants

        public static readonly string DefectsPageVMAlias = "DefectsPageVM";
        public static readonly string ReceiptDocumentPageVMAlias = "ReceiptDocumentPageVM";
        public static readonly string RepairsPageVMAlias = "RepairsPageVM";
        public static readonly string AddRecipeDocumentVMAlias = "AddRecipeDocument";
        public static readonly string NotFoundPageViewModelAlias = "404VM";

        #endregion

        #region Fields

        private readonly IViewModelsResolver _resolver;

        private ICommand goToReceiptDocumentCommand;

        private ICommand goToRepairsPageCommand;

        private ICommand goToDefectsCommand;

        private ICommand goToAddRecipeDocumentComand;

        private readonly INotifyPropertyChanged defectsPageVM;
        private readonly INotifyPropertyChanged receiptDocumentPageVM;
        private readonly INotifyPropertyChanged repairsPageVM;
        private readonly INotifyPropertyChanged addRecipeDocumentVM;

        private string userName;

        #endregion


        #region Properties

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                RaisedPropertyChanged("UserName");
            }
        }
        public ICommand GoToReceiptDocumentCommand
        {
            get { return goToReceiptDocumentCommand; }
            set
            {
                goToReceiptDocumentCommand = value;
                RaisedPropertyChanged("GoToReceiptDocumentCommand");
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
        public ICommand GoToAddRecipeDocumentComand
        {
            get
            {
                return goToAddRecipeDocumentComand;
            }
            set
            {
                goToAddRecipeDocumentComand = value;
                RaisedPropertyChanged("GoToAddRecipeDocumentComand");
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

        public INotifyPropertyChanged RepairsPageVM
        {
            get { return repairsPageVM; }
        }
        public INotifyPropertyChanged AddRecipeDocumentVM
        {
            get { return addRecipeDocumentVM; }
        }

        #endregion
        private void InitializeCommands()
        {

            GoToDefectsCommand = new RelayCommand<INotifyPropertyChanged>(GoToDefectsPageCommandExecute);

            GoToReceiptDocumentCommand = new RelayCommand<INotifyPropertyChanged>(GoToReceiptDocumentPageCommandExecute);

            GoToRepairsPageCommand = new RelayCommand<INotifyPropertyChanged>(GoToRepairsPageCommandExecute);

            GoToAddRecipeDocumentComand = new RelayCommand<INotifyPropertyChanged>(GoTOAddRecipeDocumentCommandExecute);
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

        private void GoToRepairsPageCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigation.Navigate(Navigation.Navigation.RepairsPageAlias, RepairsPageVM);
        }

        private void GoToDefectsPageCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigation.Navigate(Navigation.Navigation.DefectsPageAlias, DefectsPageVM);
        }
        public void GoTOAddRecipeDocumentCommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigation.Navigate(Navigation.Navigation.AddRecipeDocumentAlias, AddRecipeDocumentVM);
        }
        #endregion  
    }
}
