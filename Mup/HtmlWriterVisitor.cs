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