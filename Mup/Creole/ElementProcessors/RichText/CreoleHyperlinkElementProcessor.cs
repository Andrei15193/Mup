using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal class CreoleHyperlinkElementProcessor : CreoleUrlElementProcessor
    {
        internal CreoleHyperlinkElementProcessor(CreoleParserContext context, CreoleTokenRange tokens)
            : base(context, tokens)
        {
        }

        internal CreoleHyperlinkElementProcessor(CreoleParserContext context, CreoleTokenRange tokens, CreoleRichTextElementProcessor baseElementProcessor)
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