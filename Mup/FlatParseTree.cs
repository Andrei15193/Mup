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
            await helper.AcceptAsync(visitor, cancellationToken);
        }

        public Task<TResult> AcceptAsync<TResult>(ParseTreeVisitor<TResult> visitor)
            => AcceptAsync(visitor, CancellationToken.None);

        public async Task<TResult> AcceptAsync<TResult>(ParseTreeVisitor<TResult> visitor, CancellationToken cancellationToken)
        {
            var helper = new FlatParseTreeHelper(_text, _marks);
            await helper.AcceptAsync(visitor, cancellationToken);
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

            internal FlatParseTreeHelper(string text, IEnumerable<ElementMark> marks)
            {
                _text = text;
                _marks = marks;

                _imageSourceBuilder = null;
                _imageAlternativeTextBuilder = null;
                _pluginTextBuilder = null;
            }

            internal async Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            {
                if (visitor == null)
                    throw new ArgumentNullException(nameof(visitor));

                await visitor.BeginVisitAsync(cancellationToken);

                foreach (var mark in _marks)
                    await _VisitAsync(visitor, mark, cancellationToken);

                await visitor.EndVisitAsync(cancellationToken);
            }

            private async Task _VisitAsync(ParseTreeVisitor visitor, ElementMark mark, CancellationToken cancellationToken)
            {
                switch (mark.Code)
                {
                    case HorizontalRule:
                        await visitor.VisitHorizontalRuleAsync(cancellationToken);
                        break;

                    case HyperlinkStart:
                        break;

                    case HyperlinkDestination:
                        var destination = _text.Substring(mark.Start, mark.Length);
                        await visitor.VisitHyperlinkBeginningAsync(destination, cancellationToken);
                        break;

                    case HyperlinkEnd:
                        await visitor.VisitHyperlinkEndingAsync(cancellationToken);
                        break;

                    case StrongStart:
                        await visitor.VisitStrongBeginningAsync(cancellationToken);
                        break;

                    case StrongEnd:
                        await visitor.VisitStrongEndingAsync(cancellationToken);
                        break;

                    case EmphasisStart:
                        await visitor.VisitEmphasisBeginningAsync(cancellationToken);
                        break;

                    case EmphasisEnd:
                        await visitor.VisitEmphasisEndingAsync(cancellationToken);
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
                        await visitor.VisitImageAsync(_imageSourceBuilder.ToString(), _imageAlternativeTextBuilder.ToString(), cancellationToken);
                        _imageSourceBuilder = null;
                        _imageAlternativeTextBuilder = null;
                        break;

                    case LineBreak:
                        await visitor.VisitLineBreakAsync(cancellationToken);
                        break;

                    case PreformattedStart:
                        await visitor.VisitPreformattedTextBeginningAsync(cancellationToken);
                        break;

                    case PreformattedEnd:
                        await visitor.VisitPreformattedTextEndingAsync(cancellationToken);
                        break;

                    case Heading1Start:
                        await visitor.VisitHeading1BeginningAsync(cancellationToken);
                        break;

                    case Heading1End:
                        await visitor.VisitHeading1EndingAsync(cancellationToken);
                        break;

                    case Heading2Start:
                        await visitor.VisitHeading2BeginningAsync(cancellationToken);
                        break;

                    case Heading2End:
                        await visitor.VisitHeading2EndingAsync(cancellationToken);
                        break;

                    case Heading3Start:
                        await visitor.VisitHeading3BeginningAsync(cancellationToken);
                        break;

                    case Heading3End:
                        await visitor.VisitHeading3EndingAsync(cancellationToken);
                        break;

                    case Heading4Start:
                        await visitor.VisitHeading4BeginningAsync(cancellationToken);
                        break;

                    case Heading4End:
                        await visitor.VisitHeading4EndingAsync(cancellationToken);
                        break;

                    case Heading5Start:
                        await visitor.VisitHeading5BeginningAsync(cancellationToken);
                        break;

                    case Heading5End:
                        await visitor.VisitHeading5EndingAsync(cancellationToken);
                        break;

                    case Heading6Start:
                        await visitor.VisitHeading6BeginningAsync(cancellationToken);
                        break;

                    case Heading6End:
                        await visitor.VisitHeading6EndingAsync(cancellationToken);
                        break;

                    case ParagraphStart:
                        await visitor.VisitParagraphBeginningAsync(cancellationToken);
                        break;

                    case ParagraphEnd:
                        await visitor.VisitParagraphEndingAsync(cancellationToken);
                        break;

                    case PreformattedBlockStart:
                        await visitor.VisitPreformattedBlockBeginningAsync(cancellationToken);
                        break;

                    case PreformattedBlockEnd:
                        await visitor.VisitPreformattedBlockEndingAsync(cancellationToken);
                        break;

                    case TableStart:
                        await visitor.VisitTableBeginningAsync(cancellationToken);
                        break;

                    case TableRowStart:
                        await visitor.VisitTableRowBeginningAsync(cancellationToken);
                        break;

                    case TableHeaderCellStart:
                        await visitor.VisitTableHeaderCellBeginningAsync(cancellationToken);
                        break;

                    case TableHeaderCellEnd:
                        await visitor.VisitTableHeaderCellEndingAsync(cancellationToken);
                        break;

                    case TableCellStart:
                        await visitor.VisitTableCellBeginningAsync(cancellationToken);
                        break;

                    case TableCellEnd:
                        await visitor.VisitTableCellEndingAsync(cancellationToken);
                        break;

                    case TableRowEnd:
                        await visitor.VisitTableRowEndingAsync(cancellationToken);
                        break;

                    case TableEnd:
                        await visitor.VisitTableEndingAsync(cancellationToken);
                        break;

                    case UnorderedListStart:
                        await visitor.VisitUnorderedListBeginningAsync(cancellationToken);
                        break;

                    case UnorderedListEnd:
                        await visitor.VisitUnorderedListEndingAsync(cancellationToken);
                        break;

                    case OrderedListStart:
                        await visitor.VisitOrderedListBeginningAsync(cancellationToken);
                        break;

                    case OrderedListEnd:
                        await visitor.VisitOrderedListEndingAsync(cancellationToken);
                        break;

                    case ListItemStart:
                        await visitor.VisitListItemBeginningAsync(cancellationToken);
                        break;

                    case ListItemEnd:
                        await visitor.VisitListItemEndingAsync(cancellationToken);
                        break;

                    case PluginStart:
                        _pluginTextBuilder = new StringBuilder();
                        break;

                    case PlainText when (_pluginTextBuilder != null):
                        _pluginTextBuilder.Append(_text, mark.Start, mark.Length);
                        break;

                    case PluginEnd:
                        await visitor.VisitPlugInAsync(_pluginTextBuilder.ToString(), cancellationToken);
                        break;

                    case PlainText:
                        var plainText = _text.Substring(mark.Start, mark.Length);
                        await visitor.VisitTextAsync(plainText, cancellationToken);
                        break;
                }
            }
        }
    }
}