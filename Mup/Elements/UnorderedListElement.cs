﻿using System.Collections.Generic;

namespace Mup.Elements
{
    /// <summary>Represents an unordered list.</summary>
    public class UnorderedListElement : ListElement
    {
        /// <summary>Initializes a new instance of the <see cref="UnorderedListElement"/> class.</summary>
        /// <param name="items">The unordered list items.</param>
        public UnorderedListElement(IEnumerable<ListItemElement> items)
            : base(items)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="UnorderedListElement"/> class.</summary>
        /// <param name="items">The unordered list items.</param>
        public UnorderedListElement(params ListItemElement[] items)
            : this((IEnumerable<ListItemElement>)items)
        {
        }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}