using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    /// <summary>Visitor for generating HTML from <see cref="IParseResult"/>.</summary>
    public class HtmlWriterVisitor
        : ParseResultVisitor<string>
    {
        private string _result = null;
        private StringBuilder _htmlStringBuilder = null;

        /// <summary>Initializes a new instance of the <see cref="HtmlWriterVisitor"/> class.</summary>
        public HtmlWriterVisitor()
        {
        }

        /// <summary>Asynchronously visits the beginning of the visit operation. This method is called before any other visit method.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected sealed override Task BeginVisitAsync(CancellationToken cancellationToken)
            => base.BeginVisitAsync(cancellationToken);

        /// <summary>Visits the beginning of the visit operation. This method is called before any other visit method.</summary>
        protected sealed override void BeginVisit()
        {
            _result = null;
            _htmlStringBuilder = new StringBuilder();
        }

        /// <summary>Asynchronously completes the visit operation. This method is called after all other methods.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected sealed override Task EndVisitAsync(CancellationToken cancellationToken)
            => base.EndVisitAsync(cancellationToken);

        /// <summary>Completes the visit operation. This method is called after all other methods.</summary>
        protected sealed override void EndVisit()
        {
            _result = _htmlStringBuilder.ToString();
            _htmlStringBuilder = null;
        }

        /// <summary>Provides the HTML for the parsed text.</summary>
        protected override string Result => _result;

        /// <summary>Visits the beginning of a level 1 heading.</summary>
        protected override void VisitHeading1Beginning()
            => _htmlStringBuilder.Append("<h1>");

        /// <summary>Visits the ending of a level 1 heading.</summary>
        protected override void VisitHeading1Ending()
            => _htmlStringBuilder.Append("</h1>");

        /// <summary>Visits the beginning of a level 2 heading.</summary>
        protected override void VisitHeading2Beginning()
            => _htmlStringBuilder.Append("<h2>");

        /// <summary>Visits the ending of a level 2 heading.</summary>
        protected override void VisitHeading2Ending()
            => _htmlStringBuilder.Append("</h2>");

        /// <summary>Visits the beginning of a level 3 heading.</summary>
        protected override void VisitHeading3Beginning()
            => _htmlStringBuilder.Append("<h3>");

        /// <summary>Visits the ending of a level 3 heading.</summary>
        protected override void VisitHeading3Ending()
            => _htmlStringBuilder.Append("</h3>");

        /// <summary>Visits the beginning of a level 4 heading.</summary>
        protected override void VisitHeading4Beginning()
            => _htmlStringBuilder.Append("<h4>");

        /// <summary>Visits the ending of a level 4 heading.</summary>
        protected override void VisitHeading4Ending()
            => _htmlStringBuilder.Append("</h4>");

        /// <summary>Visits the beginning of a level 5 heading.</summary>
        protected override void VisitHeading5Beginning()
            => _htmlStringBuilder.Append("<h5>");

        /// <summary>Visits the ending of a level 5 heading.</summary>
        protected override void VisitHeading5Ending()
            => _htmlStringBuilder.Append("</h5>");

        /// <summary>Visits the beginning of a level 6 heading.</summary>
        protected override void VisitHeading6Beginning()
            => _htmlStringBuilder.Append("<h6>");

        /// <summary>Visits the ending of a level 6 heading.</summary>
        protected override void VisitHeading6Ending()
            => _htmlStringBuilder.Append("</h6>");

        /// <summary>Visits the beginning of a paragraph.</summary>
        protected override void VisitParagraphBeginning()
            => _htmlStringBuilder.Append("<p>");

        /// <summary>Visits the ending of a paragraph.</summary>
        protected override void VisitParagraphEnding()
            => _htmlStringBuilder.Append("</p>");

        /// <summary>Visits the beginning of a preformatted block.</summary>
        protected override void VisitPreformattedBlockBeginning()
            => _htmlStringBuilder.Append("<pre><code>");

        /// <summary>Visits the ending of a preformatted block.</summary>
        protected override void VisitPreformattedBlockEnding()
            => _htmlStringBuilder.Append("</code></pre>");

        /// <summary>Visits the beginning of a table.</summary>
        protected override void VisitTableBeginning()
            => _htmlStringBuilder.Append("<table>");

        /// <summary>Visits the ending of a table.</summary>
        protected override void VisitTableEnding()
            => _htmlStringBuilder.Append("</table>");

        /// <summary>Visits the beginning of a table row.</summary>
        protected override void VisitTableRowBeginning()
            => _htmlStringBuilder.Append("<tr>");

        /// <summary>Visits the ending of a table row.</summary>
        protected override void VisitTableRowEnding()
            => _htmlStringBuilder.Append("</tr>");

        /// <summary>Visits the beginning of a table header cell.</summary>
        protected override void VisitTableHeaderCellBeginning()
            => _htmlStringBuilder.Append("<th>");

        /// <summary>Visits the ending of a table header cell.</summary>
        protected override void VisitTableHeaderCellEnding()
            => _htmlStringBuilder.Append("</th>");

        /// <summary>Visits the beginning of a table cell.</summary>
        protected override void VisitTableCellBeginning()
            => _htmlStringBuilder.Append("<td>");

        /// <summary>Visits the ending of a table cell.</summary>
        protected override void VisitTableCellEnding()
            => _htmlStringBuilder.Append("</td>");

        /// <summary>Visits the beginning of an unordered list.</summary>
        protected override void VisitUnorderedListBeginning()
            => _htmlStringBuilder.Append("<ul>");

        /// <summary>Visits the ending of an unordered list.</summary>
        protected override void VisitUnorderedListEnding()
            => _htmlStringBuilder.Append("</ul>");

        /// <summary>Visits the beginning of an ordered list.</summary>
        protected override void VisitOrderedListBeginning()
            => _htmlStringBuilder.Append("<ol>");

        /// <summary>Visits the ending of an ordered list.</summary>
        protected override void VisitOrderedListEnding()
            => _htmlStringBuilder.Append("</ol>");

        /// <summary>Visits the beginning of a list item.</summary>
        protected override void VisitListItemBeginning()
            => _htmlStringBuilder.Append("<li>");

        /// <summary>Visits the ending of a list item.</summary>
        protected override void VisitListItemEnding()
            => _htmlStringBuilder.Append("</li>");

        /// <summary>Visits the beginning of a strong element.</summary>
        protected override void VisitStrongBeginning()
            => _htmlStringBuilder.Append("<strong>");

        /// <summary>Visits the ending of a strong element.</summary>
        protected override void VisitStrongEnding()
            => _htmlStringBuilder.Append("</strong>");

        /// <summary>Visits the beginning of an emphasised element.</summary>
        protected override void VisitEmphasisBeginning()
            => _htmlStringBuilder.Append("<em>");

        /// <summary>Visits the ending of an emphasised element.</summary>
        protected override void VisitEmphasisEnding()
            => _htmlStringBuilder.Append("</em>");

        /// <summary>Visits the beginning of a hyperlink.</summary>
        protected override void VisitHyperlinkBeginning(string destination)
            => _htmlStringBuilder.Append("<a href=\"").Append(destination).Append("\">");

        /// <summary>Visits the ending of a hyperlink.</summary>
        protected override void VisitHyperlinkEnding()
            => _htmlStringBuilder.Append("</a>");

        /// <summary>Visits an image.</summary>
        protected override void VisitImage(string source, string alternative)
        {
            _htmlStringBuilder.Append("<img src=\"").Append(source).Append('"');
            if (!string.IsNullOrWhiteSpace(alternative))
                _htmlStringBuilder.Append(" alt=\"").Append(alternative).Append('"');
            _htmlStringBuilder.Append(">");
        }

        /// <summary>Visits a line break.</summary>
        protected override void VisitLineBreak()
            => _htmlStringBuilder.Append("<br>");

        /// <summary>Visits the beginning of a preformatted text.</summary>
        protected override void VisitPreformattedTextBeginning()
            => _htmlStringBuilder.Append("<code>");

        /// <summary>Visits the ending of a preformatted text.</summary>
        protected override void VisitPreformattedTextEnding()
            => _htmlStringBuilder.Append("</code>");

        /// <summary>Visits a horizontal rule.</summary>
        protected override void VisitHorizontalRule()
            => _htmlStringBuilder.Append("<hr>");

        /// <summary>Visits plain text. This method may be called multiple times consecutively.</summary>
        protected override void VisitText(string text)
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