using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mup.Creole
{
    internal class CreoleTokenRange : IEnumerable<CreoleToken>
    {
        private readonly ReadOnlyCollection<CreoleToken> _tokens;
        private readonly int _startIndex;
        private readonly int _count;

        internal CreoleTokenRange(ReadOnlyCollection<CreoleToken> tokens)
        {
            if (tokens == null)
                throw new ArgumentNullException(nameof(tokens));

            _tokens = tokens;
            _startIndex = 0;
            _count = _tokens.Count;
        }

        internal CreoleTokenRange(ReadOnlyCollection<CreoleToken> tokens, int rangeStartIndex, int rangeLength)
        {
            if (tokens == null)
                throw new ArgumentNullException(nameof(tokens));
            if (rangeStartIndex < 0 || rangeStartIndex > tokens.Count)
                throw new ArgumentOutOfRangeException(nameof(rangeLength), "Start index must less than the length of the source collection and greater than zero.");
            if (tokens.Count < rangeStartIndex + rangeLength)
                throw new ArgumentOutOfRangeException(nameof(rangeLength), "The sum of the range start and length must be less or equal to the source collection length.");

            _tokens = tokens;
            _startIndex = rangeStartIndex;
            _count = rangeLength;
        }

        internal int Count
            => _count;

        internal CreoleTokenRange SubRange(int startIndex, int length)
            => new CreoleTokenRange(_tokens, (_startIndex + startIndex), length);

        public IEnumerator<CreoleToken> GetEnumerator()
            => new CreoleTokenRangeEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private class CreoleTokenRangeEnumerator : IEnumerator<CreoleToken>
        {
            private readonly CreoleTokenRange _creoleTokenRange;
            private int _index = -1;

            internal CreoleTokenRangeEnumerator(CreoleTokenRange creoleTokenRange)
            {
                _creoleTokenRange = creoleTokenRange;
            }

            public CreoleToken Current
            {
                get
                {
                    if (_index < 0 || _index > _creoleTokenRange._count)
                        throw new InvalidOperationException("The last call to MoveNext() method must return true for this property to be accessible.");

                    return _creoleTokenRange._tokens[_creoleTokenRange._startIndex + _index];
                }
            }

            object IEnumerator.Current
                => Current;

            public bool MoveNext()
            {
                _index++;
                return (_index < _creoleTokenRange._count);
            }

            public void Reset()
            {
                _index = -1;
            }

            void IDisposable.Dispose()
            {
            }
        }
    }
}