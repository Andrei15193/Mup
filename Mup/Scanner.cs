using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    internal class Scanner<TTokenCode>
        where TTokenCode : struct
    {
        private const int _defaultBufferSize = 2048;

        private readonly IEnumerable<KeyValuePair<TTokenCode, Func<char, bool>>> _predicates;

        internal Scanner(IEnumerable<KeyValuePair<TTokenCode, Func<char, bool>>> predicates)
        {
            _predicates = (predicates ?? throw new ArgumentNullException(nameof(predicates)));
            if (predicates.Any(predicate => predicate.Value == null))
                throw new ArgumentException("Cannot contain null.", nameof(predicates));
        }

        internal ScanResult<TTokenCode> Scan(string text)
        {
            var scanner = new TextScanner(_predicates);
            var scanResult = scanner.Scan(text);
            return scanResult;
        }

        internal Task<ScanResult<TTokenCode>> ScanAsync(string text)
            => ScanAsync(text, CancellationToken.None);

        internal async Task<ScanResult<TTokenCode>> ScanAsync(string text, CancellationToken cancellationToken)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            ScanResult<TTokenCode> result;
            var scanTask = Task.Run(
                () =>
                {
                    var scanner = new TextScanner(_predicates);
                    var scanResult = scanner.Scan(text);
                    return scanResult;
                });
            if (!cancellationToken.CanBeCanceled)
                result = await scanTask.ConfigureAwait(false);
            else
            {
                var cancellationSignal = new TaskCompletionSource<ScanResult<TTokenCode>>();
                using (cancellationToken.Register(cancellationSignal.SetCanceled))
                {
                    var compeltedTask = await Task.WhenAny(cancellationSignal.Task, scanTask).ConfigureAwait(false);
                    result = await compeltedTask.ConfigureAwait(false);
                }
            }

            return result;
        }

        internal Task<ScanResult<TTokenCode>> ScanAsync(TextReader reader)
            => ScanAsync(reader, _defaultBufferSize, CancellationToken.None);

        internal Task<ScanResult<TTokenCode>> ScanAsync(TextReader reader, int bufferSize)
            => ScanAsync(reader, bufferSize, CancellationToken.None);

        internal Task<ScanResult<TTokenCode>> ScanAsync(TextReader reader, CancellationToken cancellationToken)
            => ScanAsync(reader, _defaultBufferSize, cancellationToken);

        internal async Task<ScanResult<TTokenCode>> ScanAsync(TextReader reader, int bufferSize, CancellationToken cancellationToken)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (bufferSize <= 0)
                throw new ArgumentException("The buffer size must be greater than zero.", nameof(bufferSize));

            var scanner = new TextScanner(_predicates);
            var scanResult = await scanner.ScanAsync(reader, bufferSize, cancellationToken).ConfigureAwait(false);
            return scanResult;
        }

        private sealed class TextScanner
        {
            private int _line = 1;
            private int _column = 1;
            private int _index = 0;
            private readonly List<Token<TTokenCode>> _tokens = new List<Token<TTokenCode>>();
            private readonly IEnumerable<KeyValuePair<TTokenCode, Func<char, bool>>> _predicates;

            internal TextScanner(IEnumerable<KeyValuePair<TTokenCode, Func<char, bool>>> predicates)
                => _predicates = predicates;

            internal ScanResult<TTokenCode> Scan(string text)
            {
                foreach (var character in text)
                    _Process(character);

                _InitializeTokens();
                var scanResult = new ScanResult<TTokenCode>(text, _tokens);

                return scanResult;
            }

            internal async Task<ScanResult<TTokenCode>> ScanAsync(TextReader reader, int bufferSize, CancellationToken cancellationToken)
            {
                int bufferLength;
                var buffer = new char[bufferSize];
                var textBuilder = new StringBuilder();
                do
                {
                    bufferLength = await reader.ReadAsync(buffer, 0, bufferSize).ConfigureAwait(false);
                    cancellationToken.ThrowIfCancellationRequested();

                    textBuilder.Append(buffer, 0, bufferLength);
                    for (var bufferIndex = 0; bufferIndex < bufferLength; bufferIndex++)
                        _Process(buffer[bufferIndex]);
                }
                while (bufferLength > 0);

                var text = textBuilder.ToString();
                _InitializeTokens();
                var scanResult = new ScanResult<TTokenCode>(text, _tokens);

                return scanResult;
            }

            private void _Process(char character)
            {
                var match = _predicates.FirstOrDefault(predicate => predicate.Value(character));
                if (match.Value != null)
                {
                    var tokenBuilder = _tokens.LastOrDefault();
                    if (tokenBuilder != null && tokenBuilder.Code.Equals(match.Key))
                        tokenBuilder.Length++;
                    else
                        _tokens.Add(
                            new Token<TTokenCode>()
                            {
                                Code = match.Key,
                                Start = _index,
                                Length = 1
                            });
                }
                else
                    throw new InvalidOperationException($"Unexpected character '{character}' ({character:X2}) at line {_line}, column {_column}.");

                _index++;
                if (character == '\n')
                {
                    _line++;
                    _column = 1;
                }
                else
                    _column++;
            }

            private void _InitializeTokens()
            {
                using (var token = _tokens.GetEnumerator())
                    if (token.MoveNext())
                    {
                        var currentToken = token.Current;
                        currentToken.End = (currentToken.Start + currentToken.Length);
                        var previousToken = currentToken;

                        while (token.MoveNext())
                        {
                            currentToken = token.Current;
                            currentToken.End = (currentToken.Start + currentToken.Length);

                            currentToken.Previous = previousToken;
                            previousToken.Next = currentToken;

                            previousToken = currentToken;
                        }
                    }
            }
        }
    }
}