using System;
using System.Text;

namespace Mup
{
    public class HtmlWriterVisitor : ParseResultVisitor
    {
        private readonly StringBuilder _htmlStringBuilder;

        public HtmlWriterVisitor(StringBuilder htmlStringBuilder)
            => _htmlStringBuilder = (htmlStringBuilder ?? throw new ArgumentNullException(nameof(htmlStringBuilder)));

        protected override void BeginHeading1()
            => _htmlStringBuilder.Append("<h1>");

        protected override void EndHeading1()
            => _htmlStringBuilder.Append("</h1>");

        protected override void BeginHeading2()
            => _htmlStringBuilder.Append("<h2>");

        protected override void EndHeading2()
            => _htmlStringBuilder.Append("</h2>");

        protected override void BeginHeading3()
            => _htmlStringBuilder.Append("<h3>");

        protected override void EndHeading3()
            => _htmlStringBuilder.Append("</h3>");

        protected override void BeginHeading4()
            => _htmlStringBuilder.Append("<h4>");

        protected override void EndHeading4()
            => _htmlStringBuilder.Append("</h4>");

        protected override void BeginHeading5()
            => _htmlStringBuilder.Append("<h5>");

        protected override void EndHeading5()
            => _htmlStringBuilder.Append("</h5>");

        protected override void BeginHeading6()
            => _htmlStringBuilder.Append("<h6>");

        protected override void EndHeading6()
            => _htmlStringBuilder.Append("</h6>");

        protected override void BeginParagraph()
            => _htmlStringBuilder.Append("<p>");

        protected override void EndParagraph()
            => _htmlStringBuilder.Append("</p>");

        protected override void BeginPreformattedBlock()
            => _htmlStringBuilder.Append("<pre><code>");

        protected override void EndPreformattedBlock()
            => _htmlStringBuilder.Append("</code></pre>");

        protected override void BeginTable()
            => _htmlStringBuilder.Append("<table>");

        protected override void EndTable()
            => _htmlStringBuilder.Append("</table>");

        protected override void BeginTableRow()
            => _htmlStringBuilder.Append("<tr>");

        protected override void EndTableRow()
            => _htmlStringBuilder.Append("</tr>");

        protected override void BeginTableHeaderCell()
            => _htmlStringBuilder.Append("<th>");

        protected override void EndTableHeaderCell()
            => _htmlStringBuilder.Append("</th>");

        protected override void BeginTableCell()
            => _htmlStringBuilder.Append("<td>");

        protected override void EndTableCell()
            => _htmlStringBuilder.Append("</td>");

        protected override void BeginUnorderedList()
            => _htmlStringBuilder.Append("<ul>");

        protected override void EndUnorderedList()
            => _htmlStringBuilder.Append("</ul>");

        protected override void BeginOrderedList()
            => _htmlStringBuilder.Append("<ol>");

        protected override void EndOrderedList()
            => _htmlStringBuilder.Append("</ol>");

        protected override void BeginListItem()
            => _htmlStringBuilder.Append("<li>");

        protected override void EndListItem()
            => _htmlStringBuilder.Append("</li>");

        protected override void BeginStrong()
            => _htmlStringBuilder.Append("<strong>");

        protected override void EndStrong()
            => _htmlStringBuilder.Append("</strong>");

        protected override void BeginEmphasis()
            => _htmlStringBuilder.Append("<em>");

        protected override void EndEmphasis()
            => _htmlStringBuilder.Append("</em>");

        protected override void BeginHyperlink(string destination)
            => _htmlStringBuilder.Append("<a href=\"").Append(destination).Append("\">");

        protected override void EndHyperlink()
            => _htmlStringBuilder.Append("</a>");

        protected override void Image(string source, string alternative)
        {
            _htmlStringBuilder.Append("<img src=\"").Append(source).Append('"');
            if (!string.IsNullOrWhiteSpace(alternative))
                _htmlStringBuilder.Append(" alt=\"").Append(alternative).Append('"');
            _htmlStringBuilder.Append(">");
        }

        protected override void LineBreak()
            => _htmlStringBuilder.Append("<br>");

        protected override void BeginPreformatted()
            => _htmlStringBuilder.Append("<code>");

        protected override void EndPreformatted()
            => _htmlStringBuilder.Append("</code>");

        protected override void HorizontalRule()
            => _htmlStringBuilder.Append("<hr>");

        protected override void Text(string text)
        {
            foreach (var character in text)
                _AppendHtmlSafe(character);
        }

        private void _AppendHtmlSafe(char character)
        {
            switch (character)
            {
                case '&':
                    _htmlStringBuilder.Append("&amp;");
                    break;

                case '<':
                    _htmlStringBuilder.Append("&lt;");
                    break;

                case '>':
                    _htmlStringBuilder.Append("&gt;");
                    break;

                case '"':
                    _htmlStringBuilder.Append("&quot;");
                    break;

                case '\'':
                    _htmlStringBuilder.Append("&#39;");
                    break;

                default:
                    _htmlStringBuilder.Append(character);
                    break;
            }
        }
    }
}