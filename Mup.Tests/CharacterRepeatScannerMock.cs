using System;
using System.Collections.Generic;
using Mup.Scanner;

namespace Mup.Tests
{
    internal class CharacterRepeatScannerMock<TTokenCode> : CharacterRepeatScanner<TTokenCode>
        where TTokenCode : struct
    {
        private readonly ICollection<Token<TTokenCode>> _tokens = new List<Token<TTokenCode>>();
        private readonly IEnumerable<KeyValuePair<TTokenCode, Predicate<char>>> _predicates;

        internal CharacterRepeatScannerMock(IEnumerable<KeyValuePair<TTokenCode, Predicate<char>>> predicates)
        {
            _predicates = predicates;
        }

        internal ScanResult<TTokenCode> Result { get; private set; }

        protected override IEnumerable<KeyValuePair<TTokenCode, Predicate<char>>> Predicates
            => _predicates;

        protected override void TokenScanned(TTokenCode code, int start, int length)
            => _tokens.Add(new Token<TTokenCode>(code, start, length));

        protected override void ScanCompleted(string text)
        {
            base.ScanCompleted(text);
            Result = new ScanResult<TTokenCode>(text, _tokens);
        }

        protected override void Reset()
        {
            Result = null;
            _tokens.Clear();
            base.Reset();
        }
    }
}