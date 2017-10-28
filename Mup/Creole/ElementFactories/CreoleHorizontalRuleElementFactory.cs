using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementFactories
{
    internal class CreoleHorizontalRuleElementFactory : CreoleElementFactory
    {
        internal CreoleHorizontalRuleElementFactory(CreoleParserContext context)
            : base(context)
        {
        }

        internal override CreoleFactoryResult TryCreateFrom(CreoleToken start, CreoleToken end)
        {
            CreoleFactoryResult result = null;

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
                    result = new CreoleFactoryResult(startToken, endToken, new CreoleHorizontalRuleElement());
                }
            }

            return result;
        }
    }
}