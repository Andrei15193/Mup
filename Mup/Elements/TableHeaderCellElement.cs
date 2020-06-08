using System.Collections.Generic;

namespace Mup.Elements
{
    /// <summary>Represents a table header cell.</summary>
    public class TableHeaderCellElement : TableCellElement
    {
        /// <summary>Initializes a new instance of the <see cref="TableHeaderCellElement"/> class.</summary>
        /// <param name="content">The table header cell content.</param>
        public TableHeaderCellElement(IEnumerable<Element> content)
            : base(content)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TableHeaderCellElement"/> class.</summary>
        /// <param name="content">The table header cell content.</param>
        public TableHeaderCellElement(params Element[] content)
            : this((IEnumerable<Element>)content)
        {
        }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}