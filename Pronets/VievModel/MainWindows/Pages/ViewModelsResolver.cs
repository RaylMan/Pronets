using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Pronets.VievModel.MainWindows.Pages
{
    public interface IViewModelsResolver
    {
        INotifyPropertyChanged GetViewModelInstance(string alias);
    }
    public class ViewModelsResolver : IViewModelsResolver
    {

        private readonly Dictionary<string, Func<INotifyPropertyChanged>> _vmResolvers = new Dictionary<string, Func<INotifyPropertyChanged>>();

        public ViewModelsResolver()
        {
            _vmResolvers.Add(WorkWindowAdminVM.DefectsPageVMAlias, () => new DefectsPageVM());
            _vmResolvers.Add(WorkWindowAdminVM.ReceiptDocumentPageVMAlias, () => new ReceiptDocumentPageVM());
            _vmResolvers.Add(WorkWindowAdminVM.RepairsPageVMAlias, () => new RepairsPageVM());
            _vmResolvers.Add(WorkWindowAdminVM.AddRecipeDocumentVMAlias, () => new AddRecipeDocumentVM());
            _vmResolvers.Add(WorkWindowAdminVM.NotFoundPageViewModelAlias, () => new Page404VM());
        }

        public INotifyPropertyChanged GetViewModelInstance(string alias)
        {
            if (_vmResolvers.ContainsKey(alias))
            {
                return _vmResolvers[alias]();
            }

            return _vmResolvers[WorkWindowAdminVM.NotFoundPageViewModelAlias]();
        }
    }
}
