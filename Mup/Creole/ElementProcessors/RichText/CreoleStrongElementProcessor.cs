using Mup.Scanner;
using System.Collections.Generic;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal class CreoleStrongElementProcessor : CreoleStyleElementProcessor
    {
        internal CreoleStrongElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens)
            : base(context, tokens)
        {
        }

        internal CreoleStrongElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens, IEnumerable<CreoleRichTextElementData> baseElements)
            : base(context, tokens, baseElements)
        {
        }

        protected override CreoleTokenCode TokenCode { get; } = Asterisk;

        protected override CreoleRichTextElementType ElementType { get; } = CreoleRichTextElementType.Strong;
    }
}