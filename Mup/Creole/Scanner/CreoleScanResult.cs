using System.Collections.Generic;

namespace Mup.Creole.Scanner
{
    internal class CreoleScanResult
    {
        internal CreoleScanResult(string text, IEnumerable<CreoleToken> tokens)
        {
            Text = text;
            Tokens = tokens;
        }

        internal string Text { get; }

        internal IEnumerable<CreoleToken> Tokens { get; }
    }
}