namespace Mup.Elements
{
    /// <summary>Represents a heading.</summary>
    public abstract class HeadingElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="HeadingElement"/> class.</summary>
        /// <param name="text">The heading text.</param>
        protected HeadingElement(string text)
            => Text = text;

        /// <summary>The heading text.</summary>
        public string Text { get; }
    }
}