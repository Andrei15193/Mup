using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Mup.ElementMarkCode;

namespace Mup
{
    /// <summary>Base class of all parse result visitors containing functionality for allowing parse trees to be visited.</summary>
    public abstract class ParseResultVisitor
    {
        private static readonly Task _completedTask = Task.FromResult<object>(null);

        private StringBuilder _imageSourceBuilder = null;
        private StringBuilder _imageAlternativeTextBuilder = null;
        private StringBuilder _pluginTextBuilder = null;

        /// <summary>Initializes a new instance of the <see cref="ParseResultVisitor"/> class.</summary>
        protected ParseResultVisitor()
        {
        }

        internal Task VisitAsync(string text, IEnumerable<ElementMark> marks)
            => VisitAsync(text, marks, CancellationToken.None);

        internal async Task VisitAsync(string text, IEnumerable<ElementMark> marks, CancellationToken cancellationToken)
        {
            var beginVisitTask = BeginVisitAsync(cancellationToken);
            if (beginVisitTask != _completedTask)
                await beginVisitTask;

            foreach (var mark in marks)
                await _VisitAsync(text, mark, cancellationToken);

            var endingVisitTask = EndVisitAsync(cancellationToken);
            if (endingVisitTask != _completedTask)
                await endingVisitTask;
        }

        /// <summary>Asynchronously visits the beginning of the visit operation. This method is called before any other visit method.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task BeginVisitAsync(CancellationToken cancellationToken)
        {
            BeginVisit();
            return _completedTask;
        }

        /// <summary>Visits the beginning of the visit operation. This method is called before any other visit method.</summary>
        protected virtual void BeginVisit()
        {
        }

        /// <summary>Asynchronously completes the visit operation. This method is called after all other methods.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task EndVisitAsync(CancellationToken cancellationToken)
        {
            EndVisit();
            return _completedTask;
        }

        /// <summary>Completes the visit operation. This method is called after all other methods.</summary>
        protected virtual void EndVisit()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 1 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHeading1BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading1Beginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a level 1 heading.</summary>
        protected virtual void VisitHeading1Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 1 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHeading1EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading1Ending();
            return _completedTask;
        }

