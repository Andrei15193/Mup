using System;

namespace Mup.Creole
{
    internal class CreoleToken : IEquatable<CreoleToken>
    {
        public static bool operator ==(CreoleToken left, CreoleToken right)
            => left.Equals(right);

        public static bool operator !=(CreoleToken left, CreoleToken right)
            => !left.Equals(right);

        internal CreoleToken(CreoleTokenCode code, string text)
        {
            Code = code;
            Text = text;
        }

        internal CreoleTokenCode Code { get; }

        internal string Text { get; }

        public bool Equals(CreoleToken other)
            => (Code == other.Code && Text == other.Text);

        public override bool Equals(object obj)
            => (obj != null && ((obj as CreoleToken)?.Equals(this) ?? false));

        public override int GetHashCode()
            => (new { Code, Text }).GetHashCode();

        public override string ToString()
            => $"{Code}: {Text}";
    }
}