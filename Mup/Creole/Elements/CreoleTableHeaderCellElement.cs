using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleTableHeaderCellElement : CreoleTableCellElement
    {
        internal CreoleTableHeaderCellElement(IEnumerable<CreoleElement> content)
            : base(content)
        {
        }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitTableHeaderCellBeginning();
            foreach (var element in Content)
                element.Accept(visitor);
            visitor.VisitTableHeaderCellEnding();
        }
    }
}