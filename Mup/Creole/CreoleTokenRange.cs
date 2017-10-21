using Mup.Creole.Scanner;

namespace Mup.Creole
{
    internal class CreoleTokenRange
    {
        internal CreoleTokenRange(CreoleToken start, CreoleToken end)
        {
            Start = start;
            End = end;
        }

        internal CreoleToken Start { get; }

        internal CreoleToken End { get; }
    }
}