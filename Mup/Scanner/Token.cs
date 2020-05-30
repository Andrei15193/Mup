namespace Mup.Scanner
{
    internal class Token<TCode>
    {
        public Token(TCode code, string text, int line, int column)
        {
            Code = code;
            Text = text;
            Line = line;
            Column = column;
        }

        public TCode Code { get; }

        public string Text { get; }

        public int Line { get; }

        public int Column { get; }
    }
}