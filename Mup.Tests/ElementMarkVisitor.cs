using System.Collections.Generic;
using static Mup.ElementMarkCode;

namespace Mup.Tests
{
    internal class ElementMarkVisitor
        : ParseTreeVisitor<IEnumerable<ElementMarkCode>>
    {
        private List<ElementMarkCode> _marks = null;

        protected internal override void BeginVisit()
        {
            _marks = new List<ElementMarkCode>();
        }

        protected internal override IEnumerable<ElementMarkCode> GetResult()
            => _marks;

        protected internal override void VisitHeading1Beginning()
            => _marks.Add(Heading1Start);

        protected internal override void VisitHeading1Ending()
            => _marks.Add(Heading1End);

        protected internal override void VisitHeading2Beginning()
            => _marks.Add(Heading2Start);

        protected internal override void VisitHeading2Ending()
            => _marks.Add(Heading2End);

        protected internal override void VisitHeading3Beginning()
            => _marks.Add(Heading3Start);

        protected internal override void VisitHeading3Ending()
            => _marks.Add(Heading3End);

        protected internal override void VisitHeading4Beginning()
            => _marks.Add(Heading4Start);

        protected internal override void VisitHeading4Ending()
            => _marks.Add(Heading4End);

        protected internal override void VisitHeading5Beginning()
            => _marks.Add(Heading5Start);

        protected internal override void VisitHeading5Ending()
            => _marks.Add(Heading5End);

        protected internal override void VisitHeading6Beginning()
            => _marks.Add(Heading6Start);

        protected internal override void VisitHeading6Ending()
            => _marks.Add(Heading6End);

        protected internal override void VisitParagraphBeginning()
            => _marks.Add(ParagraphStart);

        protected internal override void VisitParagraphEnding()
            => _marks.Add(ParagraphEnd);

        protected internal override void VisitPreformattedBlock(string preformattedText)
        {
            _marks.Add(PreformattedBlockStart);
            _marks.Add(PlainText);
            _marks.Add(PreformattedBlockEnd);
        }

        protected internal override void VisitHorizontalRule()
            => _marks.Add(HorizontalRule);

        protected internal override void VisitPlugin(string value)
        {
            _marks.Add(PluginStart);
            _marks.Add(PlainText);
            _marks.Add(PluginEnd);
        }

        protected internal override void VisitTableBeginning()
            => _marks.Add(TableStart);

        protected internal override void VisitTableEnding()
            => _marks.Add(TableEnd);

        protected internal override void VisitTableRowBeginning()
            => _marks.Add(TableRowStart);

        protected internal override void VisitTableRowEnding()
            => _marks.Add(TableRowEnd);

        protected internal override void VisitTableHeaderCellBeginning()
            => _marks.Add(TableHeaderCellStart);

        protected internal override void VisitTableHeaderCellEnding()
            => _marks.Add(TableHeaderCellEnd);

        protected internal override void VisitTableCellBeginning()
            => _marks.Add(TableCellStart);

        protected internal override void VisitTableCellEnding()
            => _marks.Add(TableCellEnd);

        protected internal override void VisitUnorderedListBeginning()
            => _marks.Add(UnorderedListStart);

        protected internal override void VisitUnorderedListEnding()
            => _marks.Add(UnorderedListEnd);

        protected internal override void VisitOrderedListBeginning()
            => _marks.Add(OrderedListStart);

        protected internal override void VisitOrderedListEnding()
            => _marks.Add(OrderedListEnd);

        protected internal override void VisitListItemBeginning()
            => _marks.Add(ListItemStart);

        protected internal override void VisitListItemEnding()
            => _marks.Add(ListItemEnd);

        protected internal override void VisitStrongBeginning()
            => _marks.Add(StrongStart);

        protected internal override void VisitStrongEnding()
            => _marks.Add(StrongEnd);

        protected internal override void VisitEmphasisBeginning()
            => _marks.Add(EmphasisStart);

        protected internal override void VisitEmphasisEnding()
            => _marks.Add(EmphasisEnd);

        protected internal override void VisitHyperlinkBeginning(string destination)
        {
            _marks.Add(HyperlinkStart);
            _marks.Add(HyperlinkDestination);
        }

        protected internal override void VisitHyperlinkEnding()
            => _marks.Add(HyperlinkEnd);

        protected internal override void VisitImage(string source, string alternative)
        {
            _marks.Add(ImageStart);
            _marks.Add(ImageSource);
            _marks.Add(PlainText);
            _marks.Add(ImageEnd);
        }

        protected internal override void VisitLineBreak()
            => _marks.Add(LineBreak);

        protected internal override void VisitPreformattedText(string preformattedText)
        {
            _marks.Add(PreformattedTextStart);
            _marks.Add(PlainText);
            _marks.Add(PreformattedTextEnd);
        }

        protected internal override void VisitText(string text)
            => _marks.Add(PlainText);
    }
}