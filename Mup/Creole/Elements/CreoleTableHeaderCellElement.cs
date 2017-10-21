using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleTableHeaderCellElement : CreoleTableCellElement
    {
        internal CreoleTableHeaderCellElement(IEnumerable<CreoleElement> content)
            : base(content)
        {
        }

        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitTableHeaderCellBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var element in Content)
                await element.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitTableHeaderCellEndingAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}