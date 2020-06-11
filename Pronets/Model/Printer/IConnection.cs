using Zebra.Sdk.Comm;

namespace Pronets.Model.Printer
{
    public interface IConnection
    {
        Connection GetConnection { get; }
    }
}
