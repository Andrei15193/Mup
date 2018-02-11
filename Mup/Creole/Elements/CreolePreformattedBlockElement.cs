#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreolePreformattedBlockElement : CreoleElement
    {
        internal CreolePreformattedBlockElement(string preformattedText)
        {
            PreformattedText = preformattedText;
        }

        internal string PreformattedText { get; }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
            => visitor.VisitPreformattedBlock(PreformattedText);
#endif

#if netstandard10
        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => visitor.VisitPreformattedBlockAsync(PreformattedText, cancellationToken);
#endif
    }
}