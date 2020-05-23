using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleEmphasisElement : CreoleElement
    {
        internal CreoleEmphasisElement(IEnumerable<CreoleElement> children)
        {
            Children = children;
        }

        internal IEnumerable<CreoleElement> Children { get; }

        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitEmphasisBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var child in Children)
                await child.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitEmphasisEndingAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}