using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeMap.DeclarationNodes;
using CodeMap.DocumentationElements;

namespace Mup.Documentation
{
    public class MupAssemblyDocumentationAddition : AssemblyDocumentationAddition
    {
        public override bool CanApply(AssemblyDeclaration assembly)
            => true;

        public override SummaryDocumentationElement GetSummary(AssemblyDeclaration assembly)
            => DocumentationElement.Summary(
                DocumentationElement.Paragraph(
                    DocumentationElement.Text(
                        assembly
                            .Attributes
                            .Single(attribute => attribute.Type == typeof(AssemblyDescriptionAttribute))
                            .PositionalParameters
                            .Single()
                            .Value
                            .ToString()
                    )
                )
            );

        public override RemarksDocumentationElement GetRemarks(AssemblyDeclaration assembly)
            => DocumentationElement.Remarks(
                DocumentationElement.Paragraph(
                    DocumentationElement.Text("The library has a few core types that make it work: "),
                    DocumentationElement.Hyperlink("Mup.IMarkupParser.html", "IMarkupParser"),
                    DocumentationElement.Text(" "),
                    DocumentationElement.Hyperlink("Mup.Elements.ParseTreeRootElement.html", "ParseTreeRootElement"),
                    DocumentationElement.Text(" and "),
                    DocumentationElement.Hyperlink("Mup.ParseTreeVisitor.html", "ParseTreeVisitor"),
                    DocumentationElement.Text(".")
                ),
                DocumentationElement.Paragraph(
                    DocumentationElement.Text("Each parser (currently just Creole) implements the IMarkupParser interface which exposes a number of methods that allow parsing a "),
                    DocumentationElement.Hyperlink("https://docs.microsoft.com/dotnet/api/system.string?view=netstandard-1.0", "string"),
                    DocumentationElement.Text(" or text from a "),
                    DocumentationElement.Hyperlink("https://docs.microsoft.com/dotnet/api/system.io.textreader?view=netstandard-1.0", "TextReader"),
                    DocumentationElement.Text(". Each parser supports both synchronous and asynchronous models allowing its users to consume the API any way they want.")
                ),
                DocumentationElement.Paragraph(
                    DocumentationElement.Text("The result of any parse method is ultimately a "),
                    DocumentationElement.Hyperlink("Mup.Elements.ParseTreeRootElement.html", "ParseTreeRootElement"),
                    DocumentationElement.Text(". Surprisingly or not, this interface does not expose something like a root node or anything related to what one would expect when seeing the word \"tree\".")
                ),
                DocumentationElement.Paragraph(
                    DocumentationElement.Text("This is because trees can have different representations. For instance, we can have the usual example where we have a root node which exposes a property containing a number of nodes that are in fact child nodes, each child node also exposes such a property that contains their child nodes and so on. A different representation can be a flat one where the entire tree is stored as a list of elements that mark the beginning and end of each node.")
                ),
                DocumentationElement.Paragraph(
                    DocumentationElement.Text("Regardless of how we represent a parse tree, we need to be able to traverse it in order to generate a specific output, say HTML. This is where a "),
                    DocumentationElement.Hyperlink("Mup.ParseTreeVisitor.html", "ParseTreeVisitor"),
                    DocumentationElement.Text(" comes into play. Any "),
                    DocumentationElement.Hyperlink("Mup.Elements.ParseTreeRootElement.html", "ParseTreeRootElement"),
                    DocumentationElement.Text(" exposes methods that accept a "),
                    DocumentationElement.Hyperlink("Mup.ParseTreeVisitor.html", "ParseTreeVisitor"),
                    DocumentationElement.Text(", the entire logic for traversing the tree is encapsulated inside itself. Each time a node is being visited, a specific method for that node is called on the visitor. This helps keep the interface clean and completely decouple the language that is being parsed from the desired output format. Any new markup parser will work with existing visitors and any new visitor will work with any existing parser.")
                ),
                DocumentationElement.Paragraph(
                    DocumentationElement.Text("The one common rule for all parse trees is that they are all traversed in pre-order (see "),
                    DocumentationElement.Hyperlink("https://en.wikipedia.org/wiki/Tree_traversal", "Tree Traversal (Wikipedia)"),
                    DocumentationElement.Text(" for more about this topic).")
                )
            );

        public override IEnumerable<NamespaceDocumentationAddition> GetNamespaceAdditions(AssemblyDeclaration assembly)
        {
            yield return new MupNamespaceDocumentationAddition();
            yield return new MupElementsNamespaceDocumentationAddition();
        }
    }
}