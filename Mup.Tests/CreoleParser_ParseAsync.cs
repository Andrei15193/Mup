using System.Linq;
using System.Text;
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
            var result = await _parser.ParseAsync(text);

            var elementMarkVisitor = new ElementMarkVisitor();
            await result.AcceptAsync(elementMarkVisitor);

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), elementMarkVisitor.Marks);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesParagraphs)))]
        [InlineData("plain text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("paragraph 1\n\nparagraph 2", new object[] { ParagraphStart, PlainText, ParagraphEnd, ParagraphStart, PlainText, ParagraphEnd })]
        public async Task ParsesParagraphs(string text, object[] marks)
        {
            var result = await _parser.ParseAsync(text);

            var elementMarkVisitor = new ElementMarkVisitor();
            await result.AcceptAsync(elementMarkVisitor);

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), elementMarkVisitor.Marks);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesEscapeCharacters)))]
        [InlineData("~plain text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("~~plain text", new object[] { ParagraphStart, PlainText, PlainText, ParagraphEnd })]
        [InlineData("~~~plain text", new object[] { ParagraphStart, PlainText, PlainText, ParagraphEnd })]
        [InlineData("~~~~plain text", new object[] { ParagraphStart, PlainText, PlainText, PlainText, ParagraphEnd })]
        [InlineData("~~~~~plain text", new object[] { ParagraphStart, PlainText, PlainText, PlainText, ParagraphEnd })]
        public async Task ParsesEscapeCharacters(string text, object[] marks)
        {
            var result = await _parser.ParseAsync(text);

            var elementMarkVisitor = new ElementMarkVisitor();
            await result.AcceptAsync(elementMarkVisitor);

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), elementMarkVisitor.Marks);
        }

        [Trait("Class", nameof(CreoleParser))]

        [Theory(DisplayName = (_method + nameof(ParsesRichText)))]
        [InlineData("**plain text**", new object[] { ParagraphStart, StrongStart, PlainText, StrongEnd, ParagraphEnd })]
        [InlineData("**plain text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("plain **now strong** text", new object[] { ParagraphStart, PlainText, StrongStart, PlainText, StrongEnd, PlainText, ParagraphEnd })]
        [InlineData("**paragraph 1\n\nparagrapg 2**", new object[] { ParagraphStart, PlainText, ParagraphEnd, ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("strong ** cannot\n\ncross ** paragraphs", new object[] { ParagraphStart, PlainText, ParagraphEnd, ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("escaped ~**strong", new object[] { ParagraphStart, PlainText, PlainText, ParagraphEnd })]
        [InlineData("asterix with ~***strong** text", new object[] { ParagraphStart, PlainText, PlainText, StrongStart, PlainText, StrongEnd, PlainText, ParagraphEnd })]

        [InlineData("//plain text//", new object[] { ParagraphStart, EmphasisStart, PlainText, EmphasisEnd, ParagraphEnd })]
        [InlineData("//plain text", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("plain //now emphasised// text", new object[] { ParagraphStart, PlainText, EmphasisStart, PlainText, EmphasisEnd, PlainText, ParagraphEnd })]
        [InlineData("//paragraph 1\n\nparagrapg 2//", new object[] { ParagraphStart, PlainText, ParagraphEnd, ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("emphasis // cannot\n\ncross // paragraphs", new object[] { ParagraphStart, PlainText, ParagraphEnd, ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("escaped ~//emphasis", new object[] { ParagraphStart, PlainText, PlainText, ParagraphEnd })]
        [InlineData("slash with ~///emphasised// text", new object[] { ParagraphStart, PlainText, PlainText, EmphasisStart, PlainText, EmphasisEnd, PlainText, ParagraphEnd })]

        [InlineData("http://example.com", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("https://example.com", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("http://example.com/~", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("http://example.com/~~", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("HTTP://EXAMPLE.COM", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("~http://example.com", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("~http://example.com/ with no // emphasis", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("~~http://example.com", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("~~~http://example.com/ with no // emphasis", new object[] { ParagraphStart, PlainText, PlainText, ParagraphEnd })]
        [InlineData("tcp://example.com", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("//http://example.com//", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]

        [InlineData("[[http://example.com]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("~[[http://example.com]]", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("~[[~http://example.com]]", new object[] { ParagraphStart, PlainText, PlainText, ParagraphEnd })]
        [InlineData("[[http://example.com/~]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com|text]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com/~|text]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[http://example.com]]", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com]", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("[[http://example.com/~|text]", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("[[http://example.com/~|http://example.com]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com/~|//emphasis//]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, EmphasisStart, PlainText, EmphasisEnd, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com/~|**strong**]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, StrongStart, PlainText, StrongEnd, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com/~|**strong** and //emphasis//]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, StrongStart, PlainText, StrongEnd, PlainText, EmphasisStart, PlainText, EmphasisEnd, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com/~|//emphasis// and **strong**]]", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, EmphasisStart, PlainText, EmphasisEnd, PlainText, StrongStart, PlainText, StrongEnd, HyperlinkEnd, ParagraphEnd })]
        [InlineData("[[http://example.com/~|//no emphasis]]//", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]
        [InlineData("[[http://example.com/~|**no strong]]**", new object[] { ParagraphStart, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, PlainText, ParagraphEnd })]

        [InlineData("{{http://example.com/image.jpg}}", new object[] { ParagraphStart, ImageStart, ImageSource, PlainText, ImageEnd, ParagraphEnd })]
        [InlineData("~{{http://example.com/image.jpg}}", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("{{http://example.com/image.jpg|alternative text}}", new object[] { ParagraphStart, ImageStart, ImageSource, PlainText, ImageEnd, ParagraphEnd })]
        [InlineData("{{http://example.com/image.jpg|**no strong alternative text**}}", new object[] { ParagraphStart, ImageStart, ImageSource, PlainText, ImageEnd, ParagraphEnd })]
        [InlineData("{{http://example.com/image.jpg|//no emphasised alternative text//}}", new object[] { ParagraphStart, ImageStart, ImageSource, PlainText, ImageEnd, ParagraphEnd })]
        [InlineData("{http://example.com/image.jpg}}", new object[] { ParagraphStart, PlainText, HyperlinkStart, HyperlinkDestination, PlainText, HyperlinkEnd, ParagraphEnd })]
        [InlineData("{{http://example.com/image.jpg}", new object[] { ParagraphStart, PlainText, ParagraphEnd })]

        [InlineData(@"line\\break", new object[] { ParagraphStart, PlainText, LineBreak, PlainText, ParagraphEnd })]
        [InlineData(@"line~\\break", new object[] { ParagraphStart, PlainText, PlainText, ParagraphEnd })]
        [InlineData(@"line~~\\break", new object[] { ParagraphStart, PlainText, LineBreak, PlainText, ParagraphEnd })]
        [InlineData(@"line~\\\break", new object[] { ParagraphStart, PlainText, PlainText, LineBreak, PlainText, ParagraphEnd })]

        [InlineData("**//mixed strong emphasis**//", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        [InlineData("//**mixed emphasis strong//**", new object[] { ParagraphStart, PlainText, ParagraphEnd })]
        public async Task ParsesRichText(string text, object[] marks)
        {
            var result = await _parser.ParseAsync(text);

            var elementMarkVisitor = new ElementMarkVisitor();
            await result.AcceptAsync(elementMarkVisitor);

            Assert.Equal(marks.Cast<ElementMarkCode>().ToArray(), elementMarkVisitor.Marks);
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
        public async Task ParseHeadingsToHtml(string text, string expectedHtml)
        {
            var result = await _parser.ParseAsync(text);

            var htmlStringBuilder = new StringBuilder();
            var creoleToHtmlVisitor = new HtmlWriterVisitor(htmlStringBuilder);
            await result.AcceptAsync(creoleToHtmlVisitor);

            Assert.Equal(expectedHtml, htmlStringBuilder.ToString());
        }

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParseParagraphsToHtml)))]
        [InlineData("plain text", "<p>plain text</p>")]
        [InlineData("paragraph 1\n\nparagraph 2", "<p>paragraph 1</p><p>paragraph 2</p>")]
        public async Task ParseParagraphsToHtml(string text, string expectedHtml)
        {
            var result = await _parser.ParseAsync(text);

            var htmlStringBuilder = new StringBuilder();
            var creoleToHtmlVisitor = new HtmlWriterVisitor(htmlStringBuilder);
            await result.AcceptAsync(creoleToHtmlVisitor);

            Assert.Equal(expectedHtml, htmlStringBuilder.ToString());
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
            var result = await _parser.ParseAsync(text);

            var htmlStringBuilder = new StringBuilder();
            var creoleToHtmlVisitor = new HtmlWriterVisitor(htmlStringBuilder);
            await result.AcceptAsync(creoleToHtmlVisitor);

            Assert.Equal(expectedHtml, htmlStringBuilder.ToString());
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
        [InlineData("asterix with ***strong** text", "<p>asterix with <strong>*strong</strong> text</p>")]
        [InlineData("asterix with **strong*** text", "<p>asterix with <strong>strong</strong>* text</p>")]
        [InlineData("asterix with ~***strong** text", "<p>asterix with *<strong>strong</strong> text</p>")]
        [InlineData("asterix with **strong~*** text", "<p>asterix with <strong>strong*</strong> text</p>")]

        [InlineData("//emphasised text//", "<p><em>emphasised text</em></p>")]
        [InlineData("plain //now emphasised// text", "<p>plain <em>now emphasised</em> text</p>")]
        [InlineData("//paragraph 1\n\nparagraph 2//", "<p>//paragraph 1</p><p>paragraph 2//</p>")]
        [InlineData("emphasis // cannot\n\ncross // paragraphs", "<p>emphasis // cannot</p><p>cross // paragraphs</p>")]
        [InlineData("escaped ~//emphasis", "<p>escaped //emphasis</p>")]
        [InlineData("slash with ///emphasised// text", "<p>slash with <em>/emphasised</em> text</p>")]
        [InlineData("slash with //emphasised/// text", "<p>slash with <em>emphasised</em>/ text</p>")]
        [InlineData("slash with ~///emphasised// text", "<p>slash with /<em>emphasised</em> text</p>")]
        [InlineData("slash with //emphasised~/// text", "<p>slash with <em>emphasised/</em> text</p>")]

        [InlineData("http://example.com", "<p><a href=\"http://example.com\">http://example.com</a></p>")]
        [InlineData("https://example.com", "<p><a href=\"https://example.com\">https://example.com</a></p>")]
        [InlineData("http://example.com/~", "<p><a href=\"http://example.com/~\">http://example.com/~</a></p>")]
        [InlineData("http://example.com/~~", "<p><a href=\"http://example.com/~~\">http://example.com/~~</a></p>")]
        [InlineData("HTTP://EXAMPLE.COM", "<p><a href=\"HTTP://EXAMPLE.COM\">HTTP://EXAMPLE.COM</a></p>")]
        [InlineData("~http://example.com", "<p>http://example.com</p>")]
        [InlineData("~http://example.com/ with no // emphasis", "<p>http://example.com/ with no // emphasis</p>")]
        [InlineData("~~http://example.com", "<p>~<a href=\"http://example.com\">http://example.com</a></p>")]
        [InlineData("~~~http://example.com", "<p>~http://example.com</p>")]
        [InlineData("tcp://example.com", "<p>tcp://example.com</p>")]
        [InlineData("//http://example.com//", "<p>//<a href=\"http://example.com//\">http://example.com//</a></p>")]

        [InlineData("[[http://example.com]]", "<p><a href=\"http://example.com\">http://example.com</a></p>")]
        [InlineData("[[http://example.com/~]]", "<p><a href=\"http://example.com/~\">http://example.com/~</a></p>")]
        [InlineData("[[http://example.com|text]]", "<p><a href=\"http://example.com\">text</a></p>")]
        [InlineData("[[http://example.com/~|text]]", "<p><a href=\"http://example.com/~\">text</a></p>")]
        [InlineData("[http://example.com]]", "<p>[<a href=\"http://example.com]]\">http://example.com]]</a></p>")]
        [InlineData("[[http://example.com]", "<p>[[http://example.com]</p>")]
        [InlineData("[[http://example.com/~|text]", "<p>[[http://example.com/~|text]</p>")]
        [InlineData("[[http://example.com/~|http://example.com]]", "<p><a href=\"http://example.com/~\">http://example.com</a></p>")]
        [InlineData("[[http://example.com/~|//emphasis//]]", "<p><a href=\"http://example.com/~\"><em>emphasis</em></a></p>")]
        [InlineData("[[http://example.com/~|**strong**]]", "<p><a href=\"http://example.com/~\"><strong>strong</strong></a></p>")]
        [InlineData("[[http://example.com/~|**strong** and //emphasis//]]", "<p><a href=\"http://example.com/~\"><strong>strong</strong> and <em>emphasis</em></a></p>")]
        [InlineData("[[http://example.com/~|//emphasis// and **strong**]]", "<p><a href=\"http://example.com/~\"><em>emphasis</em> and <strong>strong</strong></a></p>")]
        [InlineData("[[http://example.com/~|//no emphasis]]//", "<p><a href=\"http://example.com/~\">//no emphasis</a>//</p>")]
        [InlineData("[[http://example.com/~|**no strong]]**", "<p><a href=\"http://example.com/~\">**no strong</a>**</p>")]

        [InlineData("{{http://example.com/image.jpg}}", "<p><img src=\"http://example.com/image.jpg\"></p>")]
        [InlineData("~{{http://example.com/image.jpg}}", "<p>~{{<a href=\"http://example.com/image.jpg}}\">http://example.com/image.jpg}}</a></p>")]
        [InlineData("{{http://example.com/image.jpg|alternative text}}", "<p><img src=\"http://example.com/image.jpg\" alt=\"alternative text\"></p>")]
        [InlineData("{{http://example.com/image.jpg|**no strong alternative text**}}", "<p><img src=\"http://example.com/image.jpg\" alt=\"**no strong alternative text**\"></p>")]
        [InlineData("{{http://example.com/image.jpg|//no emphasised alternative text//}}", "<p><img src=\"http://example.com/image.jpg\" alt=\"//no emphasised alternative text//\"></p>")]
        [InlineData("{http://example.com/image.jpg}}", "<p>{<a href=\"http://example.com/image.jpg}}\">http://example.com/image.jpg}}</a></p>")]
        [InlineData("{{http://example.com/image.jpg}", "<p>{{http://example.com/image.jpg}</p>")]

        [InlineData(@"line\\break", @"<p>line<br>break</p>")]
        [InlineData(@"line~\\break", @"<p>line\\break</p>")]
        [InlineData(@"line~~\\break", @"<p>line~<br>break</p>")]
        [InlineData(@"line~\\\break", @"<p>line\<br>break</p>")]

        [InlineData("**//mixed strong emphasis**//", "<p>**//mixed strong emphasis**//</p>")]
        [InlineData("**//mixed strong emphasis**// still no emphasis//", "<p>**//mixed strong emphasis**// still no emphasis//</p>")]
        [InlineData("**//mixed strong emphasis**// still no strong**", "<p>**//mixed strong emphasis**// still no strong**</p>")]
        [InlineData("//**mixed emphasis strong//**", "<p>//**mixed emphasis strong//**</p>")]
        [InlineData("//**mixed emphasis strong//** still no strong**", "<p>//**mixed emphasis strong//** still no strong**</p>")]
        [InlineData("//**mixed emphasis strong//** still no emphasis//", "<p>//**mixed emphasis strong//** still no emphasis//</p>")]
        public async Task ParsesRichTextToHtml(string text, string expectedHtml)
        {
            var result = await _parser.ParseAsync(text);

            var htmlStringBuilder = new StringBuilder();
            var creoleToHtmlVisitor = new HtmlWriterVisitor(htmlStringBuilder);
            await result.AcceptAsync(creoleToHtmlVisitor);

            Assert.Equal(expectedHtml, htmlStringBuilder.ToString());
        }
    }
}