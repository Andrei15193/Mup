using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleHeading3Element : CreoleHeadingElement
    {
        internal CreoleHeading3Element(string text)
            : base(text)
        {
        }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHeading3Beginning();
            visitor.VisitText(Text);
            visitor.VisitHeading3Ending();
        }
    }
}