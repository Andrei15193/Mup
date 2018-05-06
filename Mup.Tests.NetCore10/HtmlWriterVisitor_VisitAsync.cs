using System.Threading.Tasks;
using Xunit;
using static Mup.Tests.HtmlWriterVisitorTestData;

namespace Mup.Tests.NetCore10
{
    public class HtmlWriterVisitor_VisitAsync
    {
        private static readonly HtmlWriterVisitor visitor = new HtmlWriterVisitor();

        private const string _method = (nameof(HtmlWriterVisitor) + ".VisitAsync(string, IEnumerable<ElementMark>): ");

        [Trait("Class", nameof(HtmlWriterVisitor))]
        [Theory(DisplayName = (_method + nameof(EscapesHtmlSpecialCharactersForPlainText)))]
        [MemberData(nameof(EscapeSpecialCharactersForPlainTextTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForPlainText(string text, string expectedHtml)
        {
            visitor.BeginVisit();
            visitor.VisitText(text);
            visitor.EndVisit();

            var actualHtml = visitor.GetResult();
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(HtmlWriterVisitor))]
        [Theory(DisplayName = (_method + nameof(EscapesHtmlSpecialCharactersForPreformattedBlock)))]
        [MemberData(nameof(EscapeSpecialCharactersForPreformattedBlocksTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForPreformattedBlock(string text, string expectedHtml)
        {
            visitor.BeginVisit();
            visitor.VisitPreformattedBlock(text);
            visitor.EndVisit();

            var actualHtml = visitor.GetResult();
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(HtmlWriterVisitor))]
        [Theory(DisplayName = (_method + nameof(EscapesHtmlSpecialCharactersForCodeFragment)))]
        [MemberData(nameof(EscapeSpecialCharactersForCodeFragmentsTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForCodeFragment(string text, string expectedHtml)
        {
            visitor.BeginVisit();
            visitor.VisitCodeFragment(text);
            visitor.EndVisit();

            var actualHtml = visitor.GetResult();
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(HtmlWriterVisitor))]
        [Theory(DisplayName = (_method + nameof(EscapesHtmlSpecialCharactersForImages)))]
        [MemberData(nameof(EscapeSpecialCharactersForImagesTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
        public void EscapesHtmlSpecialCharactersForImages(string text, string expectedHtml)
        {
            visitor.BeginVisit();
            visitor.VisitImage(text, text);
            visitor.EndVisit();

            var actualHtml = visitor.GetResult();
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(HtmlWriterVisitor))]
        [Theory(DisplayName = (_method + nameof(EscapesHtmlSpecialCharactersForHyperlinks)))]
        [MemberData(nameof(EscapeSpecialCharactersForHyperlinksTestData), MemberType = typeof(HtmlWriterVisitorTestData))]
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