using System.Collections.Generic;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleTableDataCellElement : CreoleTableCellElement
    {
        internal CreoleTableDataCellElement(IEnumerable<CreoleElement> content)
            : base(content)
        {
        }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitTableCellBeginning();
            foreach (var element in Content)
                element.Accept(visitor);
            visitor.VisitTableCellEnding();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitTableCellBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var element in Content)
                await element.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitTableCellEndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}