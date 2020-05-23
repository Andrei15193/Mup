using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    /// <summary>Base class of all parse tree visitors.</summary>
    public abstract class ParseTreeVisitor
    {
        /// <summary>Initializes a new instance of the <see cref="ParseTreeVisitor" /> class.</summary>
        protected ParseTreeVisitor()
        {
        }

        /// <summary>Asynchronously initializes the visitor. This method is called before any visit method.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task BeginVisitAsync(CancellationToken cancellationToken)
        {
            BeginVisit();
            return Task.CompletedTask;
        }

        /// <summary>Initializes the visitor. This method is called before any visit method.</summary>
        protected internal virtual void BeginVisit()
        {
        }

        /// <summary>Asynchronously completes the visit operation. This method is called after all visit methods.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task EndVisitAsync(CancellationToken cancellationToken)
        {
            EndVisit();
            return Task.CompletedTask;
        }

        /// <summary>Completes the visit operation. This method is called after all visit methods.</summary>
        protected internal virtual void EndVisit()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 1 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHeading1BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading1Beginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a level 1 heading.</summary>
        protected internal virtual void VisitHeading1Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 1 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHeading1EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading1Ending();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a level 1 heading.</summary>
        protected internal virtual void VisitHeading1Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 2 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHeading2BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading2Beginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a level 2 heading.</summary>
        protected internal virtual void VisitHeading2Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 2 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHeading2EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading2Ending();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a level 2 heading.</summary>
        protected internal virtual void VisitHeading2Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 3 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHeading3BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading3Beginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a level 3 heading.</summary>
        protected internal virtual void VisitHeading3Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 3 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHeading3EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading3Ending();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a level 3 heading.</summary>
        protected internal virtual void VisitHeading3Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 4 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHeading4BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading4Beginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a level 4 heading.</summary>
        protected internal virtual void VisitHeading4Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 4 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHeading4EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading4Ending();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a level 4 heading.</summary>
        protected internal virtual void VisitHeading4Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 5 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHeading5BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading5Beginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a level 5 heading.</summary>
        protected internal virtual void VisitHeading5Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 5 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHeading5EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading5Ending();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a level 5 heading.</summary>
        protected internal virtual void VisitHeading5Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 6 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHeading6BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading6Beginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a level 6 heading.</summary>
        protected internal virtual void VisitHeading6Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 6 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHeading6EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading6Ending();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a level 6 heading.</summary>
        protected internal virtual void VisitHeading6Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a paragraph.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitParagraphBeginningAsync(CancellationToken cancellationToken)
        {
            VisitParagraphBeginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a paragraph.</summary>
        protected internal virtual void VisitParagraphBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a paragraph.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitParagraphEndingAsync(CancellationToken cancellationToken)
        {
            VisitParagraphEnding();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a paragraph.</summary>
        protected internal virtual void VisitParagraphEnding()
        {
        }

        /// <summary>Asynchronously visits a preformatted block.</summary>
        /// <param name="preformattedText">The preformatted text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitPreformattedBlockAsync(string preformattedText, CancellationToken cancellationToken)
        {
            VisitPreformattedBlock(preformattedText);
            return Task.CompletedTask;
        }

        /// <summary>Visits a preformatted block.</summary>
        /// <param name="preformattedText">The preformatted text.</param>
        protected internal virtual void VisitPreformattedBlock(string preformattedText)
        {
        }

        /// <summary>Asynchronously visits a horizontal rule.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHorizontalRuleAsync(CancellationToken cancellationToken)
        {
            VisitHorizontalRule();
            return Task.CompletedTask;
        }

        /// <summary>Visits a horizontal rule.</summary>
        protected internal virtual void VisitHorizontalRule()
        {
        }

        /// <summary>Asynchronously visits a plugin.</summary>
        /// <param name="text">The plugin text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitPluginAsync(string text, CancellationToken cancellationToken)
        {
            VisitPlugin(text);
            return Task.CompletedTask;
        }

        /// <summary>Visits a plugin.</summary>
        /// <param name="text">The plugin text.</param>
        protected internal virtual void VisitPlugin(string text)
        {
        }

        /// <summary>Asynchronously visits the beginning of a table.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitTableBeginningAsync(CancellationToken cancellationToken)
        {
            VisitTableBeginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a table.</summary>
        protected internal virtual void VisitTableBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a table.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitTableEndingAsync(CancellationToken cancellationToken)
        {
            VisitTableEnding();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a table.</summary>
        protected internal virtual void VisitTableEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a table row.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitTableRowBeginningAsync(CancellationToken cancellationToken)
        {
            VisitTableRowBeginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a table row.</summary>
        protected internal virtual void VisitTableRowBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a table row.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitTableRowEndingAsync(CancellationToken cancellationToken)
        {
            VisitTableRowEnding();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a table row.</summary>
        protected internal virtual void VisitTableRowEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a table header cell.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitTableHeaderCellBeginningAsync(CancellationToken cancellationToken)
        {
            VisitTableHeaderCellBeginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a table header cell.</summary>
        protected internal virtual void VisitTableHeaderCellBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a table header cell.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitTableHeaderCellEndingAsync(CancellationToken cancellationToken)
        {
            VisitTableHeaderCellEnding();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a table header cell.</summary>
        protected internal virtual void VisitTableHeaderCellEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a table cell.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitTableCellBeginningAsync(CancellationToken cancellationToken)
        {
            VisitTableCellBeginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a table cell.</summary>
        protected internal virtual void VisitTableCellBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a table cell.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitTableCellEndingAsync(CancellationToken cancellationToken)
        {
            VisitTableCellEnding();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a table cell.</summary>
        protected internal virtual void VisitTableCellEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of an unordered list.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitUnorderedListBeginningAsync(CancellationToken cancellationToken)
        {
            VisitUnorderedListBeginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of an unordered list.</summary>
        protected internal virtual void VisitUnorderedListBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of an unordered list.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitUnorderedListEndingAsync(CancellationToken cancellationToken)
        {
            VisitUnorderedListEnding();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of an unordered list.</summary>
        protected internal virtual void VisitUnorderedListEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of an ordered list.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitOrderedListBeginningAsync(CancellationToken cancellationToken)
        {
            VisitOrderedListBeginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of an ordered list.</summary>
        protected internal virtual void VisitOrderedListBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of an ordered list.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitOrderedListEndingAsync(CancellationToken cancellationToken)
        {
            VisitOrderedListEnding();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of an ordered list.</summary>
        protected internal virtual void VisitOrderedListEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a list item.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitListItemBeginningAsync(CancellationToken cancellationToken)
        {
            VisitListItemBeginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a list item.</summary>
        protected internal virtual void VisitListItemBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a list item.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitListItemEndingAsync(CancellationToken cancellationToken)
        {
            VisitListItemEnding();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a list item.</summary>
        protected internal virtual void VisitListItemEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a strong element.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitStrongBeginningAsync(CancellationToken cancellationToken)
        {
            VisitStrongBeginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a strong element.</summary>
        protected internal virtual void VisitStrongBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a strong element.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitStrongEndingAsync(CancellationToken cancellationToken)
        {
            VisitStrongEnding();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a strong element.</summary>
        protected internal virtual void VisitStrongEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of an emphasised element.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitEmphasisBeginningAsync(CancellationToken cancellationToken)
        {
            VisitEmphasisBeginning();
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of an emphasised element.</summary>
        protected internal virtual void VisitEmphasisBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of an emphasised element.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitEmphasisEndingAsync(CancellationToken cancellationToken)
        {
            VisitEmphasisEnding();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of an emphasised element.</summary>
        protected internal virtual void VisitEmphasisEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a hyperlink.</summary>
        /// <param name="destination">The hyperlink destination.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHyperlinkBeginningAsync(string destination, CancellationToken cancellationToken)
        {
            VisitHyperlinkBeginning(destination);
            return Task.CompletedTask;
        }

        /// <summary>Visits the beginning of a hyperlink.</summary>
        /// <param name="destination">The hyperlink destination.</param>
        protected internal virtual void VisitHyperlinkBeginning(string destination)
        {
        }

        /// <summary>Asynchronously visits the ending of a hyperlink.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitHyperlinkEndingAsync(CancellationToken cancellationToken)
        {
            VisitHyperlinkEnding();
            return Task.CompletedTask;
        }

        /// <summary>Visits the ending of a hyperlink.</summary>
        protected internal virtual void VisitHyperlinkEnding()
        {
        }

        /// <summary>Asynchronously visits an image.</summary>
        /// <param name="source">The source of the image.</param>
        /// <param name="alternativeText">The alternative text for the image.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitImageAsync(string source, string alternativeText, CancellationToken cancellationToken)
        {
            VisitImage(source, alternativeText);
            return Task.CompletedTask;
        }

        /// <summary>Visits an image.</summary>
        /// <param name="source">The source of the image.</param>
        /// <param name="alternativeText">The alternative text for the image.</param>
        protected internal virtual void VisitImage(string source, string alternativeText)
        {
        }

        /// <summary>Asynchronously visits a line break.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitLineBreakAsync(CancellationToken cancellationToken)
        {
            VisitLineBreak();
            return Task.CompletedTask;
        }

        /// <summary>Visits a line break.</summary>
        protected internal virtual void VisitLineBreak()
        {
        }

        /// <summary>Asynchronously visits a code fragment inside a block (e.g.: paragraph, list item or table).</summary>
        /// <param name="fragment">The code fragment.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitCodeFragmentAsync(string fragment, CancellationToken cancellationToken)
        {
            VisitCodeFragment(fragment);
            return Task.CompletedTask;
        }

        /// <summary>Visits a code fragment inside a block (e.g.: paragraph, list item or table).</summary>
        /// <param name="fragment">The code fragment.</param>
        protected internal virtual void VisitCodeFragment(string fragment)
        {
        }

        /// <summary>Asynchronously visits plain text. This method may be called multiple times consecutively.</summary>
        /// <param name="text">The plain text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        protected internal virtual Task VisitTextAsync(string text, CancellationToken cancellationToken)
        {
            VisitText(text);
            return Task.CompletedTask;
        }

        /// <summary>Visits plain text. This method may be called multiple times consecutively.</summary>
        /// <param name="text">The plain text.</param>
        protected internal virtual void VisitText(string text)
        {
        }
    }

    /// <summary>Base class for all parse tree visitors that eventually provide a result that is stored in
    /// memory (e.g. a <see cref="string"/> or a <see cref="System.IO.MemoryStream"/>).</summary>
    /// <typeparam name="TResult">The type which is constructed after visitng a parse tree.</typeparam>
    public abstract class ParseTreeVisitor<TResult>
        : ParseTreeVisitor
    {
        /// <summary>Initializes a new instance of the <see cref="ParseTreeVisitor{TResult}" /> class.</summary>
        protected ParseTreeVisitor()
        {
        }

        /// <summary>Asynchronously gets the visitor result. This method is called only after the visit operation completes.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns the result after the entire parse tree has been visited wrapped in a <see cref="Task{TResult}"/>.</returns>
        protected internal virtual Task<TResult> GetResultAsync(CancellationToken cancellationToken)
            => Task.FromResult(GetResult());

        /// <summary>Gets the visitor result. This method is called only after the visit operation completes.</summary>
        /// <returns>Returns the result after the entire parse tree has been visited.</returns>
        protected internal abstract TResult GetResult();
    }
}