using Mup.Scanner;

namespace Mup.Creole
{
    internal class CreoleToken : Token<CreoleTokenCode>
    {
        internal CreoleToken(CreoleTokenCode code, string text, int line, int column)
            : base(code, text, line, column)
        {
        }
    }
}