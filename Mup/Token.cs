namespace Mup
{
    internal class Token<TTokenCode>
        where TTokenCode : struct
    {
        internal Token(TTokenCode code, int start, int length)
        {
            Code = code;
            Start = start;
            Length = length;
        }

        internal TTokenCode Code { get; }

        internal int Start { get; }

        internal int Length { get; }

        internal int End => (Start + Length);
    }
}