using System.Collections.Generic;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal abstract class CreoleStyleElementProcessor : CreoleRichTextElementProcessor
    {
        private enum State : byte
        {
            NoElement,
            FirstStartMark,
            StartMark,
            Content,
            EndMark
        }

        private int _elementStartIndex;
        private int _elementEndIndex;
        private State _state = State.NoElement;

        internal CreoleStyleElementProcessor(CreoleParserContext context, CreoleTokenRange tokens)
            : base(context, tokens)
        {
        }

        internal CreoleStyleElementProcessor(CreoleParserContext context, CreoleTokenRange tokens, IEnumerable<CreoleRichTextElementData> baseElements)
            : base(context, tokens, baseElements)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NoElement:
                    if (Token.Code == TokenCode)
                        _state = State.FirstStartMark;
                    break;

                case State.FirstStartMark:
                    if (Token.Code == TokenCode)
                        _state = State.StartMark;
                    else
                        _state = State.NoElement;
                    break;

                case State.StartMark:
                    if (Token.Code != TokenCode)
                    {
                        _elementStartIndex = (Index - 2);
                        _state = State.Content;
                    }
                    break;

                case State.Content:
                    if (Token.Code == TokenCode)
                        _state = State.EndMark;
                    break;

                case State.EndMark:
                    if (Token.Code == TokenCode)
                    {
                        _elementEndIndex = (Index + 1);
                        SetResult(_GetElementData());
                        _state = State.NoElement;
                    }
                    else
                        _state = State.Content;
                    break;
            }
        }

        protected override void Complete()
        {
            if (_state == State.FirstStartMark)
                _state = State.NoElement;
            else if (_state == State.StartMark)
            {
                _elementStartIndex = (Index - 2);
                _state = State.Content;
            }
            else if (_state == State.EndMark)
                _state = State.Content;
        }

        protected abstract CreoleTokenCode TokenCode { get; }

        protected abstract CreoleRichTextElementType ElementType { get; }

        private CreoleRichTextElementData _GetElementData()
        {
            var contentStartIndex = (_elementStartIndex + 2);
            var contentEndIndex = (_elementEndIndex - 2);
            return new CreoleRichTextElementData(
                ElementType,
                _elementStartIndex,
                _elementEndIndex,
                contentStartIndex,
                contentEndIndex
            );
        }
    }
}