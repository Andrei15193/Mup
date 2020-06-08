namespace Mup.Elements
{
    public class HorizontalRuleElement : Element
    {
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}