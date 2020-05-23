using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleHorizontalRuleElement : CreoleElement
    {
        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => visitor.VisitHorizontalRuleAsync(cancellationToken);
    }
}