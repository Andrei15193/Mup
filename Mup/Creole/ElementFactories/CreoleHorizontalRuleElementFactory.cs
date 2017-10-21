using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementFactories
{
    internal class CreoleHorizontalRuleElementFactory : CreoleElementFactory
    {
        internal CreoleHorizontalRuleElementFactory(string text)
            : base(text)
        {
        }

        internal override CreoleFactoryResult TryCreateFrom(CreoleToken token)
        {
            CreoleFactoryResult result = null;

            if (token.Code == Dash)
            {
                var dashCount = 1;
                var start = token;
                while (token.Next?.Code == Dash)
                {
                    dashCount++;
                    token = token.Next;
                }

                if (dashCount >= 4 && (token.Next == null || ContainsLineFeed(token.Next)))
                {
                    var end = token;
                    result = new CreoleFactoryResult(start, end, new CreoleHorizontalRuleElement());
                }
            }

            return result;
        }
    }
}