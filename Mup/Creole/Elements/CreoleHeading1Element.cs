using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleHeading1Element : CreoleHeadingElement
    {
        internal CreoleHeading1Element(string text)
            : base(text)
        {
        }

        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitHeading1BeginningAsync(cancellationToken).ConfigureAwait(false);
            await visitor.VisitTextAsync(Text, cancellationToken).ConfigureAwait(false);
            await visitor.VisitHeading1EndingAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}