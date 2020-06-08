using System.Collections.Generic;

namespace Mup.Elements
{
    public class OrderedListElement : ListElement
    {
        public OrderedListElement(IEnumerable<ListItemElement> items)
            : base(items)
        {
        }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}