using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Mup.Tests.CreoleToHtmlTestData;

namespace Mup.Tests.Net45
{
    public class CreoleParser_ParseAsync_String
    {
        private static readonly CreoleParser _parser = new CreoleParser();

        private const string _method = (nameof(CreoleParser) + ".ParseAsync(string): ");

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_method + nameof(TryingToParseNullThrowsException)))]
        public void TryingToParseNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(delegate { _parser.ParseAsync(text: null); });
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseHeadingsToHtml)))]
        [MemberData(nameof(HeadingsToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public async Task ParseHeadingsToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseParagraphsToHtml)))]
        [MemberData(nameof(ParagraphsToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public async Task ParseParagraphsToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsePreforamattedBlocksToHtml)))]
        [MemberData(nameof(PreformattedBlocksToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public async Task ParsePreforamattedBlocksToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseTablesToHtml)))]
        [MemberData(nameof(TablesToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public async Task ParseTablesToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseListsToHtml)))]
        [MemberData(nameof(ListsToHtmlTestsCases), MemberType = typeof(CreoleToHtmlTestData))]
        public async Task ParseListsToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesEscapeCharactersToHtml)))]
        [MemberData(nameof(EscapeCharacterToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public async Task ParsesEscapeCharactersToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesRichTextToHtml)))]
        [MemberData(nameof(RichTextToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public async Task ParsesRichTextToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesHorizontalRuleToHtml)))]
        [MemberData(nameof(HorizontalRuleToHtmlTestsCases), MemberType = typeof(CreoleToHtmlTestData))]
        public async Task ParsesHorizontalRuleToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesPluginsToHtml)))]
        [MemberData(nameof(PluginToHtmlTestsCases), MemberType = typeof(CreoleToHtmlTestData))]
        public async Task ParsesPluginsToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Trait("WebSite", "www.wikicreole.org")]
        [Theory(DisplayName = (_method + nameof(CreoleSiteTestsCase)))]
        [MemberData(nameof(CreoleWikiTestsCases), MemberType = typeof(CreoleToHtmlTestData))]
        public async Task CreoleSiteTestsCase(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());
            Assert.Equal(expectedHtml, actualHtml);
        }
    }
}