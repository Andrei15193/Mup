using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleHeading1Element : CreoleHeadingElement
    {
        internal CreoleHeading1Element(string text)
            : base(text)
        {
        }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHeading1Beginning();
            visitor.VisitText(Text);
            visitor.VisitHeading1Ending();
        }
    }
}