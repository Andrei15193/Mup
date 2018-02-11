using System.Collections.Generic;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleTableElement : CreoleElement
    {
        internal CreoleTableElement(IEnumerable<CreoleTableRowElement> rows)
            => Rows = rows;

        internal IEnumerable<CreoleTableRowElement> Rows { get; }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitTableBeginning();
            foreach (var row in Rows)
                row.Accept(visitor);
            visitor.VisitTableEnding();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitTableBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var row in Rows)
                await row.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitTableEndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}