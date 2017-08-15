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

        /// <summary>Initializes a new instance of the <see cref="HtmlWriterVisitor"/> class.</summary>
        public HtmlWriterVisitor()
        {
        }

        /// <summary>Gets the <see cref="StringBuilder"/> where the HTML is being written.</summary>
        protected StringBuilder HtmlStringBuilder { get; private set; } = null;

        /// <summary>Provides the HTML for the parsed text.</summary>
        protected sealed override string Result => _result;

        /// <summary>Asynchronously visits the beginning of the visit operation. This method is called before any other visit method.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected sealed override Task BeginVisitAsync(CancellationToken cancellationToken)
            => base.BeginVisitAsync(cancellationToken);

        /// <summary>Visits the beginning of the visit operation. This method is called before any other visit method.</summary>
        protected sealed override void BeginVisit()
        {
            _result = null;
            HtmlStringBuilder = new StringBuilder();
        }

        /// <summary>Asynchronously completes the visit operation. This method is called after all other methods.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected sealed override Task EndVisitAsync(CancellationToken cancellationToken)
            => base.EndVisitAsync(cancellationToken);

        /// <summary>Completes the visit operation. This method is called after all other methods.</summary>
        protected sealed override void EndVisit()
        {
            _result = HtmlStringBuilder.ToString();
            HtmlStringBuilder = null;
        }

        /// <summary>Visits the beginning of a level 1 heading.</summary>
        protected override void VisitHeading1Beginning()
            => HtmlStringBuilder.Append("<h1>");

        /// <summary>Visits the ending of a level 1 heading.</summary>
        protected override void VisitHeading1Ending()
            => HtmlStringBuilder.Append("</h1>");

        /// <summary>Visits the beginning of a level 2 heading.</summary>
        protected override void VisitHeading2Beginning()
            => HtmlStringBuilder.Append("<h2>");

        /// <summary>Visits the ending of a level 2 heading.</summary>
        protected override void VisitHeading2Ending()
            => HtmlStringBuilder.Append("</h2>");

        /// <summary>Visits the beginning of a level 3 heading.</summary>
        protected override void VisitHeading3Beginning()
            => HtmlStringBuilder.Append("<h3>");

        /// <summary>Visits the ending of a level 3 heading.</summary>
        protected override void VisitHeading3Ending()
            => HtmlStringBuilder.Append("</h3>");

        /// <summary>Visits the beginning of a level 4 heading.</summary>
        protected override void VisitHeading4Beginning()
            => HtmlStringBuilder.Append("<h4>");

        /// <summary>Visits the ending of a level 4 heading.</summary>
        protected override void VisitHeading4Ending()
            => HtmlStringBuilder.Append("</h4>");

        /// <summary>Visits the beginning of a level 5 heading.</summary>
        protected override void VisitHeading5Beginning()
            => HtmlStringBuilder.Append("<h5>");

        /// <summary>Visits the ending of a level 5 heading.</summary>
        protected override void VisitHeading5Ending()
            => HtmlStringBuilder.Append("</h5>");

        /// <summary>Visits the beginning of a level 6 heading.</summary>
        protected override void VisitHeading6Beginning()
            => HtmlStringBuilder.Append("<h6>");

        /// <summary>Visits the ending of a level 6 heading.</summary>
        protected override void VisitHeading6Ending()
            => HtmlStringBuilder.Append("</h6>");

        /// <summary>Visits the beginning of a paragraph.</summary>
        protected override void VisitParagraphBeginning()
            => HtmlStringBuilder.Append("<p>");

        /// <summary>Visits the ending of a paragraph.</summary>
        protected override void VisitParagraphEnding()
            => HtmlStringBuilder.Append("</p>");

        /// <summary>Visits the beginning of a preformatted block.</summary>
        protected override void VisitPreformattedBlockBeginning()
            => HtmlStringBuilder.Append("<pre><code>");

        /// <summary>Visits the ending of a preformatted block.</summary>
        protected override void VisitPreformattedBlockEnding()
            => HtmlStringBuilder.Append("</code></pre>");

        /// <summary>Visits the beginning of a table.</summary>
        protected override void VisitTableBeginning()
            => HtmlStringBuilder.Append("<table>");

        /// <summary>Visits the ending of a table.</summary>
        protected override void VisitTableEnding()
            => HtmlStringBuilder.Append("</table>");

        /// <summary>Visits the beginning of a table row.</summary>
        protected override void VisitTableRowBeginning()
            => HtmlStringBuilder.Append("<tr>");

        /// <summary>Visits the ending of a table row.</summary>
        protected override void VisitTableRowEnding()
            => HtmlStringBuilder.Append("</tr>");

        /// <summary>Visits the beginning of a table header cell.</summary>
        protected override void VisitTableHeaderCellBeginning()
            => HtmlStringBuilder.Append("<th>");

        /// <summary>Visits the ending of a table header cell.</summary>
        protected override void VisitTableHeaderCellEnding()
            => HtmlStringBuilder.Append("</th>");

        /// <summary>Visits the beginning of a table cell.</summary>
        protected override void VisitTableCellBeginning()
            => HtmlStringBuilder.Append("<td>");

        /// <summary>Visits the ending of a table cell.</summary>
        protected override void VisitTableCellEnding()
            => HtmlStringBuilder.Append("</td>");

        /// <summary>Visits the beginning of an unordered list.</summary>
        protected override void VisitUnorderedListBeginning()
            => HtmlStringBuilder.Append("<ul>");

        /// <summary>Visits the ending of an unordered list.</summary>
        protected override void VisitUnorderedListEnding()
            => HtmlStringBuilder.Append("</ul>");

        /// <summary>Visits the beginning of an ordered list.</summary>
        protected override void VisitOrderedListBeginning()
            => HtmlStringBuilder.Append("<ol>");

        /// <summary>Visits the ending of an ordered list.</summary>
        protected override void VisitOrderedListEnding()
            => HtmlStringBuilder.Append("</ol>");

        /// <summary>Visits the beginning of a list item.</summary>
        protected override void VisitListItemBeginning()
            => HtmlStringBuilder.Append("<li>");

        /// <summary>Visits the ending of a list item.</summary>
        protected override void VisitListItemEnding()
            => HtmlStringBuilder.Append("</li>");

        /// <summary>Visits the beginning of a strong element.</summary>
        protected override void VisitStrongBeginning()
            => HtmlStringBuilder.Append("<strong>");

        /// <summary>Visits the ending of a strong element.</summary>
        protected override void VisitStrongEnding()
            => HtmlStringBuilder.Append("</strong>");

        /// <summary>Visits the beginning of an emphasised element.</summary>
        protected override void VisitEmphasisBeginning()
            => HtmlStringBuilder.Append("<em>");

        /// <summary>Visits the ending of an emphasised element.</summary>
        protected override void VisitEmphasisEnding()
            => HtmlStringBuilder.Append("</em>");

        /// <summary>Visits the beginning of a hyperlink.</summary>
        protected override void VisitHyperlinkBeginning(string destination)
            => HtmlStringBuilder.Append("<a href=\"").Append(destination).Append("\">");

        /// <summary>Visits the ending of a hyperlink.</summary>
        protected override void VisitHyperlinkEnding()
            => HtmlStringBuilder.Append("</a>");

        /// <summary>Visits an image.</summary>
        protected override void VisitImage(string source, string alternative)
        {
            HtmlStringBuilder.Append("<img src=\"").Append(source).Append('"');
            if (!string.IsNullOrWhiteSpace(alternative))
                HtmlStringBuilder.Append(" alt=\"").Append(alternative).Append('"');
            HtmlStringBuilder.Append(">");
        }

        /// <summary>Visits a line break.</summary>
        protected override void VisitLineBreak()
            => HtmlStringBuilder.Append("<br>");

        /// <summary>Visits the beginning of a preformatted text.</summary>
        protected override void VisitPreformattedTextBeginning()
            => HtmlStringBuilder.Append("<code>");

        /// <summary>Visits the ending of a preformatted text.</summary>
        protected override void VisitPreformattedTextEnding()
            => HtmlStringBuilder.Append("</code>");

        /// <summary>Visits a horizontal rule.</summary>
        protected override void VisitHorizontalRule()
            => HtmlStringBuilder.Append("<hr>");

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
                    HtmlStringBuilder.Append("&amp;");
                    break;

                case '<':
                    HtmlStringBuilder.Append("&lt;");
                    break;

                case '>':
                    HtmlStringBuilder.Append("&gt;");
                    break;

                case '"':
                    HtmlStringBuilder.Append("&quot;");
                    break;

                case '\'':
                    HtmlStringBuilder.Append("&#39;");
                    break;

                default:
                    HtmlStringBuilder.Append(character);
                    break;
            }
        }
    }
}