using System.Collections.Generic;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleOrderedListElement : CreoleListElement
    {
        internal CreoleOrderedListElement(IEnumerable<CreoleListItemElement> items)
            : base(items)
        {
        }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitOrderedListBeginning();
            foreach (var item in Items)
                item.Accept(visitor);
            visitor.VisitOrderedListEnding();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitOrderedListBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var item in Items)
                await item.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitOrderedListEndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}