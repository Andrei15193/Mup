namespace Mup.Creole.Elements
{
    internal abstract class CreoleHeadingElement : CreoleElement
    {
        protected CreoleHeadingElement(string text)
        {
            Text = text;
        }

        internal string Text { get; }
    }
}