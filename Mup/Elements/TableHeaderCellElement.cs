using System.Collections.Generic;

namespace Mup.Elements
{
    public class TableHeaderCellElement : TableCellElement
    {
        public TableHeaderCellElement(IEnumerable<Element> content)
            : base(content)
        {
        }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}