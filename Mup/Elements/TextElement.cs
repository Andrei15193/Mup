namespace Mup.Elements
{
    /// <summary>Represents plain text fragment.</summary>
    public class TextElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="TextElement"/> class.</summary>
        /// <param name="text">The plain text fragment.</param>
        public TextElement(string text)
            => Text = text;

        /// <summary>The plain text fragment.</summary>
        public string Text { get; }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}