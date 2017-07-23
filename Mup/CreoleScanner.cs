using System;
using System.Collections.Generic;
using static Mup.CreoleToken;

namespace Mup
{
    internal sealed class CreoleScanner : Scanner<CreoleToken>
    {
        private static readonly IEnumerable<KeyValuePair<CreoleToken, Func<char, bool>>> _creolePredicates =
            new List<KeyValuePair<CreoleToken, Func<char, bool>>>
            {
                new KeyValuePair<CreoleToken, Func<char, bool>>(Asterisk, character => (character == '*')),

                new KeyValuePair<CreoleToken, Func<char, bool>>(Slash, character => (character == '/')),

                new KeyValuePair<CreoleToken, Func<char, bool>>(BackSlash, character => (character == '\\')),

                new KeyValuePair<CreoleToken, Func<char, bool>>(BracketOpen, character => (character == '[')),
                new KeyValuePair<CreoleToken, Func<char, bool>>(BracketClose, character => (character == ']')),

                new KeyValuePair<CreoleToken, Func<char, bool>>(BraceOpen, character => (character == '{')),
                new KeyValuePair<CreoleToken, Func<char, bool>>(BraceClose, character => (character == '}')),

                new KeyValuePair<CreoleToken, Func<char, bool>>(AngleOpen, character => (character == '<')),
                new KeyValuePair<CreoleToken, Func<char, bool>>(AngleClose, character => (character == '>')),

                new KeyValuePair<CreoleToken, Func<char, bool>>(Equal, character => (character == '=')),

                new KeyValuePair<CreoleToken, Func<char, bool>>(Dash, character => (character == '-')),

                new KeyValuePair<CreoleToken, Func<char, bool>>(NewLine, character => (character == '\r' || character == '\n')),

                new KeyValuePair<CreoleToken, Func<char, bool>>(WhiteSpace, char.IsWhiteSpace),

                new KeyValuePair<CreoleToken, Func<char, bool>>(Hash, character => (character == '#')),

                new KeyValuePair<CreoleToken, Func<char, bool>>(Pipe, character => (character == '|')),

                new KeyValuePair<CreoleToken, Func<char, bool>>(Tilde, character => (character == '~')),

                new KeyValuePair<CreoleToken, Func<char, bool>>(Text, character => true)
            };

        public CreoleScanner() : base(_creolePredicates)
        {
        }
    }
}