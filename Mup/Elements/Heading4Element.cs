namespace Mup.Elements
{
    public class Heading4Element : HeadingElement
    {
        public Heading4Element(string text)
            : base(text)
        {
        }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}