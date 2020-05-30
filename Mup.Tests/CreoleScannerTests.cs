using Mup.Creole;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Mup.Tests
{
    public class CreoleScannerTests
    {
        private readonly CreoleScanner _scanner = new CreoleScanner();

        [Fact]
        public void CannotScanFromNullTextReaderWithBuffer()
        {
            Assert.Throws<ArgumentNullException>(() => _scanner.Scan(null, 0));
        }

        [Theory, MemberData(nameof(ScannerTestData.InvalidBufferSizeTestData), MemberType = typeof(ScannerTestData))]
        public void CannotScanWithNegativeOrZeroBufferSize(int bufferSize)
        {
            using var reader = new StringReader(string.Empty);

            Assert.Throws<ArgumentException>(() => _scanner.Scan(reader, bufferSize));
        }

        [Theory, MemberData(nameof(CreoleScannerTestData.TextToTokensTestData), MemberType = typeof(CreoleScannerTestData))]
        public void ScanTextUsingReaderWithBuffer(string text, IEnumerable<object> tokens)
        {
            using var stringReader = new StringReader(text);

            _scanner.Scan(stringReader, 10);
            Assert.Equal(tokens.Cast<CreoleTokenCode>().ToArray(), _scanner.Result.Select(token => token.Code).ToArray());
        }

        [Theory, MemberData(nameof(CreoleScannerTestData.TextFromTokensTestData), MemberType = typeof(CreoleScannerTestData))]
        public void ScannedTextFromTokensUsingReaderWithBuffer(string text, string textFromTokens)
        {
            using var stringReader = new StringReader(text);

            _scanner.Scan(stringReader, 10);
            Assert.Equal(textFromTokens, _scanner.Result.Aggregate(new StringBuilder(), (builder, token) => builder.Append(token.Text)).ToString());
        }
    }
}