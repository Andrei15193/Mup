using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mup.Creole;
using Xunit;
using static Mup.Tests.CreoleScannerTestData;
using static Mup.Tests.ScannerTestData;

namespace Mup.Tests
{
    public class CreoleScanner_Scan
    {
        private static readonly CreoleScanner _scanner = new CreoleScanner();

        private const string _textMethod = (nameof(CreoleScanner) + ".Scan(string): ");
        private const string _readerMethod = (nameof(CreoleScanner) + ".Scan(TextReader): ");
        private const string _readerWithBufferMethod = (nameof(CreoleScanner) + ".Scan(TextReader, int): ");

        [Trait("Class", nameof(CreoleScanner))]
        [Fact(DisplayName = (_textMethod + nameof(CannotScanFromNullText)))]
        public void CannotScanFromNullText()
        {
            Assert.Throws<ArgumentNullException>(() => _scanner.Scan(text: null));
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Fact(DisplayName = (_readerMethod + nameof(CannotScanFromNullTextReader)))]
        public void CannotScanFromNullTextReader()
        {
            Assert.Throws<ArgumentNullException>(() => _scanner.Scan(reader: null));
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Fact(DisplayName = (_readerWithBufferMethod + nameof(CannotScanFromNullTextReader)))]
        public void CannotScanFromNullTextReaderWithBuffer()
        {
            Assert.Throws<ArgumentNullException>(() => _scanner.Scan(reader: null, bufferSize: 0));
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_readerWithBufferMethod + nameof(CannotScanWithNegativeOrZeroBufferSize)))]
        [MemberData(nameof(InvalidBufferSizeTestData), MemberType = typeof(ScannerTestData))]
        public void CannotScanWithNegativeOrZeroBufferSize(int bufferSize)
        {
            using (var reader = new StringReader(string.Empty))
                Assert.Throws<ArgumentException>(() => _scanner.Scan(reader, bufferSize));
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_textMethod + nameof(ScanText)))]
        [MemberData(nameof(TextToTokensTestData), MemberType = typeof(CreoleScannerTestData))]

        public void ScanText(string text, IEnumerable<object> tokens)
        {
            _scanner.Scan(text);
            Assert.Equal(tokens.Cast<CreoleTokenCode>().ToArray(), _scanner.Result.Select(token => token.Code).ToArray());
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_textMethod + nameof(ScannedTextFromTokens)))]
        [MemberData(nameof(TextFromTokensTestData), MemberType = typeof(CreoleScannerTestData))]
        public void ScannedTextFromTokens(string text, string textFromTokens)
        {
            _scanner.Scan(text);
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
        [Theory(DisplayName = (_readerMethod + nameof(ScanTextUsingReader)))]
        [MemberData(nameof(TextToTokensTestData), MemberType = typeof(CreoleScannerTestData))]
        public void ScanTextUsingReader(string text, IEnumerable<object> tokens)
        {
            using (var stringReader = new StringReader(text))
            {
                _scanner.Scan(stringReader);
                Assert.Equal(tokens.Cast<CreoleTokenCode>().ToArray(), _scanner.Result.Select(token => token.Code).ToArray());
            }
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_readerMethod + nameof(ScannedTextFromTokensUsingReader)))]
        [MemberData(nameof(TextFromTokensTestData), MemberType = typeof(CreoleScannerTestData))]
        public void ScannedTextFromTokensUsingReader(string text, string textFromTokens)
        {
            using (var stringReader = new StringReader(text))
            {
                _scanner.Scan(stringReader);
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
        [Theory(DisplayName = (_readerWithBufferMethod + nameof(ScanTextUsingReaderWithBuffer)))]
        [MemberData(nameof(TextToTokensTestData), MemberType = typeof(CreoleScannerTestData))]
        public void ScanTextUsingReaderWithBuffer(string text, IEnumerable<object> tokens)
        {
            using (var stringReader = new StringReader(text))
            {
                _scanner.Scan(stringReader, 10);
                Assert.Equal(tokens.Cast<CreoleTokenCode>().ToArray(), _scanner.Result.Select(token => token.Code).ToArray());
            }
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_readerWithBufferMethod + nameof(ScannedTextFromTokensUsingReaderWithBuffer)))]
        [MemberData(nameof(TextFromTokensTestData), MemberType = typeof(CreoleScannerTestData))]
        public void ScannedTextFromTokensUsingReaderWithBuffer(string text, string textFromTokens)
        {
            using (var stringReader = new StringReader(text))
            {
                _scanner.Scan(stringReader, 10);
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