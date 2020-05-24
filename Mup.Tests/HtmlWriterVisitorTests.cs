using System.Threading.Tasks;
using Xunit;
using static Mup.Tests.HtmlWriterVisitorTestData;

namespace Mup.Tests
{
    public class HtmlWriterVisitorTests
    {
        private static readonly HtmlWriterVisitor visitor = new HtmlWriterVisitor();

        private const string _method = (nameof(HtmlWriterVisitor) + ".VisitAsync(string, IEnumerable<ElementMark>): ");

        [Theory, MemberData(nameof(EscapeSpecialCharactersForPlainTextTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForPlainText(string text, string expectedHtml)
        {
            visitor.BeginVisit();
            visitor.VisitText(text);
            visitor.EndVisit();

            var actualHtml = visitor.GetResult();
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(EscapeSpecialCharactersForPreformattedBlocksTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForPreformattedBlock(string text, string expectedHtml)
        {
            visitor.BeginVisit();
            visitor.VisitPreformattedBlock(text);
            visitor.EndVisit();

            var actualHtml = visitor.GetResult();
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(EscapeSpecialCharactersForCodeFragmentsTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForCodeFragment(string text, string expectedHtml)
        {
            visitor.BeginVisit();
            visitor.VisitCodeFragment(text);
            visitor.EndVisit();

            var actualHtml = visitor.GetResult();
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(EscapeSpecialCharactersForImagesTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForImages(string text, string expectedHtml)
        {
            visitor.BeginVisit();
            visitor.VisitImage(text, text);
            visitor.EndVisit();

            var actualHtml = visitor.GetResult();
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(EscapeSpecialCharactersForHyperlinksTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForHyperlinks(string text, string expectedHtml)
        {
            visitor.BeginVisit();
            visitor.VisitHyperlinkBeginning(text);
            visitor.VisitText(text);
            visitor.VisitHyperlinkEnding();
            visitor.EndVisit();

            var actualHtml = visitor.GetResult();
            Assert.Equal(expectedHtml, actualHtml);
        }
    }
}