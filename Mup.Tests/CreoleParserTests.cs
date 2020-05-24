using System;
using Xunit;
using static Mup.Tests.CreoleToHtmlTestData;

namespace Mup.Tests
{
    public class CreoleParserTests
    {
        private static readonly CreoleParser _parser = new CreoleParser();

        private const string _method = (nameof(CreoleParser) + ".Parse(string): ");

        [Fact]
        public void TryingToParseNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _parser.Parse(text: null));
        }

        [Theory, MemberData(nameof(HeadingsToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParseHeadingsToHtml(string text, string expectedHtml)
        {
            var actualHtml = _parser.Parse(text).Accept(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(ParagraphsToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParseParagraphsToHtml(string text, string expectedHtml)
        {
            var actualHtml = _parser.Parse(text).Accept(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(PreformattedBlocksToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParsePreforamattedBlocksToHtml(string text, string expectedHtml)
        {
            var actualHtml = _parser.Parse(text).Accept(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(TablesToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParseTablesToHtml(string text, string expectedHtml)
        {
            var actualHtml = _parser.Parse(text).Accept(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(ListsToHtmlTestsCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParseListsToHtml(string text, string expectedHtml)
        {
            var actualHtml = _parser.Parse(text).Accept(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(EscapeCharacterToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParsesEscapeCharactersToHtml(string text, string expectedHtml)
        {
            var actualHtml = _parser.Parse(text).Accept(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(RichTextToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParsesRichTextToHtml(string text, string expectedHtml)
        {
            var actualHtml = _parser.Parse(text).Accept(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(HorizontalRuleToHtmlTestsCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParsesHorizontalRuleToHtml(string text, string expectedHtml)
        {
            var actualHtml = _parser.Parse(text).Accept(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(PluginToHtmlTestsCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParsesPluginsToHtml(string text, string expectedHtml)
        {
            var actualHtml = _parser.Parse(text).Accept(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Theory, MemberData(nameof(CreoleWikiTestsCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void CreoleSiteTestsCase(string text, string expectedHtml)
        {
            var actualHtml = _parser.Parse(text).Accept(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }
    }
}