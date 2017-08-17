using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Mup.ElementMarkCode;

namespace Mup
{
    internal class FlatParseTree
        : IParseTree
    {
        private readonly string _text;
        private readonly IEnumerable<ElementMark> _marks;

        internal FlatParseTree(string text, IEnumerable<ElementMark> marks)
        {
            _text = text;
            _marks = marks;
        }

        public Task AcceptAsync(ParseTreeVisitor visitor)
            => AcceptAsync(visitor, CancellationToken.None);

        public async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
        {
            var helper = new FlatParseTreeHelper(_text, _marks);
            await helper.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
        }

        public Task<TResult> AcceptAsync<TResult>(ParseTreeVisitor<TResult> visitor)
            => AcceptAsync(visitor, CancellationToken.None);

        public async Task<TResult> AcceptAsync<TResult>(ParseTreeVisitor<TResult> visitor, CancellationToken cancellationToken)
        {
            var helper = new FlatParseTreeHelper(_text, _marks);
            await helper.AcceptAsync(visitor, cancellationToken).ConfigureAwait(false);
            var result = visitor.Result;
            return result;
        }

        private sealed class FlatParseTreeHelper
        {
            private readonly string _text;
            private readonly IEnumerable<ElementMark> _marks;

            private StringBuilder _imageSourceBuilder;
            private StringBuilder _imageAlternativeTextBuilder;
            private StringBuilder _pluginTextBuilder;
            private StringBuilder _preformattedTextBuilder;
            private StringBuilder _preformattedBlockBuilder;

            internal FlatParseTreeHelper(string text, IEnumerable<ElementMark> marks)
            {
                _text = text;
                _marks = marks;

                _imageSourceBuilder = null;
                _imageAlternativeTextBuilder = null;
                _pluginTextBuilder = null;
                _preformattedTextBuilder = null;
                _preformattedBlockBuilder = null;
            }

            internal async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            {
                if (visitor == null)
                    throw new ArgumentNullException(nameof(visitor));

                await visitor.BeginVisitAsync(cancellationToken).ConfigureAwait(false);

                foreach (var mark in _marks)
                    await _VisitAsync(visitor, mark, cancellationToken).ConfigureAwait(false);

                await visitor.EndVisitAsync(cancellationToken).ConfigureAwait(false);
            }

            private async Task _VisitAsync(ParseTreeVisitor visitor, ElementMark mark, CancellationToken cancellationToken)
            {
                switch (mark.Code)
                {
                    case HorizontalRule:
                        await visitor.VisitHorizontalRuleAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case HyperlinkStart:
                        break;

                    case HyperlinkDestination:
                        var destination = _text.Substring(mark.Start, mark.Length);
                        await visitor.VisitHyperlinkBeginningAsync(destination, cancellationToken).ConfigureAwait(false);
                        break;

                    case HyperlinkEnd:
                        await visitor.VisitHyperlinkEndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case StrongStart:
                        await visitor.VisitStrongBeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case StrongEnd:
                        await visitor.VisitStrongEndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case EmphasisStart:
                        await visitor.VisitEmphasisBeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case EmphasisEnd:
                        await visitor.VisitEmphasisEndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case ImageStart:
                        _imageSourceBuilder = new StringBuilder();
                        _imageAlternativeTextBuilder = new StringBuilder();
                        break;

                    case ImageSource:
                        _imageSourceBuilder.Append(_text, mark.Start, mark.Length);
                        break;

                    case PlainText when (_imageAlternativeTextBuilder != null):
                        _imageAlternativeTextBuilder.Append(_text, mark.Start, mark.Length);
                        break;

                    case ImageEnd:
                        await visitor.VisitImageAsync(_imageSourceBuilder.ToString(), _imageAlternativeTextBuilder.ToString(), cancellationToken).ConfigureAwait(false);
                        _imageSourceBuilder = null;
                        _imageAlternativeTextBuilder = null;
                        break;

                    case LineBreak:
                        await visitor.VisitLineBreakAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case PreformattedTextStart:
                        _preformattedTextBuilder = new StringBuilder();
                        break;

                    case PlainText when (_preformattedTextBuilder != null):
                        _preformattedTextBuilder.Append(_text, mark.Start, mark.Length);
                        break;

                    case PreformattedTextEnd:
                        var preformattedText = _preformattedTextBuilder.ToString();
                        _preformattedTextBuilder = null;
                        await visitor.VisitPreformattedTextAsync(preformattedText, cancellationToken).ConfigureAwait(false);
                        break;

                    case Heading1Start:
                        await visitor.VisitHeading1BeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case Heading1End:
                        await visitor.VisitHeading1EndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case Heading2Start:
                        await visitor.VisitHeading2BeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case Heading2End:
                        await visitor.VisitHeading2EndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case Heading3Start:
                        await visitor.VisitHeading3BeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case Heading3End:
                        await visitor.VisitHeading3EndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case Heading4Start:
                        await visitor.VisitHeading4BeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case Heading4End:
                        await visitor.VisitHeading4EndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case Heading5Start:
                        await visitor.VisitHeading5BeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case Heading5End:
                        await visitor.VisitHeading5EndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case Heading6Start:
                        await visitor.VisitHeading6BeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case Heading6End:
                        await visitor.VisitHeading6EndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case ParagraphStart:
                        await visitor.VisitParagraphBeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case ParagraphEnd:
                        await visitor.VisitParagraphEndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case PreformattedBlockStart:
                        _preformattedBlockBuilder = new StringBuilder();
                        break;

                    case PlainText when (_preformattedBlockBuilder != null):
                        _preformattedBlockBuilder.Append(_text, mark.Start, mark.Length);
                        break;

                    case PreformattedBlockEnd:
                        var preformattedBlock = _preformattedBlockBuilder.ToString();
                        _preformattedBlockBuilder = null;
                        await visitor.VisitPreformattedBlockAsync(preformattedBlock, cancellationToken).ConfigureAwait(false);
                        break;

                    case TableStart:
                        await visitor.VisitTableBeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case TableRowStart:
                        await visitor.VisitTableRowBeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case TableHeaderCellStart:
                        await visitor.VisitTableHeaderCellBeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case TableHeaderCellEnd:
                        await visitor.VisitTableHeaderCellEndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case TableCellStart:
                        await visitor.VisitTableCellBeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case TableCellEnd:
                        await visitor.VisitTableCellEndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case TableRowEnd:
                        await visitor.VisitTableRowEndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case TableEnd:
                        await visitor.VisitTableEndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case UnorderedListStart:
                        await visitor.VisitUnorderedListBeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case UnorderedListEnd:
                        await visitor.VisitUnorderedListEndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case OrderedListStart:
                        await visitor.VisitOrderedListBeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case OrderedListEnd:
                        await visitor.VisitOrderedListEndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case ListItemStart:
                        await visitor.VisitListItemBeginningAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case ListItemEnd:
                        await visitor.VisitListItemEndingAsync(cancellationToken).ConfigureAwait(false);
                        break;

                    case PluginStart:
                        _pluginTextBuilder = new StringBuilder();
                        break;

                    case PlainText when (_pluginTextBuilder != null):
                        _pluginTextBuilder.Append(_text, mark.Start, mark.Length);
                        break;

                    case PluginEnd:
                        await visitor.VisitPlugInAsync(_pluginTextBuilder.ToString(), cancellationToken).ConfigureAwait(false);
                        break;

                    case PlainText:
                        var plainText = _text.Substring(mark.Start, mark.Length);
                        await visitor.VisitTextAsync(plainText, cancellationToken).ConfigureAwait(false);
                        break;
                }
            }
        }
    }
}