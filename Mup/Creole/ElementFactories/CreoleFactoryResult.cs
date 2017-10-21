using Mup.Creole.Elements;

namespace Mup.Creole.ElementFactories
{
    internal class CreoleFactoryResult
    {
        internal CreoleFactoryResult(CreoleToken start, CreoleToken end, CreoleElement element)
        {
            Start = start;
            End = end;
            Element = element;
        }

        internal CreoleToken Start { get; }

        internal CreoleToken End { get; }

        internal CreoleElement Element { get; }
    }
}