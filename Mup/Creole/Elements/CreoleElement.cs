using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal abstract class CreoleElement
    {
        internal abstract Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken);
    }
}