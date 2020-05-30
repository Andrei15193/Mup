using Mup.Creole.Elements;
using Mup.Scanner;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors
{
    internal class CreoleHeadingElementProcessor : CreoleElementProcessor
    {
        private enum State : byte
        {
            NotInHeading,
            InHeadingBeginningMark,
            InHeadingWhiteSpaceBeginning,
            InHeadingContent,
            InHeadingWhiteSpaceEnding,
            InHeadingEndingMark
        }

        private int _contentStartIndex = 0;
        private int _contentEndIndex = 0;
        private State _state = State.NotInHeading;
        private byte _equalTokensCount = 0;

        internal CreoleHeadingElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens)
            : base(context, tokens)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NotInHeading when (IsOnNewLine):
                    if (Token.Code == Equal)
                    {
                        _state = State.InHeadingBeginningMark;
                        SetElementStartIndex();
                        _equalTokensCount = 1;
                    }
                    break;

                case State.InHeadingBeginningMark:
                    if (Token.Code == Equal)
                        _equalTokensCount++;
                    if (Token.Code == NewLine || Token.Code == BlankLine)
                    {
                        SetElementEndIndex();
                        SetResult(_GetHeadingElementFor());
                        _state = State.NotInHeading;
                    }
                    else if (Token.Code == WhiteSpace)
                    {
                        _state = State.InHeadingWhiteSpaceBeginning;
                    }
                    else if (Token.Code != Equal || _equalTokensCount > 6)
                    {
                        _state = State.InHeadingContent;
                        _contentStartIndex = Index;
                    }
                    break;

                case State.InHeadingWhiteSpaceBeginning:
                    if (Token.Code == NewLine || Token.Code == BlankLine)
                    {
                        SetElementEndIndex();
                        SetResult(_GetHeadingElementFor());
                        _state = State.NotInHeading;
                    }
                    else
                    {
                        _state = State.InHeadingContent;
                        _contentStartIndex = Index;
                    }
                    break;

                case State.InHeadingContent:
                    if (Token.Code == NewLine || Token.Code == BlankLine)
                    {
                        _contentEndIndex = Index;
                        SetElementEndIndex();
                        SetResult(_GetHeadingElementFor(GetPlainText(_contentStartIndex, _contentEndIndex)));
                        _state = State.NotInHeading;
                    }
                    else if (Token.Code == Equal)
                    {
                        _contentEndIndex = Index;
                        _state = State.InHeadingEndingMark;
                    }
                    else if (Token.Code == WhiteSpace)
                    {
                        _contentEndIndex = Index;
                        _state = State.InHeadingWhiteSpaceEnding;
                    }
                    break;

                case State.InHeadingWhiteSpaceEnding:
                    if (Token.Code == Equal)
                        _state = State.InHeadingEndingMark;
                    else
                        _state = State.InHeadingContent;
                    break;

                case State.InHeadingEndingMark:
                    if (Token.Code == NewLine || Token.Code == BlankLine)
                    {
                        SetElementEndIndex();
                        SetResult(_GetHeadingElementFor(GetPlainText(_contentStartIndex, _contentEndIndex)));
                        _state = State.NotInHeading;
                    }
                    else if (Token.Code != Equal)
                        _state = State.InHeadingContent;
                    break;
            }
        }

        protected override void Complete()
        {
            switch (_state)
            {
                case State.InHeadingBeginningMark:
                case State.InHeadingWhiteSpaceBeginning:
                    SetElementEndIndex();
                    SetResult(_GetHeadingElementFor());
                    break;

                case State.InHeadingContent:
                    _contentEndIndex = Index;
                    SetElementEndIndex();
                    SetResult(_GetHeadingElementFor(GetPlainText(_contentStartIndex, _contentEndIndex)));
                    break;

                case State.InHeadingWhiteSpaceEnding:
                case State.InHeadingEndingMark:
                    SetElementEndIndex();
                    SetResult(_GetHeadingElementFor(GetPlainText(_contentStartIndex, _contentEndIndex)));
                    break;
            }
        }

        private CreoleElement _GetHeadingElementFor()
            => _GetHeadingElementFor(string.Empty);

        private CreoleElement _GetHeadingElementFor(string text)
        {
            switch (_equalTokensCount)
            {
                case 1:
                    return new CreoleHeading1Element(text);
                case 2:
                    return new CreoleHeading2Element(text);
                case 3:
                    return new CreoleHeading3Element(text);
                case 4:
                    return new CreoleHeading4Element(text);
                case 5:
                    return new CreoleHeading5Element(text);
                default:
                    return new CreoleHeading6Element(text);
            }
        }
    }
}