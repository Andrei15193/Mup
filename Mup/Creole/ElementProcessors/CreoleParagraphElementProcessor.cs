using Mup.Creole.Elements;
using Mup.Scanner;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors
{
    internal class CreoleParagraphElementProcessor : CreoleElementProcessor
    {
        private enum State : byte
        {
            NotInParagraph,
            InParagraph,
            ParagraphMayEndInWhiteSpace
        }

        private State _state = State.NotInParagraph;

        internal CreoleParagraphElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens)
            : base(context, tokens)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NotInParagraph when (Token.Code != WhiteSpace && Token.Code != NewLine && Token.Code != BlankLine):
                    SetElementStartIndex();
                    _state = State.InParagraph;
                    break;

                case State.InParagraph:
                    if (Token.Code == WhiteSpace || Token.Code == NewLine)
                    {
                        SetElementEndIndex();
                        _state = State.ParagraphMayEndInWhiteSpace;
                    }
                    else if (Token.Code == BlankLine)
                    {
                        SetElementEndIndex();
                        SetResult(_GetParagraphElement());
                        _state = State.NotInParagraph;
                    }
                    break;

                case State.ParagraphMayEndInWhiteSpace:
                    _state = State.InParagraph;
                    break;
            }
        }

        protected override void Complete()
        {
            if (_state == State.InParagraph)
            {
                SetElementEndIndex();
                SetResult(_GetParagraphElement());
            }
            else if (_state == State.ParagraphMayEndInWhiteSpace)
                SetResult(_GetParagraphElement());
        }

        private CreoleElement _GetParagraphElement()
            => new CreoleParagraphElement(GetRichText());
    }
}