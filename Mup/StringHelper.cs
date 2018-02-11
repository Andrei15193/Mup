#if net20
using static System.Char;

namespace Mup
{
    internal static class StringHelper
    {
        internal static bool IsNullOrWhiteSpace(string value)
        {
            if (value == null)
                return true;

            bool isWhiteSpace = true;
            using (var character = value.GetEnumerator())
                while (isWhiteSpace && character.MoveNext())
                    isWhiteSpace = IsWhiteSpace(character.Current);
            return isWhiteSpace;
        }
    }
}
#endif