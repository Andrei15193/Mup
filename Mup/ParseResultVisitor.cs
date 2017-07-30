using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Mup.ElementMarkCode;

namespace Mup
{
    public abstract class ParseResultVisitor
    {
        private static readonly Task _completedTask = Task.FromResult<object>(null);

        private StringBuilder _imageSourceBuilder = null;
        private StringBuilder _imageAlternativeTextBuilder = null;
        private StringBuilder _pluginTextBuilder = null;

        protected ParseResultVisitor()
        {
        }

        internal Task VisitAsync(string text, IEnumerable<ElementMark> marks)
            => VisitAsync(text, marks, CancellationToken.None);

        internal async Task VisitAsync(string text, IEnumerable<ElementMark> marks, CancellationToken cancellationToken)
        {
            await ResetAsync();

            foreach (var mark in marks)
                await _VisitAsync(text, mark, cancellationToken);

            await CompleteAsync();
        }

        protected virtual Task ResetAsync()
        {
            Reset();
            return _completedTask;
        }

        protected virtual Task CompleteAsync()
        {
            Complete();
            return _completedTask;
        }

        protected virtual void Reset()
        {
        }

        protected virtual void Complete()
        {
        }

        private async Task _VisitAsync(string text, ElementMark mark, CancellationToken cancellationToken)
        {
            var visitTask = _completedTask;

            switch (mark.Code)
            {
                case ElementMarkCode.HorizontalRule:
                    visitTask = HorizontalRuleAsync(cancellationToken);
                    break;

                case HyperlinkStart:
                    break;
                case HyperlinkDestination:
                    var destination = text.Substring(mark.Start, mark.Length);
                    visitTask = BeginHyperlinkAsync(destination, cancellationToken);
                    break;
                case HyperlinkEnd:
                    visitTask = EndHyperlinkAsync(cancellationToken);
                    break;
                case StrongStart:
                    visitTask = BeginStrongAsync(cancellationToken);
                    break;

                case StrongEnd:
                    visitTask = EndStrongAsync(cancellationToken);
                    break;

                case EmphasisStart:
                    visitTask = BeginEmphasisAsync(cancellationToken);
                    break;

                case EmphasisEnd:
                    visitTask = EndEmphasisAsync(cancellationToken);
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
                    visitTask = ImageAsync(_imageSourceBuilder.ToString(), _imageAlternativeTextBuilder.ToString(), cancellationToken);
                    _imageSourceBuilder = null;
                    _imageAlternativeTextBuilder = null;
                    break;

                case ElementMarkCode.LineBreak:
                    visitTask = LineBreakAsync(cancellationToken);
                    break;

                case PreformattedStart:
                    visitTask = BeginPreformattedAsync(cancellationToken);
                    break;
                case PreformattedEnd:
                    visitTask = EndPreformattedAsync(cancellationToken);
                    break;

                case Heading1Start:
                    visitTask = BeginHeading1Async(cancellationToken);
                    break;

                case Heading1End:
                    visitTask = EndHeading1Async(cancellationToken);
                    break;

                case Heading2Start:
                    visitTask = BeginHeading2Async(cancellationToken);
                    break;

                case Heading2End:
                    visitTask = EndHeading2Async(cancellationToken);
                    break;

                case Heading3Start:
                    visitTask = BeginHeading3Async(cancellationToken);
                    break;

                case Heading3End:
                    visitTask = EndHeading3Async(cancellationToken);
                    break;

                case Heading4Start:
                    visitTask = BeginHeading4Async(cancellationToken);
                    break;

                case Heading4End:
                    visitTask = EndHeading4Async(cancellationToken);
                    break;

                case Heading5Start:
                    visitTask = BeginHeading5Async(cancellationToken);
                    break;

                case Heading5End:
                    visitTask = EndHeading5Async(cancellationToken);
                    break;

                case Heading6Start:
                    visitTask = BeginHeading6Async(cancellationToken);
                    break;

                case Heading6End:
                    visitTask = EndHeading6Async(cancellationToken);
                    break;

                case ParagraphStart:
                    visitTask = BeginParagraphAsync(cancellationToken);
                    break;

                case ParagraphEnd:
                    visitTask = EndParagraphAsync(cancellationToken);
                    break;

                case PreformattedBlockStart:
                    visitTask = BeginPreformattedBlockAsync(cancellationToken);
                    break;

                case PreformattedBlockEnd:
                    visitTask = EndPreformattedBlockAsync(cancellationToken);
                    break;

                case ElementMarkCode.TableStart:
                    break;
                case ElementMarkCode.TableRowStart:
                    break;
                case ElementMarkCode.TableHeaderCellStart:
                    break;
                case ElementMarkCode.TableHeaderCellEnd:
                    break;
                case ElementMarkCode.TableCellStart:
                    break;
                case ElementMarkCode.TableCellEnd:
                    break;
                case ElementMarkCode.TableCellSeparator:
                    break;
                case ElementMarkCode.TableRowEnd:
                    break;
                case ElementMarkCode.TableEnd:
                    break;
                case ElementMarkCode.BulletListStart:
                    break;
                case ElementMarkCode.BulletListItemStart:
                    break;
                case ElementMarkCode.BulletListItemEnd:
                    break;
                case ElementMarkCode.BulletListEnd:
                    break;
                case ElementMarkCode.OrderedListStart:
                    break;
                case ElementMarkCode.OrderedListItemStart:
                    break;
                case ElementMarkCode.OrderedListItemEnd:
                    break;
                case ElementMarkCode.OrderedListEnd:
                    break;

                case PluginStart:
                    _pluginTextBuilder = new StringBuilder();
                    break;
                case PlainText when (_pluginTextBuilder != null):
                    _pluginTextBuilder.Append(text, mark.Start, mark.Length);
                    break;
                case PluginEnd:
                    visitTask = PlugInAsync(_pluginTextBuilder.ToString(), cancellationToken);
                    _pluginTextBuilder = null;
                    break;

                case PlainText:
                    var plainText = text.Substring(mark.Start, mark.Length);
                    visitTask = TextAsync(plainText, cancellationToken);
                    break;

                default:
                    break;
            }

            if (visitTask != _completedTask)
                await visitTask;
        }

