using System.Collections.Generic;

namespace Mup.Tests
{
    public static class CreoleToHtmlTestData
    {
        public static IEnumerable<object[]> HeadingsToHtmlTestCases { get; } = new List<object[]>
        {
            new object[]
            {
                "= plain text", "<h1>plain text</h1>"
            },
            new object[]
            {
                "= heading 1\n== heading 2", "<h1>heading 1</h1><h2>heading 2</h2>"
            },
            new object[]
            {
                "= plain text ", "<h1>plain text</h1>"
            },
            new object[]
            {
                "= plain text =", "<h1>plain text</h1>"
            },
            new object[]
            {
                "= plain text ==", "<h1>plain text</h1>"
            },
            new object[]
            {
                "= plain text **strong**", "<h1>plain text **strong**</h1>"
            },
            new object[]
            {
                "= plain text //emphasis//", "<h1>plain text //emphasis//</h1>"
            },
            new object[]
            {
                "= plain text [[link]]", "<h1>plain text [[link]]</h1>"
            },
            new object[]
            {
                "= plain text [[link|with text]]", "<h1>plain text [[link|with text]]</h1>"
            },
            new object[]
            {
                "= plain text {{image}}", "<h1>plain text {{image}}</h1>"
            },
            new object[]
            {
                "= plain text {{image|with title}}", "<h1>plain text {{image|with title}}</h1>"
            },
            new object[]
            {
                "= plain text <<plug in>>", "<h1>plain text &lt;&lt;plug in&gt;&gt;</h1>"
            },
            new object[]
            {
                "paragraph\n= heading", "<p>paragraph</p><h1>heading</h1>"
            },
            new object[]
            {
                "paragraph 1\n= heading\nparagraph 2", "<p>paragraph 1</p><h1>heading</h1><p>paragraph 2</p>"
            },
            new object[]
            {
                "= heading\nparagraph", "<h1>heading</h1><p>paragraph</p>"
            }
        };

        public static IEnumerable<object[]> ParagraphsToHtmlTestCases { get; } = new List<object[]>
        {
            new object[]
            {
                "plain text", "<p>plain text</p>"
            },
            new object[]
            {
                "paragraph 1\n\nparagraph 2", "<p>paragraph 1</p><p>paragraph 2</p>"
            }
        };

        public static IEnumerable<object[]> PreformattedBlocksToHtmlTestCases { get; } = new List<object[]>
        {
            new object[]
            {
                "{{{\nno wiki\n}}}", "<pre><code>no wiki</code></pre>"
            },
            new object[]
            {
                "{{{\n}}}", "<pre><code></code></pre>"
            },
            new object[]
            {
                "{{{\n\n}}}", "<pre><code>\n</code></pre>"
            },
            new object[]
            {
                "{{{\nno wiki end}}}{{{has to be at end of line\n}}}", "<pre><code>no wiki end}}}{{{has to be at end of line</code></pre>"
            },
            new object[]
            {
                "{{{\nno wiki 1\n}}}\n{{{\nno wiki 2\n}}}", "<pre><code>no wiki 1</code></pre><pre><code>no wiki 2</code></pre>"
            },
            new object[]
            {
                "{{{\nno escape~\n}}}", "<pre><code>no escape~</code></pre>"
            },
            new object[]
            {
                "{{{\nno **strong**, **emphasis**, [[hyperlinks]] or {{images}}\n}}}", "<pre><code>no **strong**, **emphasis**, [[hyperlinks]] or {{images}}</code></pre>"
            },
            new object[]
            {
                "~{{{image}}}", "<p>{<img src=\"image\">}</p>"
            },
            new object[]
            {
                "{{{\ntest", "<p>{{{\ntest</p>"
            },
            new object[]
            {
                "{{{\n\n{{{\nThis is a nowiki markup block showing nowiki markup usage in a wiki (complicated stuff ;)\n~}}}\n\n}}}", "<pre><code>\n{{{\nThis is a nowiki markup block showing nowiki markup usage in a wiki (complicated stuff ;)\n}}}\n</code></pre>"
            },
            new object[]
            {
                "paragraph\n{{{\ncode\n}}}", "<p>paragraph</p><pre><code>code</code></pre>"
            },
            new object[]
            {
                "paragraph 1\n{{{\ncode\n}}}\nparagraph 2", "<p>paragraph 1</p><pre><code>code</code></pre><p>paragraph 2</p>"
            },
            new object[]
            {
                "{{{\ncode\n}}}\nparagraph", "<pre><code>code</code></pre><p>paragraph</p>"
            }
        };

