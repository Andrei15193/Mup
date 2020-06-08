namespace Mup.Elements
{
    public sealed class LineBreakElement : Element
    {
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}