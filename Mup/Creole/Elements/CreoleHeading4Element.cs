namespace Mup.Creole.Elements
{
    internal class CreoleHeading4Element : CreoleHeadingElement
    {
        internal CreoleHeading4Element(string text)
            : base(text)
        {
        }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHeading4Beginning();
            visitor.VisitText(Text);
            visitor.VisitHeading4Ending();
        }
    }
}