        public static IEnumerable<object[]> TablesToHtmlTestCases { get; } = new List<object[]>
        {
            new object[]
            {
                "|cell 1|cell 2|", "<table><tbody><tr><td>cell 1</td><td>cell 2</td></tr></tbody></table>"
            },
            new object[]
            {
                "|cell 1|cell 2", "<table><tbody><tr><td>cell 1</td><td>cell 2</td></tr></tbody></table>"
            },
            new object[]
            {
                "|=header cell 1|=header cell 2|", "<table><tbody><tr><th>header cell 1</th><th>header cell 2</th></tr></tbody></table>"
            },
            new object[]
            {
                "|=header cell 1|=header cell 2", "<table><tbody><tr><th>header cell 1</th><th>header cell 2</th></tr></tbody></table>"
            },
            new object[]
            {
                "|=header cell 1|=header cell 2|\n|cell 1|cell 2|", "<table><tbody><tr><th>header cell 1</th><th>header cell 2</th></tr><tr><td>cell 1</td><td>cell 2</td></tr></tbody></table>"
            },
            new object[]
            {
                "|=header cell 1|=header cell 2\n|cell 1|cell 2", "<table><tbody><tr><th>header cell 1</th><th>header cell 2</th></tr><tr><td>cell 1</td><td>cell 2</td></tr></tbody></table>"
            },
            new object[]
            {
                "|table 1\n\n|table 2", "<table><tbody><tr><td>table 1</td></tr></tbody></table><table><tbody><tr><td>table 2</td></tr></tbody></table>"
            },
            new object[]
            {
                "|cell ~| 1|", "<table><tbody><tr><td>cell | 1</td></tr></tbody></table>"
            },
            new object[]
            {
                "|cell 1~~|cell 2|", "<table><tbody><tr><td>cell 1~</td><td>cell 2</td></tr></tbody></table>"
            },
            new object[]
            {
                "|cell with //emphasis//, **strong**, [[hyperlink]], {{image}}, http://example.com , {{{no wiki}}}", "<table><tbody><tr><td>cell with <em>emphasis</em>, <strong>strong</strong>, <a href=\"hyperlink\">hyperlink</a>, <img src=\"image\">, <a href=\"http://example.com\">http://example.com</a> , <code>no wiki</code></td></tr></tbody></table>"
            },
            new object[]
            {
                "|//no emphasis", "<table><tbody><tr><td>//no emphasis</td></tr></tbody></table>"
            },
            new object[]
            {
                "|= \t white space is trimmed \t |= \t white space is trimmed \t \n| \t white space is trimmed \t | \t white space is trimmed \t ", "<table><tbody><tr><th>white space is trimmed</th><th>white space is trimmed</th></tr><tr><td>white space is trimmed</td><td>white space is trimmed</td></tr></tbody></table>"
            },
            new object[]
            {
                "|cell||", "<table><tbody><tr><td>cell</td><td></td></tr></tbody></table>"
            },
            new object[]
            {
                "|cell|", "<table><tbody><tr><td>cell</td></tr></tbody></table>"
            },
            new object[]
            {
                "|cell| |", "<table><tbody><tr><td>cell</td><td></td></tr></tbody></table>"
            },
            new object[]
            {
                "||cell|", "<table><tbody><tr><td></td><td>cell</td></tr></tbody></table>"
            },
            new object[]
            {
                "||cell", "<table><tbody><tr><td></td><td>cell</td></tr></tbody></table>"
            },
            new object[]
            {
                "paragraph\n|cell", "<p>paragraph</p><table><tbody><tr><td>cell</td></tr></tbody></table>"
            },
            new object[]
            {
                "paragraph 1\n|cell\nparagraph 2", "<p>paragraph 1</p><table><tbody><tr><td>cell</td></tr></tbody></table><p>paragraph 2</p>"
            },
            new object[]
            {
                "|cell\nparagraph", "<table><tbody><tr><td>cell</td></tr></tbody></table><p>paragraph</p>"
            }
        };

