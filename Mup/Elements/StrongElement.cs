using System.Collections.Generic;

namespace Mup.Elements
{
    /// <summary>Represents a strong part of text.</summary>
    public class StrongElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="StrongElement"/> class.</summary>
        /// <param name="content">The strong content.</param>
        public StrongElement(IEnumerable<Element> content)
            => Content = content;

        /// <summary>Initializes a new instance of the <see cref="StrongElement"/> class.</summary>
        /// <param name="content">The strong content.</param>
        public StrongElement(params Element[] content)
            : this((IEnumerable<Element>)content)
        {
        }

        /// <summary>The strong content.</summary>
        public IEnumerable<Element> Content { get; }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}