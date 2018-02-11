#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleHeading6Element : CreoleHeadingElement
    {
        internal CreoleHeading6Element(string text)
            : base(text)
        {
        }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHeading6Beginning();
            visitor.VisitText(Text);
            visitor.VisitHeading6Ending();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitHeading6BeginningAsync(cancellationToken).ConfigureAwait(false);
            await visitor.VisitTextAsync(Text, cancellationToken).ConfigureAwait(false);
            await visitor.VisitHeading6EndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}