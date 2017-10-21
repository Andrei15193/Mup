using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal sealed class CreoleLineBreakElement : CreoleElement
    {
        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => visitor.VisitLineBreakAsync(cancellationToken);
    }
}