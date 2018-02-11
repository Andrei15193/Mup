using System.Collections.Generic;

namespace Mup.Tests
{
    public static class HtmlWriterVisitorTestData
    {
        public static IEnumerable<object[]> EscapeSpecialCharactersForPlainTextTestData { get; } = new List<object[]>
        {
            new object[]
            {
                "<", "&lt;"
            },
            new object[]
            {
                ">", "&gt;"
            },
            new object[]
            {
                "&", "&amp;"
            },
            new object[]
            {
                "\"", "&quot;"
            },
            new object[]
            {
                "'", "&#39;"
            },
            new object[]
            {
                "<>&\"'", "&lt;&gt;&amp;&quot;&#39;"
            }
        };

        public static IEnumerable<object[]> EscapeSpecialCharactersForPreformattedBlocksTestData { get; } = new List<object[]>
        {
            new object[]
            {
                "<", "<pre><code>&lt;</code></pre>"
            },
            new object[]
            {
                ">", "<pre><code>&gt;</code></pre>"
            },
            new object[]
            {
                "&", "<pre><code>&amp;</code></pre>"
            },
            new object[]
            {
                "\"", "<pre><code>&quot;</code></pre>"
            },
            new object[]
            {
                "'", "<pre><code>&#39;</code></pre>"
            },
            new object[]
            {
                "<>&\"'", "<pre><code>&lt;&gt;&amp;&quot;&#39;</code></pre>"
            }
        };

        public static IEnumerable<object[]> EscapeSpecialCharactersForCodeFragmentsTestData { get; } = new List<object[]>
        {
            new object[]
            {
                "<", "<code>&lt;</code>"
            },
            new object[]
            {
                ">", "<code>&gt;</code>"
            },
            new object[]
            {
                "&", "<code>&amp;</code>"
            },
            new object[]
            {
                "\"", "<code>&quot;</code>"
            },
            new object[]
            {
                "'", "<code>&#39;</code>"
            },
            new object[]
            {
                "<>&\"'", "<code>&lt;&gt;&amp;&quot;&#39;</code>"
            }
        };

        public static IEnumerable<object[]> EscapeSpecialCharactersForImagesTestData { get; } = new List<object[]>
        {
            new object[]
            {
                "<", "<img src=\"&lt;\" alt=\"&lt;\">"
            },
            new object[]
            {
                ">", "<img src=\"&gt;\" alt=\"&gt;\">"
            },
            new object[]
            {
                "&", "<img src=\"&amp;\" alt=\"&amp;\">"
            },
            new object[]
            {
                "\"", "<img src=\"&quot;\" alt=\"&quot;\">"
            },
            new object[]
            {
                "'", "<img src=\"&#39;\" alt=\"&#39;\">"
            },
            new object[]
            {
                "<>&\"'", "<img src=\"&lt;&gt;&amp;&quot;&#39;\" alt=\"&lt;&gt;&amp;&quot;&#39;\">"
            }
        };

        public static IEnumerable<object[]> EscapeSpecialCharactersForHyperlinksTestData { get; } = new List<object[]>
        {
            new object[]
            {
                "<", "<a href=\"&lt;\">&lt;</a>"
            },
            new object[]
            {
                ">", "<a href=\"&gt;\">&gt;</a>"
            },
            new object[]
            {
                "&", "<a href=\"&amp;\">&amp;</a>"
            },
            new object[]
            {
                "\"", "<a href=\"&quot;\">&quot;</a>"
            },
            new object[]
            {
                "'", "<a href=\"&#39;\">&#39;</a>"
            },
            new object[]
            {
                "<>&\"'", "<a href=\"&lt;&gt;&amp;&quot;&#39;\">&lt;&gt;&amp;&quot;&#39;</a>"
            }
        };
    }
}