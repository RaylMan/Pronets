using System.Windows.Controls;

namespace Pronets.Navigation
{
    public interface IPageResolver
    {
        Page GetPageInstance(string alias);
    }
}
