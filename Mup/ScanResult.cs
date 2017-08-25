﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Mup
{
    internal class ScanResult<TTokenCode>
        where TTokenCode : struct
    {
        internal ScanResult(string text, IEnumerable<IToken<TTokenCode>> tokens)
        {
            Text = (text ?? throw new ArgumentNullException(nameof(text)));
            Tokens = (tokens ?? throw new ArgumentNullException(nameof(tokens)));

            if (tokens.Any(token => token == null))
                throw new ArgumentException("Cannot contain null.", nameof(tokens));
        }

        internal string Text { get; }

        internal IEnumerable<IToken<TTokenCode>> Tokens { get; }
    }
}