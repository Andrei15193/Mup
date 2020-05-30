using System;
using System.Collections;
using System.Collections.Generic;

namespace Mup.Scanner
{
    internal class TokenRange<TCode> : IReadOnlyList<Token<TCode>>
    {
        private readonly IReadOnlyList<Token<TCode>> _tokens;
        private readonly int _startIndex;

        public TokenRange(IReadOnlyList<Token<TCode>> tokens)
            : this(tokens, 0, tokens.Count)
        {
        }

        public TokenRange(IReadOnlyList<Token<TCode>> tokens, int rangeStartIndex, int rangeLength)
        {
            if (tokens == null)
                throw new ArgumentNullException(nameof(tokens));
            if (rangeStartIndex < 0 || rangeStartIndex > tokens.Count)
                throw new ArgumentOutOfRangeException(nameof(rangeLength), "Start index must less than the length of the source collection and greater than zero.");
            if (tokens.Count < rangeStartIndex + rangeLength)
                throw new ArgumentOutOfRangeException(nameof(rangeLength), "The sum of the range start and length must be less or equal to the source collection length.");

            _tokens = tokens;
            _startIndex = rangeStartIndex;
            Count = rangeLength;
        }

        public TokenRange<TCode> SubRange(int startIndex, int length)
            => new TokenRange<TCode>(_tokens, _startIndex + startIndex, length);

        public int Count { get; }

        public Token<TCode> this[int index]
            => _tokens[_startIndex + index];

        public IEnumerator<Token<TCode>> GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
                yield return _tokens[_startIndex + index];
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}