        public static IEnumerable<object[]> ListsToHtmlTestsCases { get; } = new List<object[]>
        {
            new object[]
            {
                "*item", "<ul><li>item</li></ul>"
            },
            new object[]
            {
                "* item", "<ul><li>item</li></ul>"
            },
            new object[]
            {
                "* item 1\n*item 2", "<ul><li>item 1</li><li>item 2</li></ul>"
            },
            new object[]
            {
                "* item 1\n*item 2\n** sub item 1\n** sub item 2", "<ul><li>item 1</li><li>item 2<ul><li>sub item 1</li><li>sub item 2</li></ul></li></ul>"
            },
            new object[]
            {
                "* item 1\n*item 2\n** sub item 1\n** sub item 2\n* item 3", "<ul><li>item 1</li><li>item 2<ul><li>sub item 1</li><li>sub item 2</li></ul></li><li>item 3</li></ul>"
            },
            new object[]
            {
                "# item 1\n#item 2", "<ol><li>item 1</li><li>item 2</li></ol>"
            },
            new object[]
            {
                "# item 1\n#item 2\n## sub item 1\n## sub item 2", "<ol><li>item 1</li><li>item 2<ol><li>sub item 1</li><li>sub item 2</li></ol></li></ol>"
            },
            new object[]
            {
                "# item 1\n#item 2\n## sub item 1\n## sub item 2\n# item 3", "<ol><li>item 1</li><li>item 2<ol><li>sub item 1</li><li>sub item 2</li></ol></li><li>item 3</li></ol>"
            },
            new object[]
            {
                "* unordered item\n# ordered item", "<ul><li>unordered item</li></ul><ol><li>ordered item</li></ol>"
            },
            new object[]
            {
                "# ordered item\n* unordered item", "<ol><li>ordered item</li></ol><ul><li>unordered item</li></ul>"
            },
            new object[]
            {
                "* item 1\n*item 2\n## sub item 1\n## sub item 2", "<ul><li>item 1</li><li>item 2<ol><li>sub item 1</li><li>sub item 2</li></ol></li></ul>"
            },
            new object[]
            {
                "# item 1\n#item 2\n** sub item 1\n** sub item 2", "<ol><li>item 1</li><li>item 2<ul><li>sub item 1</li><li>sub item 2</li></ul></li></ol>"
            },
            new object[]
            {
                "* unordered list\nline 2", "<ul><li>unordered list\nline 2</li></ul>"
            },
            new object[]
            {
                "* unordered list\n\nparagraph", "<ul><li>unordered list</li></ul><p>paragraph</p>"
            },
            new object[]
            {
                "# ordered list\nline 2", "<ol><li>ordered list\nline 2</li></ol>"
            },
            new object[]
            {
                "# ordered list\n\nparagraph", "<ol><li>ordered list</li></ol><p>paragraph</p>"
            },
            new object[]
            {
                "* plain //emphasised//, **strong**, {{image}}, [[hyperlink]], {{{no wiki}}}, http://example.com text", "<ul><li>plain <em>emphasised</em>, <strong>strong</strong>, <img src=\"image\">, <a href=\"hyperlink\">hyperlink</a>, <code>no wiki</code>, <a href=\"http://example.com\">http://example.com</a> text</li></ul>"
            },
            new object[]
            {
                "* no // emphasis", "<ul><li>no // emphasis</li></ul>"
            },
            new object[]
            {
                "* no ** strong", "<ul><li>no ** strong</li></ul>"
            },
            new object[]
            {
                "* no [[ hyperlink", "<ul><li>no [[ hyperlink</li></ul>"
            },
            new object[]
            {
                "* no {{ image", "<ul><li>no {{ image</li></ul>"
            },
            new object[]
            {
                "* no {{{ code", "<ul><li>no {{{ code</li></ul>"
            },
            new object[]
            {
                "# plain //emphasised//, **strong**, {{image}}, [[hyperlink]], {{{no wiki}}}, http://example.com text", "<ol><li>plain <em>emphasised</em>, <strong>strong</strong>, <img src=\"image\">, <a href=\"hyperlink\">hyperlink</a>, <code>no wiki</code>, <a href=\"http://example.com\">http://example.com</a> text</li></ol>"
            },
            new object[]
            {
                "# no // emphasis", "<ol><li>no // emphasis</li></ol>"
            },
            new object[]
            {
                "# no ** strong", "<ol><li>no ** strong</li></ol>"
            },
            new object[]
            {
                "# no [[ hyperlink", "<ol><li>no [[ hyperlink</li></ol>"
            },
            new object[]
            {
                "# no {{ image", "<ol><li>no {{ image</li></ol>"
            },
            new object[]
            {
                "# no {{{ code", "<ol><li>no {{{ code</li></ol>"
            },
            new object[]
            {
                "* list 1\n\n*list 2", "<ul><li>list 1</li></ul><ul><li>list 2</li></ul>"
            },
            new object[]
            {
                "# list 1\n\n#list 2", "<ol><li>list 1</li></ol><ol><li>list 2</li></ol>"
            },
            new object[]
            {
                "*item 1\n***invalid sub-item", "<ul><li>item 1</li></ul><p>***invalid sub-item</p>"
            },
            new object[]
            {
                "*item 1\n###invalid sub-item", "<ul><li>item 1</li></ul><p>###invalid sub-item</p>"
            },
            new object[]
            {
                "#item 1\n***invalid sub-item", "<ol><li>item 1</li></ol><p>***invalid sub-item</p>"
            },
            new object[]
            {
                "#item 1\n###invalid sub-item", "<ol><li>item 1</li></ol><p>###invalid sub-item</p>"
            },
            new object[]
            {
                "*item 1\n*** invalid sub-item", "<ul><li>item 1</li></ul><p>*** invalid sub-item</p>"
            },
            new object[]
            {
                "*item 1\n### invalid sub-item", "<ul><li>item 1</li></ul><p>### invalid sub-item</p>"
            },
            new object[]
            {
                "#item 1\n*** invalid sub-item", "<ol><li>item 1</li></ol><p>*** invalid sub-item</p>"
            },
            new object[]
            {
                "#item 1\n### invalid sub-item", "<ol><li>item 1</li></ol><p>### invalid sub-item</p>"
            },
            new object[]
            {
                "paragraph 1\n*item 1\n\nparagraph 2", "<p>paragraph 1</p><ul><li>item 1</li></ul><p>paragraph 2</p>"
            },
            new object[]
            {
                "paragraph\n*item 1", "<p>paragraph</p><ul><li>item 1</li></ul>"
            },
            new object[]
            {
                "*item 1\n\nparagraph", "<ul><li>item 1</li></ul><p>paragraph</p>"
            },
            new object[]
            {
                "paragraph 1\n#item 1\n\nparagraph 2", "<p>paragraph 1</p><ol><li>item 1</li></ol><p>paragraph 2</p>"
            },
            new object[]
            {
                "paragraph\n#item 1", "<p>paragraph</p><ol><li>item 1</li></ol>"
            },
            new object[]
            {
                "#item 1\n\nparagraph", "<ol><li>item 1</li></ol><p>paragraph</p>"
            }
        };

