using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mup.Creole;
using Mup.Creole.Scanner;
using Xunit;
using static Mup.Creole.CreoleTokenCode;
using static Mup.Tests.CreoleScannerTestData;

namespace Mup.Tests.NetCore
{
    public class CreoleScanner_ScanAsync
    {
        private static readonly CreoleScanner _scanner = new CreoleScanner();

        private const string _method = (nameof(CreoleScanner) + ".ScanAsync(string): ");

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_method + nameof(ScanText)))]
        [MemberData(nameof(TextToTokensTestData), MemberType = typeof(CreoleScannerTestData))]

        public async Task ScanText(string text, IEnumerable<object> tokens)
        {
            await _scanner.ScanAsync(text, CancellationToken.None);
            var scanResult = _scanner.Result;
            Assert.Equal(tokens.Cast<CreoleTokenCode>().ToArray(), scanResult.Tokens.Select(token => token.Code).ToArray());
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_method + nameof(ScannedTextFromTokens)))]
        [MemberData(nameof(TextFromTokensTestData), MemberType = typeof(CreoleScannerTestData))]
        
        public async Task ScannedTextFromTokens(string text, string textFromTokens)
        {
            await _scanner.ScanAsync(text, CancellationToken.None);
            var scanResult = _scanner.Result;
            Assert.Equal(
                textFromTokens,
                scanResult
                    .Tokens
                    .Aggregate(
                        new StringBuilder(),
                        (builder, token) => builder.Append(scanResult.Text, token.StartIndex, token.Length))
                    .ToString());
        }
    }
}