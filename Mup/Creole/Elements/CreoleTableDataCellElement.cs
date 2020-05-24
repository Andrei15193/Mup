using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleTableDataCellElement : CreoleTableCellElement
    {
        internal CreoleTableDataCellElement(IEnumerable<CreoleElement> content)
            : base(content)
        {
        }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitTableCellBeginning();
            foreach (var element in Content)
                element.Accept(visitor);
            visitor.VisitTableCellEnding();
        }
    }
}