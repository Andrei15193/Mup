using System.Collections.Generic;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

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

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHyperlinkBeginning(Destination);
            foreach (var element in Content)
                element.Accept(visitor);
            visitor.VisitHyperlinkEnding();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitHyperlinkBeginningAsync(Destination, cancellationToken).ConfigureAwait(false);
            foreach (var element in Content)
                await element.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitHyperlinkEndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}