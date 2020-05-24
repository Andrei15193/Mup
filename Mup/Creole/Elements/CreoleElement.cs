using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal abstract class CreoleElement
    {
        internal abstract void Accept(ParseTreeVisitor visitor);
    }
}