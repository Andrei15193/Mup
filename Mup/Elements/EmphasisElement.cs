using System.Collections.Generic;

namespace Mup.Elements
{
    /// <summary>Represents an emphasised part of text.</summary>
    public class EmphasisElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="EmphasisElement"/> class.</summary>
        /// <param name="content">The emphasised content.</param>
        public EmphasisElement(IEnumerable<Element> content)
            => Content = content;

        /// <summary>Initializes a new instance of the <see cref="EmphasisElement"/> class.</summary>
        /// <param name="content">The emphasised content.</param>
        public EmphasisElement(params Element[] content)
            : this((IEnumerable<Element>)content)
        {
        }

        /// <summary>The emphasised content.</summary>
        public IEnumerable<Element> Content { get; }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}