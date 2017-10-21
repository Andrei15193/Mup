using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleHeading6Element : CreoleHeadingElement
    {
        internal CreoleHeading6Element(string text)
            : base(text)
        {
        }

        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitHeading6BeginningAsync(cancellationToken).ConfigureAwait(false);
            await visitor.VisitTextAsync(Text, cancellationToken).ConfigureAwait(false);
            await visitor.VisitHeading6EndingAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}