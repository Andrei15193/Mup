using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleOrderedListElement : CreoleListElement
    {
        internal CreoleOrderedListElement(IEnumerable<CreoleListItemElement> items)
            : base(items)
        {
        }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitOrderedListBeginning();
            foreach (var item in Items)
                item.Accept(visitor);
            visitor.VisitOrderedListEnding();
        }
    }
}