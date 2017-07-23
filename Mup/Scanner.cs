using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    internal class Scanner<TTokenCode> where TTokenCode : struct
    {
        private const int _defaultBufferSize = 2048;

        private readonly IEnumerable<KeyValuePair<TTokenCode, Func<char, bool>>> _predicates;

        public Scanner(IEnumerable<KeyValuePair<TTokenCode, Func<char, bool>>> predicates)
        {
            _predicates = (predicates ?? throw new ArgumentNullException(nameof(predicates)));
            if (predicates.Any(predicate => predicate.Value == null))
                throw new ArgumentException("Cannot contain null.", nameof(predicates));
        }

        public ScanResult<TTokenCode> Scan(string text)
        {
            var scanner = new TextScanner(_predicates);
            var scanResult = scanner.Scan(text);
            return scanResult;
        }

        public Task<ScanResult<TTokenCode>> ScanAsync(string text)
            => ScanAsync(text, CancellationToken.None);

        public async Task<ScanResult<TTokenCode>> ScanAsync(string text, CancellationToken cancellationToken)
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
                result = await scanTask;
            else
            {
                var cancellationSignal = new TaskCompletionSource<ScanResult<TTokenCode>>();
                using (cancellationToken.Register(cancellationSignal.SetCanceled))
                {
                    var compeltedTask = await Task.WhenAny(cancellationSignal.Task, scanTask);
                    result = await compeltedTask;
                }
            }

            return result;
        }

        public Task<ScanResult<TTokenCode>> ScanAsync(TextReader reader)
            => ScanAsync(reader, _defaultBufferSize, CancellationToken.None);

        public Task<ScanResult<TTokenCode>> ScanAsync(TextReader reader, int bufferSize)
            => ScanAsync(reader, bufferSize, CancellationToken.None);

        public Task<ScanResult<TTokenCode>> ScanAsync(TextReader reader, CancellationToken cancellationToken)
            => ScanAsync(reader, _defaultBufferSize, cancellationToken);

        public async Task<ScanResult<TTokenCode>> ScanAsync(TextReader reader, int bufferSize, CancellationToken cancellationToken)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (bufferSize <= 0)
                throw new ArgumentException("The buffer size must be greater than zero.", nameof(bufferSize));

            var scanner = new TextScanner(_predicates);
            var scanResult = await scanner.ScanAsync(reader, bufferSize, cancellationToken);
            return scanResult;
        }

        private sealed class TextScanner
        {
            private int _line = 1;
            private int _column = 1;
            private int _index = 0;
            private readonly List<TokenBuilder> _tokenBuilders = new List<TokenBuilder>();
            private readonly IEnumerable<KeyValuePair<TTokenCode, Func<char, bool>>> _predicates;

            internal TextScanner(IEnumerable<KeyValuePair<TTokenCode, Func<char, bool>>> predicates)
                => _predicates = predicates;

            internal ScanResult<TTokenCode> Scan(string text)
            {
                foreach (var character in text)
                    _Process(character);

                var tokens = _tokenBuilders.Select(tokenBuilder => tokenBuilder.Build()).ToList();
                var scanResult = new ScanResult<TTokenCode>(text, tokens);

                return scanResult;
            }

            internal async Task<ScanResult<TTokenCode>> ScanAsync(TextReader reader, int bufferSize, CancellationToken cancellationToken)
            {
                int bufferLength;
                var buffer = new char[bufferSize];
                var textBuilder = new StringBuilder();
                do
                {
                    bufferLength = await reader.ReadAsync(buffer, 0, bufferSize);
                    cancellationToken.ThrowIfCancellationRequested();

                    textBuilder.Append(buffer, 0, bufferLength);
                    for (var bufferIndex = 0; bufferIndex < bufferLength; bufferIndex++)
                        _Process(buffer[bufferIndex]);
                }
                while (bufferLength > 0);

                var text = textBuilder.ToString();
                var tokens = _tokenBuilders.Select(tokenBuilder => tokenBuilder.Build()).ToList();
                var scanResult = new ScanResult<TTokenCode>(text, tokens);

                return scanResult;
            }

            private void _Process(char character)
            {
                var match = _predicates.FirstOrDefault(predicate => predicate.Value(character));
                if (match.Value != null)
                {
                    var tokenBuilder = _tokenBuilders.LastOrDefault();
                    if (tokenBuilder != null && tokenBuilder.Code.Equals(match.Key))
                        tokenBuilder.Length++;
                    else
                        _tokenBuilders.Add(
                            new TokenBuilder
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

            private class TokenBuilder
            {
                internal TTokenCode Code { get; set; }

                internal int Start { get; set; }

                internal int Length { get; set; }

                internal Token<TTokenCode> Build()
                    => new Token<TTokenCode>(Code, Start, Length);
            }
        }
    }
}