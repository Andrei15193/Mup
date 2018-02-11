using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Mup.Tests.CreoleToHtmlTestData;

namespace Mup.Tests.Net20
{
    public class CreoleParser_BeginEndParse_String
    {
        private static readonly CreoleParser _parser = new CreoleParser();

        private const string _method = (nameof(CreoleParser) + ".BeginParse(string)/" + nameof(CreoleParser) + ".EndParse(IAsyncResult)" + ": ");

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseHeadingsToHtml)))]
        [MemberData(nameof(HeadingsToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParseHeadingsToHtml(string text, string expectedHtml)
        {
            var parseTree = _parser.EndParse(_parser.BeginParse(text));
            var actualHtml = parseTree.EndAccept<string>(parseTree.BeginAccept(new HtmlWriterVisitor()));
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseParagraphsToHtml)))]
        [MemberData(nameof(ParagraphsToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParseParagraphsToHtml(string text, string expectedHtml)
        {
            var parseTree = _parser.EndParse(_parser.BeginParse(text));
            var actualHtml = parseTree.EndAccept<string>(parseTree.BeginAccept(new HtmlWriterVisitor()));
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsePreforamattedBlocksToHtml)))]
        [MemberData(nameof(PreformattedBlocksToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParsePreforamattedBlocksToHtml(string text, string expectedHtml)
        {
            var parseTree = _parser.EndParse(_parser.BeginParse(text));
            var actualHtml = parseTree.EndAccept<string>(parseTree.BeginAccept(new HtmlWriterVisitor()));
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseTablesToHtml)))]
        [MemberData(nameof(TablesToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParseTablesToHtml(string text, string expectedHtml)
        {
            var parseTree = _parser.EndParse(_parser.BeginParse(text));
            var actualHtml = parseTree.EndAccept<string>(parseTree.BeginAccept(new HtmlWriterVisitor()));
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseListsToHtml)))]
        [MemberData(nameof(ListsToHtmlTestsCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParseListsToHtml(string text, string expectedHtml)
        {
            var parseTree = _parser.EndParse(_parser.BeginParse(text));
            var actualHtml = parseTree.EndAccept<string>(parseTree.BeginAccept(new HtmlWriterVisitor()));
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesEscapeCharactersToHtml)))]
        [MemberData(nameof(EscapeCharacterToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParsesEscapeCharactersToHtml(string text, string expectedHtml)
        {
            var parseTree = _parser.EndParse(_parser.BeginParse(text));
            var actualHtml = parseTree.EndAccept<string>(parseTree.BeginAccept(new HtmlWriterVisitor()));
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesRichTextToHtml)))]
        [MemberData(nameof(RichTextToHtmlTestCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParsesRichTextToHtml(string text, string expectedHtml)
        {
            var parseTree = _parser.EndParse(_parser.BeginParse(text));
            var actualHtml = parseTree.EndAccept<string>(parseTree.BeginAccept(new HtmlWriterVisitor()));
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesHorizontalRuleToHtml)))]
        [MemberData(nameof(HorizontalRuleToHtmlTestsCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParsesHorizontalRuleToHtml(string text, string expectedHtml)
        {
            var parseTree = _parser.EndParse(_parser.BeginParse(text));
            var actualHtml = parseTree.EndAccept<string>(parseTree.BeginAccept(new HtmlWriterVisitor()));
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesPluginsToHtml)))]
        [MemberData(nameof(PluginToHtmlTestsCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void ParsesPluginsToHtml(string text, string expectedHtml)
        {
            var parseTree = _parser.EndParse(_parser.BeginParse(text));
            var actualHtml = parseTree.EndAccept<string>(parseTree.BeginAccept(new HtmlWriterVisitor()));
            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Trait("WebSite", "www.wikicreole.org")]
        [Theory(DisplayName = (_method + nameof(CreoleSiteTestsCase)))]
        [MemberData(nameof(CreoleWikiTestsCases), MemberType = typeof(CreoleToHtmlTestData))]
        public void CreoleSiteTestsCase(string text, string expectedHtml)
        {
            var parseTree = _parser.EndParse(_parser.BeginParse(text));
            var actualHtml = parseTree.EndAccept<string>(parseTree.BeginAccept(new HtmlWriterVisitor()));
            Assert.Equal(expectedHtml, actualHtml);
        }
    }
}