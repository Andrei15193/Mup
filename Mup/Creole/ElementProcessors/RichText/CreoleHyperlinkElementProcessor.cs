using Mup.Scanner;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal class CreoleHyperlinkElementProcessor : CreoleUrlElementProcessor
    {
        internal CreoleHyperlinkElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens)
            : base(context, tokens)
        {
        }

        internal CreoleHyperlinkElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens, CreoleRichTextElementProcessor baseElementProcessor)
            : base(context, tokens, baseElementProcessor)
        {
        }

        protected override CreoleRichTextElementType ElementType
            => CreoleRichTextElementType.Hyperlink;

        protected override CreoleTokenCode ElementOpenTokenCode
            => BracketOpen;

        protected override CreoleTokenCode ElementEndTokenCode
            => BracketClose;

    }
}