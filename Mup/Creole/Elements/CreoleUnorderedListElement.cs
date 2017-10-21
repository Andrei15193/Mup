using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleUnorderedListElement : CreoleListElement
    {
        internal CreoleUnorderedListElement(IEnumerable<CreoleListItemElement> items)
            : base(items)
        {
        }

        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitUnorderedListBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var item in Items)
                await item.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitUnorderedListEndingAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}