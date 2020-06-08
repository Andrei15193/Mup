using System.Collections.Generic;

namespace Mup.Elements
{
    public abstract class TableCellElement : Element
    {
        protected TableCellElement(IEnumerable<Element> content)
            => Content = content;

        public IEnumerable<Element> Content { get; }
    }
}