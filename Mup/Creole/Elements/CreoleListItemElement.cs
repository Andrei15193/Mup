using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleListItemElement : CreoleElement
    {
        internal CreoleListItemElement(IEnumerable<CreoleElement> content)
        {
            Content = content;
        }

        internal IEnumerable<CreoleElement> Content { get; }

        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitListItemBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var element in Content)
                await element.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitListItemEndingAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}