        protected virtual Task BeginHeading1Async(CancellationToken cancellationToken)
        {
            BeginHeading1();
            return _completedTask;
        }

        protected virtual void BeginHeading1()
        {
        }

        protected virtual Task EndHeading1Async(CancellationToken cancellationToken)
        {
            EndHeading1();
            return _completedTask;
        }

        protected virtual void EndHeading1()
        {
        }

        protected virtual Task BeginHeading2Async(CancellationToken cancellationToken)
        {
            BeginHeading2();
            return _completedTask;
        }

        protected virtual void BeginHeading2()
        {
        }

        protected virtual Task EndHeading2Async(CancellationToken cancellationToken)
        {
            EndHeading2();
            return _completedTask;
        }

        protected virtual void EndHeading2()
        {
        }

        protected virtual Task BeginHeading3Async(CancellationToken cancellationToken)
        {
            BeginHeading3();
            return _completedTask;
        }

        protected virtual void BeginHeading3()
        {
        }

        protected virtual Task EndHeading3Async(CancellationToken cancellationToken)
        {
            EndHeading3();
            return _completedTask;
        }

        protected virtual void EndHeading3()
        {
        }

        protected virtual Task BeginHeading4Async(CancellationToken cancellationToken)
        {
            BeginHeading4();
            return _completedTask;
        }

        protected virtual void BeginHeading4()
        {
        }

        protected virtual Task EndHeading4Async(CancellationToken cancellationToken)
        {
            EndHeading4();
            return _completedTask;
        }

        protected virtual void EndHeading4()
        {
        }

        protected virtual Task BeginHeading5Async(CancellationToken cancellationToken)
        {
            BeginHeading5();
            return _completedTask;
        }

        protected virtual void BeginHeading5()
        {
        }

        protected virtual Task EndHeading5Async(CancellationToken cancellationToken)
        {
            EndHeading5();
            return _completedTask;
        }

