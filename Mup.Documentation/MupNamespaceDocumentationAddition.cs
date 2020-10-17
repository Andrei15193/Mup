using System;
using CodeMap.DeclarationNodes;
using CodeMap.DocumentationElements;

namespace Mup.Documentation
{
    public class MupNamespaceDocumentationAddition : NamespaceDocumentationAddition
    {
        public override bool CanApply(NamespaceDeclaration @namespace)
            => string.Equals("Mup", @namespace.Name, StringComparison.OrdinalIgnoreCase);

        public override SummaryDocumentationElement GetSummary(NamespaceDeclaration @namespace)
            => DocumentationElement.Summary(
                DocumentationElement.Paragraph(
                    DocumentationElement.Text("Contains the base Mup types and implemented parsers.")
                )
            );
    }
}