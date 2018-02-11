using Mup.Creole.Elements;
using static System.Math;
using static Mup.Creole.CreoleTokenCode;
using static Mup.Creole.CreoleTokenHelper;
#if net20
using static Mup.StringHelper;
#else
using static System.String;
#endif

namespace Mup.Creole.ElementParsers
{
    internal class CreoleHeadingElementParser : CreoleElementParser
    {
        internal CreoleHeadingElementParser(CreoleParserContext context)
            : base(context)
        {
        }

        internal override CreoleElementParserResult TryCreateFrom(CreoleToken start, CreoleToken end)
        {
            CreoleElementParserResult result = null;

            if (start.Code == Equal)
            {
                var startToken = start;
                var headingContentStart = startToken.Next;

                var headingLevel = 1;
                while (headingContentStart != end && headingContentStart.Code == Equal && headingLevel < 6)
                {
                    headingLevel++;
                    headingContentStart = headingContentStart.Next;
                }

                var endToken = headingContentStart;
                if (endToken != end && !(endToken.Code == WhiteSpace && ContainsLineFeed(endToken)))
                {
                    while (endToken.Next != end && !ContainsLineFeed(endToken.Next))
                        endToken = endToken.Next;

                    var headingText = _GetHeadingText(headingContentStart, endToken);
                    if (!IsNullOrWhiteSpace(headingText))
                    {
                        var headingElement = _GetHeadingElement(headingLevel, headingText);

                        result = new CreoleElementParserResult(startToken, (endToken.Next ?? endToken), headingElement);
                    }
                }
            }

            return result;
        }

        private CreoleElement _GetHeadingElement(int headingLevel, string headingText)
        {
            switch (headingLevel)
            {
                case 1:
                    return new CreoleHeading1Element(headingText);

                case 2:
                    return new CreoleHeading2Element(headingText);

                case 3:
                    return new CreoleHeading3Element(headingText);

                case 4:
                    return new CreoleHeading4Element(headingText);

                case 5:
                    return new CreoleHeading5Element(headingText);

                default:
                    return new CreoleHeading6Element(headingText);
            }
        }

        private static int _GetHeadingLevelFrom(CreoleToken startToken)
            => Min(startToken.Length, 6);

        private string _GetHeadingText(CreoleToken startToken, CreoleToken endToken)
        {
            var headerTextStart = _GetHeaderStart(startToken);
            var headerTextEnd = _GetHeaderEnd(endToken);
            var headerText = Substring(Context.Text, headerTextStart, headerTextEnd.Next);
            return headerText;
        }

        private static CreoleToken _GetHeaderStart(CreoleToken startToken)
            => (startToken.Code == WhiteSpace ? startToken.Next : startToken);

        private static CreoleToken _GetHeaderEnd(CreoleToken endToken)
        {
            var endContentToken = endToken;

            while (endContentToken.Code == Equal)
                endContentToken = endContentToken.Previous;
            if (endContentToken.Code == WhiteSpace)
                endContentToken = endContentToken.Previous;

            return endContentToken;
        }
    }
}