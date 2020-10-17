using System.IO;
using CodeMap.DeclarationNodes;
using CodeMap.Handlebars;

namespace Mup.Documentation
{
    public class FileWriterTemplateWriterDeclarationNodeVisitor : TemplateWriterDeclarationNodeVisitor
    {
        private readonly IMemberReferenceResolver _memberReferenceResolver;

        public FileWriterTemplateWriterDeclarationNodeVisitor(IMemberReferenceResolver memberReferenceResolver, TemplateWriter templateWriter)
            : base(templateWriter)
            => _memberReferenceResolver = memberReferenceResolver;

        protected override TextWriter GetTextWriter(DeclarationNode declarationNode)
            => new StreamWriter(new FileStream(_memberReferenceResolver.GetFileName(declarationNode), FileMode.Create, FileAccess.Write, FileShare.Read));
    }
}