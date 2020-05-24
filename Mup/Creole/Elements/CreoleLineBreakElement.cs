namespace Mup.Creole.Elements
{
    internal sealed class CreoleLineBreakElement : CreoleElement
    {
        internal override void Accept(ParseTreeVisitor visitor)
            => visitor.VisitLineBreak();
    }
}