using System.Collections.Generic;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleUnorderedListElement : CreoleListElement
    {
        internal CreoleUnorderedListElement(IEnumerable<CreoleListItemElement> items)
            : base(items)
        {
        }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitUnorderedListBeginning();
            foreach (var item in Items)
                item.Accept(visitor);
            visitor.VisitUnorderedListEnding();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitUnorderedListBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var item in Items)
                await item.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitUnorderedListEndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}