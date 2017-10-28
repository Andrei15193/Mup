using System.Linq;
using System.Text;
using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementFactories
{
    internal class CreolePreformattedBlockElementFactory : CreoleElementFactory
    {
        private bool _canCreate = true;

        internal CreolePreformattedBlockElementFactory(CreoleParserContext context)
            : base(context)
        {
        }

        internal override CreoleFactoryResult TryCreateFrom(CreoleToken token)
        {
            CreoleFactoryResult result = null;

            if (_canCreate && _IsBeginningOfPreformattedBlock(token))
            {
                var start = token;
                var end = token.Next.Next.Next;

                while (end != null && !_IsEndingOfPreformattedBlock(end))
                    end = end.Next;

                if (end != null)
                {
                    var preformattedText = _GetPreformattedText(start.Next.Next, end.Previous.Previous);
                    var preformattedElement = new CreolePreformattedBlockElement(preformattedText);
                    result = new CreoleFactoryResult(start, end, preformattedElement);
                }
                else
                    _canCreate = false;
            }

            return result;
        }

        private bool _IsBeginningOfPreformattedBlock(CreoleToken token)
            => (IsAtBeginningOfLine(token)
                && token.Code == BraceOpen
                && token.Next?.Code == BraceOpen
                && token.Next?.Next?.Code == BraceOpen
                && IsAtEndOfLine(token.Next.Next));

        private bool _IsEndingOfPreformattedBlock(CreoleToken token)
            => (IsAtEndOfLine(token)
                && token.Code == BraceClose
                && token.Previous?.Code == BraceClose
                && token.Previous?.Previous?.Code == BraceClose
                && IsAtBeginningOfLine(token.Previous.Previous));

        private string _GetPreformattedText(CreoleToken start, CreoleToken end)
        {
            if (start.Next == end)
                return string.Empty;

            var preformattedTextStartIndex = FindLineFeeds(start.Next).First().Index;
            var preformattedTextEndIndex = FindLineFeeds(end.Previous).Last().Index;
            if (preformattedTextStartIndex == preformattedTextEndIndex)
                return string.Empty;
            else if (start.Next.Next == end)
            {
                var preformattedTextLength = (preformattedTextEndIndex - preformattedTextStartIndex);
                var preformattedText = Context.Text.Substring(preformattedTextStartIndex, preformattedTextLength);
                return preformattedText;
            }
            else
            {
                var preformattedTextBuilder = new StringBuilder();
                foreach (var token in CreoleTokenHelper.Enumerate(start.Next, end))
                    if (!(token.Code == Tilde && IsAtBeginningOfLine(token) && token.Length == 1
                        && token.Next?.Code == BraceClose && token.Next.Length == 3 && IsAtEndOfLine(token.Next)))
                        preformattedTextBuilder.Append(Context.Text, token.StartIndex, token.Length);
                _TrimUntilNewLines(preformattedTextBuilder);

                var preformattedText = preformattedTextBuilder.ToString();
                return preformattedText;
            }
        }

        private void _TrimUntilNewLines(StringBuilder stringBuilder)
        {
            var index = (stringBuilder.Length - 1);
            while (index >= 0 && stringBuilder[index] != '\n')
                index--;
            if (index >= 0)
            {
                stringBuilder.Remove(index, (stringBuilder.Length - index));
                index = 1;
                while (index < stringBuilder.Length && stringBuilder[index - 1] != '\n')
                    index++;
                stringBuilder.Remove(0, index);
            }
        }

        private int _IndexOfNextLineFeed(int startIndex)
        {
            var index = startIndex;
            while (Context.Text[index] != '\n')
                index++;
            return index;
        }

        private int _IndexOfPreviousLineFeed(int startIndex)
        {
            var index = startIndex;
            while (Context.Text[index] != '\n')
                index--;
            return index;
        }
    }
}