using System.Collections.Generic;
using System.Linq;
using static Mup.ElementMarkCode;

namespace Mup.Tests
{
    internal class ElementMarkVisitor : ParseResultVisitor
    {
        private readonly ICollection<ElementMarkCode> _marks = new List<ElementMarkCode>();

        internal IEnumerable<ElementMarkCode> Marks { get; private set; }

        protected override void Reset()
        {
            _marks.Clear();
        }

        protected override void Complete()
        {
            Marks = _marks.ToArray();
        }

        protected override void BeginHeading1()
            => _marks.Add(Heading1Start);

        protected override void EndHeading1()
            => _marks.Add(Heading1End);

        protected override void BeginHeading2()
            => _marks.Add(Heading2Start);

        protected override void EndHeading2()
            => _marks.Add(Heading2End);

        protected override void BeginHeading3()
            => _marks.Add(Heading3Start);

        protected override void EndHeading3()
            => _marks.Add(Heading3End);

        protected override void BeginHeading4()
            => _marks.Add(Heading4Start);

        protected override void EndHeading4()
            => _marks.Add(Heading4End);

        protected override void BeginHeading5()
            => _marks.Add(Heading5Start);

        protected override void EndHeading5()
            => _marks.Add(Heading5End);

        protected override void BeginHeading6()
            => _marks.Add(Heading6Start);

        protected override void EndHeading6()
            => _marks.Add(Heading6End);

        protected override void BeginParagraph()
            => _marks.Add(ParagraphStart);

        protected override void EndParagraph()
            => _marks.Add(ParagraphEnd);

        protected override void BeginStrong()
            => _marks.Add(StrongStart);

        protected override void EndStrong()
            => _marks.Add(StrongEnd);

        protected override void BeginEmphasis()
            => _marks.Add(EmphasisStart);

        protected override void EndEmphasis()
            => _marks.Add(EmphasisEnd);

        protected override void BeginHyperlink(string destination)
        {
            _marks.Add(HyperlinkStart);
            _marks.Add(HyperlinkDestination);
        }

        protected override void EndHyperlink()
            => _marks.Add(HyperlinkEnd);

        protected override void Image(string source, string alternative)
        {
            _marks.Add(ImageStart);
            _marks.Add(ImageSource);
            _marks.Add(PlainText);
            _marks.Add(ImageEnd);
        }

        protected override void Text(string text)
            => _marks.Add(PlainText);
    }
}