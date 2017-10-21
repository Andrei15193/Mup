using System.Collections.Generic;

namespace Mup.Creole.Elements
{
    internal abstract class CreoleTableCellElement : CreoleElement
    {
        protected CreoleTableCellElement(IEnumerable<CreoleElement> content)
            => Content = content;

        internal IEnumerable<CreoleElement> Content { get; }
    }
}