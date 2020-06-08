namespace Mup.Elements
{
    public class Heading2Element : HeadingElement
    {
        public Heading2Element(string text)
            : base(text)
        {
        }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}