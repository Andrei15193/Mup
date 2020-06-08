using System.Collections.Generic;

namespace Mup.Elements
{
    /// <summary>Represents a table cell.</summary>
    public abstract class TableCellElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="TableCellElement"/> class.</summary>
        /// <param name="content">The table cell content.</param>
        protected TableCellElement(IEnumerable<Element> content)
            => Content = content;

        /// <summary>The table cell content.</summary>
        public IEnumerable<Element> Content { get; }
    }
}