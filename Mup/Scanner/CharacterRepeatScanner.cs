using System;
using System.Collections.Generic;
using System.Linq;

namespace Mup.Scanner
{
    internal abstract class CharacterRepeatScanner<TTokenCode> : CharacterScanner
        where TTokenCode : struct
    {
        private TokenBuilder _tokenBuilder;

        protected abstract IEnumerable<KeyValuePair<TTokenCode, Func<char, bool>>> Predicates { get; }

        protected abstract void TokenScanned(TTokenCode code, int start, int length);

        protected override void Reset()
        {
            _tokenBuilder = null;
            base.Reset();
        }

        protected sealed override void Process(char character)
        {
            var match = Predicates.FirstOrDefault(predicate => predicate.Value(character));
            if (match.Value != null)
            {
                if (_tokenBuilder == null || !_tokenBuilder.Code.Equals(match.Key))
                {
                    if (_tokenBuilder != null)
                        TokenScanned(_tokenBuilder.Code, _tokenBuilder.Start, _tokenBuilder.Length);
                    else
                        _tokenBuilder = new TokenBuilder();

                    _tokenBuilder.Code = match.Key;
                    _tokenBuilder.Start = Index;
                    _tokenBuilder.Length = 1;
                }
                else
                    _tokenBuilder.Length++;
            }
            else
                throw new InvalidOperationException($"Unexpected character '{character}' ({character:X2}) at line {Line}, column {Column}.");
        }

        protected override void ScanCompleted(string text)
        {
            if (_tokenBuilder != null)
                TokenScanned(_tokenBuilder.Code, _tokenBuilder.Start, _tokenBuilder.Length);
        }

        protected override int DefaultBuffer => 1;

        private sealed class TokenBuilder
        {
            internal TTokenCode Code { get; set; }

            internal int Start { get; set; }

            internal int Length { get; set; }
        }
    }
}