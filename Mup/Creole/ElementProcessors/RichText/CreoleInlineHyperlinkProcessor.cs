using Mup.Scanner;
using System;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal class CreoleInlineHyperlinkProcessor : CreoleRichTextElementProcessor
    {
        private enum State : byte
        {
            NoElement,
            Protocol,
            Colon,
            FirstSlash,
            SecondSlash,
            UrlStart,
            Url,
            UrlMayEnd,
            UrlMayEndBeforeDoubleSlash,
            UrlEndsInSlash
        }

        private int _elementStartIndex;
        private int _elementEndIndex;
        private bool _isEscaped;
        private State _state = State.NoElement;

        internal CreoleInlineHyperlinkProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens)
            : base(context, tokens)
        {
        }

        internal CreoleInlineHyperlinkProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens, CreoleRichTextElementProcessor baseElementProcessor)
            : base(context, tokens, baseElementProcessor)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NoElement:
                    if (Token.Code == Tilde)
                    {
                        _elementStartIndex = Index;
                        _isEscaped = true;
                    }
                    else if (Token.Code == Text && _MatchesProtocol())
                    {
                        if (!_isEscaped)
                            _elementStartIndex = Index;
                        _state = State.Protocol;
                    }
                    else
                        _isEscaped = false;
                    break;

                case State.Protocol:
                    if (Token.Code == Punctuation && Token.Text.Length == 1 && Token.Text[0] == ':')
                        _state = State.Colon;
                    else
                    {
                        _isEscaped = false;
                        _state = State.NoElement;
                    }
                    break;

                case State.Colon:
                    if (Token.Code == Slash)
                        _state = State.FirstSlash;
                    else
                    {
                        _isEscaped = false;
                        _state = State.NoElement;
                    }
                    break;

                case State.FirstSlash:
                    if (Token.Code == Slash)
                        _state = State.UrlStart;
                    else
                    {
                        _isEscaped = false;
                        _state = State.NoElement;
                    }
                    break;

                case State.UrlStart:
                    if (Token.Code == Text)
                        _state = State.Url;
                    else
                    {
                        _isEscaped = false;
                        _state = State.NoElement;
                    }
                    break;

                case State.Url:
                    if (Token.Code == WhiteSpace || Token.Code == NewLine || Token.Code == BlankLine)
                    {
                        _elementEndIndex = Index;
                        SetResult(_GetHyperlinkElement());
                        _isEscaped = false;
                        _state = State.NoElement;
                    }
                    else if (Token.Code == Slash)
                    {
                        _elementEndIndex = Index;
                        _state = State.UrlMayEndBeforeDoubleSlash;
                    }
                    else if (Token.Code != Text)
                    {
                        _elementEndIndex = Index;
                        _state = State.UrlMayEnd;
                    }
                    break;

                case State.UrlMayEnd:
                    if (Token.Code != Text
                        && Token.Code != Dash
                        && Token.Code != Asterisk
                        && Token.Code != Dash
                        && Token.Code != Equal
                        && Token.Code != Hash
                        && Token.Code != Pipe
                        && Token.Code != Tilde)
                    {
                        SetResult(_GetHyperlinkElement());
                        _isEscaped = false;
                        _state = State.NoElement;
                    }
                    else
                        _state = State.Url;
                    break;

                case State.UrlMayEndBeforeDoubleSlash:
                    if (Token.Code == Slash)
                        _state = State.UrlEndsInSlash;
                    else if (Token.Code == WhiteSpace || Token.Code == NewLine || Token.Code == BlankLine)
                    {
                        _elementEndIndex = Index;
                        SetResult(_GetHyperlinkElement());
                        _isEscaped = false;
                        _state = State.NoElement;
                    }
                    else if (Token.Code != Text)
                    {
                        _elementEndIndex = Index;
                        _state = State.UrlMayEnd;
                    }
                    else
                        _state = State.Url;
                    break;

                case State.UrlEndsInSlash:
                    if (Token.Code == Slash)
                        _elementEndIndex++;
                    SetResult(_GetHyperlinkElement());
                    _isEscaped = false;
                    _state = State.NoElement;
                    break;
            }
        }

        protected override void Complete()
        {
            if (_state == State.Url || _state == State.UrlMayEndBeforeDoubleSlash)
            {
                _elementEndIndex = Index;
                SetResult(_GetHyperlinkElement());
            }
            else if (_state == State.UrlMayEnd || _state == State.UrlEndsInSlash)
                SetResult(_GetHyperlinkElement());
            _isEscaped = false;
            _state = State.NoElement;
        }

        private CreoleRichTextElementData _GetHyperlinkElement()
        {
            var urlStartIndex = (_isEscaped ? (_elementStartIndex + 1) : _elementStartIndex);
            var urlEndIndex = _elementEndIndex;

            var contentStartIndex = urlStartIndex;
            var contentEndIndex = urlEndIndex;

            return new CreoleRichTextElementData(
                (_isEscaped ? CreoleRichTextElementType.EscapedInlineHyperlink : CreoleRichTextElementType.InlineHyperlink),
                _elementStartIndex,
                _elementEndIndex,
                urlStartIndex,
                urlEndIndex,
                contentStartIndex,
                contentEndIndex
            );
        }

        private bool _MatchesProtocol()
        {
            var found = false;
            using (var protocol = Context.InlineHyperlinkProtocols.GetEnumerator())
                while (!found && protocol.MoveNext())
                    found = Token.Text.Equals(protocol.Current, StringComparison.OrdinalIgnoreCase);
            return found;
        }
    }
}