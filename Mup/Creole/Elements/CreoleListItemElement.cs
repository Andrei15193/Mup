using System.Collections.Generic;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleListItemElement : CreoleElement
    {
        internal CreoleListItemElement(IEnumerable<CreoleElement> content)
        {
            Content = content;
        }

        internal IEnumerable<CreoleElement> Content { get; }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitListItemBeginning();
            foreach (var element in Content)
                element.Accept(visitor);
            visitor.VisitListItemEnding();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitListItemBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var element in Content)
                await element.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitListItemEndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}