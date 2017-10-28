using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementParsers
{
    internal class CreoleParagraphElementParser : CreoleRichTextBlockElementParser
    {
        internal CreoleParagraphElementParser(CreoleParserContext context)
            : base(context)
        {
        }

        internal override CreoleElementParserResult TryCreateFrom(CreoleToken start, CreoleToken end)
        {
            CreoleElementParserResult result = null;

            if (start.Code == WhiteSpace)
                start = start.Next;

            if (start != null)
            {
                if (end == null)
                {
                    end = start;
                    while (end.Next != null)
                        end = end.Next;
                }
                if (end.Code == WhiteSpace)
                    end = end.Previous;

                if (end != null && start.StartIndex <= end.StartIndex)
                {
                    var richTextElements = CreateRichTextElementsFrom(start, end);
                    var paragraphElement = new CreoleParagraphElement(richTextElements);
                    result = new CreoleElementParserResult(start, end, paragraphElement);
                }
            }

            return result;
        }
    }
}