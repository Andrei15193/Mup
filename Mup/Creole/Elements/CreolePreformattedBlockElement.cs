using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreolePreformattedBlockElement : CreoleElement
    {
        internal CreolePreformattedBlockElement(string preformattedText)
        {
            PreformattedText = preformattedText;
        }

        internal string PreformattedText { get; }

        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => visitor.VisitPreformattedBlockAsync(PreformattedText, cancellationToken);
    }
}