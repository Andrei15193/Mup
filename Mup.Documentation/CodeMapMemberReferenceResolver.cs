using CodeMap.DeclarationNodes;
using CodeMap.Handlebars;
using CodeMap.ReferenceData;

namespace Mup.Documentation
{
    public class CodeMapMemberReferenceResolver : IMemberReferenceResolver
    {
        private readonly IMemberReferenceResolver _memberReferenceResolver;

        public CodeMapMemberReferenceResolver()
            => _memberReferenceResolver = new DefaultMemberReferenceResolver(typeof(ParseTreeVisitor).Assembly, "netstandard-1.0");

        public string GetFileName(DeclarationNode declarationNode)
        {
            if (declarationNode is AssemblyDeclaration assemblyDeclaration && assemblyDeclaration == typeof(ParseTreeVisitor).Assembly)
                return "documentation.html";
            else
                return _memberReferenceResolver.GetFileName(declarationNode);
        }

        public string GetUrl(MemberReference memberReference)
            => _memberReferenceResolver.GetUrl(memberReference);
    }
}