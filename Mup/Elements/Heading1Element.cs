namespace Mup.Elements
{
    public class Heading1Element : HeadingElement
    {
        public Heading1Element(string text)
            : base(text)
        {
        }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}