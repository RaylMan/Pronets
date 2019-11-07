using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Pronets.Viev.MainWindows.Pages;
namespace Pronets.Navigation
{
    class PagesResolver : IPageResolver
    {

        private readonly Dictionary<string, Func<Page>> _pagesResolvers = new Dictionary<string, Func<Page>>();

        public PagesResolver()
        {
            _pagesResolvers.Add(Navigation.ReceiptDocumentPageAlias, () => new ReceiptDocumentPage());
            _pagesResolvers.Add(Navigation.ReceiptDocumentPagePronetsAlias, () => new ReceiptDocumentPagePronets());
            _pagesResolvers.Add(Navigation.RepairsPageAlias, () => new RepairsPage());
            _pagesResolvers.Add(Navigation.DefectsPageAlias, () => new DefectsPage());
            _pagesResolvers.Add(Navigation.EquipmentWindowAlias, () => new EquipmentWindow());
            _pagesResolvers.Add(Navigation.NotFoundPageAlias, () => new Page404());


        }

        public Page GetPageInstance(string alias)
        {
            if (_pagesResolvers.ContainsKey(alias))
            {
                return _pagesResolvers[alias]();
            }

            return _pagesResolvers[Navigation.NotFoundPageAlias]();
        }
    }
}

