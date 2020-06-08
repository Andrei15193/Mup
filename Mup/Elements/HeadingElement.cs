namespace Mup.Elements
{
    public abstract class HeadingElement : Element
    {
        protected HeadingElement(string text)
            => Text = text;

        public string Text { get; }
    }
}