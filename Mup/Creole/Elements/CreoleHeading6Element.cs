using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleHeading6Element : CreoleHeadingElement
    {
        internal CreoleHeading6Element(string text)
            : base(text)
        {
        }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHeading6Beginning();
            visitor.VisitText(Text);
            visitor.VisitHeading6Ending();
        }
    }
}