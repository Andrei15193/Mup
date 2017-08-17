using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

using static Mup.CreoleTokenCode;

namespace Mup.Tests
{
    public class CreoleScanner_ScanAsync
    {
        private const string _method = (nameof(CreoleScanner) + ".ScanAsync(string): ");

        private static CreoleScanner _Scanner { get; } = new CreoleScanner();

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_method + nameof(ScanText)))]
        [InlineData("plain text", new object[] { Text, WhiteSpace, Text })]
        [InlineData("~escape", new object[] { Tilde, Text })]
        [InlineData("**strong**", new object[] { Asterisk, Text, Asterisk })]
        [InlineData("//emphasis//", new object[] { Slash, Text, Slash })]
        [InlineData("[[hyperlink]]", new object[] { BracketOpen, Text, BracketClose })]
        [InlineData("[[hyperlink|with text]]", new object[] { BracketOpen, Text, Pipe, Text, WhiteSpace, Text, BracketClose })]
        [InlineData("{{image}}", new object[] { BraceOpen, Text, BraceClose })]
        [InlineData("{{image|with title}}", new object[] { BraceOpen, Text, Pipe, Text, WhiteSpace, Text, BraceClose })]
        [InlineData("{{{no wiki}}}", new object[] { BraceOpen, Text, WhiteSpace, Text, BraceClose })]
        [InlineData("<<plug in>>", new object[] { AngleOpen, Text, WhiteSpace, Text, AngleClose })]
        [InlineData("line\\break", new object[] { Text, BackSlash, Text })]
        [InlineData("paragraph1\n\nparagraph2", new object[] { Text, WhiteSpace, Text })]
        [InlineData("paragraph1\r\n\nparagraph2", new object[] { Text, WhiteSpace, Text })]
        [InlineData("paragraph1\n\r\nparagraph2", new object[] { Text, WhiteSpace, Text })]
        [InlineData("paragraph1\r\n\r\nparagraph2", new object[] { Text, WhiteSpace, Text })]
        [InlineData("* unordered list", new object[] { Asterisk, WhiteSpace, Text, WhiteSpace, Text })]
        [InlineData("* unordered sub list", new object[] { Asterisk, WhiteSpace, Text, WhiteSpace, Text, WhiteSpace, Text })]
        [InlineData("# ordered list", new object[] { Hash, WhiteSpace, Text, WhiteSpace, Text })]
        [InlineData("## ordered sub list", new object[] { Hash, WhiteSpace, Text, WhiteSpace, Text, WhiteSpace, Text })]
        [InlineData("|=header 1|=header 2|", new object[] { Pipe, Equal, Text, WhiteSpace, Text, Pipe, Equal, Text, WhiteSpace, Text, Pipe })]
        [InlineData("|cell 1|cell2|", new object[] { Pipe, Text, WhiteSpace, Text, Pipe, Text, Pipe })]
        [InlineData("----", new object[] { Dash })]
        [InlineData("= heading1", new object[] { Equal, WhiteSpace, Text })]
        [InlineData("== heading2", new object[] { Equal, WhiteSpace, Text })]
        [InlineData("=== heading3", new object[] { Equal, WhiteSpace, Text })]
        [InlineData("==== heading4", new object[] { Equal, WhiteSpace, Text })]
        [InlineData("===== heading5", new object[] { Equal, WhiteSpace, Text })]
        [InlineData("====== heading6", new object[] { Equal, WhiteSpace, Text })]
        public async Task ScanText(string text, IEnumerable<object> tokens)
        {
            var scanResult = await _Scanner.ScanAsync(text);
            Assert.Equal(tokens.Cast<CreoleTokenCode>(), scanResult.Tokens.Select(token => token.Code));
        }
    }
}