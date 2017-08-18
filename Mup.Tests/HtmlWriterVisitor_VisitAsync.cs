using System.Threading.Tasks;
using Xunit;

namespace Mup.Tests
{
    public class HtmlWriterVisitor_VisitAsync
    {
        private const string _method = (nameof(HtmlWriterVisitor) + ".VisitAsync(string, IEnumerable<ElementMark>): ");

        [Trait("Class", nameof(HtmlWriterVisitor))]
        [Theory(DisplayName = (_method + nameof(EscapesHtmlSpecialCharactersForPlainText)))]
        [InlineData("<", "&lt;")]
        [InlineData(">", "&gt;")]
        [InlineData("&", "&amp;")]
        [InlineData("\"", "&quot;")]
        [InlineData("'", "&#39;")]
        [InlineData("<>&\"'", "&lt;&gt;&amp;&quot;&#39;")]
        public async Task EscapesHtmlSpecialCharactersForPlainText(string text, string expectedHtml)
        {
            var parseTree = new FlatParseTree(text, new[] { new ElementMark { Code = ElementMarkCode.PlainText, Start = 0, Length = text.Length } });
            var actualHtml = await parseTree.AcceptAsync(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(HtmlWriterVisitor))]
        [Theory(DisplayName = (_method + nameof(EscapesHtmlSpecialCharactersForPreformattedBlock)))]
        [InlineData("<", "<pre><code>&lt;</code></pre>")]
        [InlineData(">", "<pre><code>&gt;</code></pre>")]
        [InlineData("&", "<pre><code>&amp;</code></pre>")]
        [InlineData("\"", "<pre><code>&quot;</code></pre>")]
        [InlineData("'", "<pre><code>&#39;</code></pre>")]
        [InlineData("<>&\"'", "<pre><code>&lt;&gt;&amp;&quot;&#39;</code></pre>")]
        public async Task EscapesHtmlSpecialCharactersForPreformattedBlock(string text, string expectedHtml)
        {
            var parseTree = new FlatParseTree(
                text,
                new[]
                {
                    new ElementMark
                    {
                        Code = ElementMarkCode.PreformattedBlockStart,
                        Start = 0,
                        Length = 0
                    },
                    new ElementMark
                    {
                        Code = ElementMarkCode.PlainText,
                        Start = 0,
                        Length = text.Length
                    },
                    new ElementMark
                    {
                        Code = ElementMarkCode.PreformattedBlockEnd,
                        Start = text.Length,
                        Length = 0
                    }
                });
            var actualHtml = await parseTree.AcceptAsync(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(HtmlWriterVisitor))]
        [Theory(DisplayName = (_method + nameof(EscapesHtmlSpecialCharactersForPreformattedText)))]
        [InlineData("<", "<code>&lt;</code>")]
        [InlineData(">", "<code>&gt;</code>")]
        [InlineData("&", "<code>&amp;</code>")]
        [InlineData("\"", "<code>&quot;</code>")]
        [InlineData("'", "<code>&#39;</code>")]
        [InlineData("<>&\"'", "<code>&lt;&gt;&amp;&quot;&#39;</code>")]
        public async Task EscapesHtmlSpecialCharactersForPreformattedText(string text, string expectedHtml)
        {
            var parseTree = new FlatParseTree(
                text,
                new[]
                {
                    new ElementMark
                    {
                        Code = ElementMarkCode.PreformattedTextStart,
                        Start = 0,
                        Length = 0
                    },
                    new ElementMark
                    {
                        Code = ElementMarkCode.PlainText,
                        Start = 0,
                        Length = text.Length
                    },
                    new ElementMark
                    {
                        Code = ElementMarkCode.PreformattedTextEnd,
                        Start = text.Length,
                        Length = 0
                    }
                });
            var actualHtml = await parseTree.AcceptAsync(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(HtmlWriterVisitor))]
        [Theory(DisplayName = (_method + nameof(EscapesHtmlSpecialCharactersForImages)))]
        [InlineData("<", "<img src=\"&lt;\" alt=\"&lt;\">")]
        [InlineData(">", "<img src=\"&gt;\" alt=\"&gt;\">")]
        [InlineData("&", "<img src=\"&amp;\" alt=\"&amp;\">")]
        [InlineData("\"", "<img src=\"&quot;\" alt=\"&quot;\">")]
        [InlineData("'", "<img src=\"&#39;\" alt=\"&#39;\">")]
        [InlineData("<>&\"'", "<img src=\"&lt;&gt;&amp;&quot;&#39;\" alt=\"&lt;&gt;&amp;&quot;&#39;\">")]
        public async Task EscapesHtmlSpecialCharactersForImages(string text, string expectedHtml)
        {
            var parseTree = new FlatParseTree(
                text,
                new[]
                {
                    new ElementMark
                    {
                        Code = ElementMarkCode.ImageStart,
                        Start = 0,
                        Length = 0
                    },
                    new ElementMark
                    {
                        Code = ElementMarkCode.ImageSource,
                        Start = 0,
                        Length = text.Length
                    },
                    new ElementMark
                    {
                        Code = ElementMarkCode.PlainText,
                        Start = 0,
                        Length = text.Length
                    },
                    new ElementMark
                    {
                        Code = ElementMarkCode.ImageEnd,
                        Start = text.Length,
                        Length = 0
                    }
                });
            var actualHtml = await parseTree.AcceptAsync(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Trait("Class", nameof(HtmlWriterVisitor))]
        [Theory(DisplayName = (_method + nameof(EscapesHtmlSpecialCharactersForHyperlinks)))]
        [InlineData("<", "<a href=\"&lt;\">&lt;</a>")]
        [InlineData(">", "<a href=\"&gt;\">&gt;</a>")]
        [InlineData("&", "<a href=\"&amp;\">&amp;</a>")]
        [InlineData("\"", "<a href=\"&quot;\">&quot;</a>")]
        [InlineData("'", "<a href=\"&#39;\">&#39;</a>")]
        [InlineData("<>&\"'", "<a href=\"&lt;&gt;&amp;&quot;&#39;\">&lt;&gt;&amp;&quot;&#39;</a>")]
        public async Task EscapesHtmlSpecialCharactersForHyperlinks(string text, string expectedHtml)
        {
            var parseTree = new FlatParseTree(
                text,
                new[]
                {
                    new ElementMark
                    {
                        Code = ElementMarkCode.HyperlinkStart,
                        Start = 0,
                        Length = 0
                    },
                    new ElementMark
                    {
                        Code = ElementMarkCode.HyperlinkDestination,
                        Start = 0,
                        Length = text.Length
                    },
                    new ElementMark
                    {
                        Code = ElementMarkCode.PlainText,
                        Start = 0,
                        Length = text.Length
                    },
                    new ElementMark
                    {
                        Code = ElementMarkCode.HyperlinkEnd,
                        Start = text.Length,
                        Length = 0
                    }
                });
            var actualHtml = await parseTree.AcceptAsync(new HtmlWriterVisitor());

            Assert.Equal(expectedHtml, actualHtml);
        }
    }
}