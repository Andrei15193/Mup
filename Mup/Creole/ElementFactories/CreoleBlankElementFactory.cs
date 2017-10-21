using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementFactories
{
    internal class CreoleBlankElementFactory : CreoleElementFactory
    {
        internal CreoleBlankElementFactory(string text)
            : base(text)
        {
        }

        internal override CreoleFactoryResult TryCreateFrom(CreoleToken token)
            => ((token.Code == WhiteSpace) ? new CreoleFactoryResult(token, token, new CreoleBlankElement()) : null);
    }
}