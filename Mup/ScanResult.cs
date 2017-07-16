using System;
using System.Collections.Generic;
using System.Linq;

namespace Mup
{
    public class ScanResult<TTokenCode> where TTokenCode : struct
    {
        public ScanResult(string text, IEnumerable<Token<TTokenCode>> tokens)
        {
            Text = (text ?? throw new ArgumentNullException(nameof(text)));
            Tokens = (tokens ?? throw new ArgumentNullException(nameof(tokens)));

            if (tokens.Any(token => token == null))
                throw new ArgumentException("Cannot contain null.", nameof(tokens));
        }

        public string Text { get; }

        public IEnumerable<Token<TTokenCode>> Tokens { get; }
    }
}