﻿namespace Mup.Elements
{
    /// <summary>Represents a level 3 heading.</summary>
    public class Heading3Element : HeadingElement
    {
        /// <summary>Initializes a new instance of the <see cref="Heading3Element"/> class.</summary>
        /// <param name="text">The heading text.</param>
        public Heading3Element(string text)
            : base(text)
        {
        }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}