using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    /// <summary>Base class of all parse result visitors containing functionality for allowing parse trees to be visited.</summary>
    public abstract class ParseTreeVisitor
    {
        private static readonly Task _completedTask = Task.FromResult<object>(null);

        /// <summary>Initializes a new instance of the <see cref="ParseTreeVisitor"/> class.</summary>
        protected ParseTreeVisitor()
        {
        }

        /// <summary>Asynchronously visits the beginning of the visit operation. This method is called before any other visit method.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task BeginVisitAsync(CancellationToken cancellationToken)
        {
            BeginVisit();
            return _completedTask;
        }

        /// <summary>Visits the beginning of the visit operation. This method is called before any other visit method.</summary>
        protected internal virtual void BeginVisit()
        {
        }

        /// <summary>Asynchronously completes the visit operation. This method is called after all other methods.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task EndVisitAsync(CancellationToken cancellationToken)
        {
            EndVisit();
            return _completedTask;
        }

        /// <summary>Completes the visit operation. This method is called after all other methods.</summary>
        protected internal virtual void EndVisit()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 1 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHeading1BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading1Beginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a level 1 heading.</summary>
        protected internal virtual void VisitHeading1Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 1 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHeading1EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading1Ending();
            return _completedTask;
        }

        /// <summary>Visits the ending of a level 1 heading.</summary>
        protected internal virtual void VisitHeading1Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 2 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHeading2BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading2Beginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a level 2 heading.</summary>
        protected internal virtual void VisitHeading2Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 2 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHeading2EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading2Ending();
            return _completedTask;
        }

        /// <summary>Visits the ending of a level 2 heading.</summary>
        protected internal virtual void VisitHeading2Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 3 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHeading3BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading3Beginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a level 3 heading.</summary>
        protected internal virtual void VisitHeading3Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 3 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHeading3EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading3Ending();
            return _completedTask;
        }

        /// <summary>Visits the ending of a level 3 heading.</summary>
        protected internal virtual void VisitHeading3Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 4 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHeading4BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading4Beginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a level 4 heading.</summary>
        protected internal virtual void VisitHeading4Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 4 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHeading4EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading4Ending();
            return _completedTask;
        }

        /// <summary>Visits the ending of a level 4 heading.</summary>
        protected internal virtual void VisitHeading4Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 5 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHeading5BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading5Beginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a level 5 heading.</summary>
        protected internal virtual void VisitHeading5Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 5 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHeading5EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading5Ending();
            return _completedTask;
        }

        /// <summary>Visits the ending of a level 5 heading.</summary>
        protected internal virtual void VisitHeading5Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 6 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHeading6BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading6Beginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a level 6 heading.</summary>
        protected internal virtual void VisitHeading6Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 6 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHeading6EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading6Ending();
            return _completedTask;
        }

        /// <summary>Visits the ending of a level 6 heading.</summary>
        protected internal virtual void VisitHeading6Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a paragraph.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitParagraphBeginningAsync(CancellationToken cancellationToken)
        {
            VisitParagraphBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a paragraph.</summary>
        protected internal virtual void VisitParagraphBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a paragraph.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitParagraphEndingAsync(CancellationToken cancellationToken)
        {
            VisitParagraphEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a paragraph.</summary>
        protected internal virtual void VisitParagraphEnding()
        {
        }

        /// <summary>Asynchronously visits a preformatted block.</summary>
        /// <param name="preformattedText">The preformatted text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitPreformattedBlockAsync(string preformattedText, CancellationToken cancellationToken)
        {
            VisitPreformattedBlock(preformattedText);
            return _completedTask;
        }

        /// <summary>Visits a preformatted block.</summary>
        /// <param name="preformattedText">The preformatted text.</param>
        protected internal virtual void VisitPreformattedBlock(string preformattedText)
        {
        }

        /// <summary>Asynchronously visits a horizontal rule.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHorizontalRuleAsync(CancellationToken cancellationToken)
        {
            VisitHorizontalRule();
            return _completedTask;
        }

        /// <summary>Visits a horizontal rule.</summary>
        protected internal virtual void VisitHorizontalRule()
        {
        }

        /// <summary>Asynchronously visits a plugin.</summary>
        /// <param name="text">The plugin text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitPluginAsync(string text, CancellationToken cancellationToken)
        {
            VisitPlugin(text);
            return _completedTask;
        }

        /// <summary>Visits a plugin.</summary>
        /// <param name="text">The plugin text.</param>
        protected internal virtual void VisitPlugin(string text)
        {
        }

        /// <summary>Asynchronously visits the beginning of a table.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitTableBeginningAsync(CancellationToken cancellationToken)
        {
            VisitTableBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a table.</summary>
        protected internal virtual void VisitTableBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a table.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitTableEndingAsync(CancellationToken cancellationToken)
        {
            VisitTableEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a table.</summary>
        protected internal virtual void VisitTableEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a table row.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitTableRowBeginningAsync(CancellationToken cancellationToken)
        {
            VisitTableRowBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a table row.</summary>
        protected internal virtual void VisitTableRowBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a table row.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitTableRowEndingAsync(CancellationToken cancellationToken)
        {
            VisitTableRowEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a table row.</summary>
        protected internal virtual void VisitTableRowEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a table header cell.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitTableHeaderCellBeginningAsync(CancellationToken cancellationToken)
        {
            VisitTableHeaderCellBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a table header cell.</summary>
        protected internal virtual void VisitTableHeaderCellBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a table header cell.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitTableHeaderCellEndingAsync(CancellationToken cancellationToken)
        {
            VisitTableHeaderCellEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a table header cell.</summary>
        protected internal virtual void VisitTableHeaderCellEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a table cell.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitTableCellBeginningAsync(CancellationToken cancellationToken)
        {
            VisitTableCellBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a table cell.</summary>
        protected internal virtual void VisitTableCellBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a table cell.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitTableCellEndingAsync(CancellationToken cancellationToken)
        {
            VisitTableCellEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a table cell.</summary>
        protected internal virtual void VisitTableCellEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of an unordered list.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitUnorderedListBeginningAsync(CancellationToken cancellationToken)
        {
            VisitUnorderedListBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of an unordered list.</summary>
        protected internal virtual void VisitUnorderedListBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of an unordered list.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitUnorderedListEndingAsync(CancellationToken cancellationToken)
        {
            VisitUnorderedListEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of an unordered list.</summary>
        protected internal virtual void VisitUnorderedListEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of an ordered list.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitOrderedListBeginningAsync(CancellationToken cancellationToken)
        {
            VisitOrderedListBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of an ordered list.</summary>
        protected internal virtual void VisitOrderedListBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of an ordered list.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitOrderedListEndingAsync(CancellationToken cancellationToken)
        {
            VisitOrderedListEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of an ordered list.</summary>
        protected internal virtual void VisitOrderedListEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a list item.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitListItemBeginningAsync(CancellationToken cancellationToken)
        {
            VisitListItemBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a list item.</summary>
        protected internal virtual void VisitListItemBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a list item.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitListItemEndingAsync(CancellationToken cancellationToken)
        {
            VisitListItemEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a list item.</summary>
        protected internal virtual void VisitListItemEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a strong element.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitStrongBeginningAsync(CancellationToken cancellationToken)
        {
            VisitStrongBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a strong element.</summary>
        protected internal virtual void VisitStrongBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a strong element.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitStrongEndingAsync(CancellationToken cancellationToken)
        {
            VisitStrongEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a strong element.</summary>
        protected internal virtual void VisitStrongEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of an emphasised element.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitEmphasisBeginningAsync(CancellationToken cancellationToken)
        {
            VisitEmphasisBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of an emphasised element.</summary>
        protected internal virtual void VisitEmphasisBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of an emphasised element.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitEmphasisEndingAsync(CancellationToken cancellationToken)
        {
            VisitEmphasisEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of an emphasised element.</summary>
        protected internal virtual void VisitEmphasisEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a hyperlink.</summary>
        /// <param name="destination">The hyperlink destination.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHyperlinkBeginningAsync(string destination, CancellationToken cancellationToken)
        {
            VisitHyperlinkBeginning(destination);
            return _completedTask;
        }

        /// <summary>Visits the beginning of a hyperlink.</summary>
        /// <param name="destination">The hyperlink destination.</param>
        protected internal virtual void VisitHyperlinkBeginning(string destination)
        {
        }

        /// <summary>Asynchronously visits the ending of a hyperlink.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitHyperlinkEndingAsync(CancellationToken cancellationToken)
        {
            VisitHyperlinkEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a hyperlink.</summary>
        protected internal virtual void VisitHyperlinkEnding()
        {
        }

        /// <summary>Asynchronously visits an image.</summary>
        /// <param name="source">The source of the image.</param>
        /// <param name="alternative">The alternative text of the image.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitImageAsync(string source, string alternative, CancellationToken cancellationToken)
        {
            VisitImage(source, alternative);
            return _completedTask;
        }

        /// <summary>Visits an image.</summary>
        /// <param name="source">The source of the image.</param>
        /// <param name="alternative">The alternative text of the image.</param>
        protected internal virtual void VisitImage(string source, string alternative)
        {
        }

        /// <summary>Asynchronously visits a line break.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitLineBreakAsync(CancellationToken cancellationToken)
        {
            VisitLineBreak();
            return _completedTask;
        }

        /// <summary>Visits a line break.</summary>
        protected internal virtual void VisitLineBreak()
        {
        }

        /// <summary>Asynchronously visits a preformatted text inside a block (e.g.: paragraph, list item or table).</summary>
        /// <param name="preformattedText">The preformatted text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitPreformattedTextAsync(string preformattedText, CancellationToken cancellationToken)
        {
            VisitPreformattedText(preformattedText);
            return _completedTask;
        }

        /// <summary>Visits a preformatted text inside a block (e.g.: paragraph, list item or table).</summary>
        /// <param name="preformattedText">The preformatted text.</param>
        protected internal virtual void VisitPreformattedText(string preformattedText)
        {
        }

        /// <summary>Asynchronously visits plain text. This method may be called multiple times consecutively.</summary>
        /// <param name="text">The plain text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected internal virtual Task VisitTextAsync(string text, CancellationToken cancellationToken)
        {
            VisitText(text);
            return _completedTask;
        }

        /// <summary>Visits plain text. This method may be called multiple times consecutively.</summary>
        /// <param name="text">The plain text.</param>
        protected internal virtual void VisitText(string text)
        {
        }
    }

    /// <summary>Base class of all parse result visitors containing functionality for allowing parse trees to be visited and eventually
    /// providing a result that is stored in memory (e.g. a <see cref="string"/> or a <see cref="System.IO.MemoryStream"/>).</summary>
    /// <typeparam name="TResult">The type which is constructed after visitng a parse tree.</typeparam>
    public abstract class ParseTreeVisitor<TResult>
        : ParseTreeVisitor
    {
        /// <summary>Initializes a new instance of the <see cref="ParseTreeVisitor{TResult}"/> class.</summary>
        protected ParseTreeVisitor()
        {
        }

        /// <summary>Asynchronously gets the visitor result. This values is used only after the visit operation completes.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns the result after the entire parse tree has been visited.</returns>
        protected internal virtual Task<TResult> GetResultAsync(CancellationToken cancellationToken)
            => Task.FromResult(GetResult());

        /// <summary>Gets the visitor result. This values is used only after the visit operation completes.</summary>
        /// <returns>Returns the result after the entire parse tree has been visited.</returns>
        protected internal abstract TResult GetResult();
    }
}