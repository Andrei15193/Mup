using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementParsers
{
    internal class CreoleHorizontalRuleElementParser : CreoleElementParser
    {
        internal CreoleHorizontalRuleElementParser(CreoleParserContext context)
            : base(context)
        {
        }

        internal override CreoleElementParserResult TryCreateFrom(CreoleToken start, CreoleToken end)
        {
            CreoleElementParserResult result = null;

            if (start.Code == Dash)
            {
                var dashCount = 1;
                var startToken = start;
                while (start.Next != end && start.Next.Code == Dash)
                {
                    dashCount++;
                    start = start.Next;
                }

                if (dashCount >= 4 && (start.Next == end || ContainsLineFeed(start.Next)))
                {
                    var endToken = start;
                    result = new CreoleElementParserResult(startToken, endToken, new CreoleHorizontalRuleElement());
                }
            }

            return result;
        }
    }
}