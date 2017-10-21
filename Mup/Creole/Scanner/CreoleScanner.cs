using System.Collections.Generic;
using Mup.Scanner;
using static System.Char;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.Scanner
{
    internal class CreoleScanner : CharacterScanner
    {
        private const char _escapeCahracter = '~';

        private bool _isEscaped;

        private CreoleTokenCode _tokenCode;
        private int _start;
        private int _length;

        private CreoleToken _previousToken;
        private IList<CreoleToken> _tokens;

        internal CreoleScanResult Result { get; private set; }

        protected override void Reset()
        {
            base.Reset();
            _isEscaped = false;

            _tokenCode = Text;
            _start = 0;
            _length = 0;

            _previousToken = null;
            _tokens = new List<CreoleToken>();
        }

        protected override void Process(char character)
        {
            if (_isEscaped)
                _ProcessEscapedCharacter(character);
            else if (character == _escapeCahracter)
                _ProcessEscapeCharacter();
            else
                _ProcessCharacter(character);
        }

        protected override void ScanCompleted(string text)
        {
            _AddToken();
            Result = new CreoleScanResult(text, _tokens);
        }

        private void _ProcessCharacter(char character)
        {
            var characterTokenCode = _GetTokenCodeFor(character);
            switch (characterTokenCode)
            {
                case WhiteSpace:
                case Punctuation:
                case Text:
                    if (_tokenCode != characterTokenCode)
                    {
                        _AddToken();
                        _tokenCode = characterTokenCode;
                    }
                    else
                        _length++;
                    break;

                default:
                    _AddToken();
                    _tokenCode = characterTokenCode;
                    break;
            }
        }

        private void _ProcessEscapeCharacter()
        {
            _isEscaped = true;
            if (_tokenCode != Text)
                _AddToken();
            else
                _length++;
        }

        private void _ProcessEscapedCharacter(char character)
        {
            _isEscaped = false;
            if (IsLetter(character))
            {
                _length--;
                _AddToken();

                _tokenCode = Tilde;
                _start--;
                _AddToken();
            }
            else if (character == _escapeCahracter)
            {
                _length--;
                _AddToken();

                _tokenCode = Text;
                _start--;
                _AddToken();

                _start++;
                _length--;
            }
            else if (IsDigit(character))
                _length++;
            else if (IsWhiteSpace(character))
            {
                _AddToken();
                _tokenCode = WhiteSpace;
            }
            else
            {
                _length--;
                _AddToken();
            }
        }

        private void _AddToken()
        {
            if (_length != 0)
            {
                var token = new CreoleToken(_tokenCode, _start, _length, _previousToken);
                _tokens.Add(token);
                _previousToken = token;
            }

            _tokenCode = Text;
            _start = Index;
            _length = 1;
        }

        private static CreoleTokenCode _GetTokenCodeFor(char character)
        {
            switch (character)
            {
                case '*':
                    return Asterisk;

                case '/':
                    return Slash;

                case '\\':
                    return BackSlash;

                case '[':
                    return BracketOpen;

                case ']':
                    return BracketClose;

                case '{':
                    return BraceOpen;

                case '}':
                    return BraceClose;

                case '<':
                    return AngleOpen;

                case '>':
                    return AngleClose;

                case '=':
                    return Equal;

                case '-':
                    return Dash;

                case '#':
                    return Hash;

                case '|':
                    return Pipe;

                case '~':
                    return Tilde;

                default:
                    if (IsWhiteSpace(character))
                        return WhiteSpace;
                    else if (IsPunctuation(character))
                        return Punctuation;
                    else
                        return Text;
            }
        }
    }
}