using Mup.Scanner;
using System.Collections.Generic;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal class CreoleEmphasisElementProcessor : CreoleStyleElementProcessor
    {
        internal CreoleEmphasisElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens)
            : base(context, tokens)
        {
        }

        internal CreoleEmphasisElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens, IEnumerable<CreoleRichTextElementData> baseElements)
            : base(context, tokens, baseElements)
        {
        }

        protected override CreoleTokenCode TokenCode { get; } = Slash;

        protected override CreoleRichTextElementType ElementType { get; } = CreoleRichTextElementType.Emphasis;
    }
}