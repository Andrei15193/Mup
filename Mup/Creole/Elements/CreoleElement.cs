namespace Mup.Creole.Elements
{
    internal abstract class CreoleElement
    {
        internal abstract void Accept(ParseTreeVisitor visitor);
    }
}