        /// <summary>Visits the ending of a level 1 heading.</summary>
        protected virtual void VisitHeading1Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 2 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHeading2BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading2Beginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a level 2 heading.</summary>
        protected virtual void VisitHeading2Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 2 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHeading2EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading2Ending();
            return _completedTask;
        }

        /// <summary>Visits the ending of a level 2 heading.</summary>
        protected virtual void VisitHeading2Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 3 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHeading3BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading3Beginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a level 3 heading.</summary>
        protected virtual void VisitHeading3Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 3 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHeading3EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading3Ending();
            return _completedTask;
        }

        /// <summary>Visits the ending of a level 3 heading.</summary>
        protected virtual void VisitHeading3Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 4 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHeading4BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading4Beginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a level 4 heading.</summary>
        protected virtual void VisitHeading4Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 4 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHeading4EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading4Ending();
            return _completedTask;
        }

        /// <summary>Visits the ending of a level 4 heading.</summary>
        protected virtual void VisitHeading4Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 5 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHeading5BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading5Beginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a level 5 heading.</summary>
        protected virtual void VisitHeading5Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 5 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHeading5EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading5Ending();
            return _completedTask;
        }

        /// <summary>Visits the ending of a level 5 heading.</summary>
        protected virtual void VisitHeading5Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a level 6 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHeading6BeginningAsync(CancellationToken cancellationToken)
        {
            VisitHeading6Beginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a level 6 heading.</summary>
        protected virtual void VisitHeading6Beginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a level 6 heading.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHeading6EndingAsync(CancellationToken cancellationToken)
        {
            VisitHeading6Ending();
            return _completedTask;
        }

        /// <summary>Visits the ending of a level 6 heading.</summary>
        protected virtual void VisitHeading6Ending()
        {
        }

        /// <summary>Asynchronously visits the beginning of a paragraph.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitParagraphBeginningAsync(CancellationToken cancellationToken)
        {
            VisitParagraphBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a paragraph.</summary>
        protected virtual void VisitParagraphBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a paragraph.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitParagraphEndingAsync(CancellationToken cancellationToken)
        {
            VisitParagraphEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a paragraph.</summary>
        protected virtual void VisitParagraphEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a preformatted block.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitPreformattedBlockBeginningAsync(CancellationToken cancellationToken)
        {
            VisitPreformattedBlockBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a preformatted block.</summary>
        protected virtual void VisitPreformattedBlockBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a preformatted block.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitPreformattedBlockEndingAsync(CancellationToken cancellationToken)
        {
            VisitPreformattedBlockEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a preformatted block.</summary>
        protected virtual void VisitPreformattedBlockEnding()
        {
        }

        /// <summary>Asynchronously visits a horizontal rule.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHorizontalRuleAsync(CancellationToken cancellationToken)
        {
            VisitHorizontalRule();
            return _completedTask;
        }

        /// <summary>Visits a horizontal rule.</summary>
        protected virtual void VisitHorizontalRule()
        {
        }

        /// <summary>Asynchronously visits a plug in.</summary>
        /// <param name="value">The value of the plug in.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitPlugInAsync(string value, CancellationToken cancellationToken)
        {
            VisitPlugIn(value);
            return _completedTask;
        }

        /// <summary>Visits a plug in.</summary>
        /// <param name="value">The value of the plug in.</param>
        protected virtual void VisitPlugIn(string value)
        {
        }

        /// <summary>Asynchronously visits the beginning of a table.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitTableBeginningAsync(CancellationToken cancellationToken)
        {
            VisitTableBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a table.</summary>
        protected virtual void VisitTableBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a table.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitTableEndingAsync(CancellationToken cancellationToken)
        {
            VisitTableEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a table.</summary>
        protected virtual void VisitTableEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a table row.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitTableRowBeginningAsync(CancellationToken cancellationToken)
        {
            VisitTableRowBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a table row.</summary>
        protected virtual void VisitTableRowBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a table row.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitTableRowEndingAsync(CancellationToken cancellationToken)
        {
            VisitTableRowEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a table row.</summary>
        protected virtual void VisitTableRowEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a table header cell.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitTableHeaderCellBeginningAsync(CancellationToken cancellationToken)
        {
            VisitTableHeaderCellBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a table header cell.</summary>
        protected virtual void VisitTableHeaderCellBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a table header cell.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitTableHeaderCellEndingAsync(CancellationToken cancellationToken)
        {
            VisitTableHeaderCellEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a table header cell.</summary>
        protected virtual void VisitTableHeaderCellEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a table cell.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitTableCellBeginningAsync(CancellationToken cancellationToken)
        {
            VisitTableCellBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a table cell.</summary>
        protected virtual void VisitTableCellBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a table cell.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitTableCellEndingAsync(CancellationToken cancellationToken)
        {
            VisitTableCellEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a table cell.</summary>
        protected virtual void VisitTableCellEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of an unordered list.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitUnorderedListBeginningAsync(CancellationToken cancellationToken)
        {
            VisitUnorderedListBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of an unordered list.</summary>
        protected virtual void VisitUnorderedListBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of an unordered list.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitUnorderedListEndingAsync(CancellationToken cancellationToken)
        {
            VisitUnorderedListEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of an unordered list.</summary>
        protected virtual void VisitUnorderedListEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of an ordered list.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitOrderedListBeginningAsync(CancellationToken cancellationToken)
        {
            VisitOrderedListBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of an ordered list.</summary>
        protected virtual void VisitOrderedListBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of an ordered list.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitOrderedListEndingAsync(CancellationToken cancellationToken)
        {
            VisitOrderedListEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of an ordered list.</summary>
        protected virtual void VisitOrderedListEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a list item.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitListItemBeginningAsync(CancellationToken cancellationToken)
        {
            VisitListItemBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a list item.</summary>
        protected virtual void VisitListItemBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a list item.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitListItemEndingAsync(CancellationToken cancellationToken)
        {
            VisitListItemEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a list item.</summary>
        protected virtual void VisitListItemEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a strong element.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitStrongBeginningAsync(CancellationToken cancellationToken)
        {
            VisitStrongBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a strong element.</summary>
        protected virtual void VisitStrongBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a strong element.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitStrongEndingAsync(CancellationToken cancellationToken)
        {
            VisitStrongEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a strong element.</summary>
        protected virtual void VisitStrongEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of an emphasised element.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitEmphasisBeginningAsync(CancellationToken cancellationToken)
        {
            VisitEmphasisBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of an emphasised element.</summary>
        protected virtual void VisitEmphasisBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of an emphasised element.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitEmphasisEndingAsync(CancellationToken cancellationToken)
        {
            VisitEmphasisEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of an emphasised element.</summary>
        protected virtual void VisitEmphasisEnding()
        {
        }

        /// <summary>Asynchronously visits the beginning of a hyperlink.</summary>
        /// <param name="destination">The hyperlink destination.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHyperlinkBeginningAsync(string destination, CancellationToken cancellationToken)
        {
            VisitHyperlinkBeginning(destination);
            return _completedTask;
        }

        /// <summary>Visits the beginning of a hyperlink.</summary>
        /// <param name="destination">The hyperlink destination.</param>
        protected virtual void VisitHyperlinkBeginning(string destination)
        {
        }

        /// <summary>Asynchronously visits the ending of a hyperlink.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitHyperlinkEndingAsync(CancellationToken cancellationToken)
        {
            VisitHyperlinkEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a hyperlink.</summary>
        protected virtual void VisitHyperlinkEnding()
        {
        }

        /// <summary>Asynchronously visits an image.</summary>
        /// <param name="source">The source of the image.</param>
        /// <param name="alternative">The alternative text of the image.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitImageAsync(string source, string alternative, CancellationToken cancellationToken)
        {
            VisitImage(source, alternative);
            return _completedTask;
        }

        /// <summary>Visits an image.</summary>
        /// <param name="source">The source of the image.</param>
        /// <param name="alternative">The alternative text of the image.</param>
        protected virtual void VisitImage(string source, string alternative)
        {
        }

        /// <summary>Asynchronously visits a line break.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitLineBreakAsync(CancellationToken cancellationToken)
        {
            VisitLineBreak();
            return _completedTask;
        }

        /// <summary>Visits a line break.</summary>
        protected virtual void VisitLineBreak()
        {
        }

        /// <summary>Asynchronously visits the beginning of a preformatted text.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitPreformattedTextBeginningAsync(CancellationToken cancellationToken)
        {
            VisitPreformattedTextBeginning();
            return _completedTask;
        }

        /// <summary>Visits the beginning of a preformatted text.</summary>
        protected virtual void VisitPreformattedTextBeginning()
        {
        }

        /// <summary>Asynchronously visits the ending of a preformatted text.</summary>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitPreformattedTextEndingAsync(CancellationToken cancellationToken)
        {
            VisitPreformattedTextEnding();
            return _completedTask;
        }

        /// <summary>Visits the ending of a preformatted text.</summary>
        protected virtual void VisitPreformattedTextEnding()
        {
        }

        /// <summary>Asynchronously visits plain text. This method may be called multiple times consecutively.</summary>
        /// <param name="text">The plain text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        protected virtual Task VisitTextAsync(string text, CancellationToken cancellationToken)
        {
            VisitText(text);
            return _completedTask;
        }

        /// <summary>Visits plain text. This method may be called multiple times consecutively.</summary>
        /// <param name="text">The plain text.</param>
        protected virtual void VisitText(string text)
        {
        }

        private async Task _VisitAsync(string text, ElementMark mark, CancellationToken cancellationToken)
        {
            var visitTask = _completedTask;

            switch (mark.Code)
            {
                case HorizontalRule:
                    visitTask = VisitHorizontalRuleAsync(cancellationToken);
                    break;

                case HyperlinkStart:
                    break;

                case HyperlinkDestination:
                    var destination = text.Substring(mark.Start, mark.Length);
                    visitTask = VisitHyperlinkBeginningAsync(destination, cancellationToken);
                    break;

                case HyperlinkEnd:
                    visitTask = VisitHyperlinkEndingAsync(cancellationToken);
                    break;

                case StrongStart:
                    visitTask = VisitStrongBeginningAsync(cancellationToken);
                    break;

                case StrongEnd:
                    visitTask = VisitStrongEndingAsync(cancellationToken);
                    break;

                case EmphasisStart:
                    visitTask = VisitEmphasisBeginningAsync(cancellationToken);
                    break;

                case EmphasisEnd:
                    visitTask = VisitEmphasisEndingAsync(cancellationToken);
                    break;

                case ImageStart:
                    _imageSourceBuilder = new StringBuilder();
                    _imageAlternativeTextBuilder = new StringBuilder();
                    break;

                case ImageSource:
                    _imageSourceBuilder.Append(text, mark.Start, mark.Length);
                    break;

                case PlainText when (_imageAlternativeTextBuilder != null):
                    _imageAlternativeTextBuilder.Append(text, mark.Start, mark.Length);
                    break;

                case ImageEnd:
                    visitTask = VisitImageAsync(_imageSourceBuilder.ToString(), _imageAlternativeTextBuilder.ToString(), cancellationToken);
                    _imageSourceBuilder = null;
                    _imageAlternativeTextBuilder = null;
                    break;

                case LineBreak:
                    visitTask = VisitLineBreakAsync(cancellationToken);
                    break;

                case PreformattedStart:
                    visitTask = VisitPreformattedTextBeginningAsync(cancellationToken);
                    break;

                case PreformattedEnd:
                    visitTask = VisitPreformattedTextEndingAsync(cancellationToken);
                    break;

                case Heading1Start:
                    visitTask = VisitHeading1BeginningAsync(cancellationToken);
                    break;

                case Heading1End:
                    visitTask = VisitHeading1EndingAsync(cancellationToken);
                    break;

                case Heading2Start:
                    visitTask = VisitHeading2BeginningAsync(cancellationToken);
                    break;

                case Heading2End:
                    visitTask = VisitHeading2EndingAsync(cancellationToken);
                    break;

                case Heading3Start:
                    visitTask = VisitHeading3BeginningAsync(cancellationToken);
                    break;

                case Heading3End:
                    visitTask = VisitHeading3EndingAsync(cancellationToken);
                    break;

                case Heading4Start:
                    visitTask = VisitHeading4BeginningAsync(cancellationToken);
                    break;

                case Heading4End:
                    visitTask = VisitHeading4EndingAsync(cancellationToken);
                    break;

                case Heading5Start:
                    visitTask = VisitHeading5BeginningAsync(cancellationToken);
                    break;

                case Heading5End:
                    visitTask = VisitHeading5EndingAsync(cancellationToken);
                    break;

                case Heading6Start:
                    visitTask = VisitHeading6BeginningAsync(cancellationToken);
                    break;

                case Heading6End:
                    visitTask = VisitHeading6EndingAsync(cancellationToken);
                    break;

                case ParagraphStart:
                    visitTask = VisitParagraphBeginningAsync(cancellationToken);
                    break;

                case ParagraphEnd:
                    visitTask = VisitParagraphEndingAsync(cancellationToken);
                    break;

                case PreformattedBlockStart:
                    visitTask = VisitPreformattedBlockBeginningAsync(cancellationToken);
                    break;

                case PreformattedBlockEnd:
                    visitTask = VisitPreformattedBlockEndingAsync(cancellationToken);
                    break;

                case TableStart:
                    visitTask = VisitTableBeginningAsync(cancellationToken);
                    break;

                case TableRowStart:
                    visitTask = VisitTableRowBeginningAsync(cancellationToken);
                    break;

                case TableHeaderCellStart:
                    visitTask = VisitTableHeaderCellBeginningAsync(cancellationToken);
                    break;

                case TableHeaderCellEnd:
                    visitTask = VisitTableHeaderCellEndingAsync(cancellationToken);
                    break;

                case TableCellStart:
                    visitTask = VisitTableCellBeginningAsync(cancellationToken);
                    break;

                case TableCellEnd:
                    visitTask = VisitTableCellEndingAsync(cancellationToken);
                    break;

                case TableRowEnd:
                    visitTask = VisitTableRowEndingAsync(cancellationToken);
                    break;

                case TableEnd:
                    visitTask = VisitTableEndingAsync(cancellationToken);
                    break;

                case UnorderedListStart:
                    visitTask = VisitUnorderedListBeginningAsync(cancellationToken);
                    break;

                case UnorderedListEnd:
                    visitTask = VisitUnorderedListEndingAsync(cancellationToken);
                    break;

                case OrderedListStart:
                    visitTask = VisitOrderedListBeginningAsync(cancellationToken);
                    break;

                case OrderedListEnd:
                    visitTask = VisitOrderedListEndingAsync(cancellationToken);
                    break;

                case ListItemStart:
                    visitTask = VisitListItemBeginningAsync(cancellationToken);
                    break;

                case ListItemEnd:
                    visitTask = VisitListItemEndingAsync(cancellationToken);
                    break;

                case PluginStart:
                    _pluginTextBuilder = new StringBuilder();
                    break;

                case PlainText when (_pluginTextBuilder != null):
                    _pluginTextBuilder.Append(text, mark.Start, mark.Length);
                    break;

                case PluginEnd:
                    visitTask = VisitPlugInAsync(_pluginTextBuilder.ToString(), cancellationToken);
                    _pluginTextBuilder = null;
                    break;

                case PlainText:
                    var plainText = text.Substring(mark.Start, mark.Length);
                    visitTask = VisitTextAsync(plainText, cancellationToken);
                    break;

                default:
                    break;
            }

            if (visitTask != _completedTask)
                await visitTask;
        }
    }

    /// <summary>Base class of all parse result visitors containing functionality for allowing parse trees to be visited and eventually
    /// providing a result that is stored in memory (e.g. a <see cref="string"/> or a <see cref="System.IO.MemoryStream"/>).</summary>
    public abstract class ParseResultVisitor<TResult>
        : ParseResultVisitor
    {
        /// <summary>Initializes a new instance of the <see cref="ParseResultVisitor{TResult}"/> class.</summary>
        protected ParseResultVisitor()
        {
        }

        /// <summary>Gets the visitor result. This values is used only after the visit operation completes.</summary>
        protected abstract TResult Result { get; }

        new internal Task<TResult> VisitAsync(string text, IEnumerable<ElementMark> marks)
            => VisitAsync(text, marks, CancellationToken.None);

        new internal async Task<TResult> VisitAsync(string text, IEnumerable<ElementMark> marks, CancellationToken cancellationToken)
        {
            await base.VisitAsync(text, marks, cancellationToken);
            return Result;
        }
    }
}