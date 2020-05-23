using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreolePluginElement : CreoleElement
    {
        internal CreolePluginElement(string pluginText)
        {
            PluginText = pluginText;
        }

        internal string PluginText { get; }

        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => visitor.VisitPluginAsync(PluginText, cancellationToken);
    }
}