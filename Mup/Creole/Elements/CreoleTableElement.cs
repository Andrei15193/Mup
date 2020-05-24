using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleTableElement : CreoleElement
    {
        internal CreoleTableElement(IEnumerable<CreoleTableRowElement> rows)
            => Rows = rows;

        internal IEnumerable<CreoleTableRowElement> Rows { get; }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitTableBeginning();
            foreach (var row in Rows)
                row.Accept(visitor);
            visitor.VisitTableEnding();
        }
    }
}