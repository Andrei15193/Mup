using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementFactories
{
    internal class CreolePluginElementFactory : CreoleElementFactory
    {
        private bool _canCreate = true;

        internal CreolePluginElementFactory(CreoleParserContext context)
            : base(context)
        {
        }

        internal override CreoleFactoryResult TryCreateFrom(CreoleToken start, CreoleToken end)
        {
            CreoleFactoryResult result = null;

            if (_canCreate && start.Code == AngleOpen && start.Next?.Code == AngleOpen)
            {
                var startToken = start;
                var endToken = start.Next.Next;

                while (endToken != end && !(endToken.Code == AngleClose && endToken.Previous.Code == AngleClose && IsAtEndOfLine(endToken)))
                    endToken = endToken.Next;

                if (endToken == end)
                    _canCreate = false;
                else
                {
                    var pluginTextStartIndex = startToken.Next.Next.StartIndex;
                    var pluginTextEndIndex = endToken.Previous.Previous.EndIndex;
                    var pluginTextLength = (pluginTextEndIndex - pluginTextStartIndex);
                    var pluginText = Context.Text.Substring(pluginTextStartIndex, pluginTextLength);
                    result = new CreoleFactoryResult(startToken, endToken, new CreolePluginElement(pluginText));
                }
            }

            return result;
        }
    }
}