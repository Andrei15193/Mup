namespace Mup.Scanner
{
    internal class Token<TTokenCode>
        where TTokenCode : struct
    {
        internal Token(TTokenCode code, int start, int length)
        {
            Code = code;
            StartIndex = start;
            Length = length;
        }

        internal TTokenCode Code { get; }

        internal int StartIndex { get; }

        internal int Length { get; }

        internal int EndIndex
            => (StartIndex + Length);
    }
}