using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleParagraphElement : CreoleElement
    {
        private readonly IEnumerable<CreoleElement> _richTextElements;

        public CreoleParagraphElement(IEnumerable<CreoleElement> richTextElements)
        {
            _richTextElements = richTextElements;
        }

        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitParagraphBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var richTextElement in _richTextElements)
                await richTextElement.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitParagraphEndingAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}