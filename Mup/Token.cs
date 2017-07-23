namespace Mup
{
    internal class Token<TTokenCode> where TTokenCode : struct
    {
        public Token(TTokenCode code, int start, int length)
        {
            Code = code;
            Start = start;
            Length = length;
        }

        public TTokenCode Code { get; }

        public int Start { get; }

        public int Length { get; }

        public int End => (Start + Length);
    }
}