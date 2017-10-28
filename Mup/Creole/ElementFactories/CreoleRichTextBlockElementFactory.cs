using System.Collections.Generic;
using Mup.Creole.Elements;

namespace Mup.Creole.ElementFactories
{
    internal abstract class CreoleRichTextBlockElementFactory : CreoleElementFactory
    {
        internal CreoleRichTextBlockElementFactory(CreoleParserContext context)
            : base(context)
        {
        }

        protected IEnumerable<CreoleElement> CreateRichTextElementsFrom(CreoleToken start, CreoleToken end)
        {
            var creoleRichTextParser = new CreoleRichTextParser(Context);
            var richTextElements = creoleRichTextParser.Parse(start, end);
            return richTextElements;
        }
    }
}