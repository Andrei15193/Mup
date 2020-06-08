using System.Collections.Generic;

namespace Mup.Elements
{
    public class TableRowElement : Element
    {
        public TableRowElement(IEnumerable<TableCellElement> cells)
            => Cells = cells;

        public IEnumerable<TableCellElement> Cells { get; }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}