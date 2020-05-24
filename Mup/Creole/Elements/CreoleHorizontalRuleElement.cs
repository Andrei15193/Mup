namespace Mup.Creole.Elements
{
    internal class CreoleHorizontalRuleElement : CreoleElement
    {
        internal override void Accept(ParseTreeVisitor visitor)
            => visitor.VisitHorizontalRule();
    }
}