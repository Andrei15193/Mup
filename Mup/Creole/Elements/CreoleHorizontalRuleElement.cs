#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleHorizontalRuleElement : CreoleElement
    {
#if net20
        internal override void Accept(ParseTreeVisitor visitor)
            => visitor.VisitHorizontalRule();
#endif

#if netstandard10
        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => visitor.VisitHorizontalRuleAsync(cancellationToken);
#endif
    }
}