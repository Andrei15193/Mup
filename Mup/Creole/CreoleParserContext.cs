using System.Collections.Generic;

namespace Mup.Creole
{
    internal class CreoleParserContext
    {
        internal CreoleParserContext(IEnumerable<string> inlineHyperlinkProtocols)
        {
            InlineHyperlinkProtocols = inlineHyperlinkProtocols;
        }

        internal IEnumerable<string> InlineHyperlinkProtocols { get; }
    }
}