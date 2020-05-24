using Mup.Scanner;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static Mup.Creole.CreoleTokenCode;
using static System.Char;

namespace Mup.Creole
{
    internal class CreoleScanner : CharacterScanner
    {
        private const char _escapeCahracter = '~';
        private static readonly string _escapeCahracterString = _escapeCahracter.ToString();

        private bool _isEscaped;

        private CreoleTokenCode _tokenCode;

        private int _lineFeedCount = 0;

        private readonly StringBuilder _textBuilder = new StringBuilder();

        private IList<CreoleToken> _tokens;

        internal ReadOnlyCollection<CreoleToken> Result { get; private set; }

        protected override void Reset()
        {
            base.Reset();
            _isEscaped = false;

            _tokenCode = Text;
            _lineFeedCount = 0;
            _textBuilder.Length = 0;
            _tokens = new List<CreoleToken>();
        }

        protected override void Process(char character)
        {
            if (_isEscaped)
                _ProcessEscapedCharacter(character);
            else
                _ProcessCharacter(character);
        }

        protected override void ScanCompleted()
        {
            _AddToken();
            Result = new ReadOnlyCollection<CreoleToken>(_tokens);
        }

        private void _ProcessCharacter(char character)
        {
            switch (character)
            {
                case _escapeCahracter:
                    _isEscaped = true;
                    break;

                case '*':
                    _AddToken();
                    _tokens.Add(new CreoleToken(Asterisk, "*"));
                    break;

                case '/':
                    _AddToken();
                    _tokens.Add(new CreoleToken(Slash, "/"));
                    break;

                case '\\':
                    _AddToken();
                    _tokens.Add(new CreoleToken(BackSlash, "\\"));
                    break;

                case '[':
                    _AddToken();
                    _tokens.Add(new CreoleToken(BracketOpen, "["));
                    break;

                case ']':
                    _AddToken();
                    _tokens.Add(new CreoleToken(BracketClose, "]"));
                    break;

                case '{':
                    _AddToken();
                    _tokens.Add(new CreoleToken(BraceOpen, "{"));
                    break;

                case '}':
                    _AddToken();
                    _tokens.Add(new CreoleToken(BraceClose, "}"));
                    break;

                case '<':
                    _AddToken();
                    _tokens.Add(new CreoleToken(AngleOpen, "<"));
                    break;

                case '>':
                    _AddToken();
                    _tokens.Add(new CreoleToken(AngleClose, ">"));
                    break;

                case '=':
                    _AddToken();
                    _tokens.Add(new CreoleToken(Equal, "="));
                    break;

                case '-':
                    _AddToken();
                    _tokens.Add(new CreoleToken(Dash, "-"));
                    break;

                case '#':
                    _AddToken();
                    _tokens.Add(new CreoleToken(Hash, "#"));
                    break;

                case '|':
                    _AddToken();
                    _tokens.Add(new CreoleToken(Pipe, "|"));
                    break;

                default:
                    if (IsPunctuation(character))
                    {
                        _AddToken();
                        _tokens.Add(new CreoleToken(Punctuation, character.ToString()));
                    }
                    else
                    {
                        var tokenCode = (IsWhiteSpace(character) ? WhiteSpace : Text);
                        if (_tokenCode != tokenCode)
                        {
                            _AddToken();
                            _tokenCode = tokenCode;
                        }
                        if (character == '\n')
                            _lineFeedCount++;
                        _textBuilder.Append(character);
                    }
                    break;
            }
        }

        private void _ProcessEscapedCharacter(char character)
        {
            _isEscaped = false;
            if (IsLetter(character))
            {
                _AddToken();

                _tokens.Add(new CreoleToken(Tilde, _escapeCahracterString));

                _tokenCode = Text;
                _textBuilder.Append(character);
            }
            else if (character == _escapeCahracter)
            {
                _AddToken();

                _tokens.Add(new CreoleToken(Text, _escapeCahracterString));

                _tokenCode = Text;
            }
            else if (IsDigit(character))
            {
                if (_tokenCode != Text)
                {
                    _AddToken();
                    _tokenCode = Text;
                }
                _textBuilder.Append(_escapeCahracter);

                _textBuilder.Append(character);
            }
            else if (IsWhiteSpace(character))
            {
                if (_tokenCode != Text)
                {
                    _AddToken();
                    _tokenCode = Text;
                }
                _textBuilder.Append(_escapeCahracter);
                _AddToken();

                _tokenCode = WhiteSpace;
                if (character == '\n')
                    _lineFeedCount++;

                _textBuilder.Append(character);
            }
            else
            {
                _AddToken();
                _tokenCode = Text;

                _textBuilder.Append(character);
            }
        }

        private void _AddToken()
        {
            if (_isEscaped && _tokenCode == Text)
                _textBuilder.Append(_escapeCahracter);

            if (_textBuilder.Length > 0)
            {
                if (_tokenCode == WhiteSpace)
                {
                    if (_lineFeedCount == 1)
                        _tokenCode = NewLine;
                    else if (_lineFeedCount > 1)
                        _tokenCode = BlankLine;
                    _lineFeedCount = 0;
                }

                _tokens.Add(new CreoleToken(_tokenCode, _textBuilder.ToString()));
                _textBuilder.Length = 0;
            }
            else if (_isEscaped)
                _tokens.Add(new CreoleToken(Text, _escapeCahracterString));
        }
    }
}