        public static IEnumerable<object[]> EscapeCharacterToHtmlTestCases { get; } = new List<object[]>
        {
            new object[]
            {
                "~plain text", "<p>~plain text</p>"
            },
            new object[]
            {
                "~~plain text", "<p>~plain text</p>"
            },
            new object[]
            {
                "~~~plain text", "<p>~~plain text</p>"
            },
            new object[]
            {
                "~~~~plain text", "<p>~~plain text</p>"
            },
            new object[]
            {
                "~~~~~plain text", "<p>~~~plain text</p>"
            }
        };

        public static IEnumerable<object[]> RichTextToHtmlTestCases { get; } = new List<object[]>
        {
            new object[]
            {
                "", ""
            },
            new object[]
            {
                "    ", ""
            },
            new object[]
            {
                "\t\t", ""
            },
            new object[]
            {
                "\r\r", ""
            },
            new object[]
            {
                "\n\n", ""
            },

            new object[]
            {
                "**strong text**", "<p><strong>strong text</strong></p>"
            },
            new object[]
            {
                "plain **now strong** text", "<p>plain <strong>now strong</strong> text</p>"
            },
            new object[]
            {
                "**paragraph 1\n\nparagraph 2**", "<p>**paragraph 1</p><p>paragraph 2**</p>"
            },
            new object[]
            {
                "strong ** cannot\n\ncross ** paragraphs", "<p>strong ** cannot</p><p>cross ** paragraphs</p>"
            },
            new object[]
            {
                "escaped ~**strong", "<p>escaped **strong</p>"
            },
            new object[]
            {
                "asterix with ***strong** text", "<p>asterix with *<strong>strong</strong> text</p>"
            },
            new object[]
            {
                "asterix with **strong*** text", "<p>asterix with <strong>strong</strong>* text</p>"
            },
            new object[]
            {
                "asterix with ~***strong** text", "<p>asterix with *<strong>strong</strong> text</p>"
            },
            new object[]
            {
                "asterix with **strong~*** text", "<p>asterix with <strong>strong*</strong> text</p>"
            },

            new object[]
            {
                "//emphasised text//", "<p><em>emphasised text</em></p>"
            },
            new object[]
            {
                "plain //now emphasised// text", "<p>plain <em>now emphasised</em> text</p>"
            },
            new object[]
            {
                "//paragraph 1\n\nparagraph 2//", "<p>//paragraph 1</p><p>paragraph 2//</p>"
            },
            new object[]
            {
                "emphasis // cannot\n\ncross // paragraphs", "<p>emphasis // cannot</p><p>cross // paragraphs</p>"
            },
            new object[]
            {
                "escaped ~//emphasis", "<p>escaped //emphasis</p>"
            },
            new object[]
            {
                "slash with ///emphasised// text", "<p>slash with /<em>emphasised</em> text</p>"
            },
            new object[]
            {
                "slash with //emphasised/// text", "<p>slash with <em>emphasised</em>/ text</p>"
            },
            new object[]
            {
                "slash with ~///emphasised// text", "<p>slash with /<em>emphasised</em> text</p>"
            },
            new object[]
            {
                "slash with //emphasised~/// text", "<p>slash with <em>emphasised/</em> text</p>"
            },

            new object[]
            {
                "**//strong and emphasised//**", "<p><strong><em>strong and emphasised</em></strong></p>"
            },
            new object[]
            {
                "//**emphasised and strong**//", "<p><em><strong>emphasised and strong</strong></em></p>"
            },

            new object[]
            {
                "http://example.com", "<p><a href=\"http://example.com\">http://example.com</a></p>"
            },
            new object[]
            {
                "http://example.com:8080", "<p><a href=\"http://example.com:8080\">http://example.com:8080</a></p>"
            },
            new object[]
            {
                "http://example.com:8080/test", "<p><a href=\"http://example.com:8080/test\">http://example.com:8080/test</a></p>"
            },
            new object[]
            {
                "https://example.com", "<p><a href=\"https://example.com\">https://example.com</a></p>"
            },
            new object[]
            {
                "http://example.com/~", "<p><a href=\"http://example.com/~\">http://example.com/~</a></p>"
            },
            new object[]
            {
                "http://example.com/~~", "<p><a href=\"http://example.com/~\">http://example.com/~</a></p>"
            },
            new object[]
            {
                "HTTP://EXAMPLE.COM", "<p><a href=\"HTTP://EXAMPLE.COM\">HTTP://EXAMPLE.COM</a></p>"
            },
            new object[]
            {
                "~http://example.com", "<p>http://example.com</p>"
            },
            new object[]
            {
                "~http://example.com/ with no // emphasis", "<p>http://example.com/ with no // emphasis</p>"
            },
            new object[]
            {
                "~~http://example.com", "<p>~<a href=\"http://example.com\">http://example.com</a></p>"
            },
            new object[]
            {
                "~~~http://example.com", "<p>~http://example.com</p>"
            },
            new object[]
            {
                "tcp://example.com", "<p>tcp://example.com</p>"
            },
            new object[]
            {
                "//http://example.com//", "<p><em><a href=\"http://example.com\">http://example.com</a></em></p>"
            },
            new object[]
            {
                "http://example.com/", "<p><a href=\"http://example.com/\">http://example.com/</a></p>"
            },
            new object[]
            {
                "http://example.com//", "<p><a href=\"http://example.com\">http://example.com</a>//</p>"
            },
            new object[]
            {
                "http://example.com~//", "<p><a href=\"http://example.com//\">http://example.com//</a></p>"
            },
            new object[]
            {
                "http://example.com/~/", "<p><a href=\"http://example.com//\">http://example.com//</a></p>"
            },
            new object[]
            {
                "http://example.com///", "<p><a href=\"http://example.com/\">http://example.com/</a>//</p>"
            },
            new object[]
            {
                "http://example.com////", "<p><a href=\"http://example.com/\">http://example.com/</a>///</p>"
            },
            new object[]
            {
                "http://example", "<p><a href=\"http://example\">http://example</a></p>"
            },

            new object[]
            {
                "[[http://example.com]]", "<p><a href=\"http://example.com\">http://example.com</a></p>"
            },
            new object[]
            {
                "[[http://example.com/~~]]", "<p><a href=\"http://example.com/~\">http://example.com/~</a></p>"
            },
            new object[]
            {
                "[[http://example.com|text]]", "<p><a href=\"http://example.com\">text</a></p>"
            },
            new object[]
            {
                "[[http://example.com/~~|text]]", "<p><a href=\"http://example.com/~\">text</a></p>"
            },
            new object[]
            {
                "[http://example.com]]", "<p>[<a href=\"http://example.com\">http://example.com</a>]]</p>"
            },
            new object[]
            {
                "[[http://example.com]", "<p>[[<a href=\"http://example.com\">http://example.com</a>]</p>"
            },
            new object[]
            {
                "[[http://example.com/~~|text]", "<p>[[<a href=\"http://example.com/~|text\">http://example.com/~|text</a>]</p>"
            },
            new object[]
            {
                "[[http://example.com/~~|http://example.com]]", "<p><a href=\"http://example.com/~\">http://example.com</a></p>"
            },
            new object[]
            {
                "[[http://example.com/~~|//emphasis//]]", "<p><a href=\"http://example.com/~\"><em>emphasis</em></a></p>"
            },
            new object[]
            {
                "[[http://example.com/~~|**strong**]]", "<p><a href=\"http://example.com/~\"><strong>strong</strong></a></p>"
            },
            new object[]
            {
                "[[http://example.com/~~|**strong** and //emphasis//]]", "<p><a href=\"http://example.com/~\"><strong>strong</strong> and <em>emphasis</em></a></p>"
            },
            new object[]
            {
                "[[http://example.com/~~|//emphasis// and **strong**]]", "<p><a href=\"http://example.com/~\"><em>emphasis</em> and <strong>strong</strong></a></p>"
            },
            new object[]
            {
                "[[http://example.com/~~|//no emphasis]]//", "<p><a href=\"http://example.com/~\">//no emphasis</a>//</p>"
            },
            new object[]
            {
                "[[http://example.com/~~|**no strong]]**", "<p><a href=\"http://example.com/~\">**no strong</a>**</p>"
            },
            new object[]
            {
                "[[this is a link]]", "<p><a href=\"this is a link\">this is a link</a></p>"
            },
            new object[]
            {
                "[[this is a link|with a different name]]", "<p><a href=\"this is a link\">with a different name</a></p>"
            },
            new object[]
            {
                "//**[[Important page]]**//", "<p><em><strong><a href=\"Important page\">Important page</a></strong></em></p>"
            },
            new object[]
            {
                "//[[Important page|this link is italicized]]//\n**[[Important page]]**\n//**[[Important page]]**//", "<p><em><a href=\"Important page\">this link is italicized</a></em>\n<strong><a href=\"Important page\">Important page</a></strong>\n<em><strong><a href=\"Important page\">Important page</a></strong></em></p>"
            },

            new object[]
            {
                "{{http://example.com/image.jpg}}", "<p><img src=\"http://example.com/image.jpg\"></p>"
            },
            new object[]
            {
                "~{{http://example.com/image.jpg}}", "<p>{{<a href=\"http://example.com/image.jpg\">http://example.com/image.jpg</a>}}</p>"
            },
            new object[]
            {
                "{{http://example.com/image.jpg|alternative text}}", "<p><img src=\"http://example.com/image.jpg\" alt=\"alternative text\"></p>"
            },
            new object[]
            {
                "{{http://example.com/image.jpg|**no strong alternative text**}}", "<p><img src=\"http://example.com/image.jpg\" alt=\"**no strong alternative text**\"></p>"
            },
            new object[]
            {
                "{{http://example.com/image.jpg|//no emphasised alternative text//}}", "<p><img src=\"http://example.com/image.jpg\" alt=\"//no emphasised alternative text//\"></p>"
            },
            new object[]
            {
                "{http://example.com/image.jpg}}", "<p>{<a href=\"http://example.com/image.jpg\">http://example.com/image.jpg</a>}}</p>"
            },
            new object[]
            {
                "{{http://example.com/image.jpg}", "<p>{{<a href=\"http://example.com/image.jpg\">http://example.com/image.jpg</a>}</p>"
            },
            new object[]
            {
                "{{this is a link}}", "<p><img src=\"this is a link\"></p>"
            },
            new object[]
            {
                "{{this is a link|with an alternative text}}", "<p><img src=\"this is a link\" alt=\"with an alternative text\"></p>"
            },

            new object[]
            {
                @"line\\break", @"<p>line<br>break</p>"
            },
            new object[]
            {
                @"line~\\break", @"<p>line\\break</p>"
            },
            new object[]
            {
                @"line~~\\break", @"<p>line~<br>break</p>"
            },
            new object[]
            {
                @"line~\\\break", @"<p>line\<br>break</p>"
            },
            new object[]
            {
                @"double line\\\\break", @"<p>double line<br><br>break</p>"
            },
            new object[]
            {
                @"line break\\", @"<p>line break<br></p>"
            },
            new object[]
            {
                @"line break\", @"<p>line break\</p>"
            },

            new object[]
            {
                "plain {{{no wiki}}} text", "<p>plain <code>no wiki</code> text</p>"
            },
            new object[]
            {
                "plain {{{no escape ~~}}} text", "<p>plain <code>no escape ~</code> text</p>"
            },
            new object[]
            {
                "plain {{{code can contain ~}}} only when escaped}}} text", "<p>plain <code>code can contain }}} only when escaped</code> text</p>"
            },
            new object[]
            {
                "plain {{{no **strong**, no //emphais//, no [[hyperlinks]], no {{images}}}}} text", "<p>plain <code>no **strong**, no //emphais//, no [[hyperlinks]], no {{images}}</code> text</p>"
            },
            new object[]
            {
                "plain {{{}}} text", "<p>plain <code></code> text</p>"
            },
            new object[]
            {
                "preformatted text {{{\n\n}}} cannot bridge paragraphs", "<p>preformatted text {{{</p><p>}}} cannot bridge paragraphs</p>"
            },

            new object[]
            {
                "**//mixed strong emphasis**//", "<p>**//mixed strong emphasis**//</p>"
            },
            new object[]
            {
                "**//mixed strong emphasis**// still no emphasis//", "<p>**//mixed strong emphasis**// still no emphasis//</p>"
            },
            new object[]
            {
                "**//mixed strong emphasis**// still no strong**", "<p>**//mixed strong emphasis**// still no strong**</p>"
            },
            new object[]
            {
                "//**mixed emphasis strong//**", "<p>//**mixed emphasis strong//**</p>"
            },
            new object[]
            {
                "//**mixed emphasis strong//** still no strong**", "<p>//**mixed emphasis strong//** still no strong**</p>"
            },
            new object[]
            {
                "//**mixed emphasis strong//** still no emphasis//", "<p>//**mixed emphasis strong//** still no emphasis//</p>"
            },

            new object[]
            {
                "test <<test>> test", "<p>test <!-- test --> test</p>"
            },
            new object[]
            {
                "test\n\n<<test>>\n\ntest", "<p>test</p><!-- test --><p>test</p>"
            },

            new object[]
            {
                "plain **bold 1*** plain ****bold 2***** plain", "<p>plain <strong>bold 1</strong>* plain **<strong>bold 2</strong>*** plain</p>"
            }
        };

