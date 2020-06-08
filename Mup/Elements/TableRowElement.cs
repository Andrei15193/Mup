using System.Collections.Generic;

namespace Mup.Elements
{
    /// <summary>Represents a table row.</summary>
    public class TableRowElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="TableCellElement"/> class.</summary>
        /// <param name="cells">The table row cells.</param>
        public TableRowElement(IEnumerable<TableCellElement> cells)
            => Cells = cells;

        /// <summary>Initializes a new instance of the <see cref="TableCellElement"/> class.</summary>
        /// <param name="cells">The table row cells.</param>
        public TableRowElement(params TableCellElement[] cells)
            : this((IEnumerable<TableCellElement>)cells)
        {
        }

        /// <summary>The table row cells.</summary>
        public IEnumerable<TableCellElement> Cells { get; }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}