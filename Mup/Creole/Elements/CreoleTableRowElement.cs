using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleTableRowElement : CreoleElement
    {
        internal CreoleTableRowElement(IEnumerable<CreoleTableCellElement> cells)
            => Cells = cells;

        internal IEnumerable<CreoleTableCellElement> Cells { get; }

        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitTableRowBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var cell in Cells)
                await cell.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitTableRowEndingAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}