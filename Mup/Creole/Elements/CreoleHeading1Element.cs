#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleHeading1Element : CreoleHeadingElement
    {
        internal CreoleHeading1Element(string text)
            : base(text)
        {
        }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHeading1Beginning();
            visitor.VisitText(Text);
            visitor.VisitHeading1Ending();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitHeading1BeginningAsync(cancellationToken).ConfigureAwait(false);
            await visitor.VisitTextAsync(Text, cancellationToken).ConfigureAwait(false);
            await visitor.VisitHeading1EndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}