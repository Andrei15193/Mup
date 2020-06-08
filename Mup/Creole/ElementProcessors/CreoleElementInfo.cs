using Mup.Elements;

namespace Mup.Creole.ElementProcessors
{
    internal class CreoleElementInfo
    {
        internal CreoleElementInfo(int startIndex, int endIndex, Element element)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            Element = element;
        }

        internal int StartIndex { get; }

        internal int EndIndex { get; }

        internal Element Element { get; }
    }
}