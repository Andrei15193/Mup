namespace Mup
{
    internal class Token<TTokenCode> : IToken<TTokenCode>
        where TTokenCode : struct
    {
        public TTokenCode Code { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public int End { get; set; }

        public IToken<TTokenCode> Previous { get; set; }

        public IToken<TTokenCode> Next { get; set; }
    }
}