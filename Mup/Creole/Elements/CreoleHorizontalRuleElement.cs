using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleHorizontalRuleElement : CreoleElement
    {
        internal override void Accept(ParseTreeVisitor visitor)
            => visitor.VisitHorizontalRule();
    }
}