using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mup.Creole;
using Mup.Creole.Scanner;
using Xunit;
using static Mup.Creole.CreoleTokenCode;

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
        [InlineData("~~escape", new object[] { Text, Text })]
        [InlineData("~~~escape", new object[] { Text, Tilde, Text })]
        [InlineData("**strong**", new object[] { Asterisk, Asterisk, Text, Asterisk, Asterisk })]
        [InlineData("//emphasis//", new object[] { Slash, Slash, Text, Slash, Slash })]
        [InlineData("~~http://test.com", new object[] { Text, Text, Punctuation, Slash, Slash, Text, Punctuation, Text })]
        [InlineData("[[hyperlink]]", new object[] { BracketOpen, BracketOpen, Text, BracketClose, BracketClose })]
        [InlineData("[[hyperlink|with text]]", new object[] { BracketOpen, BracketOpen, Text, Pipe, Text, WhiteSpace, Text, BracketClose, BracketClose })]
        [InlineData("{{image}}", new object[] { BraceOpen, BraceOpen, Text, BraceClose, BraceClose })]
        [InlineData("{{image|with title}}", new object[] { BraceOpen, BraceOpen, Text, Pipe, Text, WhiteSpace, Text, BraceClose, BraceClose })]
        [InlineData("{{{no wiki}}}", new object[] { BraceOpen, BraceOpen, BraceOpen, Text, WhiteSpace, Text, BraceClose, BraceClose, BraceClose })]
        [InlineData("<<plug in>>", new object[] { AngleOpen, AngleOpen, Text, WhiteSpace, Text, AngleClose, AngleClose })]
        [InlineData(@"line\\break", new object[] { Text, BackSlash, BackSlash, Text })]
        [InlineData("paragraph1\n\nparagraph2", new object[] { Text, WhiteSpace, Text })]
        [InlineData("paragraph1\r\n\nparagraph2", new object[] { Text, WhiteSpace, Text })]
        [InlineData("paragraph1\n\r\nparagraph2", new object[] { Text, WhiteSpace, Text })]
        [InlineData("paragraph1\r\n\r\nparagraph2", new object[] { Text, WhiteSpace, Text })]
        [InlineData("* unordered list", new object[] { Asterisk, WhiteSpace, Text, WhiteSpace, Text })]
        [InlineData("** unordered sub list", new object[] { Asterisk, Asterisk, WhiteSpace, Text, WhiteSpace, Text, WhiteSpace, Text })]
        [InlineData("# ordered list", new object[] { Hash, WhiteSpace, Text, WhiteSpace, Text })]
        [InlineData("## ordered sub list", new object[] { Hash, Hash, WhiteSpace, Text, WhiteSpace, Text, WhiteSpace, Text })]
        [InlineData("|=header 1|=header 2|", new object[] { Pipe, Equal, Text, WhiteSpace, Text, Pipe, Equal, Text, WhiteSpace, Text, Pipe })]
        [InlineData("|cell 1|cell2|", new object[] { Pipe, Text, WhiteSpace, Text, Pipe, Text, Pipe })]
        [InlineData("----", new object[] { Dash, Dash, Dash, Dash })]
        [InlineData("= heading1", new object[] { Equal, WhiteSpace, Text })]
        [InlineData("== heading2", new object[] { Equal, Equal, WhiteSpace, Text })]
        [InlineData("=== heading3", new object[] { Equal, Equal, Equal, WhiteSpace, Text })]
        [InlineData("==== heading4", new object[] { Equal, Equal, Equal, Equal, WhiteSpace, Text })]
        [InlineData("===== heading5", new object[] { Equal, Equal, Equal, Equal, Equal, WhiteSpace, Text })]
        [InlineData("====== heading6", new object[] { Equal, Equal, Equal, Equal, Equal, Equal, WhiteSpace, Text })]
        public async Task ScanText(string text, IEnumerable<object> tokens)
        {
            await _Scanner.ScanAsync(text, CancellationToken.None);
            var scanResult = _Scanner.Result;
            Assert.Equal(tokens.Cast<CreoleTokenCode>().ToArray(), scanResult.Tokens.Select(token => token.Code).ToArray());
        }

        [Trait("Class", nameof(CreoleScanner))]
        [Theory(DisplayName = (_method + nameof(ScannedTextFromTokens)))]
        [InlineData("plain text", "plain text")]
        [InlineData("~escape", "~escape")]
        [InlineData("~~escape", "~escape")]
        [InlineData("**strong**", "**strong**")]
        [InlineData("//emphasis//", "//emphasis//")]
        [InlineData("~/escape", "/escape")]
        [InlineData("~~http://test.com", "~http://test.com")]
        [InlineData("[[hyperlink]]", "[[hyperlink]]")]
        [InlineData("[[hyperlink|with text]]", "[[hyperlink|with text]]")]
        [InlineData("~[[hyperlink]]", "[[hyperlink]]")]
        [InlineData("{{image}}", "{{image}}")]
        [InlineData("{{image|with title}}", "{{image|with title}}")]
        [InlineData("~{{image}}", "{{image}}")]
        [InlineData("{{{no wiki}}}", "{{{no wiki}}}")]
        [InlineData("~{{{no wiki}}}", "{{{no wiki}}}")]
        [InlineData("<<plug in>>", "<<plug in>>")]
        [InlineData("line\\break", "line\\break")]
        [InlineData("paragraph1\n\nparagraph2", "paragraph1\n\nparagraph2")]
        [InlineData("paragraph1\r\n\nparagraph2", "paragraph1\r\n\nparagraph2")]
        [InlineData("paragraph1\n\r\nparagraph2", "paragraph1\n\r\nparagraph2")]
        [InlineData("paragraph1\r\n\r\nparagraph2", "paragraph1\r\n\r\nparagraph2")]
        [InlineData("* unordered list", "* unordered list")]
        [InlineData("* unordered sub list", "* unordered sub list")]
        [InlineData("# ordered list", "# ordered list")]
        [InlineData("## ordered sub list", "## ordered sub list")]
        [InlineData("|=header 1|=header 2|", "|=header 1|=header 2|")]
        [InlineData("|cell 1|cell2|", "|cell 1|cell2|")]
        [InlineData("----", "----")]
        [InlineData("= heading1", "= heading1")]
        [InlineData("== heading2", "== heading2")]
        [InlineData("=== heading3", "=== heading3")]
        [InlineData("==== heading4", "==== heading4")]
        [InlineData("===== heading5", "===== heading5")]
        [InlineData("====== heading6", "====== heading6")]
        public async Task ScannedTextFromTokens(string text, string textFromTokens)
        {
            await _Scanner.ScanAsync(text, CancellationToken.None);
            var scanResult = _Scanner.Result;
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