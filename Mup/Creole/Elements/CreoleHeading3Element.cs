using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleHeading3Element : CreoleHeadingElement
    {
        internal CreoleHeading3Element(string text)
            : base(text)
        {
        }

        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitHeading3BeginningAsync(cancellationToken).ConfigureAwait(false);
            await visitor.VisitTextAsync(Text, cancellationToken).ConfigureAwait(false);
            await visitor.VisitHeading3EndingAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}