using System.Collections.Generic;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Tests
{
    public static class CreoleScannerTestData
    {
        public static IEnumerable<object[]> TextToTokensTestData { get; } = new List<object[]>
        {
            new object[]
            {
                "plain text", new object[] { Text, WhiteSpace, Text }
            },
            new object[]
            {
                "~escape", new object[] { Tilde, Text }
            },
            new object[]
            {
                "~~escape", new object[] { Text, Text }
            },
            new object[]
            {
                "~~~escape", new object[] { Text, Tilde, Text }
            },
            new object[]
            {
                "**strong**", new object[] { Asterisk, Asterisk, Text, Asterisk, Asterisk }
            },
            new object[]
            {
                "//emphasis//", new object[] { Slash, Slash, Text, Slash, Slash }
            },
            new object[]
            {
                "~~http://test.com", new object[] { Text, Text, Punctuation, Slash, Slash, Text, Punctuation, Text }
            },
            new object[]
            {
                "[[hyperlink]]", new object[] { BracketOpen, BracketOpen, Text, BracketClose, BracketClose }
            },
            new object[]
            {
                "[[hyperlink|with text]]", new object[] { BracketOpen, BracketOpen, Text, Pipe, Text, WhiteSpace, Text, BracketClose, BracketClose }
            },
            new object[]
            {
                "{{image}}", new object[] { BraceOpen, BraceOpen, Text, BraceClose, BraceClose }
            },
            new object[]
            {
                "{{image|with title}}", new object[] { BraceOpen, BraceOpen, Text, Pipe, Text, WhiteSpace, Text, BraceClose, BraceClose }
            },
            new object[]
            {
                "{{{no wiki}}}", new object[] { BraceOpen, BraceOpen, BraceOpen, Text, WhiteSpace, Text, BraceClose, BraceClose, BraceClose }
            },
            new object[]
            {
                "<<plug in>>", new object[] { AngleOpen, AngleOpen, Text, WhiteSpace, Text, AngleClose, AngleClose }
            },
            new object[]
            {
                @"line\\break", new object[] { Text, BackSlash, BackSlash, Text }
            },
            new object[]
            {
                "paragraph1\n\nparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\r\n\nparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\n\r\nparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\r\n\r\nparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\t\n\nparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\n\t\nparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\n\n\tparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\t\n\t\nparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\n\t\n\tparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\t\n\n\tparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\t\n\t\n\tparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1 \n\nparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\n \nparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\n\n paragraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1 \n \nparagraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1\n \n paragraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1 \n\n paragraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1 \n \n paragraph2", new object[] { Text, BlankLine, Text }
            },
            new object[]
            {
                "paragraph1 \n \n paragraph2 \n \n paragraph3", new object[] { Text, BlankLine, Text, BlankLine, Text }
            },
            new object[]
            {
                "line1\nline2", new object[] { Text, NewLine, Text }
            },
            new object[]
            {
                "line1\t\nline2", new object[] { Text, NewLine, Text }
            },
            new object[]
            {
                "line1\n\tline2", new object[] { Text, NewLine, Text }
            },
            new object[]
            {
                "line1 \nline2", new object[] { Text, NewLine, Text }
            },
            new object[]
            {
                "line1\n line2", new object[] { Text, NewLine, Text }
            },
            new object[]
            {
                "line1 \n line2 \n line3", new object[] { Text, NewLine, Text, NewLine, Text }
            },
            new object[]
            {
                "* unordered list", new object[] { Asterisk, WhiteSpace, Text, WhiteSpace, Text }
            },
            new object[]
            {
                "** unordered sub list", new object[] { Asterisk, Asterisk, WhiteSpace, Text, WhiteSpace, Text, WhiteSpace, Text }
            },
            new object[]
            {
                "# ordered list", new object[] { Hash, WhiteSpace, Text, WhiteSpace, Text }
            },
            new object[]
            {
                "## ordered sub list", new object[] { Hash, Hash, WhiteSpace, Text, WhiteSpace, Text, WhiteSpace, Text }
            },
            new object[]
            {
                "|=header 1|=header 2|", new object[] { Pipe, Equal, Text, WhiteSpace, Text, Pipe, Equal, Text, WhiteSpace, Text, Pipe }
            },
            new object[]
            {
                "|cell 1|cell2|", new object[] { Pipe, Text, WhiteSpace, Text, Pipe, Text, Pipe }
            },
            new object[]
            {
                "----", new object[] { Dash, Dash, Dash, Dash }
            },
            new object[]
            {
                "= heading1", new object[] { Equal, WhiteSpace, Text }
            },
            new object[]
            {
                "== heading2", new object[] { Equal, Equal, WhiteSpace, Text }
            },
            new object[]
            {
                "=== heading3", new object[] { Equal, Equal, Equal, WhiteSpace, Text }
            },
            new object[]
            {
                "==== heading4", new object[] { Equal, Equal, Equal, Equal, WhiteSpace, Text }
            },
            new object[]
            {
                "===== heading5", new object[] { Equal, Equal, Equal, Equal, Equal, WhiteSpace, Text }
            },
            new object[]
            {
                "====== heading6", new object[] { Equal, Equal, Equal, Equal, Equal, Equal, WhiteSpace, Text }
            }
        };

        public static IEnumerable<object[]> TextFromTokensTestData { get; } = new List<object[]>
        {
            new object[]
            {
                "plain text", "plain text"
            },
            new object[]
            {
                "~escape", "~escape"
            },
            new object[]
            {
                "~~escape", "~escape"
            },
            new object[]
            {
                "**strong**", "**strong**"
            },
            new object[]
            {
                "//emphasis//", "//emphasis//"
            },
            new object[]
            {
                "~/escape", "/escape"
            },
            new object[]
            {
                "~~http://test.com", "~http://test.com"
            },
            new object[]
            {
                "[[hyperlink]]", "[[hyperlink]]"
            },
            new object[]
            {
                "[[hyperlink|with text]]", "[[hyperlink|with text]]"
            },
            new object[]
            {
                "~[[hyperlink]]", "[[hyperlink]]"
            },
            new object[]
            {
                "{{image}}", "{{image}}"
            },
            new object[]
            {
                "{{image|with title}}", "{{image|with title}}"
            },
            new object[]
            {
                "~{{image}}", "{{image}}"
            },
            new object[]
            {
                "{{{no wiki}}}", "{{{no wiki}}}"
            },
            new object[]
            {
                "~{{{no wiki}}}", "{{{no wiki}}}"
            },
            new object[]
            {
                "<<plug in>>", "<<plug in>>"
            },
            new object[]
            {
                "line\\break", "line\\break"
            },
            new object[]
            {
                "paragraph1\n\nparagraph2", "paragraph1\n\nparagraph2"
            },
            new object[]
            {
                "paragraph1\r\n\nparagraph2", "paragraph1\r\n\nparagraph2"
            },
            new object[]
            {
                "paragraph1\n\r\nparagraph2", "paragraph1\n\r\nparagraph2"
            },
            new object[]
            {
                "paragraph1\r\n\r\nparagraph2", "paragraph1\r\n\r\nparagraph2"
            },
            new object[]
            {
                "* unordered list", "* unordered list"
            },
            new object[]
            {
                "* unordered sub list", "* unordered sub list"
            },
            new object[]
            {
                "# ordered list", "# ordered list"
            },
            new object[]
            {
                "## ordered sub list", "## ordered sub list"
            },
            new object[]
            {
                "|=header 1|=header 2|", "|=header 1|=header 2|"
            },
            new object[]
            {
                "|cell 1|cell2|", "|cell 1|cell2|"
            },
            new object[]
            {
                "----", "----"
            },
            new object[]
            {
                "= heading1", "= heading1"
            },
            new object[]
            {
                "== heading2", "== heading2"
            },
            new object[]
            {
                "=== heading3", "=== heading3"
            },
            new object[]
            {
                "==== heading4", "==== heading4"
            },
            new object[]
            {
                "===== heading5", "===== heading5"
            },
            new object[]
            {
                "====== heading6", "====== heading6"
            }
        };
    }
}