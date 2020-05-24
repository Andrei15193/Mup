using System.Collections.Generic;

namespace Mup.Creole.Elements
{
    internal class CreoleTableRowElement : CreoleElement
    {
        internal CreoleTableRowElement(IEnumerable<CreoleTableCellElement> cells)
            => Cells = cells;

        internal IEnumerable<CreoleTableCellElement> Cells { get; }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitTableRowBeginning();
            foreach (var cell in Cells)
                cell.Accept(visitor);
            visitor.VisitTableRowEnding();
        }
    }
}