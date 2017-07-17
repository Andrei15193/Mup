using System;

namespace Mup
{
    internal class Token<TTokenCode> where TTokenCode : struct
    {
        private readonly Lazy<string> _value;

        public Token(string text, TTokenCode code, int start, int length)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (start < 0 || text.Length <= start)
                throw new ArgumentException("Start cannot exceed the text bounds.", nameof(start));
            if (length < 0 || text.Length < (start + length))
                throw new ArgumentException("Length cannot exceed the text bounds.", nameof(length));

            Code = code;
            Start = start;
            Length = length;
            _value = new Lazy<string>(() => text.Substring(start, length));
        }

        public TTokenCode Code { get; }

        public int Start { get; }

        public int Length { get; }

        public string Value => _value.Value;
    }
}