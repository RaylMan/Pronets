using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Pronets.Model
{
    public class FittedDocumentPaginator : DocumentPaginator
    {
        public DocumentPaginator Base { get; private set; }
        public double Scale { get; private set; }
        private readonly ScaleTransform _sTransform;

        public FittedDocumentPaginator(DocumentPaginator baseDp, double scale)
        {
            if (baseDp == null)
                throw new ArgumentNullException("baseDp");

            Base = baseDp;
            Scale = scale;
            _sTransform = new ScaleTransform(scale, scale);
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            var page = Base.GetPage(pageNumber);
            ((ContainerVisual)page.Visual).Transform = _sTransform;

            return page;
        }

        public override bool IsPageCountValid
        {
            get { return Base.IsPageCountValid; }
        }

        public override int PageCount
        {
            get { return Base.PageCount; }
        }

        public override Size PageSize
        {
            get { return Base.PageSize; }
            set { Base.PageSize = value; }
        }

        public override IDocumentPaginatorSource Source
        {
            get { return Base.Source; }
        }
    }
}
