using System.Collections.Generic;

namespace Mup.Elements
{
    /// <summary>Represents a hyperlink.</summary>
    public class HyperlinkElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="HyperlinkElement"/> class.</summary>
        /// <param name="destination">The hyperlink destination, such as a URL.</param>
        /// <param name="content">The hyperlink content.</param>
        public HyperlinkElement(string destination, IEnumerable<Element> content)
        {
            Destination = destination;
            Content = content;
        }

        /// <summary>Initializes a new instance of the <see cref="HyperlinkElement"/> class.</summary>
        /// <param name="destination">The hyperlink destination, such as a URL.</param>
        /// <param name="content">The hyperlink content.</param>
        public HyperlinkElement(string destination, params Element[] content)
            : this(destination, (IEnumerable<Element>)content)
        {
        }

        /// <summary>The hyperlink destination, such as a URL.</summary>
        public string Destination { get; }

        /// <summary>The hyperlink content.</summary>
        public IEnumerable<Element> Content { get; }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}