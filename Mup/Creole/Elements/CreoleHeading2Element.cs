using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleHeading2Element : CreoleHeadingElement
    {
        internal CreoleHeading2Element(string text)
            : base(text)
        {
        }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHeading2Beginning();
            visitor.VisitText(Text);
            visitor.VisitHeading2Ending();
        }
    }
}