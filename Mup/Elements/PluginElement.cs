namespace Mup.Elements
{
    public class PluginElement : Element
    {
        public PluginElement(string pluginText)
            => PluginText = pluginText;

        public string PluginText { get; }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}