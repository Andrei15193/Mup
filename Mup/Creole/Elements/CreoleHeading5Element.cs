using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleHeading5Element : CreoleHeadingElement
    {
        internal CreoleHeading5Element(string text)
            : base(text)
        {
        }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHeading5Beginning();
            visitor.VisitText(Text);
            visitor.VisitHeading5Ending();
        }
    }
}