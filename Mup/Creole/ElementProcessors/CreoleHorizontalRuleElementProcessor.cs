using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors
{
    internal class CreoleHorizontalRuleElementProcessor : CreoleElementProcessor
    {
        private enum State : byte
        {
            NotInHorizontalRule,
            InHorizontalRule
        }

        private static readonly CreoleElement _creoleHorizontalRuleElement = new CreoleHorizontalRuleElement();

        private int dashCount = 0;
        private State _state = State.NotInHorizontalRule;

        internal CreoleHorizontalRuleElementProcessor(CreoleParserContext context, CreoleTokenRange tokens)
            : base(context, tokens)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NotInHorizontalRule when (IsOnNewLine):
                    if (Token.Code == Dash)
                    {
                        SetElementStartIndex();
                        _state = State.InHorizontalRule;
                        dashCount = 1;
                    }
                    break;

                case State.InHorizontalRule:
                    if (Token.Code == Dash)
                        dashCount++;
                    else if (Token.Code == NewLine || Token.Code == BlankLine)
                        if (dashCount >= 4)
                        {
                            SetElementEndIndex();
                            SetResult(_creoleHorizontalRuleElement);
                        }
                        else
                            _state = State.NotInHorizontalRule;
                    else if (Token.Code != WhiteSpace)
                        _state = State.NotInHorizontalRule;
                    break;
            }
        }

        protected override void Complete()
        {
            if (_state == State.InHorizontalRule && dashCount >= 4)
            {
                SetElementEndIndex();
                SetResult(_creoleHorizontalRuleElement);
            }
        }
    }
}