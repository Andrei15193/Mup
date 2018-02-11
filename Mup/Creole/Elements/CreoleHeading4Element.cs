#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleHeading4Element : CreoleHeadingElement
    {
        internal CreoleHeading4Element(string text)
            : base(text)
        {
        }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHeading4Beginning();
            visitor.VisitText(Text);
            visitor.VisitHeading4Ending();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitHeading4BeginningAsync(cancellationToken).ConfigureAwait(false);
            await visitor.VisitTextAsync(Text, cancellationToken).ConfigureAwait(false);
            await visitor.VisitHeading4EndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}