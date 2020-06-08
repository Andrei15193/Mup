using System.Collections.Generic;

namespace Mup.Elements
{
    /// <summary>Represents a list.</summary>
    public abstract class ListElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="ListElement"/> class.</summary>
        /// <param name="items">The list items.</param>
        protected ListElement(IEnumerable<ListItemElement> items)
            => Items = items;

        /// <summary>The list items.</summary>
        public IEnumerable<ListItemElement> Items { get; }
    }
}