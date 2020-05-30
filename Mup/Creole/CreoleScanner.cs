using Mup.Scanner;
using System.Collections.Generic;
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

        private List<Token<CreoleTokenCode>> _tokens;

        internal IReadOnlyList<Token<CreoleTokenCode>> Result { get; private set; }

        protected override void Reset()
        {
            base.Reset();
            _isEscaped = false;

            _tokenCode = Text;
            _lineFeedCount = 0;
            _textBuilder.Length = 0;
            _tokens = new List<Token<CreoleTokenCode>>();
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
            Result = _tokens;
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
                    _tokens.Add(new Token<CreoleTokenCode>(Asterisk, "*", Line, Column));
                    break;

                case '/':
                    _AddToken();
                    _tokens.Add(new Token<CreoleTokenCode>(Slash, "/", Line, Column));
                    break;

                case '\\':
                    _AddToken();
                    _tokens.Add(new Token<CreoleTokenCode>(BackSlash, "\\", Line, Column));
                    break;

                case '[':
                    _AddToken();
                    _tokens.Add(new Token<CreoleTokenCode>(BracketOpen, "[", Line, Column));
                    break;

                case ']':
                    _AddToken();
                    _tokens.Add(new Token<CreoleTokenCode>(BracketClose, "]", Line, Column));
                    break;

                case '{':
                    _AddToken();
                    _tokens.Add(new Token<CreoleTokenCode>(BraceOpen, "{", Line, Column));
                    break;

                case '}':
                    _AddToken();
                    _tokens.Add(new Token<CreoleTokenCode>(BraceClose, "}", Line, Column));
                    break;

                case '<':
                    _AddToken();
                    _tokens.Add(new Token<CreoleTokenCode>(AngleOpen, "<", Line, Column));
                    break;

                case '>':
                    _AddToken();
                    _tokens.Add(new Token<CreoleTokenCode>(AngleClose, ">", Line, Column));
                    break;

                case '=':
                    _AddToken();
                    _tokens.Add(new Token<CreoleTokenCode>(Equal, "=", Line, Column));
                    break;

                case '-':
                    _AddToken();
                    _tokens.Add(new Token<CreoleTokenCode>(Dash, "-", Line, Column));
                    break;

                case '#':
                    _AddToken();
                    _tokens.Add(new Token<CreoleTokenCode>(Hash, "#", Line, Column));
                    break;

                case '|':
                    _AddToken();
                    _tokens.Add(new Token<CreoleTokenCode>(Pipe, "|", Line, Column));
                    break;

                default:
                    if (IsPunctuation(character))
                    {
                        _AddToken();
                        _tokens.Add(new Token<CreoleTokenCode>(Punctuation, character.ToString(), Line, Column));
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

                _tokens.Add(new Token<CreoleTokenCode>(Tilde, _escapeCahracterString, Line, Column));

                _tokenCode = Text;
                _textBuilder.Append(character);
            }
            else if (character == _escapeCahracter)
            {
                _AddToken();

                _tokens.Add(new Token<CreoleTokenCode>(Text, _escapeCahracterString, Line, Column));

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

                _tokens.Add(new Token<CreoleTokenCode>(_tokenCode, _textBuilder.ToString(), Line, Column));
                _textBuilder.Length = 0;
            }
            else if (_isEscaped)
                _tokens.Add(new Token<CreoleTokenCode>(Text, _escapeCahracterString, Line, Column));
        }
    }
}