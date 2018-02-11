#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreolePluginElement : CreoleElement
    {
        internal CreolePluginElement(string pluginText)
        {
            PluginText = pluginText;
        }

        internal string PluginText { get; }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
            => visitor.VisitPlugin(PluginText);
#endif

#if netstandard10
        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => visitor.VisitPluginAsync(PluginText, cancellationToken);
#endif
    }
}