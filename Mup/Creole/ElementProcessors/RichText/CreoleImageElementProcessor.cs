using System.Collections.Generic;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal class CreoleImageElementProcessor : CreoleUrlElementProcessor
    {
        internal CreoleImageElementProcessor(CreoleParserContext context, CreoleTokenRange tokens)
            : base(context, tokens)
        {
        }

        internal CreoleImageElementProcessor(CreoleParserContext context, CreoleTokenRange tokens, CreoleRichTextElementProcessor baseElementProcessor)
            : base(context, tokens, baseElementProcessor)
        {
        }

        protected override CreoleRichTextElementType ElementType
            => CreoleRichTextElementType.Image;

        protected override CreoleTokenCode ElementOpenTokenCode
            => BraceOpen;

        protected override CreoleTokenCode ElementEndTokenCode
            => BraceClose;
    }
}