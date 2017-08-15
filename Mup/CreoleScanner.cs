using System;
using System.Collections.Generic;
using static Mup.CreoleTokenCode;

namespace Mup
{
    internal sealed class CreoleScanner
        : Scanner<CreoleTokenCode>
    {
        private static readonly IEnumerable<KeyValuePair<CreoleTokenCode, Func<char, bool>>> _creolePredicates =
            new List<KeyValuePair<CreoleTokenCode, Func<char, bool>>>
            {
                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(Asterisk, character => (character == '*')),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(Slash, character => (character == '/')),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(BackSlash, character => (character == '\\')),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(BracketOpen, character => (character == '[')),
                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(BracketClose, character => (character == ']')),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(BraceOpen, character => (character == '{')),
                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(BraceClose, character => (character == '}')),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(AngleOpen, character => (character == '<')),
                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(AngleClose, character => (character == '>')),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(Equal, character => (character == '=')),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(Dash, character => (character == '-')),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(NewLine, character => (character == '\r' || character == '\n')),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(WhiteSpace, char.IsWhiteSpace),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(Hash, character => (character == '#')),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(Pipe, character => (character == '|')),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(Tilde, character => (character == '~')),

                new KeyValuePair<CreoleTokenCode, Func<char, bool>>(Text, character => true)
            };

        internal CreoleScanner()
            : base(_creolePredicates)
        {
        }
    }
}