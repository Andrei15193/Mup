using System.Collections.Generic;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal class CreoleLineBreakElementProcessor : CreoleRichTextElementProcessor
    {
        private enum State : byte
        {
            NoElement,
            LineBreakMark
        }

        private int _elementStartIndex;
        private State _state = State.NoElement;

        internal CreoleLineBreakElementProcessor(CreoleParserContext context, CreoleTokenRange tokens)
            : base(context, tokens)
        {
        }

        internal CreoleLineBreakElementProcessor(CreoleParserContext context, CreoleTokenRange tokens, CreoleRichTextElementProcessor baseElementProcessor)
            : base(context, tokens, baseElementProcessor)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NoElement:
                    if (Token.Code == BackSlash)
                    {
                        _elementStartIndex = Index;
                        _state = State.LineBreakMark;
                    }
                    break;

                case State.LineBreakMark:
                    if (Token.Code == BackSlash)
                    {
                        var elementEndIndex = (_elementStartIndex + 2);
                        SetResult(
                            new CreoleRichTextElementData(
                                CreoleRichTextElementType.LineBreak,
                                _elementStartIndex,
                                elementEndIndex
                            )
                        );
                    }
                    _state = State.NoElement;
                    break;
            }
        }

        protected override void Complete()
        {
            _state = State.NoElement;
        }
    }
}