using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleBlankElement : CreoleElement
    {
        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => Task.FromResult<object>(null);
    }
}