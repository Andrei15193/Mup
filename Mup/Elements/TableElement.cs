using System.Collections.Generic;

namespace Mup.Elements
{
    public class TableElement : Element
    {
        public TableElement(IEnumerable<TableRowElement> rows)
            => Rows = rows;

        public IEnumerable<TableRowElement> Rows { get; }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}