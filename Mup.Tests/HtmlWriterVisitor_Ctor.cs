using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mup.Tests
{
    public class HtmlWriterVisitor_Ctor
    {
        private const string _method = (nameof(HtmlWriterVisitor) + "(StringBuilder): ");

        [Trait("Class", nameof(HtmlWriterVisitor))]
        [Fact(DisplayName = (_method + nameof(StringBuilderCannotBeNull)))]
        public void StringBuilderCannotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HtmlWriterVisitor(null));
        }

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
            var htmlStringBuilder = new StringBuilder();
            var creoleToHtmlVisitor = new HtmlWriterVisitor(htmlStringBuilder);
            await creoleToHtmlVisitor.VisitAsync(text, new[] { new ElementMark { Code = ElementMarkCode.PlainText, Start = 0, Length = text.Length } });

            Assert.Equal(expectedHtml, htmlStringBuilder.ToString());
        }
    }
}