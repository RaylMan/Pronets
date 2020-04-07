using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace Pronets.Model.Barcode
{
    public class Paginator : Document​Paginator
    {
        Visual[] pages;

        public Paginator(params Visual[] pages)
        {
            this.pages = pages;
            this.Source = new S() { DocumentPaginator = this };
        }

        public override bool IsPageCountValid => true;
        public override int PageCount => pages.Length;

        class S : IDocumentPaginatorSource
        {
            public DocumentPaginator DocumentPaginator { get; set; }
        }

        public override IDocumentPaginatorSource Source { get; }
        public override System.Windows.Size PageSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override DocumentPage GetPage(int pageNumber) =>
            new DocumentPage(pages[pageNumber]);
    }
}
