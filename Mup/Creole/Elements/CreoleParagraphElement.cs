using System.Collections.Generic;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleParagraphElement : CreoleElement
    {
        private readonly IEnumerable<CreoleElement> _richTextElements;

        public CreoleParagraphElement(IEnumerable<CreoleElement> richTextElements)
        {
            _richTextElements = richTextElements;
        }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitParagraphBeginning();
            foreach (var richTextElement in _richTextElements)
                richTextElement.Accept(visitor);
            visitor.VisitParagraphEnding();
        }
#endif

#if netstandard10
        internal override async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            await visitor.VisitParagraphBeginningAsync(cancellationToken).ConfigureAwait(false);
            foreach (var richTextElement in _richTextElements)
                await richTextElement.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            await visitor.VisitParagraphEndingAsync(cancellationToken).ConfigureAwait(false);
        }
#endif
    }
}