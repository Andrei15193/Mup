using System.Collections.Generic;

namespace Mup.Elements
{
    /// <summary>Represents a table data cell.</summary>
    public class TableDataCellElement : TableCellElement
    {
        /// <summary>Initializes a new instance of the <see cref="TableDataCellElement"/> class.</summary>
        /// <param name="content">The table data cell content.</param>
        public TableDataCellElement(IEnumerable<Element> content)
            : base(content)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TableDataCellElement"/> class.</summary>
        /// <param name="content">The table data cell content.</param>
        public TableDataCellElement(params Element[] content)
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