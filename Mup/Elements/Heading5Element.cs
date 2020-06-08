namespace Mup.Elements
{
    public class Heading5Element : HeadingElement
    {
        public Heading5Element(string text)
            : base(text)
        {
        }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}