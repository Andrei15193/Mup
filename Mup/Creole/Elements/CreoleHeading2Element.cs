#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleHeading2Element : CreoleHeadingElement
    {
        internal CreoleHeading2Element(string text)
            : base(text)
        {
        }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHeading2Beginning();
            visitor.VisitText(Text);
            visitor.VisitHeading2Ending();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitHeading2BeginningAsync(cancellationToken).ConfigureAwait(false);
            await visitor.VisitTextAsync(Text, cancellationToken).ConfigureAwait(false);
            await visitor.VisitHeading2EndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}