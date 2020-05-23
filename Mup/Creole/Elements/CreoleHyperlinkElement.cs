using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleHyperlinkElement : CreoleElement
    {
        internal CreoleHyperlinkElement(string destination, IEnumerable<CreoleElement> content)
        {
            Destination = destination;
            Content = content;
        }

        internal string Destination { get; }

        internal IEnumerable<CreoleElement> Content { get; }

        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitHyperlinkBeginningAsync(Destination, cancellationToken).ConfigureAwait(false);
            foreach (var element in Content)
                await element.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitHyperlinkEndingAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}