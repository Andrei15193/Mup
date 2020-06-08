using Mup.Elements;
using Xunit;
using static Mup.Tests.HtmlWriterVisitorTestData;

namespace Mup.Tests
{
    public class HtmlWriterVisitorTests
    {
        [Theory, MemberData(nameof(EscapeSpecialCharactersForPlainTextTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForPlainText(string text, string expectedHtml)
        {
            var actualHtml = new TextElement(text).Accept(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(EscapeSpecialCharactersForPreformattedBlocksTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForPreformattedBlock(string text, string expectedHtml)
        {
            var actualHtml = new PreformattedBlockElement(text).Accept(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(EscapeSpecialCharactersForCodeFragmentsTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForCodeFragment(string text, string expectedHtml)
        {
            var actualHtml = new CodeElement(text).Accept(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(EscapeSpecialCharactersForImagesTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForImages(string text, string expectedHtml)
        {
            var actualHtml = new ImageElement(text, text).Accept(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(EscapeSpecialCharactersForHyperlinksTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForHyperlinks(string text, string expectedHtml)
        {
            var actualHtml = new HyperlinkElement(text, new TextElement[] { new TextElement(text) }).Accept(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }
    }
}