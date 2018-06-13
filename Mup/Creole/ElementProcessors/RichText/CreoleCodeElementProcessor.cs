using System.Collections.Generic;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal class CreoleCodeElementProcessor : CreoleRichTextElementProcessor
    {
        private enum State : byte
        {
            NoElement,
            CodeFirstStartMark,
            CodeSecondStartMark,
            CodeContent,
            CodeFirstEndMark,
            CodeSecondEndMark,
            CodeEndMark
        }

        private int _elementStartIndex;
        private int _elementEndIndex;
        private State _state = State.NoElement;

        internal CreoleCodeElementProcessor(CreoleParserContext context, CreoleTokenRange tokens)
            : base(context, tokens)
        {
        }

        internal CreoleCodeElementProcessor(CreoleParserContext context, CreoleTokenRange tokens, CreoleRichTextElementProcessor baseElementProcessor)
            : base(context, tokens, baseElementProcessor)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NoElement:
                    if (Token.Code == BraceOpen)
                    {
                        _elementStartIndex = Index;
                        _state = State.CodeFirstStartMark;
                    }
                    break;

                case State.CodeFirstStartMark:
                    if (Token.Code == BraceOpen)
                        _state = State.CodeSecondStartMark;
                    else
                        _state = State.NoElement;
                    break;

                case State.CodeSecondStartMark:
                    if (Token.Code == BraceOpen)
                        _state = State.CodeContent;
                    else
                        _state = State.NoElement;
                    break;

                case State.CodeContent:
                    if (Token.Code == BraceClose)
                        _state = State.CodeFirstEndMark;
                    break;

                case State.CodeFirstEndMark:
                    if (Token.Code == BraceClose)
                        _state = State.CodeSecondEndMark;
                    else
                        _state = State.CodeContent;
                    break;

                case State.CodeSecondEndMark:
                    if (Token.Code == BraceClose)
                        _state = State.CodeEndMark;
                    else
                        _state = State.CodeContent;
                    break;

                case State.CodeEndMark:
                    if (Token.Code != BraceClose)
                    {
                        _elementEndIndex = Index;
                        var contentStartIndex = (_elementStartIndex + 3);
                        var contentEndIndex = (_elementEndIndex - 3);
                        SetResult(
                            new CreoleRichTextElementData(
                                CreoleRichTextElementType.Code,
                                _elementStartIndex,
                                _elementEndIndex,
                                contentStartIndex,
                                contentEndIndex
                            )
                        );
                        _state = State.NoElement;
                    }
                    break;
            }
        }

        protected override void Complete()
        {
            if (_state == State.CodeEndMark)
            {
                _elementEndIndex = Index;
                var contentStartIndex = (_elementStartIndex + 3);
                var contentEndIndex = (_elementEndIndex - 3);
                SetResult(
                    new CreoleRichTextElementData(
                        CreoleRichTextElementType.Code,
                        _elementStartIndex,
                        _elementEndIndex,
                        contentStartIndex,
                        contentEndIndex
                    )
                );
            }
            _state = State.NoElement;
        }
    }
}