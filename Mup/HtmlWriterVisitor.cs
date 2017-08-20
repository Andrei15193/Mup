using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    /// <summary>Visitor for generating HTML from <see cref="IParseTree"/>.</summary>
    public class HtmlWriterVisitor
        : ParseTreeVisitor<string>
    {
        private string _result = null;

        /// <summary>Initializes a new instance of the <see cref="HtmlWriterVisitor"/> class.</summary>
        public HtmlWriterVisitor()
        {
        }

        /// <summary>Gets the <see cref="StringBuilder"/> where the HTML is being written.</summary>
        protected StringBuilder HtmlStringBuilder { get; private set; } = null;

        /// <summary>Gets the visitor result. This values is used only after the visit operation completes.</summary>
        /// <returns>Returns the result after the entire parse tree has been visited.</returns>
        protected internal override string GetResult()
            => _result;

        /// <summary>Asynchronously visits the beginning of the visit operation. This method is called before any other visit method.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal sealed override Task BeginVisitAsync(CancellationToken cancellationToken)
            => base.BeginVisitAsync(cancellationToken);

        /// <summary>Visits the beginning of the visit operation. This method is called before any other visit method.</summary>
        protected internal sealed override void BeginVisit()
        {
            _result = null;
            HtmlStringBuilder = new StringBuilder();
        }

        /// <summary>Asynchronously completes the visit operation. This method is called after all other methods.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal sealed override Task EndVisitAsync(CancellationToken cancellationToken)
            => base.EndVisitAsync(cancellationToken);

        /// <summary>Completes the visit operation. This method is called after all other methods.</summary>
        protected internal sealed override void EndVisit()
        {
            _result = HtmlStringBuilder.ToString();
            HtmlStringBuilder = null;
        }

        /// <summary>Visits the beginning of a level 1 heading.</summary>
        protected internal override void VisitHeading1Beginning()
            => HtmlStringBuilder.Append("<h1>");

        /// <summary>Visits the ending of a level 1 heading.</summary>
        protected internal override void VisitHeading1Ending()
            => HtmlStringBuilder.Append("</h1>");

        /// <summary>Visits the beginning of a level 2 heading.</summary>
        protected internal override void VisitHeading2Beginning()
            => HtmlStringBuilder.Append("<h2>");

        /// <summary>Visits the ending of a level 2 heading.</summary>
        protected internal override void VisitHeading2Ending()
            => HtmlStringBuilder.Append("</h2>");

        /// <summary>Visits the beginning of a level 3 heading.</summary>
        protected internal override void VisitHeading3Beginning()
            => HtmlStringBuilder.Append("<h3>");

        /// <summary>Visits the ending of a level 3 heading.</summary>
        protected internal override void VisitHeading3Ending()
            => HtmlStringBuilder.Append("</h3>");

        /// <summary>Visits the beginning of a level 4 heading.</summary>
        protected internal override void VisitHeading4Beginning()
            => HtmlStringBuilder.Append("<h4>");

        /// <summary>Visits the ending of a level 4 heading.</summary>
        protected internal override void VisitHeading4Ending()
            => HtmlStringBuilder.Append("</h4>");

        /// <summary>Visits the beginning of a level 5 heading.</summary>
        protected internal override void VisitHeading5Beginning()
            => HtmlStringBuilder.Append("<h5>");

        /// <summary>Visits the ending of a level 5 heading.</summary>
        protected internal override void VisitHeading5Ending()
            => HtmlStringBuilder.Append("</h5>");

        /// <summary>Visits the beginning of a level 6 heading.</summary>
        protected internal override void VisitHeading6Beginning()
            => HtmlStringBuilder.Append("<h6>");

        /// <summary>Visits the ending of a level 6 heading.</summary>
        protected internal override void VisitHeading6Ending()
            => HtmlStringBuilder.Append("</h6>");

        /// <summary>Visits the beginning of a paragraph.</summary>
        protected internal override void VisitParagraphBeginning()
            => HtmlStringBuilder.Append("<p>");

        /// <summary>Visits the ending of a paragraph.</summary>
        protected internal override void VisitParagraphEnding()
            => HtmlStringBuilder.Append("</p>");

        /// <summary>Visits a preformatted block.</summary>
        /// <param name="preformattedText">The preformatted text.</param>
        protected internal override void VisitPreformattedBlock(string preformattedText)
        {
            HtmlStringBuilder.Append("<pre><code>");
            AppendHtmlSafe(preformattedText);
            HtmlStringBuilder.Append("</code></pre>");
        }

        /// <summary>Visits the beginning of a table.</summary>
        protected internal override void VisitTableBeginning()
            => HtmlStringBuilder.Append("<table><tbody>");

        /// <summary>Visits the ending of a table.</summary>
        protected internal override void VisitTableEnding()
            => HtmlStringBuilder.Append("</tbody></table>");

        /// <summary>Visits the beginning of a table row.</summary>
        protected internal override void VisitTableRowBeginning()
            => HtmlStringBuilder.Append("<tr>");

        /// <summary>Visits the ending of a table row.</summary>
        protected internal override void VisitTableRowEnding()
            => HtmlStringBuilder.Append("</tr>");

        /// <summary>Visits the beginning of a table header cell.</summary>
        protected internal override void VisitTableHeaderCellBeginning()
            => HtmlStringBuilder.Append("<th>");

        /// <summary>Visits the ending of a table header cell.</summary>
        protected internal override void VisitTableHeaderCellEnding()
            => HtmlStringBuilder.Append("</th>");

        /// <summary>Visits the beginning of a table cell.</summary>
        protected internal override void VisitTableCellBeginning()
            => HtmlStringBuilder.Append("<td>");

        /// <summary>Visits the ending of a table cell.</summary>
        protected internal override void VisitTableCellEnding()
            => HtmlStringBuilder.Append("</td>");

        /// <summary>Visits the beginning of an unordered list.</summary>
        protected internal override void VisitUnorderedListBeginning()
            => HtmlStringBuilder.Append("<ul>");

        /// <summary>Visits the ending of an unordered list.</summary>
        protected internal override void VisitUnorderedListEnding()
            => HtmlStringBuilder.Append("</ul>");

        /// <summary>Visits the beginning of an ordered list.</summary>
        protected internal override void VisitOrderedListBeginning()
            => HtmlStringBuilder.Append("<ol>");

        /// <summary>Visits the ending of an ordered list.</summary>
        protected internal override void VisitOrderedListEnding()
            => HtmlStringBuilder.Append("</ol>");

        /// <summary>Visits the beginning of a list item.</summary>
        protected internal override void VisitListItemBeginning()
            => HtmlStringBuilder.Append("<li>");

        /// <summary>Visits the ending of a list item.</summary>
        protected internal override void VisitListItemEnding()
            => HtmlStringBuilder.Append("</li>");

        /// <summary>Visits the beginning of a strong element.</summary>
        protected internal override void VisitStrongBeginning()
            => HtmlStringBuilder.Append("<strong>");

        /// <summary>Visits the ending of a strong element.</summary>
        protected internal override void VisitStrongEnding()
            => HtmlStringBuilder.Append("</strong>");

        /// <summary>Visits the beginning of an emphasised element.</summary>
        protected internal override void VisitEmphasisBeginning()
            => HtmlStringBuilder.Append("<em>");

        /// <summary>Visits the ending of an emphasised element.</summary>
        protected internal override void VisitEmphasisEnding()
            => HtmlStringBuilder.Append("</em>");

        /// <summary>Visits the beginning of a hyperlink.</summary>
        protected internal override void VisitHyperlinkBeginning(string destination)
        {
            HtmlStringBuilder.Append("<a href=\"");
            AppendHtmlSafe(destination);
            HtmlStringBuilder.Append("\">");
        }

        /// <summary>Visits the ending of a hyperlink.</summary>
        protected internal override void VisitHyperlinkEnding()
            => HtmlStringBuilder.Append("</a>");

        /// <summary>Visits an image.</summary>
        protected internal override void VisitImage(string source, string alternative)
        {
            HtmlStringBuilder.Append("<img src=\"");
            AppendHtmlSafe(source);
            HtmlStringBuilder.Append('"');
            if (!string.IsNullOrWhiteSpace(alternative))
            {
                HtmlStringBuilder.Append(" alt=\"");
                AppendHtmlSafe(alternative);
                HtmlStringBuilder.Append('"');
            }
            HtmlStringBuilder.Append(">");
        }

        /// <summary>Visits a line break.</summary>
        protected internal override void VisitLineBreak()
            => HtmlStringBuilder.Append("<br>");

        /// <summary>Visits a preformatted text inside a block (e.g.: paragraph, list item or table).</summary>
        /// <param name="preformattedText">The preformatted text.</param>
        protected internal override void VisitPreformattedText(string preformattedText)
        {
            HtmlStringBuilder.Append("<code>");
            AppendHtmlSafe(preformattedText);
            HtmlStringBuilder.Append("</code>");
        }

        /// <summary>Visits a horizontal rule.</summary>
        protected internal override void VisitHorizontalRule()
            => HtmlStringBuilder.Append("<hr>");

        /// <summary>Visits plain text. This method may be called multiple times consecutively.</summary>
        protected internal override void VisitText(string text)
            => AppendHtmlSafe(text);

        /// <summary>Appends the HTML encoded, if necessary, <paramref name="text"/>.</summary>
        /// <param name="text">The text to append to <see cref="HtmlStringBuilder"/>.</param>
        protected void AppendHtmlSafe(string text)
        {
            foreach (var character in text)
                AppendHtmlSafe(character);
        }

        /// <summary>Appends the HTML encoded, if necessary, <paramref name="character"/>.</summary>
        /// <param name="character">The character to append to <see cref="HtmlStringBuilder"/>.</param>
        protected void AppendHtmlSafe(char character)
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