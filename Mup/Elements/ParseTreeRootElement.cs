using System.Collections.Generic;

namespace Mup.Elements
{
    /// <summary>Represents the root node of a parse tree.</summary>
    public class ParseTreeRootElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="ParseTreeRootElement"/> class.</summary>
        /// <param name="elements">The immediate descendants of the parse tree root node.</param>
        public ParseTreeRootElement(IEnumerable<Element> elements)
            => Elements = elements;

        /// <summary>The immediate descendants of the parse tree root node.</summary>
        public IEnumerable<Element> Elements { get; }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}