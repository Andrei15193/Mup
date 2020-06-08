using Mup.Elements;
using Mup.Scanner;
using System.Text;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors
{
    internal class CreoleCodeBlockElementProcessor : CreoleElementProcessor
    {
        private enum State : byte
        {
            LookingForBraceOpen,
            Read1BraceOpen,
            Read2BraceOpens,
            Read3BraceOpens,
            LookingForBraceClose,
            Read1BraceClose,
            Read2BraceCloses,
            Read3BraceCloses,
            CreateCodeBlockElement,
        }

        private State _state = State.LookingForBraceOpen;
        private int _contentStartIndex = 0;
        private int _contentEndIndex = 0;

        private bool _isAtBeginningOfLine = true;

        internal CreoleCodeBlockElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens)
            : base(context, tokens)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.LookingForBraceOpen:
                    if (_isAtBeginningOfLine && Token.Code == BraceOpen)
                    {
                        SetElementStartIndex();
                        _state = State.Read1BraceOpen;
                    }
                    break;

                case State.Read1BraceOpen:
                    _state = (Token.Code == BraceOpen ? State.Read2BraceOpens : State.LookingForBraceOpen);
                    break;

                case State.Read2BraceOpens:
                    _state = (Token.Code == BraceOpen ? State.Read3BraceOpens : State.LookingForBraceOpen);
                    break;

                case State.Read3BraceOpens:
                    if (Token.Code == NewLine || Token.Code == BlankLine)
                    {
                        _state = State.LookingForBraceClose;
                        _contentStartIndex = Index;
                    }
                    else
                        _state = State.LookingForBraceOpen;
                    break;

                case State.LookingForBraceClose:
                    if (_isAtBeginningOfLine && Token.Code == BraceClose)
                    {
                        _state = State.Read1BraceClose;
                        _contentEndIndex = Index;
                    }
                    break;

                case State.Read1BraceClose:
                    _state = (Token.Code == BraceClose ? State.Read2BraceCloses : State.LookingForBraceClose);
                    break;

                case State.Read2BraceCloses:
                    _state = (Token.Code == BraceClose ? State.Read3BraceCloses : State.LookingForBraceClose);
                    break;

                case State.Read3BraceCloses:
                    if (Token.Code == NewLine || Token.Code == BlankLine)
                        _state = State.CreateCodeBlockElement;
                    else
                        _state = State.LookingForBraceClose;
                    break;

                case State.CreateCodeBlockElement:
                    SetElementEndIndex();
                    SetResult(_GetCodeBlockElement());
                    if (_isAtBeginningOfLine && Token.Code == BraceOpen)
                    {
                        SetElementStartIndex();
                        _state = State.Read1BraceOpen;
                    }
                    else
                        _state = State.LookingForBraceOpen;
                    break;
            }
            _isAtBeginningOfLine = _HasNewLineAtEnd();
        }

        protected override void Complete()
        {
            if (_state == State.Read3BraceCloses)
            {
                SetElementEndIndex();
                SetResult(_GetCodeBlockElement());
            }
        }

        private Element _GetCodeBlockElement()
        {
            var codeBlockTokenRange = GetTokens(_contentStartIndex, _contentEndIndex);
            var codeBlockText = _GetCodeBlockTextFrom(codeBlockTokenRange);
            return new PreformattedBlockElement(codeBlockText);
        }

        private string _GetCodeBlockTextFrom(TokenRange<CreoleTokenCode> tokens)
        {
            var builder = new StringBuilder();

            using (var token = tokens.GetEnumerator())
                if (token.MoveNext())
                {
                    var startIndex = (token.Current.Text.IndexOf('\n') + 1);
                    builder.Append(token.Current.Text, startIndex, token.Current.Text.Length - startIndex);

                    if (token.MoveNext())
                    {
                        var previousToken = token.Current;
                        while (token.MoveNext())
                        {
                            builder.Append(previousToken.Text);
                            previousToken = token.Current;
                        }

                        startIndex = previousToken.Text.LastIndexOf('\n');
                        if (startIndex >= 0)
                        {
                            if (startIndex > 0 && previousToken.Text[startIndex - 1] == '\r')
                                startIndex--;
                            builder.Append(previousToken.Text, 0, startIndex);
                        }
                        else
                            builder.Append(previousToken.Text);
                    }
                }

            return builder.ToString();
        }

        private bool _HasNewLineAtEnd()
        {
            if (Token.Code != NewLine && Token.Code != BlankLine)
                return false;

            var tokenText = Token.Text;
            return (tokenText.Length > 0 && tokenText[tokenText.Length - 1] == '\n');
        }
    }
}