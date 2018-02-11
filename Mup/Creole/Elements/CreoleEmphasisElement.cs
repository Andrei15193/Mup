using System.Collections.Generic;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleEmphasisElement : CreoleElement
    {
        internal CreoleEmphasisElement(IEnumerable<CreoleElement> children)
        {
            Children = children;
        }

        internal IEnumerable<CreoleElement> Children { get; }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitEmphasisBeginning();
            foreach (var child in Children)
                child.Accept(visitor);
            visitor.VisitEmphasisEnding();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitEmphasisBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var child in Children)
                await child.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitEmphasisEndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}