using System.Collections.Generic;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleStrongElement : CreoleElement
    {
        internal CreoleStrongElement(IEnumerable<CreoleElement> children)
        {
            Children = children;
        }

        internal IEnumerable<CreoleElement> Children { get; }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitStrongBeginning();
            foreach (var child in Children)
                child.Accept(visitor);
            visitor.VisitStrongEnding();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitStrongBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var child in Children)
                await child.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitStrongEndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}