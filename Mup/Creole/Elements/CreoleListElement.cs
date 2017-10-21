using System.Collections.Generic;

namespace Mup.Creole.Elements
{
    internal abstract class CreoleListElement : CreoleElement
    {
        protected CreoleListElement(IEnumerable<CreoleListItemElement> items)
        {
            Items = items;
        }

        internal IEnumerable<CreoleListItemElement> Items { get; }
    }
}