        public static IEnumerable<object[]> HorizontalRuleToHtmlTestsCases { get; } = new List<object[]>
        {
            new object[]
            {
                "----", "<hr>"
            },
            new object[]
            {
                "  ----", "<hr>"
            },
            new object[]
            {
                "\t----", "<hr>"
            },
            new object[]
            {
                "\r----", "<hr>"
            },
            new object[]
            {
                "\n----", "<hr>"
            },
            new object[]
            {
                "--------", "<hr>"
            },
            new object[]
            {
                "paragraph 1\n----\nparagraph 2", "<p>paragraph 1</p><hr><p>paragraph 2</p>"
            },
            new object[]
            {
                "~----", "<p>----</p>"
            }
        };

        public static IEnumerable<object[]> PluginToHtmlTestsCases { get; } = new List<object[]>
        {
            new object[]
            {
                "<<plug in>>", "<!-- plug in -->"
            },
            new object[]
            {
                "<<<plug in>>", "<!-- &lt;plug in -->"
            },
            new object[]
            {
                "<<plug in>>>", "<!-- plug in&gt; -->"
            },
            new object[]
            {
                "<<<plug in>>>", "<!-- &lt;plug in&gt; -->"
            },
            new object[]
            {
                "~<<plain text>>", "<p>&lt;&lt;plain text&gt;&gt;</p>"
            },
            new object[]
            {
                "<<test", "<p>&lt;&lt;test</p>"
            },
            new object[]
            {
                "paragraph\n<<plug in>>", "<p>paragraph</p><!-- plug in -->"
            },
            new object[]
            {
                "paragraph 1\n<<plug in>>\nparagraph 2", "<p>paragraph 1</p><!-- plug in --><p>paragraph 2</p>"
            },
            new object[]
            {
                "<<plug in>>\nparagraph", "<!-- plug in --><p>paragraph</p>"
            }
        };

