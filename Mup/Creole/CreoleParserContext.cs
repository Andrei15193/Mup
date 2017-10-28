using System.Collections.Generic;

namespace Mup.Creole
{
    internal class CreoleParserContext
    {
        internal CreoleParserContext(string text, IEnumerable<string> inlineHyperlinkProtocols)
        {
            Text = text;
            InlineHyperlinkProtocols = inlineHyperlinkProtocols;
        }

        internal string Text { get; }

        internal IEnumerable<string> InlineHyperlinkProtocols { get; }
    }
}