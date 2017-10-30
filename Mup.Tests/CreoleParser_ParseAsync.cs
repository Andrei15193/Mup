using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Mup.ElementMarkCode;

namespace Mup.Tests
{
    public class CreoleParser_ParseAsync
    {
        private static readonly CreoleParser _parser = new CreoleParser();

        private const string _method = (nameof(CreoleParser) + ".ParseAsync(string): ");

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesHeadings)))]
        [InlineData("= plain text", new object[] { Heading1Start, PlainText, Heading1End })]
        [InlineData("= plain text =", new object[] { Heading1Start, PlainText, Heading1End })]
        [InlineData("== plain text", new object[] { Heading2Start, PlainText, Heading2End })]
        [InlineData("== plain text ==", new object[] { Heading2Start, PlainText, Heading2End })]
        [InlineData("=== plain text ===", new object[] { Heading3Start, PlainText, Heading3End })]
        [InlineData("=== plain text", new object[] { Heading3Start, PlainText, Heading3End })]
        [InlineData("==== plain text", new object[] { Heading4Start, PlainText, Heading4End })]
        [InlineData("==== plain text =====", new object[] { Heading4Start, PlainText, Heading4End })]
        [InlineData("===== plain text", new object[] { Heading5Start, PlainText, Heading5End })]
        [InlineData("===== plain text =====", new object[] { Heading5Start, PlainText, Heading5End })]
        [InlineData("====== plain text", new object[] { Heading6Start, PlainText, Heading6End })]
        [InlineData("====== plain text ======", new object[] { Heading6Start, PlainText, Heading6End })]
        [InlineData("= plain text **strong**", new object[] { Heading1Start, PlainText, Heading1End })]
        [InlineData("= plain text //emphasis//", new object[] { Heading1Start, PlainText, Heading1End })]
        [InlineData("= plain text [[link]]", new object[] { Heading1Start, PlainText, Heading1End })]
        [InlineData("= plain text [[link|with text]]", new object[] { Heading1Start, PlainText, Heading1End })]
        [InlineData("= plain text {{image}}", new object[] { Heading1Start, PlainText, Heading1End })]
        [InlineData("= plain text {{image|with title}}", new object[] { Heading1Start, PlainText, Heading1End })]
        [InlineData("= plain text <<plug in>>", new object[] { Heading1Start, PlainText, Heading1End })]
        public async Task ParsesHeadings(string text, object[] marks)
        {
            var actualMarks = await _parser.ParseAsync(text).With(new ElementMarkVisitor());

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), actualMarks);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesParagraphs)))]
        [InlineData("plain text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("paragraph 1\n\nparagraph 2", new object[] { ParagraphStart, PlainText, ParagraphEnd, ParagraphStart, PlainText, ParagraphEnd })]
        public async Task ParsesParagraphs(string text, object[] marks)
        {
            var actualMarks = await _parser.ParseAsync(text).With(new ElementMarkVisitor());

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), actualMarks);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesPreforamattedBlocks)))]
        [InlineData("{{{\nno wiki\n}}}", new object[] { PreformattedBlockStart, PlainText, PreformattedBlockEnd })]
        [InlineData("{{{\nno wiki end}}}{{{has to be at end of line\n}}}", new object[] { PreformattedBlockStart, PlainText, PreformattedBlockEnd })]
        [InlineData("{{{\nno wiki 1\n}}}\n{{{\nno wiki 2\n}}}", new object[] { PreformattedBlockStart, PlainText, PreformattedBlockEnd, PreformattedBlockStart, PlainText, PreformattedBlockEnd })]
        [InlineData("{{{\nno escape~\n}}}", new object[] { PreformattedBlockStart, PlainText, PreformattedBlockEnd })]
        [InlineData("{{{\nno **strong**, **emphasis**, [[hyperlinks]] or {{images}}\n}}}", new object[] { PreformattedBlockStart, PlainText, PreformattedBlockEnd })]
        [InlineData("~{{{image}}}", new object[] { ParagraphStart, PlainText, ImageStart, ImageSource, PlainText, ImageEnd, PlainText, ParagraphEnd })]
        [InlineData("{{{\ntest", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        public async Task ParsesPreforamattedBlocks(string text, object[] marks)
        {
            var actualMarks = await _parser.ParseAsync(text).With(new ElementMarkVisitor());

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), actualMarks);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesPlugins)))]
        [InlineData("<<plug in>>", new object[] { PluginStart, PlainText, PluginEnd })]
        [InlineData("<<<plug in>>", new object[] { PluginStart, PlainText, PluginEnd })]
        [InlineData("<<plug in>>>", new object[] { PluginStart, PlainText, PluginEnd })]
        [InlineData("<<<plug in>>>", new object[] { PluginStart, PlainText, PluginEnd })]
        [InlineData("~<<plain text>>", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("<<test", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        public async Task ParsesPlugins(string text, object[] marks)
        {
            var actualMarks = await _parser.ParseAsync(text).With(new ElementMarkVisitor());

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), actualMarks);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesTables)))]
        [InlineData("|cell 1|cell 2|", new object[] { TableStart, TableRowStart, TableCellStart, PlainText, TableCellEnd, TableCellStart, PlainText, TableCellEnd, TableRowEnd, TableEnd })]
        [InlineData("|cell 1|cell 2", new object[] { TableStart, TableRowStart, TableCellStart, PlainText, TableCellEnd, TableCellStart, PlainText, TableCellEnd, TableRowEnd, TableEnd })]
        [InlineData("|=header cell 1|=header cell 2|", new object[] { TableStart, TableRowStart, TableHeaderCellStart, PlainText, TableHeaderCellEnd, TableHeaderCellStart, PlainText, TableHeaderCellEnd, TableRowEnd, TableEnd })]
        [InlineData("|=header cell 1|=header cell 2", new object[] { TableStart, TableRowStart, TableHeaderCellStart, PlainText, TableHeaderCellEnd, TableHeaderCellStart, PlainText, TableHeaderCellEnd, TableRowEnd, TableEnd })]
        [InlineData("|=header cell 1|=header cell 2|\n|cell 1|cell 2|", new object[] { TableStart, TableRowStart, TableHeaderCellStart, PlainText, TableHeaderCellEnd, TableHeaderCellStart, PlainText, TableHeaderCellEnd, TableRowEnd, TableRowStart, TableCellStart, PlainText, TableCellEnd, TableCellStart, PlainText, TableCellEnd, TableRowEnd, TableEnd })]
        [InlineData("|=header cell 1|=header cell 2\n|cell 1|cell 2", new object[] { TableStart, TableRowStart, TableHeaderCellStart, PlainText, TableHeaderCellEnd, TableHeaderCellStart, PlainText, TableHeaderCellEnd, TableRowEnd, TableRowStart, TableCellStart, PlainText, TableCellEnd, TableCellStart, PlainText, TableCellEnd, TableRowEnd, TableEnd })]
        [InlineData("|table 1\n\n|table 2", new object[] { TableStart, TableRowStart, TableCellStart, PlainText, TableCellEnd, TableRowEnd, TableEnd, TableStart, TableRowStart, TableCellStart, PlainText, TableCellEnd, TableRowEnd, TableEnd })]
        [InlineData("|cell ~| 1|", new object[] { TableStart, TableRowStart, TableCellStart, PlainText, TableCellEnd, TableRowEnd, TableEnd })]
        [InlineData("|cell 1~~|cell 2|", new object[] { TableStart, TableRowStart, TableCellStart, PlainText, TableCellEnd, TableCellStart, PlainText, TableCellEnd, TableRowEnd, TableEnd })]
        [InlineData("|cell with //emphasis//, **strong**, [[hyperlink]], {{image}}, http://example.com , {{{no wiki}}}", new object[] { TableStart, TableRowStart, TableCellStart, PlainText, EmphasisStart, PlainText, EmphasisEnd, PlainText, StrongStart, PlainText, StrongEnd, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ImageStart, ImageSource, PlainText, ImageEnd, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, PreformattedTextStart, PlainText, PreformattedTextEnd, TableCellEnd, TableRowEnd, TableEnd })]
        [InlineData("|//no emphasis", new object[] { TableStart, TableRowStart, TableCellStart, PlainText, TableCellEnd, TableRowEnd, TableEnd })]
        public async Task ParsesTables(string text, object[] marks)
        {
            var actualMarks = await _parser.ParseAsync(text).With(new ElementMarkVisitor());

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), actualMarks);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesLists)))]
        [InlineData("*item", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd })]
        [InlineData("* item", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd })]
        [InlineData("* item 1\n*item 2", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd })]
        [InlineData("* item 1\n*item 2\n** sub item 1\n** sub item 2", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, UnorderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd, ListItemEnd, UnorderedListEnd })]
        [InlineData("* item 1\n*item 2\n** sub item 1\n** sub item 2\n* item 3", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, UnorderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd, ListItemEnd, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd })]
        [InlineData("#item", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, OrderedListEnd })]
        [InlineData("# item", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, OrderedListEnd })]
        [InlineData("# item 1\n#item 2", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, ListItemEnd, OrderedListEnd })]
        [InlineData("# item 1\n#item 2\n## sub item 1\n## sub item 2", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, OrderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, ListItemEnd, OrderedListEnd, ListItemEnd, OrderedListEnd })]
        [InlineData("# item 1\n#item 2\n## sub item 1\n## sub item 2\n# item 3", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, OrderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, ListItemEnd, OrderedListEnd, ListItemEnd, ListItemStart, PlainText, ListItemEnd, OrderedListEnd })]
        [InlineData("* unordered item\n# ordered item", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd, OrderedListStart, ListItemStart, PlainText, ListItemEnd, OrderedListEnd })]
        [InlineData("# ordered item\n* unordered item", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, OrderedListEnd, UnorderedListStart, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd })]
        [InlineData("* item 1\n*item 2\n## sub item 1\n## sub item 2", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, OrderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, ListItemEnd, OrderedListEnd, ListItemEnd, UnorderedListEnd })]
        [InlineData("# item 1\n#item 2\n** sub item 1\n** sub item 2", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, UnorderedListStart, ListItemStart, PlainText, ListItemEnd, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd, ListItemEnd, OrderedListEnd })]
        [InlineData("* unordered list\nline 2", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd })]
        [InlineData("* unordered list\n\nparagraph", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd, ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("# ordered list\nline 2", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, OrderedListEnd })]
        [InlineData("# ordered list\n\nparagraph", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, OrderedListEnd, ParagraphStart, PlainText, ParagraphEnd })]

        [InlineData("* plain //emphasised//, **strong**, {{image}}, [[hyperlink]], {{{no wiki}}}, http://example.com text", new object[] { UnorderedListStart, ListItemStart, PlainText, EmphasisStart, PlainText, EmphasisEnd, PlainText, StrongStart, PlainText, StrongEnd, PlainText, ImageStart, ImageSource, PlainText, ImageEnd, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, PreformattedTextStart, PlainText, PreformattedTextEnd, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ListItemEnd, UnorderedListEnd })]
        [InlineData("* no // emphasis", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd })]
        [InlineData("* no ** strong", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd })]
        [InlineData("* no [[ hyperlink", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd })]
        [InlineData("* no {{ image", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd })]
        [InlineData("* no {{{ code", new object[] { UnorderedListStart, ListItemStart, PlainText, ListItemEnd, UnorderedListEnd })]
        [InlineData("# plain //emphasised//, **strong**, {{image}}, [[hyperlink]], {{{no wiki}}}, http://example.com text", new object[] { OrderedListStart, ListItemStart, PlainText, EmphasisStart, PlainText, EmphasisEnd, PlainText, StrongStart, PlainText, StrongEnd, PlainText, ImageStart, ImageSource, PlainText, ImageEnd, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, PreformattedTextStart, PlainText, PreformattedTextEnd, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ListItemEnd, OrderedListEnd })]
        [InlineData("# no // emphasis", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, OrderedListEnd })]
        [InlineData("# no ** strong", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, OrderedListEnd })]
        [InlineData("# no [[ hyperlink", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, OrderedListEnd })]
        [InlineData("# no {{ image", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, OrderedListEnd })]
        [InlineData("# no {{{ code", new object[] { OrderedListStart, ListItemStart, PlainText, ListItemEnd, OrderedListEnd })]
        public async Task ParsesLists(string text, object[] marks)
        {
            var actualMarks = await _parser.ParseAsync(text).With(new ElementMarkVisitor());

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), actualMarks);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesEscapeCharacters)))]
        [InlineData("~plain text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("~~plain text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("~~~plain text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("~~~~plain text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("~~~~~plain text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        public async Task ParsesEscapeCharacters(string text, object[] marks)
        {
            var actualMarks = await _parser.ParseAsync(text).With(new ElementMarkVisitor());

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), actualMarks);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesRichText)))]
        [InlineData("**plain text**", new object[] { ParagraphStart, StrongStart, PlainText, StrongEnd, ParagraphEnd })]
        [InlineData("**plain text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("plain **now strong** text", new object[] { ParagraphStart, PlainText, StrongStart, PlainText, StrongEnd, PlainText, ParagraphEnd })]
        [InlineData("**paragraph 1\n\nparagrapg 2**", new object[] { ParagraphStart, PlainText, ParagraphEnd, ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("strong ** cannot\n\ncross ** paragraphs", new object[] { ParagraphStart, PlainText, ParagraphEnd, ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("escaped ~**strong", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("asterix with ~***strong** text", new object[] { ParagraphStart, PlainText, StrongStart, PlainText, StrongEnd, PlainText, ParagraphEnd })]

        [InlineData("//plain text//", new object[] { ParagraphStart, EmphasisStart, PlainText, EmphasisEnd, ParagraphEnd })]
        [InlineData("//plain text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("plain //now emphasised// text", new object[] { ParagraphStart, PlainText, EmphasisStart, PlainText, EmphasisEnd, PlainText, ParagraphEnd })]
        [InlineData("//paragraph 1\n\nparagrapg 2//", new object[] { ParagraphStart, PlainText, ParagraphEnd, ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("emphasis // cannot\n\ncross // paragraphs", new object[] { ParagraphStart, PlainText, ParagraphEnd, ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("escaped ~//emphasis", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("slash with ~///emphasised// text", new object[] { ParagraphStart, PlainText, EmphasisStart, PlainText, EmphasisEnd, PlainText, ParagraphEnd })]

        [InlineData("http://example.com", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("https://example.com", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("http://example.com/~", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("http://example.com/~~", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("HTTP://EXAMPLE.COM", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("~http://example.com", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("~http://example.com/ with no // emphasis", new object[] { ParagraphStart, PlainText, PlainText, ParagraphEnd })]
        [InlineData("~~http://example.com", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("~~~http://example.com/ with no // emphasis", new object[] { ParagraphStart, PlainText, PlainText, PlainText, ParagraphEnd })]
        [InlineData("tcp://example.com", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("//http://example.com//", new object[] { ParagraphStart, EmphasisStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, EmphasisEnd, ParagraphEnd })]
        [InlineData("http://example.com/", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("http://example.com//", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]
        [InlineData("http://example.com///", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]

        [InlineData("[[http://example.com]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("~[[http://example.com]]", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]
        [InlineData("~[[~http://example.com]]", new object[] { ParagraphStart, PlainText, PlainText, PlainText, ParagraphEnd })]
        [InlineData("[[http://example.com/~]]", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]
        [InlineData("[[http://example.com/~~]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com|text]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com/~|text]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[http://example.com]]", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]
        [InlineData("[[http://example.com]", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]
        [InlineData("[[http://example.com/~|text]", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]
        [InlineData("[[http://example.com/~~|http://example.com]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com/~~|//emphasis//]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, EmphasisStart, PlainText, EmphasisEnd, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com/~~|**strong**]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, StrongStart, PlainText, StrongEnd, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com/~~|**strong** and //emphasis//]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, StrongStart, PlainText, StrongEnd, PlainText, EmphasisStart, PlainText, EmphasisEnd, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com/~~|//emphasis// and **strong**]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, EmphasisStart, PlainText, EmphasisEnd, PlainText, StrongStart, PlainText, StrongEnd, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com/~~|//no emphasis]]//", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]
        [InlineData("[[http://example.com/~~|**no strong]]**", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]

        [InlineData("{{http://example.com/image.jpg}}", new object[] { ParagraphStart, ImageStart, ImageSource, PlainText, ImageEnd, ParagraphEnd })]
        [InlineData("~{{http://example.com/image.jpg}}", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]
        [InlineData("{{http://example.com/image.jpg|alternative text}}", new object[] { ParagraphStart, ImageStart, ImageSource, PlainText, ImageEnd, ParagraphEnd })]
        [InlineData("{{http://example.com/image.jpg|**no strong alternative text**}}", new object[] { ParagraphStart, ImageStart, ImageSource, PlainText, ImageEnd, ParagraphEnd })]
        [InlineData("{{http://example.com/image.jpg|//no emphasised alternative text//}}", new object[] { ParagraphStart, ImageStart, ImageSource, PlainText, ImageEnd, ParagraphEnd })]
        [InlineData("{http://example.com/image.jpg}}", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]
        [InlineData("{{http://example.com/image.jpg}", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]

        [InlineData(@"line\\break", new object[] { ParagraphStart, PlainText, LineBreak, PlainText, ParagraphEnd })]
        [InlineData(@"line~\\break", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData(@"line~~\\break", new object[] { ParagraphStart, PlainText, LineBreak, PlainText, ParagraphEnd })]
        [InlineData(@"line~\\\break", new object[] { ParagraphStart, PlainText, LineBreak, PlainText, ParagraphEnd })]

        [InlineData("plain {{{no wiki}}} text", new object[] { ParagraphStart, PlainText, PreformattedTextStart, PlainText, PreformattedTextEnd, PlainText, ParagraphEnd })]
        [InlineData("plain {{{escaped nowiki end ~}}} text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("plain {{{no **strong**, no //emphais//, no [[hyperlinks]], no {{images}}}}} text", new object[] { ParagraphStart, PlainText, PreformattedTextStart, PlainText, PreformattedTextEnd, PlainText, ParagraphEnd })]
        [InlineData("plain {{{no wiki}}} text", new object[] { ParagraphStart, PlainText, PreformattedTextStart, PlainText, PreformattedTextEnd, PlainText, ParagraphEnd })]
        [InlineData("plain {{{}}} text", new object[] { ParagraphStart, PlainText, PreformattedTextStart, PlainText, PreformattedTextEnd, PlainText, ParagraphEnd })]
        [InlineData("preformatted text {{{\n\n}}} cannot bridge paragraphs", new object[] { ParagraphStart, PlainText, ParagraphEnd, ParagraphStart, PlainText, ParagraphEnd })]

        [InlineData("**//mixed strong emphasis**//", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("//**mixed emphasis strong//**", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        public async Task ParsesRichText(string text, object[] marks)
        {
            var actualMarks = await _parser.ParseAsync(text).With(new ElementMarkVisitor());

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), actualMarks.ToArray());
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesHorizontalRules)))]
        [InlineData("----", new object[] { HorizontalRule })]
        [InlineData("  ----", new object[] { HorizontalRule })]
        [InlineData("\t----", new object[] { HorizontalRule })]
        [InlineData("\r----", new object[] { HorizontalRule })]
        [InlineData("\n----", new object[] { HorizontalRule })]
        [InlineData("--------", new object[] { HorizontalRule })]
        [InlineData("paragraph 1\n----\nparagraph 2", new object[] { ParagraphStart, PlainText, ParagraphEnd, HorizontalRule, ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("~----", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        public async Task ParsesHorizontalRules(string text, object[] marks)
        {
            var actualMarks = await _parser.ParseAsync(text).With(new ElementMarkVisitor());

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), actualMarks);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseHeadingsToHtml)))]
        [InlineData("= plain text", "<h1>plain text</h1>")]
        [InlineData("= heading 1\n== heading 2", "<h1>heading 1</h1><h2>heading 2</h2>")]
        [InlineData("= plain text ", "<h1>plain text</h1>")]
        [InlineData("= plain text =", "<h1>plain text</h1>")]
        [InlineData("= plain text ==", "<h1>plain text</h1>")]
        [InlineData("= plain text **strong**", "<h1>plain text **strong**</h1>")]
        [InlineData("= plain text //emphasis//", "<h1>plain text //emphasis//</h1>")]
        [InlineData("= plain text [[link]]", "<h1>plain text [[link]]</h1>")]
        [InlineData("= plain text [[link|with text]]", "<h1>plain text [[link|with text]]</h1>")]
        [InlineData("= plain text {{image}}", "<h1>plain text {{image}}</h1>")]
        [InlineData("= plain text {{image|with title}}", "<h1>plain text {{image|with title}}</h1>")]
        [InlineData("= plain text <<plug in>>", "<h1>plain text &lt;&lt;plug in&gt;&gt;</h1>")]
        [InlineData("paragraph\n= heading", "<p>paragraph</p><h1>heading</h1>")]
        [InlineData("paragraph 1\n= heading\nparagraph 2", "<p>paragraph 1</p><h1>heading</h1><p>paragraph 2</p>")]
        [InlineData("= heading\nparagraph", "<h1>heading</h1><p>paragraph</p>")]
        public async Task ParseHeadingsToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseParagraphsToHtml)))]
        [InlineData("plain text", "<p>plain text</p>")]
        [InlineData("paragraph 1\n\nparagraph 2", "<p>paragraph 1</p><p>paragraph 2</p>")]
        public async Task ParseParagraphsToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsePreforamattedBlocksToHtml)))]
        [InlineData("{{{\nno wiki\n}}}", "<pre><code>no wiki</code></pre>")]
        [InlineData("{{{\n}}}", "<pre><code></code></pre>")]
        [InlineData("{{{\n\n}}}", "<pre><code>\n</code></pre>")]
        [InlineData("{{{\nno wiki end}}}{{{has to be at end of line\n}}}", "<pre><code>no wiki end}}}{{{has to be at end of line</code></pre>")]
        [InlineData("{{{\nno wiki 1\n}}}\n{{{\nno wiki 2\n}}}", "<pre><code>no wiki 1</code></pre><pre><code>no wiki 2</code></pre>")]
        [InlineData("{{{\nno escape~\n}}}", "<pre><code>no escape~</code></pre>")]
        [InlineData("{{{\nno **strong**, **emphasis**, [[hyperlinks]] or {{images}}\n}}}", "<pre><code>no **strong**, **emphasis**, [[hyperlinks]] or {{images}}</code></pre>")]
        [InlineData("~{{{image}}}", "<p>{<img src=\"image\">}</p>")]
        [InlineData("{{{\ntest", "<p>{{{\ntest</p>")]
        [InlineData("{{{\n\n{{{\nThis is a nowiki markup block showing nowiki markup usage in a wiki (complicated stuff ;)\n~}}}\n\n}}}", "<pre><code>\n{{{\nThis is a nowiki markup block showing nowiki markup usage in a wiki (complicated stuff ;)\n}}}\n</code></pre>")]
        [InlineData("paragraph\n{{{\ncode\n}}}", "<p>paragraph</p><pre><code>code</code></pre>")]
        [InlineData("paragraph 1\n{{{\ncode\n}}}\nparagraph 2", "<p>paragraph 1</p><pre><code>code</code></pre><p>paragraph 2</p>")]
        [InlineData("{{{\ncode\n}}}\nparagraph", "<pre><code>code</code></pre><p>paragraph</p>")]
        public async Task ParsePreforamattedBlocksToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseTablesToHtml)))]
        [InlineData("|cell 1|cell 2|", "<table><tbody><tr><td>cell 1</td><td>cell 2</td></tr></tbody></table>")]
        [InlineData("|cell 1|cell 2", "<table><tbody><tr><td>cell 1</td><td>cell 2</td></tr></tbody></table>")]
        [InlineData("|=header cell 1|=header cell 2|", "<table><tbody><tr><th>header cell 1</th><th>header cell 2</th></tr></tbody></table>")]
        [InlineData("|=header cell 1|=header cell 2", "<table><tbody><tr><th>header cell 1</th><th>header cell 2</th></tr></tbody></table>")]
        [InlineData("|=header cell 1|=header cell 2|\n|cell 1|cell 2|", "<table><tbody><tr><th>header cell 1</th><th>header cell 2</th></tr><tr><td>cell 1</td><td>cell 2</td></tr></tbody></table>")]
        [InlineData("|=header cell 1|=header cell 2\n|cell 1|cell 2", "<table><tbody><tr><th>header cell 1</th><th>header cell 2</th></tr><tr><td>cell 1</td><td>cell 2</td></tr></tbody></table>")]
        [InlineData("|table 1\n\n|table 2", "<table><tbody><tr><td>table 1</td></tr></tbody></table><table><tbody><tr><td>table 2</td></tr></tbody></table>")]
        [InlineData("|cell ~| 1|", "<table><tbody><tr><td>cell | 1</td></tr></tbody></table>")]
        [InlineData("|cell 1~~|cell 2|", "<table><tbody><tr><td>cell 1~</td><td>cell 2</td></tr></tbody></table>")]
        [InlineData("|cell with //emphasis//, **strong**, [[hyperlink]], {{image}}, http://example.com , {{{no wiki}}}", "<table><tbody><tr><td>cell with <em>emphasis</em>, <strong>strong</strong>, <a href=\"hyperlink\">hyperlink</a>, <img src=\"image\">, <a href=\"http://example.com\">http://example.com</a> , <code>no wiki</code></td></tr></tbody></table>")]
        [InlineData("|//no emphasis", "<table><tbody><tr><td>//no emphasis</td></tr></tbody></table>")]
        [InlineData("|= \t white space is trimmed \t |= \t white space is trimmed \t \n| \t white space is trimmed \t | \t white space is trimmed \t ", "<table><tbody><tr><th>white space is trimmed</th><th>white space is trimmed</th></tr><tr><td>white space is trimmed</td><td>white space is trimmed</td></tr></tbody></table>")]
        [InlineData("|cell||", "<table><tbody><tr><td>cell</td><td></td></tr></tbody></table>")]
        [InlineData("|cell|", "<table><tbody><tr><td>cell</td></tr></tbody></table>")]
        [InlineData("|cell| |", "<table><tbody><tr><td>cell</td><td></td></tr></tbody></table>")]
        [InlineData("||cell|", "<table><tbody><tr><td></td><td>cell</td></tr></tbody></table>")]
        [InlineData("||cell", "<table><tbody><tr><td></td><td>cell</td></tr></tbody></table>")]
        [InlineData("paragraph\n|cell", "<p>paragraph</p><table><tbody><tr><td>cell</td></tr></tbody></table>")]
        [InlineData("paragraph 1\n|cell\nparagraph 2", "<p>paragraph 1</p><table><tbody><tr><td>cell</td></tr></tbody></table><p>paragraph 2</p>")]
        [InlineData("|cell\nparagraph", "<table><tbody><tr><td>cell</td></tr></tbody></table><p>paragraph</p>")]
        public async Task ParseTablesToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseListsToHtml)))]
        [InlineData("*item", "<ul><li>item</li></ul>")]
        [InlineData("* item", "<ul><li>item</li></ul>")]
        [InlineData("* item 1\n*item 2", "<ul><li>item 1</li><li>item 2</li></ul>")]
        [InlineData("* item 1\n*item 2\n** sub item 1\n** sub item 2", "<ul><li>item 1</li><li>item 2<ul><li>sub item 1</li><li>sub item 2</li></ul></li></ul>")]
        [InlineData("* item 1\n*item 2\n** sub item 1\n** sub item 2\n* item 3", "<ul><li>item 1</li><li>item 2<ul><li>sub item 1</li><li>sub item 2</li></ul></li><li>item 3</li></ul>")]
        [InlineData("# item 1\n#item 2", "<ol><li>item 1</li><li>item 2</li></ol>")]
        [InlineData("# item 1\n#item 2\n## sub item 1\n## sub item 2", "<ol><li>item 1</li><li>item 2<ol><li>sub item 1</li><li>sub item 2</li></ol></li></ol>")]
        [InlineData("# item 1\n#item 2\n## sub item 1\n## sub item 2\n# item 3", "<ol><li>item 1</li><li>item 2<ol><li>sub item 1</li><li>sub item 2</li></ol></li><li>item 3</li></ol>")]
        [InlineData("* unordered item\n# ordered item", "<ul><li>unordered item</li></ul><ol><li>ordered item</li></ol>")]
        [InlineData("# ordered item\n* unordered item", "<ol><li>ordered item</li></ol><ul><li>unordered item</li></ul>")]
        [InlineData("* item 1\n*item 2\n## sub item 1\n## sub item 2", "<ul><li>item 1</li><li>item 2<ol><li>sub item 1</li><li>sub item 2</li></ol></li></ul>")]
        [InlineData("# item 1\n#item 2\n** sub item 1\n** sub item 2", "<ol><li>item 1</li><li>item 2<ul><li>sub item 1</li><li>sub item 2</li></ul></li></ol>")]
        [InlineData("* unordered list\nline 2", "<ul><li>unordered list\nline 2</li></ul>")]
        [InlineData("* unordered list\n\nparagraph", "<ul><li>unordered list</li></ul><p>paragraph</p>")]
        [InlineData("# ordered list\nline 2", "<ol><li>ordered list\nline 2</li></ol>")]
        [InlineData("# ordered list\n\nparagraph", "<ol><li>ordered list</li></ol><p>paragraph</p>")]
        [InlineData("* plain //emphasised//, **strong**, {{image}}, [[hyperlink]], {{{no wiki}}}, http://example.com text", "<ul><li>plain <em>emphasised</em>, <strong>strong</strong>, <img src=\"image\">, <a href=\"hyperlink\">hyperlink</a>, <code>no wiki</code>, <a href=\"http://example.com\">http://example.com</a> text</li></ul>")]
        [InlineData("* no // emphasis", "<ul><li>no // emphasis</li></ul>")]
        [InlineData("* no ** strong", "<ul><li>no ** strong</li></ul>")]
        [InlineData("* no [[ hyperlink", "<ul><li>no [[ hyperlink</li></ul>")]
        [InlineData("* no {{ image", "<ul><li>no {{ image</li></ul>")]
        [InlineData("* no {{{ code", "<ul><li>no {{{ code</li></ul>")]
        [InlineData("# plain //emphasised//, **strong**, {{image}}, [[hyperlink]], {{{no wiki}}}, http://example.com text", "<ol><li>plain <em>emphasised</em>, <strong>strong</strong>, <img src=\"image\">, <a href=\"hyperlink\">hyperlink</a>, <code>no wiki</code>, <a href=\"http://example.com\">http://example.com</a> text</li></ol>")]
        [InlineData("# no // emphasis", "<ol><li>no // emphasis</li></ol>")]
        [InlineData("# no ** strong", "<ol><li>no ** strong</li></ol>")]
        [InlineData("# no [[ hyperlink", "<ol><li>no [[ hyperlink</li></ol>")]
        [InlineData("# no {{ image", "<ol><li>no {{ image</li></ol>")]
        [InlineData("# no {{{ code", "<ol><li>no {{{ code</li></ol>")]
        [InlineData("* list 1\n\n*list 2", "<ul><li>list 1</li></ul><ul><li>list 2</li></ul>")]
        [InlineData("# list 1\n\n#list 2", "<ol><li>list 1</li></ol><ol><li>list 2</li></ol>")]
        [InlineData("*item 1\n***invalid sub-item", "<ul><li>item 1</li></ul><p>***invalid sub-item</p>")]
        [InlineData("*item 1\n###invalid sub-item", "<ul><li>item 1</li></ul><p>###invalid sub-item</p>")]
        [InlineData("#item 1\n***invalid sub-item", "<ol><li>item 1</li></ol><p>***invalid sub-item</p>")]
        [InlineData("#item 1\n###invalid sub-item", "<ol><li>item 1</li></ol><p>###invalid sub-item</p>")]
        [InlineData("*item 1\n*** invalid sub-item", "<ul><li>item 1</li></ul><p>*** invalid sub-item</p>")]
        [InlineData("*item 1\n### invalid sub-item", "<ul><li>item 1</li></ul><p>### invalid sub-item</p>")]
        [InlineData("#item 1\n*** invalid sub-item", "<ol><li>item 1</li></ol><p>*** invalid sub-item</p>")]
        [InlineData("#item 1\n### invalid sub-item", "<ol><li>item 1</li></ol><p>### invalid sub-item</p>")]
        [InlineData("paragraph 1\n*item 1\n\nparagraph 2", "<p>paragraph 1</p><ul><li>item 1</li></ul><p>paragraph 2</p>")]
        [InlineData("paragraph\n*item 1", "<p>paragraph</p><ul><li>item 1</li></ul>")]
        [InlineData("*item 1\n\nparagraph", "<ul><li>item 1</li></ul><p>paragraph</p>")]
        [InlineData("paragraph 1\n#item 1\n\nparagraph 2", "<p>paragraph 1</p><ol><li>item 1</li></ol><p>paragraph 2</p>")]
        [InlineData("paragraph\n#item 1", "<p>paragraph</p><ol><li>item 1</li></ol>")]
        [InlineData("#item 1\n\nparagraph", "<ol><li>item 1</li></ol><p>paragraph</p>")]
        public async Task ParseListsToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesEscapeCharactersToHtml)))]
        [InlineData("~plain text", "<p>~plain text</p>")]
        [InlineData("~~plain text", "<p>~plain text</p>")]
        [InlineData("~~~plain text", "<p>~~plain text</p>")]
        [InlineData("~~~~plain text", "<p>~~plain text</p>")]
        [InlineData("~~~~~plain text", "<p>~~~plain text</p>")]
        public async Task ParsesEscapeCharactersToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesRichTextToHtml)))]

        [InlineData("", "")]
        [InlineData("    ", "")]
        [InlineData("\t\t", "")]
        [InlineData("\r\r", "")]
        [InlineData("\n\n", "")]

        [InlineData("**strong text**", "<p><strong>strong text</strong></p>")]
        [InlineData("plain **now strong** text", "<p>plain <strong>now strong</strong> text</p>")]
        [InlineData("**paragraph 1\n\nparagraph 2**", "<p>**paragraph 1</p><p>paragraph 2**</p>")]
        [InlineData("strong ** cannot\n\ncross ** paragraphs", "<p>strong ** cannot</p><p>cross ** paragraphs</p>")]
        [InlineData("escaped ~**strong", "<p>escaped **strong</p>")]
        [InlineData("asterix with ***strong** text", "<p>asterix with *<strong>strong</strong> text</p>")]
        [InlineData("asterix with **strong*** text", "<p>asterix with <strong>strong</strong>* text</p>")]
        [InlineData("asterix with ~***strong** text", "<p>asterix with *<strong>strong</strong> text</p>")]
        [InlineData("asterix with **strong~*** text", "<p>asterix with <strong>strong*</strong> text</p>")]

        [InlineData("//emphasised text//", "<p><em>emphasised text</em></p>")]
        [InlineData("plain //now emphasised// text", "<p>plain <em>now emphasised</em> text</p>")]
        [InlineData("//paragraph 1\n\nparagraph 2//", "<p>//paragraph 1</p><p>paragraph 2//</p>")]
        [InlineData("emphasis // cannot\n\ncross // paragraphs", "<p>emphasis // cannot</p><p>cross // paragraphs</p>")]
        [InlineData("escaped ~//emphasis", "<p>escaped //emphasis</p>")]
        [InlineData("slash with ///emphasised// text", "<p>slash with /<em>emphasised</em> text</p>")]
        [InlineData("slash with //emphasised/// text", "<p>slash with <em>emphasised</em>/ text</p>")]
        [InlineData("slash with ~///emphasised// text", "<p>slash with /<em>emphasised</em> text</p>")]
        [InlineData("slash with //emphasised~/// text", "<p>slash with <em>emphasised/</em> text</p>")]

        [InlineData("**//strong and emphasised//**", "<p><strong><em>strong and emphasised</em></strong></p>")]
        [InlineData("//**emphasised and strong**//", "<p><em><strong>emphasised and strong</strong></em></p>")]

        [InlineData("http://example.com", "<p><a href=\"http://example.com\">http://example.com</a></p>")]
        [InlineData("http://example.com:8080", "<p><a href=\"http://example.com:8080\">http://example.com:8080</a></p>")]
        [InlineData("http://example.com:8080/test", "<p><a href=\"http://example.com:8080/test\">http://example.com:8080/test</a></p>")]
        [InlineData("https://example.com", "<p><a href=\"https://example.com\">https://example.com</a></p>")]
        [InlineData("http://example.com/~", "<p><a href=\"http://example.com/~\">http://example.com/~</a></p>")]
        [InlineData("http://example.com/~~", "<p><a href=\"http://example.com/~\">http://example.com/~</a></p>")]
        [InlineData("HTTP://EXAMPLE.COM", "<p><a href=\"HTTP://EXAMPLE.COM\">HTTP://EXAMPLE.COM</a></p>")]
        [InlineData("~http://example.com", "<p>http://example.com</p>")]
        [InlineData("~http://example.com/ with no // emphasis", "<p>http://example.com/ with no // emphasis</p>")]
        [InlineData("~~http://example.com", "<p>~<a href=\"http://example.com\">http://example.com</a></p>")]
        [InlineData("~~~http://example.com", "<p>~http://example.com</p>")]
        [InlineData("tcp://example.com", "<p>tcp://example.com</p>")]
        [InlineData("//http://example.com//", "<p><em><a href=\"http://example.com\">http://example.com</a></em></p>")]
        [InlineData("http://example.com/", "<p><a href=\"http://example.com/\">http://example.com/</a></p>")]
        [InlineData("http://example.com//", "<p><a href=\"http://example.com\">http://example.com</a>//</p>")]
        [InlineData("http://example.com~//", "<p><a href=\"http://example.com//\">http://example.com//</a></p>")]
        [InlineData("http://example.com/~/", "<p><a href=\"http://example.com//\">http://example.com//</a></p>")]
        [InlineData("http://example.com///", "<p><a href=\"http://example.com/\">http://example.com/</a>//</p>")]
        [InlineData("http://example.com////", "<p><a href=\"http://example.com/\">http://example.com/</a>///</p>")]

        [InlineData("[[http://example.com]]", "<p><a href=\"http://example.com\">http://example.com</a></p>")]
        [InlineData("[[http://example.com/~~]]", "<p><a href=\"http://example.com/~\">http://example.com/~</a></p>")]
        [InlineData("[[http://example.com|text]]", "<p><a href=\"http://example.com\">text</a></p>")]
        [InlineData("[[http://example.com/~~|text]]", "<p><a href=\"http://example.com/~\">text</a></p>")]
        [InlineData("[http://example.com]]", "<p>[<a href=\"http://example.com\">http://example.com</a>]]</p>")]
        [InlineData("[[http://example.com]", "<p>[[<a href=\"http://example.com\">http://example.com</a>]</p>")]
        [InlineData("[[http://example.com/~~|text]", "<p>[[<a href=\"http://example.com/~|text\">http://example.com/~|text</a>]</p>")]
        [InlineData("[[http://example.com/~~|http://example.com]]", "<p><a href=\"http://example.com/~\">http://example.com</a></p>")]
        [InlineData("[[http://example.com/~~|//emphasis//]]", "<p><a href=\"http://example.com/~\"><em>emphasis</em></a></p>")]
        [InlineData("[[http://example.com/~~|**strong**]]", "<p><a href=\"http://example.com/~\"><strong>strong</strong></a></p>")]
        [InlineData("[[http://example.com/~~|**strong** and //emphasis//]]", "<p><a href=\"http://example.com/~\"><strong>strong</strong> and <em>emphasis</em></a></p>")]
        [InlineData("[[http://example.com/~~|//emphasis// and **strong**]]", "<p><a href=\"http://example.com/~\"><em>emphasis</em> and <strong>strong</strong></a></p>")]
        [InlineData("[[http://example.com/~~|//no emphasis]]//", "<p><a href=\"http://example.com/~\">//no emphasis</a>//</p>")]
        [InlineData("[[http://example.com/~~|**no strong]]**", "<p><a href=\"http://example.com/~\">**no strong</a>**</p>")]
        [InlineData("[[this is a link]]", "<p><a href=\"this is a link\">this is a link</a></p>")]
        [InlineData("[[this is a link|with a different name]]", "<p><a href=\"this is a link\">with a different name</a></p>")]
        [InlineData("//**[[Important page]]**//", "<p><em><strong><a href=\"Important page\">Important page</a></strong></em></p>")]
        [InlineData("//[[Important page|this link is italicized]]//\n**[[Important page]]**\n//**[[Important page]]**//", "<p><em><a href=\"Important page\">this link is italicized</a></em>\n<strong><a href=\"Important page\">Important page</a></strong>\n<em><strong><a href=\"Important page\">Important page</a></strong></em></p>")]

        [InlineData("{{http://example.com/image.jpg}}", "<p><img src=\"http://example.com/image.jpg\"></p>")]
        [InlineData("~{{http://example.com/image.jpg}}", "<p>{{<a href=\"http://example.com/image.jpg\">http://example.com/image.jpg</a>}}</p>")]
        [InlineData("{{http://example.com/image.jpg|alternative text}}", "<p><img src=\"http://example.com/image.jpg\" alt=\"alternative text\"></p>")]
        [InlineData("{{http://example.com/image.jpg|**no strong alternative text**}}", "<p><img src=\"http://example.com/image.jpg\" alt=\"**no strong alternative text**\"></p>")]
        [InlineData("{{http://example.com/image.jpg|//no emphasised alternative text//}}", "<p><img src=\"http://example.com/image.jpg\" alt=\"//no emphasised alternative text//\"></p>")]
        [InlineData("{http://example.com/image.jpg}}", "<p>{<a href=\"http://example.com/image.jpg\">http://example.com/image.jpg</a>}}</p>")]
        [InlineData("{{http://example.com/image.jpg}", "<p>{{<a href=\"http://example.com/image.jpg\">http://example.com/image.jpg</a>}</p>")]
        [InlineData("{{this is a link}}", "<p><img src=\"this is a link\"></p>")]
        [InlineData("{{this is a link|with an alternative text}}", "<p><img src=\"this is a link\" alt=\"with an alternative text\"></p>")]

        [InlineData(@"line\\break", @"<p>line<br>break</p>")]
        [InlineData(@"line~\\break", @"<p>line\\break</p>")]
        [InlineData(@"line~~\\break", @"<p>line~<br>break</p>")]
        [InlineData(@"line~\\\break", @"<p>line\<br>break</p>")]
        [InlineData(@"double line\\\\break", @"<p>double line<br><br>break</p>")]

        [InlineData("plain {{{no wiki}}} text", "<p>plain <code>no wiki</code> text</p>")]
        [InlineData("plain {{{no escape ~~}}} text", "<p>plain <code>no escape ~</code> text</p>")]
        [InlineData("plain {{{code can contain ~}}} only when escaped}}} text", "<p>plain <code>code can contain }}} only when escaped</code> text</p>")]
        [InlineData("plain {{{no **strong**, no //emphais//, no [[hyperlinks]], no {{images}}}}} text", "<p>plain <code>no **strong**, no //emphais//, no [[hyperlinks]], no {{images}}</code> text</p>")]
        [InlineData("plain {{{no wiki}}} text", "<p>plain <code>no wiki</code> text</p>")]
        [InlineData("plain {{{}}} text", "<p>plain <code></code> text</p>")]
        [InlineData("preformatted text {{{\n\n}}} cannot bridge paragraphs", "<p>preformatted text {{{</p><p>}}} cannot bridge paragraphs</p>")]

        [InlineData("**//mixed strong emphasis**//", "<p>**//mixed strong emphasis**//</p>")]
        [InlineData("**//mixed strong emphasis**// still no emphasis//", "<p>**//mixed strong emphasis**// still no emphasis//</p>")]
        [InlineData("**//mixed strong emphasis**// still no strong**", "<p>**//mixed strong emphasis**// still no strong**</p>")]
        [InlineData("//**mixed emphasis strong//**", "<p>//**mixed emphasis strong//**</p>")]
        [InlineData("//**mixed emphasis strong//** still no strong**", "<p>//**mixed emphasis strong//** still no strong**</p>")]
        [InlineData("//**mixed emphasis strong//** still no emphasis//", "<p>//**mixed emphasis strong//** still no emphasis//</p>")]

        [InlineData("test <<test>> test", "<p>test <!-- test --> test</p>")]
        [InlineData("test\n\n<<test>>\n\ntest", "<p>test</p><!-- test --><p>test</p>")]

        [InlineData("plain **bold 1*** plain ****bold 2***** plain", "<p>plain <strong>bold 1</strong>* plain **<strong>bold 2</strong>*** plain</p>")]
        public async Task ParsesRichTextToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesHorizontalRuleToHtml)))]
        [InlineData("----", "<hr>")]
        [InlineData("  ----", "<hr>")]
        [InlineData("\t----", "<hr>")]
        [InlineData("\r----", "<hr>")]
        [InlineData("\n----", "<hr>")]
        [InlineData("--------", "<hr>")]
        [InlineData("paragraph 1\n----\nparagraph 2", "<p>paragraph 1</p><hr><p>paragraph 2</p>")]
        [InlineData("~----", "<p>----</p>")]
        public async Task ParsesHorizontalRuleToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesPluginsToHtml)))]
        [InlineData("<<plug in>>", "<!-- plug in -->")]
        [InlineData("<<<plug in>>", "<!-- &lt;plug in -->")]
        [InlineData("<<plug in>>>", "<!-- plug in&gt; -->")]
        [InlineData("<<<plug in>>>", "<!-- &lt;plug in&gt; -->")]
        [InlineData("~<<plain text>>", "<p>&lt;&lt;plain text&gt;&gt;</p>")]
        [InlineData("<<test", "<p>&lt;&lt;test</p>")]
        [InlineData("paragraph\n<<plug in>>", "<p>paragraph</p><!-- plug in -->")]
        [InlineData("paragraph 1\n<<plug in>>\nparagraph 2", "<p>paragraph 1</p><!-- plug in --><p>paragraph 2</p>")]
        [InlineData("<<plug in>>\nparagraph", "<!-- plug in --><p>paragraph</p>")]
        public async Task ParsesPluginsToHtml(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }
    }
}