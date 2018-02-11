using System.Collections.Generic;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleTableRowElement : CreoleElement
    {
        internal CreoleTableRowElement(IEnumerable<CreoleTableCellElement> cells)
            => Cells = cells;

        internal IEnumerable<CreoleTableCellElement> Cells { get; }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitTableRowBeginning();
            foreach (var cell in Cells)
                cell.Accept(visitor);
            visitor.VisitTableRowEnding();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitTableRowBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var cell in Cells)
                await cell.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitTableRowEndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}