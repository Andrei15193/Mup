using System.Text;

namespace Mup.Creole
{
    internal static class CreoleTokenRangeHelper
    {
        internal static string GetPlainText(CreoleTokenRange tokens)
        {
            var stringBuilder = new StringBuilder();
            foreach (var token in tokens)
                stringBuilder.Append(token.Text);
            return stringBuilder.ToString();
        }
    }
}