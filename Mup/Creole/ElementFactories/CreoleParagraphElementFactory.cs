using System.Collections.Generic;
using System.Linq;
using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementFactories
{
    internal class CreoleParagraphElementFactory : CreoleRichTextBlockElementFactory
    {
        internal CreoleParagraphElementFactory(string text, IEnumerable<string> inlineHyperlinkProtocols)
            : base(text, inlineHyperlinkProtocols)
        {
        }

        internal override CreoleFactoryResult TryCreateFrom(CreoleToken token)
        {
            CreoleFactoryResult result = null;

            if (token.Code != WhiteSpace)
            {
                var start = token;
                var end = token;

                while (end.Next != null && !FindLineFeeds(end.Next).Skip(1).Any())
                    end = end.Next;

                var richTextElements = CreateRichTextElementsFrom(start, end);
                var paragraphElement = new CreoleParagraphElement(richTextElements);
                result = new CreoleFactoryResult(start, (end.Next ?? end), paragraphElement);
            }

            return result;
        }
    }
}