        protected virtual void EndHeading5()
        {
        }

        protected virtual Task BeginHeading6Async(CancellationToken cancellationToken)
        {
            BeginHeading6();
            return _completedTask;
        }

        protected virtual void BeginHeading6()
        {
        }

        protected virtual Task EndHeading6Async(CancellationToken cancellationToken)
        {
            EndHeading6();
            return _completedTask;
        }

        protected virtual void EndHeading6()
        {
        }

        protected virtual Task BeginParagraphAsync(CancellationToken cancellationToken)
        {
            BeginParagraph();
            return _completedTask;
        }

        protected virtual void BeginParagraph()
        {
        }

        protected virtual Task EndParagraphAsync(CancellationToken cancellationToken)
        {
            EndParagraph();
            return _completedTask;
        }

        protected virtual void EndParagraph()
        {
        }

        protected virtual Task BeginPreformattedBlockAsync(CancellationToken cancellationToken)
        {
            BeginPreformattedBlock();
            return _completedTask;
        }

        protected virtual void BeginPreformattedBlock()
        {
        }

        protected virtual Task EndPreformattedBlockAsync(CancellationToken cancellationToken)
        {
            EndPreformattedBlock();
            return _completedTask;
        }

        protected virtual void EndPreformattedBlock()
        {
        }

        protected virtual Task HorizontalRuleAsync(CancellationToken cancellationToken)
        {
            HorizontalRule();
            return _completedTask;
        }

        protected virtual void HorizontalRule()
        {
        }

        protected virtual Task PlugInAsync(string value, CancellationToken cancellationToken)
        {
            PlugIn(value);
            return _completedTask;
        }

        protected virtual void PlugIn(string value)
        {
        }

        protected virtual Task BeginStrongAsync(CancellationToken cancellationToken)
        {
            BeginStrong();
            return _completedTask;
        }

        protected virtual void BeginStrong()
        {
        }

        protected virtual Task EndStrongAsync(CancellationToken cancellationToken)
        {
            EndStrong();
            return _completedTask;
        }

        protected virtual void EndStrong()
        {
        }

        protected virtual Task BeginEmphasisAsync(CancellationToken cancellationToken)
        {
            BeginEmphasis();
            return _completedTask;
        }

        protected virtual void BeginEmphasis()
        {
        }

        protected virtual Task EndEmphasisAsync(CancellationToken cancellationToken)
        {
            EndEmphasis();
            return _completedTask;
        }

        protected virtual void EndEmphasis()
        {
        }

        protected virtual Task BeginHyperlinkAsync(string hyperlinkDestinaton, CancellationToken cancellationToken)
        {
            BeginHyperlink(hyperlinkDestinaton);
            return _completedTask;
        }

        protected virtual void BeginHyperlink(string destination)
        {
        }

        protected virtual Task EndHyperlinkAsync(CancellationToken cancellationToken)
        {
            EndHyperlink();
            return _completedTask;
        }

        protected virtual void EndHyperlink()
        {
        }

        protected virtual Task ImageAsync(string source, string alternative, CancellationToken cancellationToken)
        {
            Image(source, alternative);
            return _completedTask;
        }

        protected virtual void Image(string source, string alternative)
        {
        }

        protected virtual Task LineBreakAsync(CancellationToken cancellationToken)
        {
            LineBreak();
            return _completedTask;
        }

        protected virtual void LineBreak()
        {
        }

        protected virtual Task BeginPreformattedAsync(CancellationToken cancellationToken)
        {
            BeginPreformatted();
            return _completedTask;
        }

        protected virtual void BeginPreformatted()
        {
        }

        protected virtual Task EndPreformattedAsync(CancellationToken cancellationToken)
        {
            EndPreformatted();
            return _completedTask;
        }

        protected virtual void EndPreformatted()
        {
        }

        protected virtual Task TextAsync(string text, CancellationToken cancellationToken)
        {
            Text(text);
            return _completedTask;
        }

        protected virtual void Text(string text)
        {
        }
    }
}