namespace Mup.Elements
{
    /// <summary>Represents an image.</summary>
    public class ImageElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="ImageElement"/> class.</summary>
        /// <param name="source">The image source, such as a URL.</param>
        public ImageElement(string source)
            : this(source, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ImageElement"/> class.</summary>
        /// <param name="source">The image source, such as a URL.</param>
        /// <param name="alternative">The image alternative text.</param>
        public ImageElement(string source, string alternative)
        {
            Source = source;
            AlternativeText = alternative;
        }

        /// <summary>The image source, such as a URL.</summary>
        public string Source { get; }

        /// <summary>The image alternative text.</summary>
        public string AlternativeText { get; }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}