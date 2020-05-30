using System.Text;

namespace Mup.Scanner
{
    internal static class TokenRangeHelper
    {
        internal static string GetPlainText<TCode>(TokenRange<TCode> tokens)
        {
            var stringBuilder = new StringBuilder();
            foreach (var token in tokens)
                stringBuilder.Append(token.Text);
            return stringBuilder.ToString();
        }
    }
}