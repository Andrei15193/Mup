using System.Collections.Generic;

namespace Mup.Elements
{
    /// <summary>Represents a table.</summary>
    public class TableElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="TableElement"/> class.</summary>
        /// <param name="rows">The table rows.</param>
        public TableElement(IEnumerable<TableRowElement> rows)
            => Rows = rows;

        /// <summary>Initializes a new instance of the <see cref="TableElement"/> class.</summary>
        /// <param name="rows">The table rows.</param>
        public TableElement(params TableRowElement[] rows)
            : this((IEnumerable<TableRowElement>)rows)
        {
        }

        /// <summary>The table rows.</summary>
        public IEnumerable<TableRowElement> Rows { get; }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}