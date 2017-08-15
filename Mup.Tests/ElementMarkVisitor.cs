using System.Collections.Generic;
using static Mup.ElementMarkCode;

namespace Mup.Tests
{
    internal class ElementMarkVisitor
        : ParseResultVisitor<IEnumerable<ElementMarkCode>>
    {
        private List<ElementMarkCode> _marks = null;

        protected override void BeginVisit()
        {
            _marks = new List<ElementMarkCode>();
        }

        protected override IEnumerable<ElementMarkCode> Result => _marks;

        protected override void VisitHeading1Beginning()
            => _marks.Add(Heading1Start);

        protected override void VisitHeading1Ending()
            => _marks.Add(Heading1End);

        protected override void VisitHeading2Beginning()
            => _marks.Add(Heading2Start);

        protected override void VisitHeading2Ending()
            => _marks.Add(Heading2End);

        protected override void VisitHeading3Beginning()
            => _marks.Add(Heading3Start);

        protected override void VisitHeading3Ending()
            => _marks.Add(Heading3End);

        protected override void VisitHeading4Beginning()
            => _marks.Add(Heading4Start);

        protected override void VisitHeading4Ending()
            => _marks.Add(Heading4End);

        protected override void VisitHeading5Beginning()
            => _marks.Add(Heading5Start);

        protected override void VisitHeading5Ending()
            => _marks.Add(Heading5End);

        protected override void VisitHeading6Beginning()
            => _marks.Add(Heading6Start);

        protected override void VisitHeading6Ending()
            => _marks.Add(Heading6End);

        protected override void VisitParagraphBeginning()
            => _marks.Add(ParagraphStart);

        protected override void VisitParagraphEnding()
            => _marks.Add(ParagraphEnd);

        protected override void VisitPreformattedBlockBeginning()
            => _marks.Add(PreformattedBlockStart);

        protected override void VisitPreformattedBlockEnding()
            => _marks.Add(PreformattedBlockEnd);

        protected override void VisitHorizontalRule()
            => _marks.Add(HorizontalRule);

        protected override void VisitPlugIn(string value)
        {
            _marks.Add(PluginStart);
            _marks.Add(PlainText);
            _marks.Add(PluginEnd);
        }

        protected override void VisitTableBeginning()
            => _marks.Add(TableStart);

        protected override void VisitTableEnding()
            => _marks.Add(TableEnd);

        protected override void VisitTableRowBeginning()
            => _marks.Add(TableRowStart);

        protected override void VisitTableRowEnding()
            => _marks.Add(TableRowEnd);

        protected override void VisitTableHeaderCellBeginning()
            => _marks.Add(TableHeaderCellStart);

        protected override void VisitTableHeaderCellEnding()
            => _marks.Add(TableHeaderCellEnd);

        protected override void VisitTableCellBeginning()
            => _marks.Add(TableCellStart);

        protected override void VisitTableCellEnding()
            => _marks.Add(TableCellEnd);

        protected override void VisitUnorderedListBeginning()
            => _marks.Add(UnorderedListStart);

        protected override void VisitUnorderedListEnding()
            => _marks.Add(UnorderedListEnd);

        protected override void VisitOrderedListBeginning()
            => _marks.Add(OrderedListStart);

        protected override void VisitOrderedListEnding()
            => _marks.Add(OrderedListEnd);

        protected override void VisitListItemBeginning()
            => _marks.Add(ListItemStart);

        protected override void VisitListItemEnding()
            => _marks.Add(ListItemEnd);

        protected override void VisitStrongBeginning()
            => _marks.Add(StrongStart);

        protected override void VisitStrongEnding()
            => _marks.Add(StrongEnd);

        protected override void VisitEmphasisBeginning()
            => _marks.Add(EmphasisStart);

        protected override void VisitEmphasisEnding()
            => _marks.Add(EmphasisEnd);

        protected override void VisitHyperlinkBeginning(string destination)
        {
            _marks.Add(HyperlinkStart);
            _marks.Add(HyperlinkDestination);
        }

        protected override void VisitHyperlinkEnding()
            => _marks.Add(HyperlinkEnd);

        protected override void VisitImage(string source, string alternative)
        {
            _marks.Add(ImageStart);
            _marks.Add(ImageSource);
            _marks.Add(PlainText);
            _marks.Add(ImageEnd);
        }

        protected override void VisitLineBreak()
            => _marks.Add(LineBreak);

        protected override void VisitPreformattedTextBeginning()
            => _marks.Add(PreformattedStart);

        protected override void VisitPreformattedTextEnding()
            => _marks.Add(PreformattedEnd);

        protected override void VisitText(string text)
            => _marks.Add(PlainText);
    }
}