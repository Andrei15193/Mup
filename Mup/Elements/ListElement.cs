using System.Collections.Generic;

namespace Mup.Elements
{
    public abstract class ListElement : Element
    {
        protected ListElement(IEnumerable<ListItemElement> items)
            => Items = items;

        public IEnumerable<ListItemElement> Items { get; }
    }
}