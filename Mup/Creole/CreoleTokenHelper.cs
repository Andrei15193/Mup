using System.Collections.Generic;
using System.Text;

namespace Mup.Creole
{
    internal static class CreoleTokenHelper
    {
        internal static IEnumerable<CreoleToken> Enumerate(CreoleToken start, CreoleToken end)
        {
            for (var token = start; token != end; token = token.Next)
                yield return token;
        }

        internal static string Substring(string text, CreoleToken start, CreoleToken end)
        {
            var stringBuilder = new StringBuilder();
            for (var token = start; token != end; token = token.Next)
                stringBuilder.Append(text, token.StartIndex, token.Length);
            return stringBuilder.ToString();
        }
    }
}