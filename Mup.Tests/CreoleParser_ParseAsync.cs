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
        [InlineData("not://example.com", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
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
        [InlineData("http://example", "<p><a href=\"http://example\">http://example</a></p>")]

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

        [Trait("Class", nameof(CreoleParser))]
        [Trait("WebSite", "www.wikicreole.org")]
        [Theory(DisplayName = (_method + nameof(CreoleSiteTestsCase)))]
        [InlineData(
            "**//bold italics**//\n" +
            "//**bold italics//**",
            "<p>**//bold italics**//\n" +
            "//**bold italics//**</p>")]
        [InlineData(
            "",
            "")]
        [InlineData(
            "**bold**",
            "<p><strong>bold</strong></p>")]
        [InlineData(
            "//italics//",
            "<p><em>italics</em></p>")]
        [InlineData(
            "Bold and italics should //be\n" +
            "able// to cross lines.\n" +
            "\n" +
            "But, should //not be...\n" +
            "\n" +
            "...able// to cross paragraphs.",
            "<p>Bold and italics should <em>be\n" +
            "able</em> to cross lines.</p>"
            + "<p>But, should //not be...</p>"
            + "<p>...able// to cross paragraphs.</p>")]
        [InlineData(
            "**//bold italics//**\n" +
            "//**bold italics**//\n" +
            "//This is **also** good.//",
            "<p><strong><em>bold italics</em></strong>\n" +
            "<em><strong>bold italics</strong></em>\n" +
            "<em>This is <strong>also</strong> good.</em></p>")]
        [InlineData(
            "= Level 1 (largest) =\n" +
            "== Level 2 ==\n" +
            "=== Level 3 ===\n" +
            "==== Level 4 ====\n" +
            "===== Level 5 =====\n" +
            "====== Level 6 ======\n" +
            "=== Also level 3\n" +
            "=== Also level 3 =\n" +
            "=== Also level 3 ==\n" +
            "=== **not** //parsed// ===",
            "<h1>Level 1 (largest)</h1>"
            + "<h2>Level 2</h2>"
            + "<h3>Level 3</h3>"
            + "<h4>Level 4</h4>"
            + "<h5>Level 5</h5>"
            + "<h6>Level 6</h6>"
            + "<h3>Also level 3</h3>"
            + "<h3>Also level 3</h3>"
            + "<h3>Also level 3</h3>"
            + "<h3>**not** //parsed//</h3>")]
        [InlineData(
            "[[link]]\n" +
            "[[MyBigPage|Go to my page]]\n" +
            "[[http://www.wikicreole.org/]]\n" +
            "http://www.rawlink.org/, http://www.another.rawlink.org\n" +
            "[[http://www.wikicreole.org/|Visit the WikiCreole website]]\n" +
            "[[Weird Stuff|**Weird** //Stuff//]]\n" +
            "[[Ohana:WikiFamily]]",
            "<p><a href=\"link\">link</a>\n" +
            "<a href=\"MyBigPage\">Go to my page</a>\n" +
            "<a href=\"http://www.wikicreole.org/\">http://www.wikicreole.org/</a>\n" +
            "<a href=\"http://www.rawlink.org/\">http://www.rawlink.org/</a>, <a href=\"http://www.another.rawlink.org\">http://www.another.rawlink.org</a>\n" +
            "<a href=\"http://www.wikicreole.org/\">Visit the WikiCreole website</a>\n" +
            "<a href=\"Weird Stuff\"><strong>Weird</strong> <em>Stuff</em></a>\n" +
            "<a href=\"Ohana:WikiFamily\">Ohana:WikiFamily</a></p>")]
        [InlineData(
            @"This is the first line,\\and this is the second.",
            "<p>This is the first line,<br>and this is the second.</p>")]
        [InlineData(
            "This is my text.\n" +
            "\n" +
            "This is more text.",
            "<p>This is my text.</p>"
            + "<p>This is more text.</p>")]
        [InlineData(
            "* Item 1\n" +
            "* Item 2\n" +
            "** Item 2.1\n" +
            "* Item 3",
            "<ul>" +
                "<li>Item 1</li>" +
                "<li>" +
                    "Item 2" +
                    "<ul>" +
                        "<li>Item 2.1</li>" +
                    "</ul>" +
                "</li>" +
                "<li>Item 3</li>" +
            "</ul>")]
        [InlineData(
            "# Item 1\n" +
            "## Item 1.1\n" +
            "# Item 2\n" +
            "\n" +
            " # Blanks\n" +
            "    ## are also\n" +
            "  # allowed.",
            "<ol>" +
                "<li>" +
                    "Item 1" +
                    "<ol>" +
                    "<li>Item 1.1</li>" +
                    "</ol>" +
                "</li>" +
                "<li>Item 2</li>" +
            "</ol>" +
            "<ol>" +
                "<li>" +
                    "Blanks" +
                    "<ol>" +
                        "<li>are also</li>" +
                    "</ol>" +
                "</li>" +
                "<li>allowed.</li>" +
            "</ol>")]
        [InlineData(
            "Text\n" +
            "\n" +
            "----\n" +
            "\n" +
            "Text",
            "<p>Text</p>" +
            "<hr>" +
            "<p>Text</p>")]
        [InlineData(
            "{{myimage.png|this is my image}}",
            "<p><img src=\"myimage.png\" alt=\"this is my image\"></p>")]
        [InlineData(
            "{{{\n" +
            "//This// does **not** get [[formatted]] \n" +
            "}}}",
            "<pre><code>//This// does **not** get [[formatted]] </code></pre>")]
        [InlineData(
            "Some examples of markup are: {{{** <i>this</i> ** }}}, this is also inline literal markup: {{{{{if (a>b) { b = a; }}}}}}.",
            "<p>Some examples of markup are: <code>** &lt;i&gt;this&lt;/i&gt; ** </code>, this is also inline literal markup: <code>{{if (a&gt;b) { b = a; }}}</code>.</p>")]
        [InlineData(
            "{{{\n" +
            "if (x != NULL) {\n" +
            "  for (i = 0; i < size; i++) {\n" +
            "    if (x[i] > 0) {\n" +
            "      x[i]--;\n" +
            "  }}}\n" +
            "}}}",
            "<pre><code>if (x != NULL) {\n" +
            "  for (i = 0; i &lt; size; i++) {\n" +
            "    if (x[i] &gt; 0) {\n" +
            "      x[i]--;\n" +
            "  }}}</code></pre>")]
        [InlineData(
            "~#1\n" +
            "http://www.foo.com/~bar/\n" +
            "~http://www.foo.com/\n" +
            "CamelCaseLink\n" +
            "~CamelCaseLink",
            "<p>#1\n" +
            "<a href=\"http://www.foo.com/~bar/\">http://www.foo.com/~bar/</a>\n" +
            "http://www.foo.com/\n" +
            "CamelCaseLink\n" +
            "~CamelCaseLink</p>")]
        [InlineData(
            "//[[Important page|this link is italicized]]//\n" +
            "**[[Important page]]**\n" +
            "//**[[Important page]]**//",
            "<p><em><a href=\"Important page\">this link is italicized</a></em>\n" +
            "<strong><a href=\"Important page\">Important page</a></strong>\n" +
            "<em><strong><a href=\"Important page\">Important page</a></strong></em></p>")]
        [InlineData(
            "* **bold** item\n" +
            "* //italic// item\n" +
            "# item about a [[certain page]]\n" +
            "# {{{ //this// is **not** [[processed]] }}}",
            "<ul>" +
                "<li><strong>bold</strong> item</li>" +
                "<li><em>italic</em> item</li>" +
            "</ul>" +
            "<ol>" +
                "<li>item about a <a href=\"certain page\">certain page</a></li>" +
                "<li><code> //this// is **not** [[processed]] </code></li>" +
            "</ol>")]
        [InlineData(
            "|=Heading Col 1 |=Heading Col 2         |\n" +
            @"|Cell 1.1       |Two lines\\in Cell 1.2 |" + "\n" +
            "|Cell 2.1       |Cell 2.2               |",
            "<table><tbody>" +
            "<tr><th>Heading Col 1</th><th>Heading Col 2</th></tr>" +
            "<tr><td>Cell 1.1</td><td>Two lines<br>in Cell 1.2</td></tr>" +
            "<tr><td>Cell 2.1</td><td>Cell 2.2</td></tr>" +
            "</tbody></table>")]
        [InlineData(
            "<<CurrentTimePlugin format='yyyy.MM.dd'>>",
            "<!-- CurrentTimePlugin format=&#39;yyyy.MM.dd&#39; -->")]
        [InlineData(
            "= Top-level heading (1)\n" +
            "== This a test for creole 0.1 (2)\n" +
            "=== This is a Subheading (3)\n" +
            "==== Subsub (4)\n" +
            "===== Subsubsub (5)\n" +
            "\n" +
            "The ending equal signs should not be displayed:\n" +
            "\n" +
            "= Top-level heading (1) =\n" +
            "== This a test for creole 0.1 (2) ==\n" +
            "=== This is a Subheading (3) ===\n" +
            "==== Subsub (4) ====\n" +
            "===== Subsubsub (5) =====\n" +
            "\n" +
            "\n" +
            "You can make things **bold** or //italic// or **//both//** or //**both**//.\n" +
            "\n" +
            "Character formatting extends across line breaks: **bold,\n" +
            "this is still bold. This line deliberately does not end in star-star.\n" +
            "\n" +
            "Not bold. Character formatting does not cross paragraph boundaries.\n" +
            "\n" +
            "You can use [[internal links]] or [[http://www.wikicreole.org|external links]],\n" +
            "give the link a [[internal links|different]] name.\n" +
            "\n" +
            "Here's another sentence: This wisdom is taken from [[Ward Cunningham's]]\n" +
            "[[http://www.c2.com/doc/wikisym/WikiSym2006.pdf|Presentation at the Wikisym 06]].\n" +
            "\n" +
            "Here's a external link without a description: [[http://www.wikicreole.org]]\n" +
            "\n" +
            "Be careful that italic links are rendered properly:  //[[http://my.book.example/|My Book Title]]// \n" +
            "\n" +
            "Free links without braces should be rendered as well, like http://www.wikicreole.org/ and http://www.wikicreole.org/users/~example. \n" +
            "\n" +
            "Creole1.0 specifies that http://bar and ftp://bar should not render italic,\n" +
            "something like foo://bar should render as italic.\n" +
            "\n" +
            "You can use this to draw a line to separate the page:\n" +
            "----\n" +
            "\n" +
            "You can use lists, start it at the first column for now, please...\n" +
            "\n" +
            "unnumbered lists are like\n" +
            "* item a\n" +
            "* item b\n" +
            "* **bold item c**\n" +
            "\n" +
            "blank space is also permitted before lists like:\n" +
            "  *   item a\n" +
            " * item b\n" +
            "* item c\n" +
            " ** item c.a\n" +
            "\n" +
            "or you can number them\n" +
            "# [[item 1]]\n" +
            "# item 2\n" +
            "# // italic item 3 //\n" +
            "    ## item 3.1\n" +
            "  ## item 3.2\n" +
            "\n" +
            "up to five levels\n" +
            "* 1\n" +
            "** 2\n" +
            "*** 3\n" +
            "**** 4\n" +
            "***** 5\n" +
            "\n" +
            "* You can have\n" +
            "multiline list items\n" +
            "* this is a second multiline\n" +
            "list item\n" +
            "\n" +
            "You can use nowiki syntax if you would like do stuff like this:\n" +
            "\n" +
            "{{{\n" +
            "Guitar Chord C:\n" +
            "\n" +
            "||---|---|---|\n" +
            "||-0-|---|---|\n" +
            "||---|---|---|\n" +
            "||---|-0-|---|\n" +
            "||---|---|-0-|\n" +
            "||---|---|---|\n" +
            "}}}\n" +
            "\n" +
            "You can also use it inline nowiki {{{ in a sentence }}} like this.\n" +
            "\n" +
            "= Escapes =\n" +
            "Normal Link: http://wikicreole.org/ - now same link, but escaped: ~http://wikicreole.org/ \n" +
            "\n" +
            "Normal asterisks: ~**not bold~**\n" +
            "\n" +
            "a tilde alone: ~\n" +
            "\n" +
            "a tilde escapes itself: ~~xxx\n" +
            "\n" +
            "=== Creole 0.2 ===\n" +
            "\n" +
            "This should be a flower with the ALT text \"this is a flower\" if your wiki supports ALT text on images:\n" +
            "\n" +
            "{{Red-Flower.jpg|here is a red flower}}\n" +
            "\n" +
            "=== Creole 0.4 ===\n" +
            "\n" +
            "Tables are done like this:\n" +
            "\n" +
            "|=header col1|=header col2| \n" +
            "|col1|col2| \n" +
            "|you         |can         | \n" +
            "|also        |align\\\\ it. | \n" +
            "\n" +
            "You can format an address by simply forcing linebreaks:\n" +
            "\n" +
            "My contact dates:\\\\\n" +
            "Pone: xyz\\\\\n" +
            "Fax: +45\\\\\n" +
            "Mobile: abc\n" +
            "\n" +
            "=== Creole 0.5 ===\n" +
            "\n" +
            "|= Header title               |= Another header title     |\n" +
            "| {{{ //not italic text// }}} | {{{ **not bold text** }}} |\n" +
            "| //italic text//             | **  bold text **          |\n" +
            "\n" +
            "=== Creole 1.0 ===\n" +
            "\n" +
            "If interwiki links are setup in your wiki, this links to the WikiCreole page about Creole 1.0 test cases: [[WikiCreole:Creole1.0TestCases]].\n",
            "<h1>Top-level heading (1)</h1>" +
            "<h2>This a test for creole 0.1 (2)</h2>" +
            "<h3>This is a Subheading (3)</h3>" +
            "<h4>Subsub (4)</h4>" +
            "<h5>Subsubsub (5)</h5>" +
            "<p>The ending equal signs should not be displayed:</p>" +
            "<h1>Top-level heading (1)</h1>" +
            "<h2>This a test for creole 0.1 (2)</h2>" +
            "<h3>This is a Subheading (3)</h3>" +
            "<h4>Subsub (4)</h4>" +
            "<h5>Subsubsub (5)</h5>" +
            "<p>You can make things <strong>bold</strong> or <em>italic</em> or <strong><em>both</em></strong> or <em><strong>both</strong></em>.</p>" +
            "<p>Character formatting extends across line breaks: **bold,\n" +
            "this is still bold. This line deliberately does not end in star-star.</p>" +
            "<p>Not bold. Character formatting does not cross paragraph boundaries.</p>" +
            "<p>You can use <a href=\"internal links\">internal links</a> or <a href=\"http://www.wikicreole.org\">external links</a>,\n" +
            "give the link a <a href=\"internal links\">different</a> name.</p>" +
            "<p>Here&#39;s another sentence: This wisdom is taken from <a href=\"Ward Cunningham&#39;s\">Ward Cunningham&#39;s</a>\n" +
            "<a href=\"http://www.c2.com/doc/wikisym/WikiSym2006.pdf\">Presentation at the Wikisym 06</a>.</p>" +
            "<p>Here&#39;s a external link without a description: <a href=\"http://www.wikicreole.org\">http://www.wikicreole.org</a></p>" +
            "<p>Be careful that italic links are rendered properly:  <em><a href=\"http://my.book.example/\">My Book Title</a></em></p>" +
            "<p>Free links without braces should be rendered as well, like <a href=\"http://www.wikicreole.org/\">http://www.wikicreole.org/</a> and <a href=\"http://www.wikicreole.org/users/~example\">http://www.wikicreole.org/users/~example</a>.</p>" +
            "<p>Creole1.0 specifies that <a href=\"http://bar\">http://bar</a> and <a href=\"ftp://bar\">ftp://bar</a> should not render italic,\n" +
            "something like foo://bar should render as italic.</p>" +
            "<p>You can use this to draw a line to separate the page:</p>" +
            "<hr>" +
            "<p>You can use lists, start it at the first column for now, please...</p>" +
            "<p>unnumbered lists are like</p>" +
            "<ul>" +
                "<li>item a</li>" +
                "<li>item b</li>" +
                "<li><strong>bold item c</strong></li>" +
            "</ul>" +
            "<p>blank space is also permitted before lists like:</p>" +
            "<ul>" +
                "<li>item a</li>" +
                "<li>item b</li>" +
                "<li>" +
                    "item c" +
                    "<ul>" +
                        "<li>item c.a</li>" +
                    "</ul>" +
                "</li>" +
            "</ul>" +
            "<p>or you can number them</p>" +
            "<ol>" +
                "<li><a href=\"item 1\">item 1</a></li>" +
                "<li>item 2</li>" +
                "<li>" +
                    "<em> italic item 3 </em>" +
                    "<ol>" +
                        "<li>item 3.1</li>" +
                        "<li>item 3.2</li>" +
                    "</ol>" +
                "</li>" +
            "</ol>" +
            "<p>up to five levels</p>" +
            "<ul>" +
                "<li>" +
                    "1" +
                    "<ul>" +
                        "<li>" +
                            "2" +
                            "<ul>" +
                                "<li>" +
                                    "3" +
                                    "<ul>" +
                                        "<li>" +
                                            "4" +
                                            "<ul>" +
                                                "<li>5</li>" +
                                            "</ul>" +
                                        "</li>" +
                                    "</ul>" +
                                "</li>" +
                            "</ul>" +
                        "</li>" +
                    "</ul>" +
                "</li>" +
            "</ul>" +
            "<ul>" +
                "<li>" +
                    "You can have\n" +
                    "multiline list items" +
                "</li>" +
                "<li>" +
                    "this is a second multiline\n" +
                    "list item" +
                "</li>" +
            "</ul>" +
            "<p>You can use nowiki syntax if you would like do stuff like this:</p>" +
            "<pre><code>Guitar Chord C:\n" +
            "\n" +
            "||---|---|---|\n" +
            "||-0-|---|---|\n" +
            "||---|---|---|\n" +
            "||---|-0-|---|\n" +
            "||---|---|-0-|\n" +
            "||---|---|---|</code></pre>" +
            "<p>You can also use it inline nowiki <code> in a sentence </code> like this.</p>" +
            "<h1>Escapes</h1>" +
            "<p>Normal Link: <a href=\"http://wikicreole.org/\">http://wikicreole.org/</a> - now same link, but escaped: http://wikicreole.org/</p>" +
            "<p>Normal asterisks: **not bold**</p>" +
            "<p>a tilde alone: ~</p>" +
            "<p>a tilde escapes itself: ~xxx</p>" +
            "<h3>Creole 0.2</h3>" +
            "<p>This should be a flower with the ALT text &quot;this is a flower&quot; if your wiki supports ALT text on images:</p>" +
            "<p><img src=\"Red-Flower.jpg\" alt=\"here is a red flower\"></p>" +
            "<h3>Creole 0.4</h3>" +
            "<p>Tables are done like this:</p>" +
            "<table><tbody>" +
                "<tr><th>header col1</th><th>header col2</th></tr>" +
                "<tr><td>col1</td><td>col2</td></tr>" +
                "<tr><td>you</td><td>can</td></tr>" +
                "<tr><td>also</td><td>align<br> it.</td></tr>" +
            "</tbody></table>" +
            "<p>You can format an address by simply forcing linebreaks:</p>" +
            "<p>My contact dates:<br>\n" +
            "Pone: xyz<br>\n" +
            "Fax: +45<br>\n" +
            "Mobile: abc</p>" +
            "<h3>Creole 0.5</h3>" +
            "<table><tbody>" +
                "<tr><th>Header title</th><th>Another header title</th></tr>" +
                "<tr><td><code> //not italic text// </code></td><td><code> **not bold text** </code></td></tr>" +
                "<tr><td><em>italic text</em></td><td><strong>  bold text </strong></td></tr>" +
            "</tbody></table>" +
            "<h3>Creole 1.0</h3>" +
            "<p>If interwiki links are setup in your wiki, this links to the WikiCreole page about Creole 1.0 test cases: <a href=\"WikiCreole:Creole1.0TestCases\">WikiCreole:Creole1.0TestCases</a>.</p>")]
        [InlineData(
            "* Item 1\n" +
            "with **multiple** lines.\n" +
            "* Item 2\n" +
            "** Item 2.1\n" +
            "has also\n" +
            "multiple\n" +
            "lines.\n" +
            "* Item 3",
            "<ul>" +
                "<li>" +
                    "Item 1\n" +
                    "with <strong>multiple</strong> lines." +
                "</li>" +
                "<li>" +
                    "Item 2" +
                    "<ul>" +
                        "<li>" +
                            "Item 2.1\n" +
                            "has also\n" +
                            "multiple\n" +
                            "lines." +
                        "</li>" +
                    "</ul>" +
                "</li>" +
                "<li>Item 3</li>" +
            "</ul>")]
        [InlineData(
            "= Broken markup\n" +
            "\n" +
            "[[ {{ -- ** __ // foo | * - # \"Â§$%&/( (( ",
            "<h1>Broken markup</h1>" +
            "<p>[[ {{ -- ** __ // foo | * - # &quot;Â§$%&amp;/( ((</p>")]
        public async Task CreoleSiteTestsCase(string text, string expectedHtml)
        {
            var actualHtml = await _parser.ParseAsync(text).With(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }
    }
}