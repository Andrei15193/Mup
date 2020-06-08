namespace Mup.Elements
{
    /// <summary>Represents a preforamtted block of text.</summary>
    public class PreformattedBlockElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="PreformattedBlockElement"/> class.</summary>
        /// <param name="preformattedText">The preformatted text.</param>
        public PreformattedBlockElement(string preformattedText)
            => PreformattedText = preformattedText;

        /// <summary>The preformatted text.</summary>
        public string PreformattedText { get; }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}