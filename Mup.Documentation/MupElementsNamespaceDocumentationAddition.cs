using System;
using CodeMap.DeclarationNodes;
using CodeMap.DocumentationElements;

namespace Mup.Documentation
{
    public class MupElementsNamespaceDocumentationAddition : NamespaceDocumentationAddition
    {
        public override bool CanApply(NamespaceDeclaration @namespace)
            => string.Equals("Mup.Elements", @namespace.Name, StringComparison.OrdinalIgnoreCase);

        public override SummaryDocumentationElement GetSummary(NamespaceDeclaration @namespace)
            => DocumentationElement.Summary(
                DocumentationElement.Paragraph(
                    DocumentationElement.Text("Contains the parse nodes representing a mark-up document.")
                )
            );
    }
}