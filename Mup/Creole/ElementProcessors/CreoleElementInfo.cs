using Mup.Creole.Elements;

namespace Mup.Creole.ElementProcessors
{
    internal class CreoleElementInfo
    {
        internal CreoleElementInfo(int startIndex, int endIndex, CreoleElement element)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            Element = element;
        }

        internal int StartIndex { get; }

        internal int EndIndex { get; }

        internal CreoleElement Element { get; }
    }
}