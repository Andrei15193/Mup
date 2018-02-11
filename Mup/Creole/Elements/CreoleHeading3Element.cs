#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleHeading3Element : CreoleHeadingElement
    {
        internal CreoleHeading3Element(string text)
            : base(text)
        {
        }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHeading3Beginning();
            visitor.VisitText(Text);
            visitor.VisitHeading3Ending();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitHeading3BeginningAsync(cancellationToken).ConfigureAwait(false);
            await visitor.VisitTextAsync(Text, cancellationToken).ConfigureAwait(false);
            await visitor.VisitHeading3EndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}