using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mup.Creole;
using Xunit;
using static Mup.Tests.CreoleScannerTestData;
using static Mup.Tests.ScannerTestData;

namespace Mup.Tests.NetCore10
{
    public class CreoleScanner_ScanAsync
    {
        private static readonly CreoleScanner _scanner = new CreoleScanner();

        private const string _textMethod = (nameof(CreoleScanner) + ".Scan(string): ");
        private const string _readerMethod = (nameof(CreoleScanner) + ".Scan(TextReader): ");
        private const string _readerWithBufferMethod = (nameof(CreoleScanner) + ".Scan(TextReader, int): ");

        [Trait("Class", nameof(CreoleScanner))]
        [Fact(DisplayName = (_textMethod + nameof(CannotScanFromNullText)))]
        public void CannotScanFromNullText()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _scanner.ScanAsync(text: null, cancellationToken: CancellationToken.None));
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Fact(DisplayName = (_readerMethod + nameof(CannotScanFromNullTextReader)))]
        public void CannotScanFromNullTextReader()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _scanner.ScanAsync(reader: null, cancellationToken: CancellationToken.None));
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Fact(DisplayName = (_readerWithBufferMethod + nameof(CannotScanFromNullTextReaderWithBuffer)))]
        public void CannotScanFromNullTextReaderWithBuffer()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _scanner.ScanAsync(reader: null, bufferSize: 0, cancellationToken: CancellationToken.None));
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_readerWithBufferMethod + nameof(CannotScanWithNegativeOrZeroBufferSize)))]
        [MemberData(nameof(InvalidBufferSizeTestData), MemberType = typeof(ScannerTestData))]
        public void CannotScanWithNegativeOrZeroBufferSize(int bufferSize)
        {
            using (var reader = new StringReader(string.Empty))
                Assert.ThrowsAsync<ArgumentException>(() => _scanner.ScanAsync(reader, bufferSize, CancellationToken.None));
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_textMethod + nameof(ScanText)))]
        [MemberData(nameof(TextToTokensTestData), MemberType = typeof(CreoleScannerTestData))]

        public async Task ScanText(string text, IEnumerable<object> tokens)
        {
            await _scanner.ScanAsync(text, CancellationToken.None);
            Assert.Equal(tokens.Cast<CreoleTokenCode>().ToArray(), _scanner.Result.Select(token => token.Code).ToArray());
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_textMethod + nameof(ScannedTextFromTokens)))]
        [MemberData(nameof(TextFromTokensTestData), MemberType = typeof(CreoleScannerTestData))]

        public async Task ScannedTextFromTokens(string text, string textFromTokens)
        {
            await _scanner.ScanAsync(text, CancellationToken.None);
            var scanResult = _scanner.Result;
            Assert.Equal(
                textFromTokens,
                _scanner
                    .Result
                    .Aggregate(
                        new StringBuilder(),
                        (builder, token) => builder.Append(token.Text))
                    .ToString());
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_readerMethod + nameof(ScanTextWithReader)))]
        [MemberData(nameof(TextToTokensTestData), MemberType = typeof(CreoleScannerTestData))]

        public async Task ScanTextWithReader(string text, IEnumerable<object> tokens)
        {
            using (var stringReader = new StringReader(text))
            {
                await _scanner.ScanAsync(stringReader, CancellationToken.None);
                Assert.Equal(tokens.Cast<CreoleTokenCode>().ToArray(), _scanner.Result.Select(token => token.Code).ToArray());
            }
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_readerMethod + nameof(ScannedTextFromTokensWithReader)))]
        [MemberData(nameof(TextFromTokensTestData), MemberType = typeof(CreoleScannerTestData))]

        public async Task ScannedTextFromTokensWithReader(string text, string textFromTokens)
        {
            using (var stringReader = new StringReader(text))
            {
                await _scanner.ScanAsync(stringReader, CancellationToken.None);
                var scanResult = _scanner.Result;
                Assert.Equal(
                    textFromTokens,
                    _scanner
                        .Result
                        .Aggregate(
                            new StringBuilder(),
                            (builder, token) => builder.Append(token.Text))
                        .ToString());
            }
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_readerWithBufferMethod + nameof(ScanTextWithReader)))]
        [MemberData(nameof(TextToTokensTestData), MemberType = typeof(CreoleScannerTestData))]

        public async Task ScanTextWithReaderAndBuffer(string text, IEnumerable<object> tokens)
        {
            using (var stringReader = new StringReader(text))
            {
                await _scanner.ScanAsync(stringReader, 10, CancellationToken.None);
                Assert.Equal(tokens.Cast<CreoleTokenCode>().ToArray(), _scanner.Result.Select(token => token.Code).ToArray());
            }
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_readerWithBufferMethod + nameof(ScannedTextFromTokensWithReader)))]
        [MemberData(nameof(TextFromTokensTestData), MemberType = typeof(CreoleScannerTestData))]

        public async Task ScannedTextFromTokensWithReaderAndBuffer(string text, string textFromTokens)
        {
            using (var stringReader = new StringReader(text))
            {
                await _scanner.ScanAsync(stringReader, 10, CancellationToken.None);
                var scanResult = _scanner.Result;
                Assert.Equal(
                    textFromTokens,
                    _scanner
                        .Result
                        .Aggregate(
                            new StringBuilder(),
                            (builder, token) => builder.Append(token.Text))
                        .ToString());
            }
        }
    }
}