using System.Collections.Generic;

namespace Mup.Elements
{
    public class TableDataCellElement : TableCellElement
    {
        public TableDataCellElement(IEnumerable<Element> content)
            : base(content)
        {
        }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}