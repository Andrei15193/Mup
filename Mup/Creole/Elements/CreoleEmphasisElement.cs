using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleEmphasisElement : CreoleElement
    {
        internal CreoleEmphasisElement(IEnumerable<CreoleElement> children)
        {
            Children = children;
        }

        internal IEnumerable<CreoleElement> Children { get; }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitEmphasisBeginning();
            foreach (var child in Children)
                child.Accept(visitor);
            visitor.VisitEmphasisEnding();
        }
    }
}