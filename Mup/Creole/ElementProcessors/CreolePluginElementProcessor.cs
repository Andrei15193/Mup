using System.Collections.Generic;
using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors
{
    internal class CreolePluginElementProcessor : CreoleElementProcessor
    {
        private enum State : byte
        {
            NotInPlugin,
            PluginWhiteSpaceBeginning,
            PluginBeginning,
            PluginContentBeginning,
            PluginContent,
            PluginEnding,
            PluginEvenEnding,
            PluginOddEnding
        }

        private int _contentStartIndex = 0;
        private int _contentEndIndexEvenEndingIndex = 0;
        private int _contentEndIndexOddEndingIndex = 0;
        private State _state = State.NotInPlugin;

        internal CreolePluginElementProcessor(CreoleParserContext context, CreoleTokenRange tokens)
            : base(context, tokens)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NotInPlugin:
                    if (Token.Code == WhiteSpace)
                    {
                        SetElementStartIndex();
                        _state = State.PluginWhiteSpaceBeginning;
                    }
                    else if (IsOnNewLine && Token.Code == AngleOpen)
                    {
                        SetElementStartIndex();
                        _state = State.PluginBeginning;
                    }
                    break;

                case State.PluginWhiteSpaceBeginning:
                    _state = (Token.Code == AngleOpen ? State.PluginBeginning : State.NotInPlugin);
                    break;

                case State.PluginBeginning:
                    _state = (Token.Code == AngleOpen ? State.PluginContentBeginning : State.NotInPlugin);
                    break;

                case State.PluginContentBeginning:
                    _contentStartIndex = Index;
                    if (Token.Code == NewLine || Token.Code == BlankLine)
                        _state = State.NotInPlugin;
                    if (Token.Code == AngleClose)
                    {
                        _contentEndIndexEvenEndingIndex = Index;
                        _state = State.PluginEnding;
                    }
                    else
                        _state = State.PluginContent;
                    break;

                case State.PluginContent:
                    if (Token.Code == NewLine || Token.Code == BlankLine)
                        _state = State.NotInPlugin;
                    if (Token.Code == AngleClose)
                    {
                        _contentEndIndexEvenEndingIndex = Index;
                        _state = State.PluginEnding;
                    }
                    break;

                case State.PluginEnding:
                    if (Token.Code == AngleClose)
                    {
                        _contentEndIndexOddEndingIndex = Index;
                        _state = State.PluginEvenEnding;
                    }
                    else if (Token.Code == NewLine || Token.Code == BlankLine)
                        _state = State.NotInPlugin;
                    else
                        _state = State.PluginContent;
                    break;

                case State.PluginEvenEnding:
                    if (Token.Code == AngleClose)
                    {
                        _contentEndIndexEvenEndingIndex = Index;
                        _state = State.PluginOddEnding;
                    }
                    else if (Token.Code == NewLine || Token.Code == BlankLine)
                    {
                        SetElementEndIndex();
                        SetResult(new CreolePluginElement(GetPlainText(_contentStartIndex, _contentEndIndexEvenEndingIndex)));
                        _state = State.NotInPlugin;
                    }
                    else
                        _state = State.PluginContent;
                    break;

                case State.PluginOddEnding:
                    if (Token.Code == AngleClose)
                    {
                        _contentEndIndexOddEndingIndex = Index;
                        _state = State.PluginEvenEnding;
                    }
                    else if (Token.Code == NewLine || Token.Code == BlankLine)
                    {
                        SetElementEndIndex();
                        SetResult(new CreolePluginElement(GetPlainText(_contentStartIndex, _contentEndIndexOddEndingIndex)));
                        _state = State.NotInPlugin;
                    }
                    else
                        _state = State.PluginContent;
                    break;
            }
        }

        protected override void Complete()
        {
            if (_state == State.PluginEvenEnding)
            {
                SetElementEndIndex();
                SetResult(new CreolePluginElement(GetPlainText(_contentStartIndex, _contentEndIndexEvenEndingIndex)));
            }
            else if (_state == State.PluginOddEnding)
            {
                SetElementEndIndex();
                SetResult(new CreolePluginElement(GetPlainText(_contentStartIndex, _contentEndIndexOddEndingIndex)));
            }
        }
    }
}