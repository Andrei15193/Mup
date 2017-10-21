using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleTableElement : CreoleElement
    {
        internal CreoleTableElement(IEnumerable<CreoleTableRowElement> rows)
            => Rows = rows;

        internal IEnumerable<CreoleTableRowElement> Rows { get; }

        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitTableBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var row in Rows)
                await row.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitTableEndingAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}