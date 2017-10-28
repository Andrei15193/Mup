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

        internal override CreoleFactoryResult TryCreateFrom(CreoleToken token)
        {
            CreoleFactoryResult result = null;

            if (_canCreate && token.Code == AngleOpen && token.Next?.Code == AngleOpen)
            {
                var start = token;
                var end = token.Next.Next;

                while (end != null && !(end.Code == AngleClose && end.Previous.Code == AngleClose && IsAtEndOfLine(end)))
                    end = end.Next;

                if (end == null)
                    _canCreate = false;
                else
                {
                    var pluginTextStartIndex = start.Next.Next.StartIndex;
                    var pluginTextEndIndex = end.Previous.Previous.EndIndex;
                    var pluginTextLength = (pluginTextEndIndex - pluginTextStartIndex);
                    var pluginText = Context.Text.Substring(pluginTextStartIndex, pluginTextLength);
                    result = new CreoleFactoryResult(start, end, new CreolePluginElement(pluginText));
                }
            }

            return result;
        }
    }
}