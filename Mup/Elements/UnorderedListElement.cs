using System.Collections.Generic;

namespace Mup.Elements
{
    public class UnorderedListElement : ListElement
    {
        public UnorderedListElement(IEnumerable<ListItemElement> items)
            : base(items)
        {
        }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}