using System.Threading.Tasks;
using Xunit;

namespace Mup.Tests
{
    public class HtmlWriterVisitor_VisitAsync
    {
        private const string _method = (nameof(HtmlWriterVisitor) + ".VisitAsync(string, IEnumerable<ElementMark>): ");

        [Trait("Class", nameof(HtmlWriterVisitor))]
        [Theory(DisplayName = (_method + nameof(EscapesHtmlSpecialCharacters)))]
        [InlineData("<", "&lt;")]
        [InlineData(">", "&gt;")]
        [InlineData("&", "&amp;")]
        [InlineData("\"", "&quot;")]
        [InlineData("'", "&#39;")]
        [InlineData("<>&\"'", "&lt;&gt;&amp;&quot;&#39;")]
        public async Task EscapesHtmlSpecialCharacters(string text, string expectedHtml)
        {
            var parseTree = new FlatParseTree(text, new[] { new ElementMark { Code = ElementMarkCode.PlainText, Start = 0, Length = text.Length } });
            var actualHtml = await parseTree.AcceptAsync(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }
    }
}