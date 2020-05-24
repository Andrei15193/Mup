using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleBlankElement : CreoleElement
    {
        internal override void Accept(ParseTreeVisitor visitor)
            => Task.FromResult<object>(null);
    }
}