        public static IEnumerable<object[]> CreoleWikiTestsCases { get; } = new List<object[]>
        {
            new object[]
            {
                "**//bold italics**//\n" +
                "//**bold italics//**",
                "<p>**//bold italics**//\n" +
                "//**bold italics//**</p>"
            },
            new object[]
            {
                "",
                ""
            },
            new object[]
            {
                "**bold**",
                "<p><strong>bold</strong></p>"
            },
            new object[]
            {
                "//italics//",
                "<p><em>italics</em></p>"
            },
            new object[]
            {
                "Bold and italics should //be\n" +
                "able// to cross lines.\n" +
                "\n" +
                "But, should //not be...\n" +
                "\n" +
                "...able// to cross paragraphs.",
                "<p>Bold and italics should <em>be\n" +
                "able</em> to cross lines.</p>"
                + "<p>But, should //not be...</p>"
                + "<p>...able// to cross paragraphs.</p>"
            },
            new object[]
            {
                "**//bold italics//**\n" +
                "//**bold italics**//\n" +
                "//This is **also** good.//",
                "<p><strong><em>bold italics</em></strong>\n" +
                "<em><strong>bold italics</strong></em>\n" +
                "<em>This is <strong>also</strong> good.</em></p>"
            },
            new object[]
            {
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
                + "<h3>**not** //parsed//</h3>"
            },
            new object[]
            {
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
                "<a href=\"Ohana:WikiFamily\">Ohana:WikiFamily</a></p>"
            },
            new object[]
            {
                @"This is the first line,\\and this is the second.",
                "<p>This is the first line,<br>and this is the second.</p>"
            },
            new object[]
            {
                "This is my text.\n" +
                "\n" +
                "This is more text.",
                "<p>This is my text.</p>"
                + "<p>This is more text.</p>"
            },
            new object[]
            {
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
                "</ul>"
            },
            new object[]
            {
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
                "</ol>"
            },
            new object[]
            {
                "Text\n" +
                "\n" +
                "----\n" +
                "\n" +
                "Text",
                "<p>Text</p>" +
                "<hr>" +
                "<p>Text</p>"
            },
            new object[]
            {
                "{{myimage.png|this is my image}}",
                "<p><img src=\"myimage.png\" alt=\"this is my image\"></p>"
            },
            new object[]
            {
                "{{{\n" +
                "//This// does **not** get [[formatted]] \n" +
                "}}}",
                "<pre><code>//This// does **not** get [[formatted]] </code></pre>"
            },
            new object[]
            {
                "Some examples of markup are: {{{** <i>this</i> ** }}}, this is also inline literal markup: {{{{{if (a>b) { b = a; }}}}}}.",
                "<p>Some examples of markup are: <code>** &lt;i&gt;this&lt;/i&gt; ** </code>, this is also inline literal markup: <code>{{if (a&gt;b) { b = a; }}}</code>.</p>"
            },
            new object[]
            {
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
                "  }}}</code></pre>"
            },
            new object[]
            {
                "~#1\n" +
                "http://www.foo.com/~bar/\n" +
                "~http://www.foo.com/\n" +
                "CamelCaseLink\n" +
                "~CamelCaseLink",
                "<p>#1\n" +
                "<a href=\"http://www.foo.com/~bar/\">http://www.foo.com/~bar/</a>\n" +
                "http://www.foo.com/\n" +
                "CamelCaseLink\n" +
                "~CamelCaseLink</p>"
            },
            new object[]
            {
                "//[[Important page|this link is italicized]]//\n" +
                "**[[Important page]]**\n" +
                "//**[[Important page]]**//",
                "<p><em><a href=\"Important page\">this link is italicized</a></em>\n" +
                "<strong><a href=\"Important page\">Important page</a></strong>\n" +
                "<em><strong><a href=\"Important page\">Important page</a></strong></em></p>"
            },
            new object[]
            {
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
                "</ol>"
            },
            new object[]
            {
                "|=Heading Col 1 |=Heading Col 2         |\n" +
                @"|Cell 1.1       |Two lines\\in Cell 1.2 |" + "\n" +
                "|Cell 2.1       |Cell 2.2               |",
                "<table><tbody>" +
                "<tr><th>Heading Col 1</th><th>Heading Col 2</th></tr>" +
                "<tr><td>Cell 1.1</td><td>Two lines<br>in Cell 1.2</td></tr>" +
                "<tr><td>Cell 2.1</td><td>Cell 2.2</td></tr>" +
                "</tbody></table>"
            },
            new object[]
            {
                "<<CurrentTimePlugin format='yyyy.MM.dd'>>",
                "<!-- CurrentTimePlugin format=&#39;yyyy.MM.dd&#39; -->"
            },
            new object[]
            {
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
                "<p>If interwiki links are setup in your wiki, this links to the WikiCreole page about Creole 1.0 test cases: <a href=\"WikiCreole:Creole1.0TestCases\">WikiCreole:Creole1.0TestCases</a>.</p>"
            },
            new object[]
            {
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
                "</ul>"
            },
            new object[]
            {
                "= Broken markup\n" +
                "\n" +
                "[[ {{ -- ** __ // foo | * - # \"Â§$%&/( (( ",
                "<h1>Broken markup</h1>" +
                "<p>[[ {{ -- ** __ // foo | * - # &quot;Â§$%&amp;/( ((</p>"
            }
        };
    }
}