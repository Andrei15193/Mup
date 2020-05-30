using Mup.Scanner;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal class CreolePluginElementProcessor : CreoleRichTextElementProcessor
    {
        private enum State : byte
        {
            NoElement,
            PluginFirstStartMark,
            PluginContent,
            PluginFirstEndMark,
            PluginEndMark
        }

        private int _elementStartIndex;
        private int _elementEndIndex;
        private State _state = State.NoElement;

        internal CreolePluginElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens)
            : base(context, tokens)
        {
        }

        internal CreolePluginElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens, CreoleRichTextElementProcessor baseElementProcessor)
            : base(context, tokens, baseElementProcessor)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NoElement:
                    if (Token.Code == AngleOpen)
                    {
                        _elementStartIndex = Index;
                        _state = State.PluginFirstStartMark;
                    }
                    break;

                case State.PluginFirstStartMark:
                    if (Token.Code == AngleOpen)
                        _state = State.PluginContent;
                    else
                        _state = State.NoElement;
                    break;

                case State.PluginContent:
                    if (Token.Code == AngleClose)
                        _state = State.PluginFirstEndMark;
                    break;

                case State.PluginFirstEndMark:
                    if (Token.Code == AngleClose)
                        _state = State.PluginEndMark;
                    else
                        _state = State.PluginContent;
                    break;

                case State.PluginEndMark:
                    if (Token.Code != AngleClose)
                    {
                        _elementEndIndex = Index;
                        var contentStartIndex = (_elementStartIndex + 2);
                        var contentEndIndex = (_elementEndIndex - 2);
                        SetResult(
                            new CreoleRichTextElementData(
                                CreoleRichTextElementType.Plugin,
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
            if (_state == State.PluginEndMark)
            {
                _elementEndIndex = Index;
                var contentStartIndex = (_elementStartIndex + 2);
                var contentEndIndex = (_elementEndIndex - 2);
                SetResult(
                    new CreoleRichTextElementData(
                        CreoleRichTextElementType.Plugin,
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