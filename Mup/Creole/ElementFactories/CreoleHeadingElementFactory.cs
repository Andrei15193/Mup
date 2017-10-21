using Mup.Creole.Elements;
using static System.Math;
using static System.String;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementFactories
{
    internal class CreoleHeadingElementFactory : CreoleElementFactory
    {
        internal CreoleHeadingElementFactory(string text)
            : base(text)
        {
        }

        internal override CreoleFactoryResult TryCreateFrom(CreoleToken token)
        {
            CreoleFactoryResult result = null;

            if (token.Code == Equal)
            {
                var startToken = token;
                var headingContentStart = startToken.Next;

                var headingLevel = 1;
                while (headingContentStart != null && headingContentStart.Code == Equal && headingLevel < 6)
                {
                    headingLevel++;
                    headingContentStart = headingContentStart.Next;
                }

                var endToken = headingContentStart;
                if (endToken != null && !(endToken.Code == WhiteSpace && ContainsLineFeed(endToken)))
                {
                    while (endToken.Next != null && !ContainsLineFeed(endToken.Next))
                        endToken = endToken.Next;

                    var headingText = _GetHeadingText(headingContentStart, endToken);
                    if (!IsNullOrWhiteSpace(headingText))
                    {
                        var headingElement = _GetHeadingElement(headingLevel, headingText);

                        result = new CreoleFactoryResult(startToken, (endToken.Next ?? endToken), headingElement);
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
            var headerTextStartIndex = _GetHeaderStartIndex(startToken);
            var headerTextEndIndex = _GetHeaderEndIndex(endToken);
            var headerTextLength = (headerTextEndIndex - headerTextStartIndex);
            var headerText = Text.Substring(headerTextStartIndex, headerTextLength);
            return headerText;
        }

        private static int _GetHeaderStartIndex(CreoleToken startToken)
            => (startToken.Code == WhiteSpace ? startToken.Next.StartIndex : startToken.StartIndex);

        private int _GetHeaderEndIndex(CreoleToken endToken)
        {
            var endContentToken = endToken;

            while (endContentToken.Code == Equal)
                endContentToken = endContentToken.Previous;
            if (endContentToken.Code == WhiteSpace)
                endContentToken = endContentToken.Previous;

            return endContentToken.EndIndex;
        }
    }
}