using System.Collections.Generic;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal abstract class CreoleUrlElementProcessor : CreoleRichTextElementProcessor
    {
        private enum State : byte
        {
            NoElement,
            ElementFirstStartMark,
            ElementStartMark,
            ElementUrl,
            ElementContent,
            ElementEndMarkFromUrl,
            ElementEndMarkFromContent
        }

        private int _elementStartIndex;
        private int _elementEndIndex;
        private int _urlStartIndex;
        private int _urlEndIndex;
        private int _contentStartIndex;
        private int _contentEndIndex;
        private State _state = State.NoElement;

        protected CreoleUrlElementProcessor(CreoleParserContext context, CreoleTokenRange tokens)
            : base(context, tokens)
        {
        }

        protected CreoleUrlElementProcessor(CreoleParserContext context, CreoleTokenRange tokens, CreoleRichTextElementProcessor baseElementProcessor)
            : base(context, tokens, baseElementProcessor)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NoElement:
                    if (Token.Code == ElementOpenTokenCode)
                        _state = State.ElementFirstStartMark;
                    break;

                case State.ElementFirstStartMark:
                    if (Token.Code == ElementOpenTokenCode)
                        _state = State.ElementStartMark;
                    else
                        _state = State.NoElement;
                    break;

                case State.ElementStartMark:
                    if (Token.Code != ElementOpenTokenCode)
                    {
                        _elementStartIndex = (Index - 2);
                        _urlStartIndex = Index;
                        _state = State.ElementUrl;
                    }
                    break;

                case State.ElementUrl:
                    if (Token.Code == Pipe)
                    {
                        _urlEndIndex = Index;
                        _contentStartIndex = (Index + 1);
                        _state = State.ElementContent;
                    }
                    else if (Token.Code == ElementEndTokenCode)
                    {
                        _urlEndIndex = Index; _urlEndIndex = Index;
                        _contentStartIndex = _urlStartIndex;
                        _contentEndIndex = _urlEndIndex;
                        _state = State.ElementEndMarkFromUrl;
                    }
                    break;

                case State.ElementContent:
                    if (Token.Code == ElementEndTokenCode)
                    {
                        _contentEndIndex = Index;
                        _state = State.ElementEndMarkFromContent;
                    }
                    break;

                case State.ElementEndMarkFromUrl:
                    if (Token.Code == ElementEndTokenCode)
                    {
                        _elementEndIndex = (Index + 1);
                        SetResult(
                            new CreoleRichTextElementData(
                                ElementType,
                                _elementStartIndex,
                                _elementEndIndex,
                                _urlStartIndex,
                                _urlEndIndex,
                                _contentStartIndex,
                                _contentEndIndex
                            )
                        );
                        _state = State.NoElement;
                    }
                    else
                        _state = State.ElementUrl;
                    break;

                case State.ElementEndMarkFromContent:
                    if (Token.Code == ElementEndTokenCode)
                    {
                        _elementEndIndex = (Index + 1);
                        SetResult(
                            new CreoleRichTextElementData(
                                ElementType,
                                _elementStartIndex,
                                _elementEndIndex,
                                _urlStartIndex,
                                _urlEndIndex,
                                _contentStartIndex,
                                _contentEndIndex
                            )
                        );
                        _state = State.NoElement;
                    }
                    else
                        _state = State.ElementContent;
                    break;
            }
        }

        protected override void Complete()
        {
            _state = State.NoElement;
        }

        protected abstract CreoleRichTextElementType ElementType { get; }

        protected abstract CreoleTokenCode ElementOpenTokenCode { get; }

        protected abstract CreoleTokenCode ElementEndTokenCode { get; }
    }
}