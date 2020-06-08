namespace Mup.Elements
{
    /// <summary>Represents a plugin descriptor.</summary>
    public class PluginElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="ParagraphElement"/> class.</summary>
        /// <param name="pluginText">The plugin descriptor.</param>
        public PluginElement(string pluginText)
            => PluginText = pluginText;

        /// <summary>The plugin descriptor.</summary>
        public string PluginText { get; }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}