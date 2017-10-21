using Mup.Scanner;

namespace Mup.Creole
{
    internal class CreoleToken : Token<CreoleTokenCode>
    {
        internal CreoleToken(CreoleTokenCode code, int start, int length, CreoleToken previous = null)
            : base(code, start, length)
        {
            if (previous != null)
            {
                Previous = previous;
                previous.Next = this;
            }
        }

        internal CreoleToken Previous { get; }

        internal CreoleToken Next { get; private set; }
    }
}